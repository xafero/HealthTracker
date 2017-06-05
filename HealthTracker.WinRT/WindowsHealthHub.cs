using HealthTracker.API;
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
            const DeviceKind kind = DeviceKind.Scale;
            var addr = args.BluetoothAddress;
            var ts = args.Timestamp;
            var data = args.Advertisement.DataSections.First(d => d.DataType == 255);
            var bytes = data.Data.ToArray();
            var weightKg = BitConverter.ToUInt16(new[] { bytes[5], bytes[4] }, 0) / 10f;
            var finished = bytes[6] != 255 && bytes[7] != 255;
            OnHealthEvent?.Invoke(this, new SimpleData
            {
                Kind = kind,
                Origin = addr,
                Time = ts,
                Data = weightKg,
                Unit = Unit.kg,
                Status = finished ? DataKind.Final : DataKind.Transitional
            });
        }
#endif
    }
}