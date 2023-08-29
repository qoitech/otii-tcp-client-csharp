using Newtonsoft.Json;

namespace Otii {
    public partial class Arc {
        private class ArcRequestData {
            [JsonProperty("device_id")]
            public string DeviceId { get; set; }

            public ArcRequestData(string deviceId) {
                DeviceId = deviceId;
            }
        }

        private class EnableRequestData : ArcRequestData {
            [JsonProperty("enable")]
            public bool Enable { get; set; }

            public EnableRequestData(string deviceId, bool enable) : base(deviceId) {
                Enable = enable;
            }
        }

        private class SetDoubleRequestData : ArcRequestData {
            [JsonProperty("value")]
            public double Value { get; set; }

            public SetDoubleRequestData(string deviceId, double value) : base(deviceId) {
                Value = value;
            }
        }

        private class SupplyData {
            [JsonProperty("supply_id")]
            public int SupplyId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("manufacturer")]
            public string Manufacturer { get; set; }

            [JsonProperty("model")]
            public string Model { get; set; }
        }

        private class AddToProjectRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public AddToProjectRequest(string deviceId) : base("arc_add_to_project") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class CalibrateRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public CalibrateRequest(string deviceId) : base("arc_calibrate") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class Enable5VRequest : Request {
            [JsonProperty("data")]
            public EnableRequestData Data { get; set; }

            public Enable5VRequest(string deviceId, bool enable) : base("arc_enable_5v") {
                Data = new EnableRequestData(deviceId, enable);
            }
        }

        private class EnableExpPortRequest : Request {
            [JsonProperty("data")]
            public EnableRequestData Data { get; set; }

            public EnableExpPortRequest(string deviceId, bool enable) : base("arc_enable_exp_port") {
                Data = new EnableRequestData(deviceId, enable);
            }
        }

        private class EnableChannelRequest : Request {
            public class EnableChannelData : EnableRequestData {
                [JsonProperty("channel")]
                public string Channel { get; set; }

                public EnableChannelData(string deviceId, string channel, bool enable) : base(deviceId, enable) {
                    Channel = channel;
                }
            }

            [JsonProperty("data")]
            public EnableChannelData Data { get; set; }

            public EnableChannelRequest(string deviceId, string channel, bool enable) : base("arc_enable_channel") {
                Data = new EnableChannelData(deviceId, channel, enable);
            }
        }

        private class EnableUartRequest : Request {
            public class EnableUartRequestData : ArcRequestData {
                [JsonProperty("enable")]
                public bool Enable { get; set; }

                public EnableUartRequestData(string deviceId, bool enable) : base(deviceId) {
                    Enable = enable;
                }
            }

            [JsonProperty("data")]
            public EnableUartRequestData Data { get; set; }

            public EnableUartRequest(string deviceId, bool enable) : base("arc_enable_uart") {
                Data = new EnableUartRequestData(deviceId, enable);
            }
        }

        private class Get4WireRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public Get4WireRequest(string deviceId) : base("arc_get_4wire") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class Get4WireResponse : Response {
            public class Get4WireResponseData {
                [JsonProperty("value")]
                public string Value { get; set; }
            }

            [JsonProperty("data")]
            public Get4WireResponseData Data { get; set; }
        }

        private class GetAdcResistorRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetAdcResistorRequest(string deviceId) : base("arc_get_adc_resistor") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetAdcResistorResponse : Response {
            public class GetAdcResistorResponseData {
                [JsonProperty("value")]
                public double Value { get; set; }
            }

            [JsonProperty("data")]
            public GetAdcResistorResponseData Data { get; set; }
        }

        private class GetExpVoltageRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetExpVoltageRequest(string deviceId) : base("arc_get_exp_voltage") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetExpVoltageResponse : Response {
            public class GetExpVoltageResponseData {
                [JsonProperty("value")]
                public double Value { get; set; }
            }

            [JsonProperty("data")]
            public GetExpVoltageResponseData Data { get; set; }
        }

        private class GetGpiRequest : Request {
            public class GetGpiRequestData : ArcRequestData {
                [JsonProperty("pin")]
                public int Pin { get; set; }

                public GetGpiRequestData(string deviceId, int pin) : base(deviceId) {
                    Pin = pin;
                }
            }

            [JsonProperty("data")]
            public GetGpiRequestData Data { get; set; }

            public GetGpiRequest(string deviceId, int pin) : base("arc_get_gpi") {
                Data = new GetGpiRequestData(deviceId, pin);
            }
        }

