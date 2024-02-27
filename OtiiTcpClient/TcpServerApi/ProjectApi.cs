using System;
using Newtonsoft.Json;

namespace OtiiTcpClient {
    public partial class Project {
        private class ProjectRequestData {
            [JsonProperty("project_id")]
            public int ProjectId { get; set; }

            public ProjectRequestData(int projectId) {
                ProjectId = projectId;
            }
        }

        private class RecordingResponseData {
            [JsonProperty("recording_id")]
            public int RecordingId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("running")]
            public bool Running { get; set; }

            [JsonProperty("start-time")]
            public DateTimeOffset StartTime { get; set; }
        }

        private class CloseRequest : Request {
            public class CloseRequestData : ProjectRequestData {
                [JsonProperty("force")]
                public bool Force { get; set; }

                public CloseRequestData(int projectId, bool force) : base(projectId) {
                    Force = force;
                }
            }

            [JsonProperty("data")]
            public CloseRequestData Data { get; set; }

            public CloseRequest(int projectId, bool force) : base("project_close") {
                Data = new CloseRequestData(projectId, force);
            }
        }

        private class CropDataRequest : Request {
            public class CropDataRequestData : ProjectRequestData {
                [JsonProperty("start")]
                public double Start { get; set; }

                [JsonProperty("end")]
                public double End { get; set; }

                public CropDataRequestData(int projectId, double start, double end) : base(projectId) {
                    Start = start;
                    End = end;
                }
            }

            [JsonProperty("data")]
            public CropDataRequestData Data { get; set; }

            public CropDataRequest(int projectId, double start, double end) : base("project_crop_data") {
                Data = new CropDataRequestData(projectId, start, end);
            }
        }

        private class GetLastRecordingRequest : Request {
            [JsonProperty("data")]
            public ProjectRequestData Data { get; set; }

            public GetLastRecordingRequest(int projectId) : base("project_get_last_recording") {
                Data = new ProjectRequestData(projectId);
            }
        }

        private class GetLastRecordingResponse : Response {
            [JsonProperty("data")]
            public RecordingResponseData Data { get; set; }
        }

        private class GetRecordingsRequest : Request {
            [JsonProperty("data")]
            public ProjectRequestData Data { get; set; }

            public GetRecordingsRequest(int projectId) : base("project_get_recordings") {
                Data = new ProjectRequestData(projectId);
            }
        }

        private class GetRecordingsResponse : Response {
            public class GetRecordingsResponseData {
                [JsonProperty("recordings")]
                public RecordingResponseData[] Recordings { get; set; }
            }

            [JsonProperty("data")]
            public GetRecordingsResponseData Data { get; set; }
        }

        private class SaveRequest : Request {
            public class SaveRequestData : ProjectRequestData {
                [JsonProperty("filename")]
                public string Filename { get; set; }

                [JsonProperty("force")]
                public bool Force { get; set; }

                [JsonProperty("progress")]
                public bool Progress { get; set; }

                public SaveRequestData(int projectId, string filename, bool force, bool progress) : base(projectId) {
                    Filename = filename;
                    Force = force;
                    Progress = progress;
                }
            }

            [JsonProperty("data")]
            public SaveRequestData Data { get; set; }

            public SaveRequest(int projectId, string filename, bool force, bool progress) : base("project_save") {
                Data = new SaveRequestData(projectId, filename, force, progress);
            }
        }

        private class SaveResponse : Response {
            public class SaveResponseData {
                [JsonProperty("filename")]
                public string Filename { get; set; }
            }

            [JsonProperty("data")]
            public SaveResponseData Data { get; set; }
        }

        private class StartRecordingRequest : Request {
            [JsonProperty("data")]
            public ProjectRequestData Data { get; set; }

            public StartRecordingRequest(int projectId) : base("project_start_recording") {
                Data = new ProjectRequestData(projectId);
            }
        }

        private class StopRecordingRequest : Request {
            [JsonProperty("data")]
            public ProjectRequestData Data { get; set; }

            public StopRecordingRequest(int projectId) : base("project_stop_recording") {
                Data = new ProjectRequestData(projectId);
            }
        }
    }
}