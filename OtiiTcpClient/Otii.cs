using System.Linq;

namespace OtiiTcpClient {
    /// <summary>
    /// The Otii class provides methods at the application level of Otii.
    /// </summary>
    public partial class Otii {
        private readonly OtiiClient _client;

        internal Otii(OtiiClient client) {
            _client = client;
        }

        public class License {
            public int Id;
            public string Type;
            public bool Available;
            public string ReservedTo;
            public string Hostname;
            public string[] AddonTypes;

            public License(int id, string type, bool available, string reservedTo, string hostname, string[] addonTypes) {
                Id = id;
                Type = type;
                Available = available;
                ReservedTo = reservedTo;
                Hostname = hostname;
                AddonTypes = addonTypes;
            }
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
        /// Get a list of all licenses for the logged in user.
        /// </summary>
        /// <returns>A list of all licenses.</returns>
        public License[] GetLicenses() {
            var request = new GetLicensesRequest();
            var response = _client.PostRequest<GetLicensesRequest, GetLicensesResponse>(request);
            var licenses = response.Data.Licenses.Select(license => new License(
                license.Id,
                license.Type,
                license.Available,
                license.ReservedTo,
                license.Hostname,
                license.Addons.Select(addon => addon.Id).ToArray()
            ));
            return licenses.ToArray();
        }

        /// <summary>
        /// Login to license server.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="password">password.</param>
        /// <returns></returns>
        public void Login(string username, string password) {
            var request = new LoginRequest(username, password);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Logout from license server.
        /// </summary>
        /// <returns></returns>
        public void Logout() {
            var request = new LogoutRequest();
            _client.PostRequest(request);
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
        /// Reserve license.
        /// </summary>
        /// <param name="licenseId">License id to reserve.</param>
        /// <returns></returns>
        public void ReserveLicense(int licenseId) {
            var request = new ReserveLicenseRequest(licenseId);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Return license.
        /// </summary>
        /// <param name="licenseId">License id to return.</param>
        /// <returns></returns>
        public void ReturnLicense(int licenseId) {
            var request = new ReturnLicenseRequest(licenseId);
            _client.PostRequest(request);
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