        private class GetGpiResponse : Response {
            public class GetGpiResponseData {
                [JsonProperty("value")]
                public bool Value { get; set; }
            }

            [JsonProperty("data")]
            public GetGpiResponseData Data { get; set; }
        }

        private class GetMainVoltageRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetMainVoltageRequest(string deviceId) : base("arc_get_main_voltage") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetMainVoltageResponse : Response {
            public class GetMainVoltageResponseData {
                [JsonProperty("value")]
                public double Value { get; set; }
            }

            [JsonProperty("data")]
            public GetMainVoltageResponseData Data { get; set; }
        }

        private class GetMaxCurrentRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetMaxCurrentRequest(string deviceId) : base("arc_get_max_current") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetMaxCurrentResponse : Response {
            public class GetMaxCurrentResponseData {
                [JsonProperty("value")]
                public double Value { get; set; }
            }

            [JsonProperty("data")]
            public GetMaxCurrentResponseData Data { get; set; }
        }

        private class GetRangeRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetRangeRequest(string deviceId) : base("arc_get_range") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetRangeResponse : Response {
            public class GetRangeResponseData {
                [JsonProperty("range")]
                public string Range { get; set; }
            }

            [JsonProperty("data")]
            public GetRangeResponseData Data { get; set; }
        }

        private class GetRxRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetRxRequest(string deviceId) : base("arc_get_rx") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetRxResponse : Response {
            public class GetRxResponseData {
                [JsonProperty("value")]
                public bool Value { get; set; }
            }

            [JsonProperty("data")]
            public GetRxResponseData Data { get; set; }
        }

        private class GetSourceCurrentLimitEnabledRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetSourceCurrentLimitEnabledRequest(string deviceId) : base("arc_get_src_cur_limit_enabled") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetSourceCurrentLimitEnabledResponse : Response {
            public class GetSourceCurrentLimitEnabledResponseData {
                [JsonProperty("enabled")]
                public bool Enabled { get; set; }
            }

            [JsonProperty("data")]
            public GetSourceCurrentLimitEnabledResponseData Data { get; set; }
        }

        private class GetUartBaudrateRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetUartBaudrateRequest(string deviceId) : base("arc_get_uart_baudrate") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetUartBaudrateResponse : Response {
            public class GetUartBaudrateResponseData {
                [JsonProperty("value")]
                public int Value { get; set; }
            }

            [JsonProperty("data")]
            public GetUartBaudrateResponseData Data { get; set; }
        }

        private class GetValueRequest : Request {
            public class GetValueRequestData : ArcRequestData {
                [JsonProperty("channel")]
                public string Channel { get; set; }

                public GetValueRequestData(string deviceId, string channel) : base(deviceId) {
                    Channel = channel;
                }
            }

            [JsonProperty("data")]
            public GetValueRequestData Data { get; set; }

            public GetValueRequest(string deviceId, string channel) : base("arc_get_value") {
                Data = new GetValueRequestData(deviceId, channel);
            }
        }

        private class GetValueResponse : Response {
            public class GetValueResponseData {
                [JsonProperty("value")]
                public double Value { get; set; }
            }

            [JsonProperty("data")]
            public GetValueResponseData Data { get; set; }
        }

        private class GetVersionRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public GetVersionRequest(string deviceId) : base("arc_get_version") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class GetVersionResponse : Response {
            public class GetVersionResponseData {
                [JsonProperty("hw_version")]
                public string HardwareVersion { get; set; }

                [JsonProperty("fw_version")]
                public string FirmwareVersion { get; set; }
            }

            [JsonProperty("data")]
            public GetVersionResponseData Data { get; set; }
        }

        private class IsConnectedRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public IsConnectedRequest(string deviceId) : base("arc_is_connected") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class IsConnectedResponse : Response {
            public class IsConnectedResponseData {
                [JsonProperty("connected")]
                public bool Connected { get; set; }
            }

            [JsonProperty("data")]
            public IsConnectedResponseData Data { get; set; }
        }

        private class Set4WireRequest : Request {
            [JsonProperty("data")]
            public EnableRequestData Data { get; set; }

            public Set4WireRequest(string deviceId, bool enable) : base("arc_set_4wire") {
                Data = new EnableRequestData(deviceId, enable);
            }
        }

        private class SetAdcResistorRequest : Request {
            [JsonProperty("data")]
            public SetDoubleRequestData Data { get; set; }

