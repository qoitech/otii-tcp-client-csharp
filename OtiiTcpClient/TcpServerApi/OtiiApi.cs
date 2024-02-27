using Newtonsoft.Json;

namespace Otii {
    public partial class Otii {
        private class CreateProjectRequest : Request {
            public CreateProjectRequest() : base("otii_create_project") {
            }
        }

        private class CreateProjectResponse : Response {
            [JsonProperty("data")]
            public ProjectData Data { get; set; }
        }

        private class GetActiveProjectRequest : Request {
            public GetActiveProjectRequest() : base("otii_get_active_project") {
            }
        }

        private class GetActiveProjectResponse : Response {
            [JsonProperty("data")]
            public ProjectData Data { get; set; }
        }

        private class GetDeviceIdRequest : Request {
            public class GetDeviceIdData {
                [JsonProperty("device_name")]
                public string DeviceName { get; set; }

                public GetDeviceIdData(string deviceName) {
                    DeviceName = deviceName;
                }
            }

            [JsonProperty("data")]
            public GetDeviceIdData Data { get; set; }

            public GetDeviceIdRequest(string deviceName) : base("otii_get_device_id") {
                Data = new GetDeviceIdData(deviceName);
            }
        }

        private class GetDeviceIdResponse : Response {
            public class GetDeviceIdResponseData {
                [JsonProperty("device_id")]
                public string DeviceId { get; set; }
            }

            [JsonProperty("data")]
            public GetDeviceIdResponseData Data { get; set; }
        }

        private class GetDevicesRequest : Request {
            public class GetDevicesData {
                [JsonProperty("timeout")]
                public int Timeout { get; set; }

                public GetDevicesData(int timeout) {
                    Timeout = timeout;
                }
            }

            [JsonProperty("data")]
            public GetDevicesData Data { get; set; }

            public GetDevicesRequest(int timeout) : base("otii_get_devices") {
                Data = new GetDevicesData(timeout);
            }
        }

        private class GetDevicesResponse : Response {
            public class DevicesData {
                [JsonProperty("devices")]
                public ArcData[] Devices { get; set; }
            }

            [JsonProperty("data")]
            public DevicesData Data { get; set; }
        }

        private class GetLicensesRequest : Request {
            public GetLicensesRequest() : base("otii_get_licenses") {
            }
        }

        private class GetLicensesResponse : Response {
            public class LicensesData {
                [JsonProperty("licenses")]
                public LicenseData[] Licenses { get; set; }
            }

            [JsonProperty("data")]
            public LicensesData Data { get; set; }
        }

        private class LoginRequest : Request {
            public class LoginRequestData {
                [JsonProperty("username")]
                public string Username { get; set; }

                [JsonProperty("password")]
                public string Password { get; set; }

                public LoginRequestData(string username, string password) {
                    Username = username;
                    Password = password;
                }
            }

            [JsonProperty("data")]
            public LoginRequestData Data { get; set; }

            public LoginRequest(string username, string password) : base("otii_login") {
                Data = new LoginRequestData(username, password);
            }
        }

        private class LogoutRequest : Request {
            public LogoutRequest() : base("otii_logout") {
            }
        }

        private class OpenProjectRequest : Request {
            public class OpenProjectRequestData {
                [JsonProperty("filename")]
                public string Filename { get; set; }

                [JsonProperty("force")]
                public bool Force { get; set; }

                [JsonProperty("progress")]
                public bool Progress { get; set; }

                public OpenProjectRequestData(string filename, bool force, bool progress) {
                    Filename = filename;
                    Force = force;
                    Progress = progress;
                }
            }

            [JsonProperty("data")]
            public OpenProjectRequestData Data { get; set; }

            public OpenProjectRequest(string filename, bool force, bool progress) : base("otii_open_project") {
                Data = new OpenProjectRequestData(filename, force, progress);
            }
        }

        private class OpenProjectResponse : Response {
            public class OpenProjectResponseData : ProjectData {
                [JsonProperty("filename")]
                public string Filename { get; set; }
            }

            [JsonProperty("data")]
            public OpenProjectResponseData Data { get; set; }
        }

        private class ReserveLicenseRequest : Request {
            public class ReserveLicenseRequestData {
                [JsonProperty("license_id")]
                public int LicenseId { get; set; }

                public ReserveLicenseRequestData(int licenseId) {
                    LicenseId = licenseId;
                }
            }

            [JsonProperty("data")]
            public ReserveLicenseRequestData Data { get; set; }

            public ReserveLicenseRequest(int licenseId) : base("otii_reserve_license") {
                Data = new ReserveLicenseRequestData(licenseId);
            }
        }

        private class ReturnLicenseRequest : Request {
            public class ReturnLicenseRequestData {
                [JsonProperty("license_id")]
                public int LicenseId { get; set; }

                public ReturnLicenseRequestData(int licenseId) {
                    LicenseId = licenseId;
                }
            }

            [JsonProperty("data")]
            public ReturnLicenseRequestData Data { get; set; }

            public ReturnLicenseRequest(int licenseId) : base("otii_return_license") {
                Data = new ReturnLicenseRequestData(licenseId);
            }
        }

        private class SetAllMainRequest : Request {
            public class SetAllMainRequestData {
                [JsonProperty("enable")]
                public bool Enable { get; set; }

                public SetAllMainRequestData(bool enable) {
                    Enable = enable;
                }
            }

            [JsonProperty("data")]
            public SetAllMainRequestData Data { get; set; }

            public SetAllMainRequest(bool enable) : base("otii_set_all_main") {
                Data = new SetAllMainRequestData(enable);
            }
        }

        private class ShutdownRequest : Request {
            public ShutdownRequest() : base("otii_shutdown") {
            }
        }

        private class ProjectData {
            [JsonProperty("project_id")]
            public int ProjectId { get; set; }
        }

        private class ArcData {
            [JsonProperty("device_id")]
            public string DeviceId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        private class LicenseData {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("available")]
            public bool Available { get; set; }

            [JsonProperty("reserved_to")]
            public string ReservedTo { get; set; }

            [JsonProperty("hostname")]
            public string Hostname { get; set; }

            [JsonProperty("addons")]
            public Addon[] Addons { get; set; }
        }

        private class Addon {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("attributes")]
            public AddonAttribute Attributes { get; set; }
        }

        private class AddonAttribute {
            [JsonProperty("channels")]
            public int Channels { get; set; }
        }
    }
}
