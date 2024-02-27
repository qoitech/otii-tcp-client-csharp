

using OtiiTcpClient;
using OtiiTcpClient.Types;
using System;
using System.Threading;


namespace BasicMeasurement {
    class Program {
        private const int RecordingTime = 30000;

        static void Main(string[] args) {
            // Calling Connect without parameters will connect to a local instance of Otii

            var client = new OtiiClient();
            client.Connect();

            // Create a local reference to the Otii property for convenience
            var otii = client.Otii;

            // Get a list of all Otii devices available
            var devices = otii.GetDevices();
            if (devices.Length == 0) {
                throw new Exception("No available devices");
            }

            // Get a reference to the first device in the list
            var arc = devices[0];

            var project = otii.GetActiveProject();
            if (project == null) {
                project = otii.CreateProject();
            }

            // Configuration
            arc.SetMainVoltage(3.3);
            arc.SetMaxCurrent(0.5);
            arc.EnableUart(true);
            arc.SetUartBaudrate(115200);
            arc.EnableChannel(Channel.MainCurrent, true);
            arc.EnableChannel(Channel.MainVoltage, true);
            arc.EnableChannel(Channel.UartLogs, true);
            arc.EnableChannel(Channel.Gpi1, true);

            // Record
            project.StartRecording();
            arc.SetMain(true);
            Thread.Sleep(RecordingTime);
            arc.SetMain(false);
            project.StopRecording();

            // Close the connection
            client.Close();
        }
    }
}
