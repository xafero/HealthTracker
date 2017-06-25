using BlueBridgeNet.API;
using BlueBridgeNet.Json;
using HealthTracker.API;
using HealthTracker.Core;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HealthTracker.Remote
{
    public class RemoteHealthHub : IHealthHub
    {
        private readonly JsonSerializerSettings config;
        private readonly ScaleReader reader;
        private readonly UdpClient socket;
        private readonly Thread thread;

        public RemoteHealthHub()
        {
            config = JsonConfig.GetDefault();
            var appCfg = ConfigurationManager.AppSettings;
            var multiPort = appCfg["remote-port"] ?? "4242";
            reader = new ScaleReader();
            socket = new UdpClient(int.Parse(multiPort));
            thread = new Thread(Loop) { IsBackground = true };
            thread.Start();
        }

        private void Loop(object state)
        {
            IPEndPoint endpoint = null;
            byte[] bytes;
            while ((bytes = socket.Receive(ref endpoint))?.Length >= 1 && Thread.CurrentThread.IsAlive)
            {
                var json = Encoding.UTF8.GetString(bytes);
                var advertise = JsonConvert.DeserializeObject<Advertisement>(json, config);
                var hasServiceData = advertise.ServiceData?.Length >= 1;
                var data = hasServiceData ? advertise.ServiceData : advertise.ManufacturerData;
                foreach (var raw in reader.Read(data))
                {
                    var parsed = (SimpleData)raw;
                    parsed.Origin = advertise.Address;
                    parsed.Time = DateTime.Now;
                    OnHealthEvent?.Invoke(this, parsed);
                }
            }
        }

        public event DataHandler OnHealthEvent;

        public void Dispose()
        {
            socket.Close();
            thread.Abort();
        }
    }
}