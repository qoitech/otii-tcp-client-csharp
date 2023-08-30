using System;
using System.Linq;

namespace Otii {
    public partial class Recording {
        private readonly OtiiClient _client;
        private readonly int _recordingId;

        public string Name;
        public DateTimeOffset StartTime;

        internal Recording(OtiiClient client, int recordingId, string name, DateTimeOffset startTime) {
            _client = client;
            _recordingId = recordingId;
            Name = name;
            StartTime = startTime;
        }

        public class AnalogData {
            public double Timestamp;
            public double Interval;
            public double[] Values;

            public AnalogData(double timestamp, double interval, double[] values) {
                Timestamp = timestamp;
                Interval = interval;
                Values = values;
            }
        }

        public class DigitalData {
            public double Timestamp;
            public bool Value;

            public DigitalData(double timestamp, bool value) {
                Timestamp = timestamp;
                Value = value;
            }
        }

        public class LogData {
            public double Timestamp;
            public string Value;

            public LogData(double timestamp, string value) {
                Timestamp = timestamp;
                Value = value;
            }
        }

        public class ChannelInfo {
            public double Offset;
            public double From;
            public double To;
            public long SampleRate;

            public ChannelInfo(double offset, double from, double to, long sampleRate) {
                Offset = offset;
                From = from;
                To = to;
                SampleRate = sampleRate;
            }
        }

        public class ChannelStatistics {
            public double Min;
            public double Max;
            public double Average;
            public double Energy;

            public ChannelStatistics(double min, double max, double average, double energy) {
                Min = min;
                Max = max;
                Average = average;
                Energy = energy;
            }
        }

