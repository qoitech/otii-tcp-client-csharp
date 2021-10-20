using System.Linq;

namespace Otii {
    /// <summary>
    /// The Otii class provides methods at the application level of Otii.
    /// </summary>
    public partial class Otii {
        private readonly OtiiClient _client;

        internal Otii(OtiiClient client) {
            _client = client;
        }

        /// <summary>
        /// Create a new project.
        /// </summary>
        /// <returns>An object representing the newly created project.</returns>
        public Project CreateProject() {
            var request = new CreateProjectRequest();
            var response = _client.PostRequest<CreateProjectRequest, CreateProjectResponse>(request);
            return new Project(_client, response.Data.ProjectId);
        }

        /// <summary>
        /// Get the active project.
        /// </summary>
        /// <returns>An object representing the current project if one exists, otherwise null</returns>
        public Project GetActiveProject() {
            var request = new GetActiveProjectRequest();
            var response = _client.PostRequest<GetActiveProjectRequest, GetActiveProjectResponse>(request);
            var projectId = response.Data.ProjectId;
            return projectId == -1 ? null : new Project(_client, projectId);
        }

        /// <summary>
        /// Get the device id from the name of the id.
        /// </summary>
        /// <param name="deviceName">the name of the connected device to return the if of.</param>
        /// <returns>The id of the named device.</returns>
        public string GetDeviceId(string deviceName) {
            var request = new GetDeviceIdRequest(deviceName);
            var response = _client.PostRequest<GetDeviceIdRequest, GetDeviceIdResponse>(request);
            return response.Data.DeviceId;
        }

        /// <summary>
        /// Get a list of all connected devices.
        /// </summary>
        /// <param name="timeout">An optional timeout in seconds.</param>
        /// <returns>A list of all connected devices.</returns>
        public Arc[] GetDevices(int timeout = 0) {
            var request = new GetDevicesRequest(timeout);
            var response = _client.PostRequest<GetDevicesRequest, GetDevicesResponse>(request);
            var devices = response.Data.Devices.Select(device => new Arc(_client, device.DeviceId, device.Name, device.Type));
            return devices.ToArray();
        }

        /// <summary>
        /// Open an existing project.
        /// </summary>
        /// <param name="filename">path to the project.</param>
        /// <param name="force">set to true to open a file even if the current project has unsaved data.</param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public Project OpenProject(string filename, bool force = false, bool progress = false) {
            var request = new OpenProjectRequest(filename, force, progress);
            var response = _client.PostRequest<OpenProjectRequest, OpenProjectResponse>(request);
            var projectId = response.Data.ProjectId;
            return projectId == -1 ? null : new Project(_client, projectId);
        }

        /// <summary>
        /// Turn on or off the main power on all connected devices.
        /// </summary>
        /// <param name="enable">true turns on power, false turns off power.</param>
        public void SetAllMain(bool enable) {
            var request = new SetAllMainRequest(enable);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Shutdown TCP server.
        /// </summary>
        public void Shutdown() {
            var request = new ShutdownRequest();
            try {
                _client.PostRequest(request);
            } catch (OtiiClient.DisconnectedException) {
            }
        }
    }
}