            public SetAdcResistorRequest(string deviceId, double value) : base("arc_set_adc_resistor") {
                Data = new SetDoubleRequestData(deviceId, value);
            }
        }

        private class SetExpVoltageRequest : Request {
            [JsonProperty("data")]
            public SetDoubleRequestData Data { get; set; }

            public SetExpVoltageRequest(string deviceId, double value) : base("arc_set_exp_voltage") {
                Data = new SetDoubleRequestData(deviceId, value);
            }
        }

        private class SetGpoRequest : Request {
            public class SetGpoRequestData : ArcRequestData {
                [JsonProperty("pin")]
                public int Pin { get; set; }

                [JsonProperty("value")]
                public bool Value { get; set; }

                public SetGpoRequestData(string deviceId, int pin, bool value) : base(deviceId) {
                    Pin = pin;
                    Value = value;
                }
            }

            [JsonProperty("data")]
            public SetGpoRequestData Data { get; set; }

            public SetGpoRequest(string deviceId, int pin, bool value) : base("arc_set_gpo") {
                Data = new SetGpoRequestData(deviceId, pin, value);
            }
        }

        private class SetMainRequest : Request {
            [JsonProperty("data")]
            public EnableRequestData Data { get; set; }

            public SetMainRequest(string deviceId, bool enable) : base("arc_set_main") {
                Data = new EnableRequestData(deviceId, enable);
            }
        }

        private class SetMainCurrentRequest : Request {
            public class SetMainCurrentData : ArcRequestData {
                [JsonProperty("value")]
                public double Value { get; set; }

                public SetMainCurrentData(string deviceId, double voltage) : base(deviceId) {
                    Value = voltage;
                }
            }

            [JsonProperty("data")]
            public SetMainCurrentData Data { get; set; }

            public SetMainCurrentRequest(string deviceId, double voltage) : base("arc_set_main_current") {
                Data = new SetMainCurrentData(deviceId, voltage);
            }
        }

        private class SetMainVoltageRequest : Request {
            public class SetMainVoltageData : ArcRequestData {
                [JsonProperty("value")]
                public double Value { get; set; }

                public SetMainVoltageData(string deviceId, double voltage) : base(deviceId) {
                    Value = voltage;
                }
            }

            [JsonProperty("data")]
            public SetMainVoltageData Data { get; set; }

            public SetMainVoltageRequest(string deviceId, double voltage) : base("arc_set_main_voltage") {
                Data = new SetMainVoltageData(deviceId, voltage);
            }
        }

        private class SetMaxCurrentRequest : Request {
            public class SetMaxCurrentData : ArcRequestData {
                [JsonProperty("value")]
                public double Value { get; set; }

                public SetMaxCurrentData(string deviceId, double voltage) : base(deviceId) {
                    Value = voltage;
                }
            }

            [JsonProperty("data")]
            public SetMaxCurrentData Data { get; set; }

            public SetMaxCurrentRequest(string deviceId, double voltage) : base("arc_set_max_current") {
                Data = new SetMaxCurrentData(deviceId, voltage);
            }
        }

        private class SetPowerRegulationRequest : Request {
            public class SetPowerRegulationRequestData : ArcRequestData {
                [JsonProperty("mode")]
                public string Mode { get; set; }

                public SetPowerRegulationRequestData(string deviceId, string mode) : base(deviceId) {
                    Mode = mode;
                }
            }

            [JsonProperty("data")]
            public SetPowerRegulationRequestData Data { get; set; }

            public SetPowerRegulationRequest(string deviceId, string mode) : base("arc_set_power_regulation") {
                Data = new SetPowerRegulationRequestData(deviceId, mode);
            }
        }

        private class SetRangeRequest : Request {
            public class SetRangeRequestData : ArcRequestData {
                [JsonProperty("range")]
                public string Range { get; set; }

                public SetRangeRequestData(string deviceId, string range) : base(deviceId) {
                    Range = range;
                }
            }

            [JsonProperty("data")]
            public SetRangeRequestData Data { get; set; }

            public SetRangeRequest(string deviceId, string range) : base("arc_set_range") {
                Data = new SetRangeRequestData(deviceId, range);
            }
        }

        private class SetSupplyBatteryEmulatorRequest : Request {
            public class SetSupplyBatteryEmulatorRequestData : ArcRequestData {
                [JsonProperty("battery_profile_id")]
                public string BatteryProfileId { get; set; }

                [JsonProperty("series")]
                public long Series { get; set; }

