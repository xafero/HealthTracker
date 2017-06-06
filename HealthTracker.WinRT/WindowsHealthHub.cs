using HealthTracker.API;
using HealthTracker.Core;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
#if MONO
#else
using Windows.Devices.Bluetooth.Advertisement;
#endif

namespace HealthTracker.WinRT
{
    public class WindowsHealthHub : IHealthHub
    {
#if MONO
#else
        private readonly BluetoothLEAdvertisementWatcher watcher;
#endif
        private static readonly ScaleReader reader = new ScaleReader();

        public WindowsHealthHub()
        {
#if MONO
#else
            watcher = new BluetoothLEAdvertisementWatcher()
            {
                ScanningMode = BluetoothLEScanningMode.Passive
            };
            watcher.Received += Watcher_Received;
            watcher.Start();
#endif
        }

        public event DataHandler OnHealthEvent;

        public void Dispose()
        {
#if MONO
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