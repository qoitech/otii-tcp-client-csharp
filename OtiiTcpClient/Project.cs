using System.Linq;

namespace OtiiTcpClient {
    public partial class Project {
        private readonly OtiiClient _client;
        private readonly int _projectId;

        internal Project(OtiiClient client, int projectId) {
            _client = client;
            _projectId = projectId;
        }

        /// <summary>
        /// Close an open project.
        /// </summary>
        /// <param name="force">if you want to close a project with unsaved data set this to true. (false by default)</param>
        public void Close(bool force = false) {
            var request = new CloseRequest(_projectId, force);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Crop all data.
        /// </summary>
        /// <param name="start">from sample time in seconds.</param>
        /// <param name="end">to sample time in seconds.</param>
        public void CropData(double start, double end) {
            var request = new CropDataRequest(_projectId, start, end);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Return last captured recording.
        /// </summary>
        /// <returns>An object that can be used to get data from and to manipulate the recording</returns>
        public Recording GetLastRecording() {
            var request = new GetLastRecordingRequest(_projectId);
            var response = _client.PostRequest<GetLastRecordingRequest, GetLastRecordingResponse>(request);
            return new Recording(_client, response.Data.RecordingId, response.Data.Name, response.Data.StartTime);
        }

        /// <summary>
        /// Return list of captured recordings.
        /// </summary>
        /// <returns>a list of recordings.</returns>
        public Recording[] GetRecordings() {
            var request = new GetRecordingsRequest(_projectId);
            var response =_client.PostRequest<GetRecordingsRequest, GetRecordingsResponse>(request);
            var recordings = response.Data.Recordings.Select(
                recording => new Recording(_client, recording.RecordingId, recording.Name, recording.StartTime)
            );
            return recordings.ToArray();
        }

        /// <summary>
        /// Save the current project.
        /// </summary>
        /// <param name="filename">the path of the file to save.</param>
        /// <param name="force">if true, overwrites filename if it already exists.</param>
        /// <param name="progress">if true, returns progress notifications.</param>
        public void Save(string filename, bool force, bool progress) {
            var request = new SaveRequest(_projectId, filename, force, progress);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Start a new recording. If a recording is currently ongoing, it will be stopped first.
        /// </summary>
        public void StartRecording() {
            var request = new StartRecordingRequest(_projectId);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Stop the current recording.
        /// </summary>
        public void StopRecording() {
            var request = new StopRecordingRequest(_projectId);
            _client.PostRequest(request);
        }
    }
}