                [JsonProperty("parallel")]
                public long Parallel { get; set; }

                [JsonProperty("used_capacity")]
                public long UsedCapacity { get; set; }

                [JsonProperty("soc")]
                public long Soc { get; set; }

                [JsonProperty("soc_tracking")]
                public bool SocTracking { get; set; }

                public SetSupplyBatteryEmulatorRequestData(
                    string deviceId,
                    string batteryProfileId,
                    long series,
                    long parallel,
                    long usedCapacity,
                    long soc,
                    bool socTracking
                ) : base(deviceId) {
                    BatteryProfileId = batteryProfileId;
                    Series = series;
                    Parallel = parallel;
                    if (usedCapacity != 0) {
                        UsedCapacity = usedCapacity;
                    }
                    if (soc != 100) {
                        Soc = soc;
                    }
                    SocTracking = socTracking;
                }
            }

            [JsonProperty("data")]
            public SetSupplyBatteryEmulatorRequestData Data { get; set; }

            public SetSupplyBatteryEmulatorRequest(
                string deviceId,
                string batteryProfileId,
                long series,
                long parallel,
                long usedCapacity,
                long soc,
                bool socTracking
            ) : base("arc_set_supply_battery_emulator") {
                Data = new SetSupplyBatteryEmulatorRequestData(
                    deviceId,
                    batteryProfileId,
                    series,
                    parallel,
                    usedCapacity,
                    soc,
                    socTracking
                );
            }
        }

        private class SetSupplyBatteryEmulatorResponse : Response {
            public class SetSupplyBatteryEmulatorResponseData {
                [JsonProperty("battery_emulator_id")]
                public string BatteryEmulatorId { get; set; }
            }

            [JsonProperty("data")]
            public SetSupplyBatteryEmulatorResponseData Data { get; set; }
        }

        private class SetSupplyPowerBoxRequest : Request {
            [JsonProperty("data")]
            public ArcRequestData Data { get; set; }

            public SetSupplyPowerBoxRequest(string deviceId) : base("arc_set_supply_power_box") {
                Data = new ArcRequestData(deviceId);
            }
        }

        private class SetSourceCurrentLimitEnabledRequest : Request {
            public class SetSourceCurrentLimitEnabledRequestData : ArcRequestData {
                [JsonProperty("enable")]
                public bool Enable { get; set; }

                public SetSourceCurrentLimitEnabledRequestData(string deviceId, bool enable) : base(deviceId) {
                    Enable = enable;
                }
            }

            [JsonProperty("data")]
            public SetSourceCurrentLimitEnabledRequestData Data { get; set; }

            public SetSourceCurrentLimitEnabledRequest(string deviceId, bool enable) : base("arc_set_src_cur_limit_enabled") {
                Data = new SetSourceCurrentLimitEnabledRequestData(deviceId, enable);
            }
        }

        private class SetTxRequest : Request {
            public class SetTxRequestData : ArcRequestData {
                [JsonProperty("value")]
                public bool Value { get; set; }

                public SetTxRequestData(string deviceId, bool value) : base(deviceId) {
                    Value = value;
                }
            }

            [JsonProperty("data")]
            public SetTxRequestData Data { get; set; }

            public SetTxRequest(string deviceId, bool value) : base("arc_set_tx") {
                Data = new SetTxRequestData(deviceId, value);
            }
        }

        private class SetUartBaudrateRequest : Request {
            public class SetUartBaudrateRequestData : ArcRequestData {
                [JsonProperty("value")]
                public int Value { get; set; }

                public SetUartBaudrateRequestData(string deviceId, int value) : base(deviceId) {
                    Value = value;
                }
            }

            [JsonProperty("data")]
            public SetUartBaudrateRequestData Data { get; set; }

            public SetUartBaudrateRequest(string deviceId, int value) : base("arc_set_uart_baudrate") {
                Data = new SetUartBaudrateRequestData(deviceId, value);
            }
        }

        private class WriteTxRequest : Request {
            public class WriteTxRequestData : ArcRequestData {
                [JsonProperty("value")]
                public string Value { get; set; }

                public WriteTxRequestData(string deviceId, string value) : base(deviceId) {
                    Value = value;
                }
            }

            [JsonProperty("data")]
            public WriteTxRequestData Data { get; set; }

            public WriteTxRequest(string deviceId, string value) : base("arc_write_tx") {
                Data = new WriteTxRequestData(deviceId, value);
            }
        }
    }
}
