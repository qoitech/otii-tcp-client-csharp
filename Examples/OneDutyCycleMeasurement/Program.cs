using System;
using System.Threading;
using System.Linq;
using Otii;

namespace Program {
    class Program {
        const int RecordingTime = 80_000;

        static void Main(string[] args) {
            var client = new OtiiClient();
            client.Connect();
            var otii = client.Otii;

            var project = otii.GetActiveProject();
            if (project == null) {
                project = otii.CreateProject();
            }

            var devices = otii.GetDevices();
            if (devices.Length != 1) {
                throw new Exception($"Expected exactly one Arc to be connected, found {devices.Length}");
            }
            var arc = devices[0];

            arc.SetMainVoltage(3.7);
            arc.SetMaxCurrent(0.5);
            arc.SetExpVoltage(3.3);
            arc.SetUartBaudrate(115200);
            arc.EnableUart(true);

            arc.EnableChannel("mc", true);
            arc.EnableChannel("gpi1", true);
            arc.EnableChannel("rx", true);

            project.StartRecording();
            arc.SetMain(true);
            Thread.Sleep(RecordingTime);
            arc.SetMain(false);
            project.StopRecording();

            var recording = project.GetLastRecording();

            var energyRX = ComputeEnergyFromRX(arc, recording);
            Console.WriteLine($"Energy = {energyRX / 3600}");

            var energyGPI1 = ComputeEnergyFromGPI1(arc, recording);
            Console.WriteLine($"Energy = {energyGPI1 / 3600}");
        }

        static double ComputeEnergyFromRX(Arc arc, Recording recording) {
            var count = recording.GetChannelDataCount(arc.DeviceId, "rx");
            var rxData = recording.GetLogChannelData(arc.DeviceId, "rx", 0, count);
            var timestamps = rxData.Where(log => log.Value == "Waking up").Select(log => log.Timestamp).ToArray();
            if (timestamps.Length < 1) {
                throw new Exception("Need at last three \"Waking up\" timestamps to be able to calulate the energy consumption for a period");
            }
            var timeFrom = timestamps[1];
            var timeTo = timestamps[2];

            var statistics = recording.GetChannelStatistics(arc.DeviceId, "mc", timeFrom, timeTo);
            return statistics.Energy;
        }

        static double ComputeEnergyFromGPI1(Arc arc, Recording recording) {
            var count = recording.GetChannelDataCount(arc.DeviceId, "gpi1");
            var data = recording.GetDigitalChannelData(arc.DeviceId, "gpi1", 0, count);
            var timestamps = data.Where(log => log.Value).Select(log => log.Timestamp).ToArray();
            if (timestamps.Length < 3) {
                throw new Exception("Need at last three positive flank timestamps to be able to calulate the energy consumption for a period");
            }
            var timeFrom = timestamps[1];
            var timeTo = timestamps[2];

            var statistics = recording.GetChannelStatistics(arc.DeviceId, "mc", timeFrom, timeTo);
            return statistics.Energy;
        }
    }
}
