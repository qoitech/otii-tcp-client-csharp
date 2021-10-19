using System;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Otii {
    public class OtiiClient {
        private const string DefaultServer = "localhost";
        private const int DefaultPort = 1905;
        private readonly TcpClient _client = null;
        private readonly NetworkStream _stream = null;
        private int transId = 0;

        public Otii Otii { get; }

        public OtiiClient(string server = DefaultServer, int port = DefaultPort) {
            _client = new TcpClient(server, port);
            _stream = _client.GetStream();

            var streamReader = new StreamReader(_stream, System.Text.Encoding.UTF8);
            var responseData = streamReader.ReadLine();
            ServerStatus status = JsonConvert.DeserializeObject<ServerStatus>(responseData);
            Console.WriteLine($"Type: {status.Type}");
            Console.WriteLine($"Info: {status.Info}");
            Console.WriteLine($"Version: {status.Data.OtiiVersion}");
            Console.WriteLine($"Protocol version: {status.Data.ProtocolVersion}");

            Otii = new Otii(this);
        }

        public void Close() {
            _stream.Close();
            _client.Close();
        }

        public class DisconnectedException : Exception {
        }

        internal U PostRequest<T, U>(T request, bool log = false) where T : Request where U : Response {
            request.TransId = $"{++transId}";
            var jsonString = JsonConvert.SerializeObject(request) + "\n";
            if (log) {
                Console.Write(jsonString);
            }
            var data = System.Text.Encoding.UTF8.GetBytes(jsonString);
            _stream.Write(data, 0, data.Length);

            var streamReader = new StreamReader(_stream, System.Text.Encoding.UTF8);
            var responseData = streamReader.ReadLine();
            if (responseData == null) {
                throw new DisconnectedException();
            }
            if (log) {
                Console.Write(responseData.Substring(0, Math.Min(responseData.Length, 256)));
            }
            var response = JsonConvert.DeserializeObject<U>(responseData);
            if (response.Type == "error") {
                throw new Exception(responseData);
            }
            if (response.TransId != request.TransId) {
                throw new Exception("Trans id mismatch");
            }
            return response;
        }

        internal void PostRequest<T>(T request, bool log = false) where T : Request {
            PostRequest<T, Response>(request, log);
        }

        private class ServerStatus {
            public class ServerStatusData {
                [JsonProperty("otii_version")]
                public string OtiiVersion { get; set; }

                [JsonProperty("protocol_version")]
                public string ProtocolVersion { get; set; }

                [JsonProperty("server")]
                public string Server { get; set; }
            }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("info")]
            public string Info { get; set; }

            [JsonProperty("data")]
            public ServerStatusData Data { get; set; }
        }
    }

    internal class Request {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("cmd")]
        public string Cmd { get; set; }

        [JsonProperty("trans_id")]
        public string TransId { get; set; }

        public Request(string cmd) {
            Type = "request";
            Cmd = cmd;
            TransId = "";
        }
    }

    internal class Response {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("cmd")]
        public string Cmd { get; set; }

        [JsonProperty("trans_id")]
        public string TransId { get; set; }
    }

}