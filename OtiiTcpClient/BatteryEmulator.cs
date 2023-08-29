namespace Otii {
    public partial class BatteryEmulator {
        private readonly OtiiClient _client;
        private readonly string _batteryEmulatorId;

        public enum UpdateProfileMode {
            KeepSoc,
            Reset
        }

        internal BatteryEmulator(OtiiClient client, string batteryEmulatorId) {
            _client = client;
            _batteryEmulatorId = batteryEmulatorId;
        }

        /// <summary>
        /// Get current number of emulated battery in parallel.
        /// </summary>
        public long GetParallel() {
            var request = new GetParallelRequest(_batteryEmulatorId);
            var response = _client.PostRequest<GetParallelRequest, GetParallelResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get current number of emulated battery in series.
        /// </summary>
        public long GetSeries() {
            var request = new GetSeriesRequest(_batteryEmulatorId);
            var response = _client.PostRequest<GetSeriesRequest, GetSeriesResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get State Of Charge of emulated battery.
        /// </summary>
        public long GetSoc() {
            var request = new GetSocRequest(_batteryEmulatorId);
            var response = _client.PostRequest<GetSocRequest, GetSocResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Get State Of Charge tracking of emulated battery.
        /// </summary>
        public bool GetSocTracking() {
            var request = new GetSocTrackingRequest(_batteryEmulatorId);
            var response = _client.PostRequest<GetSocTrackingRequest, GetSocTrackingResponse>(request);
            return response.Data.Enabled;
        }

        /// <summary>
        /// Get used capacity of emulated battery.
        /// </summary>
        public long GetUsedCapacity() {
            var request = new GetUsedCapacityRequest(_batteryEmulatorId);
            var response = _client.PostRequest<GetUsedCapacityRequest, GetUsedCapacityResponse>(request);
            return response.Data.Value;
        }

        /// <summary>
        /// Set State Of Charge of emulated battery.
        /// </summary>
        /// <param name="value">state of charge.</param>
        public void SetSoc(long value) {
            var request = new SetSocRequest(_batteryEmulatorId, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set State Of Charge tracking of emulated battery.
        /// </summary>
        /// <param name="enabled">state of SOC tracking.</param>
        public void SetSocTracking(bool enabled) {
            var request = new SetSocTrackingRequest(_batteryEmulatorId, enabled);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Set used capacity of emulated battery.
        /// </summary>
        /// <param name="value">used capacity of emulatoed battery.</param>
        public void SetUsedCapacity(long value) {
            var request = new SetUsedCapacityRequest(_batteryEmulatorId, value);
            _client.PostRequest(request);
        }

        /// <summary>
        /// Update battery profile.
        /// </summary>
        /// <param name="batteryProfileId">id of battery profile</param>
        /// <param name="mode">Keep SOC or reset SOC to 100%</param>
        public void UpdateProfile(string batteryProfileId, UpdateProfileMode mode) {
            var textMode = mode == UpdateProfileMode.KeepSoc ? "keep_soc" : "reset";
            var request = new UpdateProfileRequest(_batteryEmulatorId, batteryProfileId, textMode);
            _client.PostRequest(request);
        }
    }
}
