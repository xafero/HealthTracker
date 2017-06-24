using HealthTracker.API;
using HealthTracker.Core;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
#if MONO
#else
using Windows.Devices.Bluetooth.Advertisement;
#endif

namespace HealthTracker.WinRT
{
    public class WindowsHealthHub : IHealthHub
    {
#if MONO
        private Thread thread;
#else
        private readonly BluetoothLEAdvertisementWatcher watcher;
#endif
        private static readonly ScaleReader reader = new ScaleReader();

        public WindowsHealthHub()
        {
#if MONO
            thread = new Thread(Rand);
            thread.Start();
#else
            watcher = new BluetoothLEAdvertisementWatcher()
            {
                ScanningMode = BluetoothLEScanningMode.Passive
            };
            watcher.Received += Watcher_Received;
            watcher.Start();
#endif
        }

        private void Rand()
        {
            var rnd = new Random();
            Thread.Sleep(10 * 1000);
            while (true)
            {
                var isFinal = rnd.Next(1, 100) > 50;
                OnHealthEvent?.Invoke(this, new SimpleData
                {
                    Data = (int)(rnd.NextDouble() * 100),
                    Kind = DeviceKind.Scale,
                    Origin = 42,
                    Status = isFinal ? DataKind.Final : DataKind.Transitional,
                    Time = DateTime.Now,
                    Unit = Unit.kg
                });
                Thread.Sleep(5 * 100);
            }
        }

        public event DataHandler OnHealthEvent;

        public void Dispose()
        {
#if MONO
            thread.Abort();
#else
            watcher.Stop();
#endif
        }

#if MONO
#else
        private void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            switch (args.Advertisement.LocalName)
            {
                case "YoHealth":
                    Scale_Received(sender, args);
                    break;
                default:
                    // No known device, sorry!
                    break;
            }
        }

        private void Scale_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            var addr = args.BluetoothAddress;
            var ts = args.Timestamp;
            var data = args.Advertisement.DataSections.First(d => d.DataType == 255);
            var bytes = data.Data.ToArray();
            foreach (var raw in reader.Read(bytes))
            {
                var parsed = (SimpleData)raw;
                parsed.Origin = addr;
                parsed.Time = ts;
                OnHealthEvent?.Invoke(this, parsed);
            }
        }
#endif
    }
}