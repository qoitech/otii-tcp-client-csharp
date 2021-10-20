namespace Otii {
    public partial class Arc {
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        private readonly OtiiClient _client;

        internal Arc(OtiiClient client, string device_id, string name, string type) {
            _client = client;
            DeviceId = device_id;
            Name = name;
            Type = type;
        }

        public class Version {
            public string HardwareVersion;
            public string FirmwareVersion;

            public Version(string hardwareVersion, string firmwareVersion) {
                HardwareVersion = hardwareVersion;
                FirmwareVersion = firmwareVersion;
            }
        }

        /// <summary>
        /// Perform internal calibration of an Arc device.
        /// </summary>
        public void Calibrate() {
            var request = new CalibrateRequest(DeviceId);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Enable 5V in expansion port.
        /// </summary>
        /// <param name="enable">true enables 5V output, false disables it.</param>
        public void Enable5V(bool enable) {
            var request = new Enable5VRequest(DeviceId, enable);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Enables the expansion port.
        /// </summary>
        /// <param name="enable">true to enable and false to disable exp port.</param>
        public void EnableExpPort(bool enable) {
            var request = new EnableExpPortRequest(DeviceId, enable);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Enable or disable a measurement channel.
        /// <para><b>Available channels:</b></para>
        /// <list type="table">
            /// <listheader>
                /// <term>Channel</term>
                /// <term>Description</term>
                /// <term>Unit</term>
            /// </listheader>
            /// <item>
                /// <term>mc</term>
                /// <term>Main Current</term>
                /// <term>A</term>
            /// </item>
            /// <item>
                /// <term>mv</term>
                /// <term>Main Voltage</term>
                /// <term>V</term>
            /// </item>
            /// <item>
                /// <term>ac</term>
                /// <term>ADC Current</term>
                /// <term>A</term>
            /// </item>
            /// <item>
                /// <term>av</term>
                /// <term>ADC Voltage</term>
                /// <term>V</term>
            /// </item>
            /// <item>
                /// <term>sv</term>
                /// <term>Sense+ Voltage</term>
                /// <term>V</term>
            /// </item>
            /// <item>
                /// <term>sn</term>
                /// <term>Sense- Voltage</term>
                /// <term>V</term>
            /// </item>
            /// <item>
                /// <term>vb</term>
                /// <term>VBUS</term>
                /// <term>V</term>
            /// </item>
            /// <item>
                /// <term>vj</term>
                /// <term>DC-Jack</term>
                /// <term>V</term>
            /// </item>
            /// <item>
                /// <term>tp</term>
                /// <term>Temperature</term>
                /// <term>°C</term>
            /// </item>
            /// <item>
                /// <term>rx</term>
                /// <term>UART Logs</term>
                /// <term>Text</term>
            /// </item>
            /// <item>
                /// <term>i1</term>
                /// <term>GPI1</term>
                /// <term>Digital</term>
            /// </item>
            /// <item>
                /// <term>i2</term>
                /// <term>GPI2</term>
                /// <term>Digital</term>
            /// </item>
        /// </list>
        /// <para>In addition to above channels, two more channels are enabled automatically when their respective current channel is enabled:</para>
        /// <list type="table">
            /// <listheader>
                /// <term>Channel</term>
                /// <term>Description</term>
                /// <term>Unit</term>
            /// </listheader>
            /// <item>
                /// <term>me</term>
                /// <term>Main Energy</term>
                /// <term>J</term>
            /// </item>
            /// <item>
                /// <term>ae</term>
                /// <term>ADC Energy</term>
                /// <term>J</term>
            /// </item>
        /// </list>
        /// </summary>
        /// <param name="channel">the channel to enable or disable.</param>
        /// <param name="enable">set to true to enable, and to false to disable.</param>
        public void EnableChannel(string channel, bool enable) {
            var request = new EnableChannelRequest(DeviceId, channel, enable);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Enables RX and TX pins to be a UART.
        /// Required to be disabled to use RX and TX pins as GPI/GPO.
        /// </summary>
        /// <param name="enable">true to enable and false to disable UART.</param>
        public void EnableUart(bool enable) {
            var request = new EnableUartRequest(DeviceId, enable);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Get the 4-wire measurement state (available from otii version 2.7.1).
        /// </summary>
        /// <returns>The current state, "cal_invalid", "disabled", "inactive" or "active".</returns>
        public string Get4Wire() {
            var request = new Get4WireRequest(DeviceId);
            var response = _client.PostRequest<Get4WireRequest, Get4WireResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get adc resistor value.
        /// </summary>
        /// <returns></returns>
        public double GetAdcResistor() {
            var request = new GetAdcResistorRequest(DeviceId);
            var response = _client.PostRequest<GetAdcResistorRequest, GetAdcResistorResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get the voltage of the expansion port.
        /// </summary>
        /// <returns></returns>
        public double GetExpVoltage() {
            var request = new GetExpVoltageRequest(DeviceId);
            var response = _client.PostRequest<GetExpVoltageRequest, GetExpVoltageResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get the state of a GPI pin.
        /// Requires expansion port to be enabled.
        /// </summary>
        /// <param name="pin">Id of the GPI pin, 1 or 2.</param>
        /// <returns>the state of the GPI pin.</returns>
        public bool GetGpi(int pin) {
            var request = new GetGpiRequest(DeviceId, pin);
            var response = _client.PostRequest<GetGpiRequest, GetGpiResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get main voltage value.
        /// </summary>
        /// <returns>Voltage in V.</returns>
        public double GetMainVoltage() {
            var request = new GetMainVoltageRequest(DeviceId);
            var response = _client.PostRequest<GetMainVoltageRequest, GetMainVoltageResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get the max allowed current.
        /// </summary>
        /// <returns></returns>
        public double GetMaxCurrent() {
            var request = new GetMaxCurrentRequest(DeviceId);
            var response = _client.PostRequest<GetMaxCurrentRequest, GetMaxCurrentResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get the current measurement range on the main output.
        /// </summary>
        /// <returns>the current range, "low" or "high".</returns>
        public string GetRange() {
            var request = new GetRangeRequest(DeviceId);
            var response = _client.PostRequest<GetRangeRequest, GetRangeResponse>(request);
            return response.Data.Range;
        }

        /// <summary>
        /// The RX pin can be used as a GPI when the UART is disabled.
        /// Requires expansion port to be enabled.
        /// </summary>
        /// <returns>the state of the RX pin.</returns>
        public bool GetRx() {
            var request = new GetRxRequest(DeviceId);
            var response = _client.PostRequest<GetRxRequest, GetRxResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get current state of voltage source current limiting. (available from otii version 2.7.1)
        /// </summary>
        /// <returns>true if set to constant current, false if set to cut-off.</returns>
        public bool GetSourceCurrentLimitEnabled() {
            var request = new GetSourceCurrentLimitEnabledRequest(DeviceId);
            var response = _client.PostRequest<GetSourceCurrentLimitEnabledRequest, GetSourceCurrentLimitEnabledResponse>(request);
            return response.Data.Enabled;
        }

        /// <summary>
        /// Get the UART baud rate.
        /// </summary>
        /// <returns>the requested baud rate.</returns>
        public int GetUartBaudrate() {
            var request = new GetUartBaudrateRequest(DeviceId);
            var response = _client.PostRequest<GetUartBaudrateRequest, GetUartBaudrateResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get value from specified channel.
        /// Command not available for the rx channel.
        /// </summary>
        /// <param name="channel">the channel name.</param>
        /// <returns>the value in A, V, °C and Digital.</returns>
        public double GetValue(string channel) {
            var request = new GetValueRequest(DeviceId, channel);
            var response = _client.PostRequest<GetValueRequest, GetValueResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get hardware and firmware versions of device.
        /// </summary>
        /// <returns></returns>
        public Version GetVersion() {
            var request = new GetVersionRequest(DeviceId);
            var response = _client.PostRequest<GetVersionRequest, GetVersionResponse>(request);
            return new Version(response.Data.HardwareVersion, response.Data.FirmwareVersion);
        }

        /// <summary>
        /// Check if a device is connected.
        /// </summary>
        /// <returns></returns>
        public bool IsConnected() {
            var request = new IsConnectedRequest(DeviceId);
            var response = _client.PostRequest<IsConnectedRequest, IsConnectedResponse>(request);
            return response.Data.Connected;
        }

        /// <summary>
        /// Enable/disable 4-wire measurements using Sense+/- (available from otii version 2.7.1).
        /// </summary>
        /// <param name="enable">true to enable 4-wire, false to disable.</param>
        public void Set4Wire(bool enable) {
            var request = new Set4WireRequest(DeviceId, enable);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set the value of the shunt resistor for the ADC.
        /// </summary>
        /// <param name="value">the ADC resistor in Ω (0.001 to 22Ω).</param>
        public void SetAdcResistor(double value) {
            var request = new SetAdcResistorRequest(DeviceId, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set the voltage of the expansion port.
        /// </summary>
        /// <param name="value">voltage in V (1.2 - 5V).</param>
        public void SetExpVoltage(double value) {
            var request = new SetExpVoltageRequest(DeviceId, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set the state of a GPO.
        /// Requires expansion port to be enabled.
        /// </summary>
        /// <param name="pin">id of the GPI pin, 1 or 2.</param>
        /// <param name="value">state of pin</param>
        public void SetGpo(int pin, bool value) {
            var request = new SetGpoRequest(DeviceId, pin, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Turn on or off main power on a device.
        /// </summary>
        /// <param name="enable">true turns on power, false turns off power.</param>
        public void SetMain(bool enable) {
            var request = new SetMainRequest(DeviceId, enable);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set the main current on Arc.
        /// Used when the Otii device is set in constant current mode.
        /// </summary>
        /// <param name="value">current to set in A.</param>
        public void SetMainCurrent(double value) {
            var request = new SetMainCurrentRequest(DeviceId, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set the main voltage on Arc.
        /// Used when the Otii device is set in constant voltage mode.
        /// </summary>
        /// <param name="value">voltage to set in V.</param>
        public void SetMainVoltage(double value) {
            var request = new SetMainVoltageRequest(DeviceId, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// When the current exceeds this value, the main power will cut off.
        /// </summary>
        /// <param name="value">current in A.</param>
        public void SetMaxCurrent(double value) {
            var request = new SetMaxCurrentRequest(DeviceId, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set power regulation mode.
        /// </summary>
        /// <param name="mode">one of the following: "voltage", "current", "off".</param>
        public void SetPowerRegulation(string mode) {
            var request = new SetPowerRegulationRequest(DeviceId, mode);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set the main outputs measurement range.
        /// </summary>
        /// <param name="range">"low" or "high". "low" will enable auto-range, "high" will force the use of high-range.</param>
        public void SetRange(string range) {
            var request = new SetRangeRequest(DeviceId, range);
            _client.PostRequest(request);
        }

        /// <summary>
        /// The TX pin can be used as a GPO when the UART is disabled.
        /// Requires expansion port to be enabled.
        /// </summary>
        /// <param name="value">state of tx pin.</param>
        public void SetTx(bool value) {
            var request = new SetTxRequest(DeviceId, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Enable voltage source current limit (CC) operation. (available from otii version 2.7.1)
        /// </summary>
        /// <param name="enable">true means enable constant current, false means cut-off.</param>
        public void SetSourceCurrentLimitEnabled(bool enable) {
            var request = new SetSourceCurrentLimitEnabledRequest(DeviceId, enable);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set UART baud rate.
        /// </summary>
        /// <param name="baudrate">Baud rate to set.</param>
        public void SetUartBaudrate(int baudrate) {
            var request = new SetUartBaudrateRequest(DeviceId, baudrate);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Write data to TX.
        /// Requires expansion port and UART to be enabled.
        /// </summary>
        /// <param name="value">data to write.</param>
        public void WriteTx(string value) {
            var request = new WriteTxRequest(DeviceId, value);
            _client.PostRequest(request);
        }
    }
}