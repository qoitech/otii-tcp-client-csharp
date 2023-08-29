using Newtonsoft.Json;

namespace Otii {
    public partial class BatteryEmulator {
        private class BatteryEmulatorRequestData {
            [JsonProperty("battery_emulator_id")]
            public string BatteryEmulatorId { get; set; }

            public BatteryEmulatorRequestData(string batteryEmulatorId) {
                BatteryEmulatorId = batteryEmulatorId;
            }
        }

        private class GetParallelRequest : Request {
            [JsonProperty("data")]
            public BatteryEmulatorRequestData Data { get; set; }

            public GetParallelRequest(string batteryEmulatorId) : base("battery_emulator_get_parallel") {
                Data = new BatteryEmulatorRequestData(batteryEmulatorId);
            }
        }

        private class GetParallelResponse : Response {
            public class GetParallelResponseData {
                [JsonProperty("value")]
                public long Value { get; set; }
            }

            [JsonProperty("data")]
            public GetParallelResponseData Data { get; set; }
        }

        private class GetSeriesRequest : Request {
            [JsonProperty("data")]
            public BatteryEmulatorRequestData Data { get; set; }

            public GetSeriesRequest(string batteryEmulatorId) : base("battery_emulator_get_series") {
                Data = new BatteryEmulatorRequestData(batteryEmulatorId);
            }
        }

        private class GetSeriesResponse : Response {
            public class GetSeriesResponseData {
                [JsonProperty("value")]
                public long Value { get; set; }
            }

            [JsonProperty("data")]
            public GetSeriesResponseData Data { get; set; }
        }

        private class GetSocRequest : Request {
            [JsonProperty("data")]
            public BatteryEmulatorRequestData Data { get; set; }

            public GetSocRequest(string batteryEmulatorId) : base("battery_emulator_get_soc") {
                Data = new BatteryEmulatorRequestData(batteryEmulatorId);
            }
        }

        private class GetSocResponse : Response {
            public class GetSocResponseData {
                [JsonProperty("value")]
                public long Value { get; set; }
            }

            [JsonProperty("data")]
            public GetSocResponseData Data { get; set; }
        }

        private class GetSocTrackingRequest : Request {
            [JsonProperty("data")]
            public BatteryEmulatorRequestData Data { get; set; }

            public GetSocTrackingRequest(string batteryEmulatorId) : base("battery_emulator_get_soc_tracking") {
                Data = new BatteryEmulatorRequestData(batteryEmulatorId);
            }
        }

        private class GetSocTrackingResponse : Response {
            public class GetSocTrackingResponseData {
                [JsonProperty("enabled")]
                public bool Enabled { get; set; }
            }

            [JsonProperty("data")]
            public GetSocTrackingResponseData Data { get; set; }
        }

        private class GetUsedCapacityRequest : Request {
            [JsonProperty("data")]
            public BatteryEmulatorRequestData Data { get; set; }

            public GetUsedCapacityRequest(string batteryEmulatorId) : base("battery_emulator_get_used_capacity") {
                Data = new BatteryEmulatorRequestData(batteryEmulatorId);
            }
        }

        private class GetUsedCapacityResponse : Response {
            public class GetUsedCapacityResponseData {
                [JsonProperty("value")]
                public long Value { get; set; }
            }

            [JsonProperty("data")]
            public GetUsedCapacityResponseData Data { get; set; }
        }

        private class SetSocRequest : Request {
            public class SetSocRequestData : BatteryEmulatorRequestData {
                [JsonProperty("value")]
                public long Soc { get; set; }

                public SetSocRequestData(string batteryEmulatorId, long value) : base(batteryEmulatorId) {
                    Soc = Soc;
                }
            }

            [JsonProperty("data")]
            public SetSocRequestData Data { get; set; }

            public SetSocRequest(string batteryEmulatorId, long value) : base("battery_emulator_set_soc") {
                Data = new SetSocRequestData(batteryEmulatorId, value);
            }
        }

        private class SetSocTrackingRequest : Request {
            public class SetSocTrackingRequestData : BatteryEmulatorRequestData {
                [JsonProperty("enabled")]
                public bool Enabled { get; set; }

                public SetSocTrackingRequestData(string batteryEmulatorId, bool enabled) : base(batteryEmulatorId) {
                    Enabled = enabled;
                }
            }

            [JsonProperty("data")]
            public SetSocTrackingRequestData Data { get; set; }

            public SetSocTrackingRequest(string batteryEmulatorId, bool enabled) : base("battery_emulator_set_soc_tracking") {
                Data = new SetSocTrackingRequestData(batteryEmulatorId, enabled);
            }
        }

        private class SetUsedCapacityRequest : Request {
            public class SetUsedCapacityRequestData : BatteryEmulatorRequestData {
                [JsonProperty("value")]
                public long Value { get; set; }

                public SetUsedCapacityRequestData(string batteryEmulatorId, long value) : base(batteryEmulatorId) {
                    Value = value;
                }
            }

            [JsonProperty("data")]
            public SetUsedCapacityRequestData Data { get; set; }

            public SetUsedCapacityRequest(string batteryEmulatorId, long value) : base("battery_emulator_set_used_capacity") {
                Data = new SetUsedCapacityRequestData(batteryEmulatorId, value);
            }
        }

        private class UpdateProfileRequest : Request {
            public class UpdateProfileRequestData : BatteryEmulatorRequestData {
                [JsonProperty("battery_profile_id")]
                public string BatteryProfileId { get; set; }

                [JsonProperty("mode")]
                public string Mode { get; set; }

                public UpdateProfileRequestData(string batteryEmulatorId, string batteryProfileId, string mode) : base(batteryEmulatorId) {
                    BatteryProfileId = batteryProfileId;
                    Mode = mode;
                }
            }

            [JsonProperty("data")]
            public UpdateProfileRequestData Data { get; set; }

            public UpdateProfileRequest(string batteryEmulatorId, string batteryProfileId, string mode) : base("battery_emulator_update_profile") {
                Data = new UpdateProfileRequestData(batteryEmulatorId, batteryProfileId, mode);
            }
        }
    }
}