        /// <summary>
        /// Delete a recording.
        /// </summary>
        public void Delete() {
            var request = new DeleteRequest(_recordingId);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Downsample all recordings on a channel.
        /// </summary>
        /// <param name="deviceId">id of capturing device.</param>
        /// <param name="channel">channel name.</param>
        /// <param name="factor">factor to downsample channel with.</param>
        public void DownsampleChannel(string deviceId, string channel, int factor) {
            var request = new DownsampleChannelRequest(_recordingId, deviceId, channel, factor);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Get number of data entries in a channel for a specific recording.
        /// </summary>
        /// <param name="deviceId">id of capturing device.</param>
        /// <param name="channel">channel name.</param>
        /// <returns>The number of data entries.</returns>
        public long GetChannelDataCount(string deviceId, string channel) {
            var request = new GetChannelDataCountRequest(_recordingId, deviceId, channel);
            var response = _client.PostRequest<GetChannelDataCountRequest, GetChannelDataCountResponse>(request);
            return response.Data.Count;
        }

        /// <summary>
        /// Get the index in a channel for a specific recording for the given timestamp.
        /// </summary>
        /// <param name="deviceId">id of capturing device.</param>
        /// <param name="channel">channel name.</param>
        /// <param name="timestamp">timestamp in s.</param>
        /// <returns></returns>
        public long GetChannelDataIndex(string deviceId, string channel, double timestamp) {
            var request = new GetChannelDataIndexRequest(_recordingId, deviceId, channel, timestamp);
            var response = _client.PostRequest<GetChannelDataIndexRequest, GetChannelDataIndexResponse>(request);
            return response.Data.Index;
        }

        /// <summary>
        /// Get analog data entries from a specified channel of a specific recording.
        /// </summary>
        /// <param name="deviceId">id of capturing device.</param>
        /// <param name="channel">channel name.</param>
        /// <param name="index">start index to fetch data entries from, first index at 0.</param>
        /// <param name="count">number of data entries to fetch.</param>
        /// <returns></returns>
        public AnalogData GetAnalogChannelData(string deviceId, string channel, long index, long count) {
            var request = new GetChannelDataRequest(_recordingId, deviceId, channel, index, count);
            var response = _client.PostRequest<GetChannelDataRequest, GetAnalogChannelDataResponse>(request);
            if (response.Data.DataType != "analog") {
                throw new Exception("No analog data");
            }
            return new AnalogData(response.Data.Timestamp, response.Data.Interval, response.Data.Values);
        }

        /// <summary>
        /// Get digital entries from a specified channel of a specific recording.
        /// </summary>
        /// <param name="deviceId">id of capturing device.</param>
        /// <param name="channel">channel name.</param>
        /// <param name="index">start index to fetch data entries from, first index at 0.</param>
        /// <param name="count">number of data entries to fetch.</param>
        /// <returns></returns>
        public DigitalData[] GetDigitalChannelData(string deviceId, string channel, long index, long count) {
            var request = new GetChannelDataRequest(_recordingId, deviceId, channel, index, count);
            var response = _client.PostRequest<GetChannelDataRequest, GetDigitalChannelDataResponse>(request);
            var data = response.Data.Values.Select(value => new DigitalData(value.Timestamp, value.Value));
            return data.ToArray();
        }

        /// <summary>
        /// Get log entries from a specified channel of a specific recording.
        /// </summary>
        /// <param name="deviceId">id of capturing device.</param>
        /// <param name="channel">channel name.</param>
        /// <param name="index">start index to fetch data entries from, first index at 0.</param>
        /// <param name="count">number of data entries to fetch.</param>
        /// <param name="strip">strip control characters (defaults to true).</param>
        /// <returns></returns>
        public LogData[] GetLogChannelData(string deviceId, string channel, long index, long count, bool strip = true) {
            var request = new GetChannelDataRequest(_recordingId, deviceId, channel, index, count);
            var response = _client.PostRequest<GetChannelDataRequest, GetLogChannelDataResponse>(request);
            var data = response.Data.Values.Select(value => new LogData(value.Timestamp, value.Value));
            if (strip) {
                data = data.Select(d => new LogData(d.Timestamp, new string(d.Value.Where(c => !char.IsControl(c)).ToArray())));
            }
            return data.ToArray();
        }

        /// <summary>
        /// Returns information about the channel.
        /// </summary>
        /// <param name="deviceId">device id of the capturing device. Exclude for imported logs.</param>
        /// <param name="channel">the channel name. For imported logs, use the log_id returned by import_log.</param>
        /// <returns></returns>
        public ChannelInfo GetChannelInfo(string deviceId, string channel) {
            var request = new GetChannelInfoRequest(_recordingId, deviceId, channel);
            var response = _client.PostRequest<GetChannelInfoRequest, GetIChannelnfoResponse>(request);
            var data = response.Data;
            return new ChannelInfo(data.Offset, data.From, data.To, data.SampleRate);
        }

        /// <summary>
        /// Returns statistics about the channel.
        /// </summary>
        /// <param name="deviceId">device id of the capturing device.</param>
        /// <param name="channel">the channel name.</param>
        /// <param name="from">from time.</param>
        /// <param name="to">from time.</param>
        /// <returns></returns>
        public ChannelStatistics GetChannelStatistics(string deviceId, string channel, double from, double to) {
            var request = new GetChannelStatisticsRequest(_recordingId, deviceId, channel, from, to);
            var response = _client.PostRequest<GetChannelStatisticsRequest, GetChannelStatisticsResponse>(request);
            var data = response.Data;
            return new ChannelStatistics(data.Min, data.Max, data.Average, data.Energy);
        }

        /// <summary>
        /// Returns the offset of a log.
        /// </summary>
        /// <param name="deviceId">device id of the capturing device. Exclude for imported logs.</param>
        /// <param name="channel">the channel name. For imported logs, use the log_id returned by import_log.</param>
        /// <returns></returns>
        public long GetLogOffset(string deviceId, string channel) {
            var request = new GetLogOffsetRequest(_recordingId, deviceId, channel);
            var response = _client.PostRequest<GetLogOffsetRequest, GetLogOffsetResponse>(request);
            return response.Data.Offset;
        }

        /// <summary>
        /// Returns the offset of the recording.
        /// </summary>
        /// <returns></returns>
        public long GetOffset() {
            var request = new GetOffsetRequest(_recordingId);
            var response = _client.PostRequest<GetOffsetRequest, GetOffsetResponse>(request);
            return response.Data.Offset;
        }

        /// <summary>
        /// Import a log file.
        /// </summary>
        /// <param name="filename">the path of the log to import.</param>
        /// <param name="converter">the filename of the log converter to use (e.g. adb.lua).</param>
        public void ImportLog(string filename, string converter) {
            var request = new ImportLogRequest(_recordingId, filename, converter);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Returns true if this recording is currently running.
        /// </summary>
        /// <returns></returns>
        public bool IsRunning() {
            var request = new IsRunningRequest(_recordingId);
            var response = _client.PostRequest<IsRunningRequest, IsRunningResponse>(request);
            return response.Data.Running;
        }

        /// <summary>
        /// Write text to time synchronized log window.
        /// This function will add a timestamped text to a log.
        /// The first time it is called, it will create a new log. Note that a recording has to be running for this to produce any output.
        /// </summary>
        /// <param name="text">text to add to the log window.</param>
        /// <param name="timestamp">timestamp in milliseconds since 1970-01-01. If omitted the current time will be used.</param>
        public void Log(string text, long timestamp = 0) {
            var request = new LogRequest(_recordingId, text, timestamp);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Rename recording.
        /// </summary>
        /// <param name="name">new name of recording.</param>
        public void Rename(string name) {
            var request = new RenameRequest(_recordingId, name);
            _client.PostRequest(request);
            Name = name;
        }

        /// <summary>
        /// Set offset of a log.
        /// </summary>
        /// <param name="deviceId">device id of the capturing device. Exclude for imported logs.</param>
        /// <param name="channel">the channel name. For imported logs, use the log_id returned by import_log.</param>
        /// <param name="offset">the new offset to apply in microseconds.</param>
        public void SetLogOffset(string deviceId, string channel, long offset) {
            var request = new SetLogOffsetRequest(_recordingId, deviceId, channel, offset);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Sets the offset of the recording.
        /// </summary>
        /// <param name="offset">The new offset to apply in microseconds.</param>
        public void SetOffset(long offset) {
            var request = new SetOffsetRequest(_recordingId, offset);
            _client.PostRequest(request);
        }
    }
}