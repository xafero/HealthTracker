using HealthTracker.API;
using HealthTracker.Core;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HealthTracker.Linux
{
    public class LinuxHealthHub : IHealthHub
    {
        private static readonly ScaleReader reader = new ScaleReader();

        private bool shouldRun = true;
        private readonly Thread watcher;

        public LinuxHealthHub()
        {
            var devId = Bluez.hci_get_route(IntPtr.Zero);
            var sockFd = Linux.socket(Consts.AF_BLUETOOTH, Consts.SOCK_RAW | Consts.SOCK_CLOEXEC, Consts.BTPROTO_HCI);
            var addr = new Sockaddr_hci
            {
                hci_family = Consts.AF_BLUETOOTH,
                hci_dev = (ushort)devId,
                hci_channel = Consts.HCI_CHANNEL_RAW
            };
            var status = Linux.bind(sockFd, ref addr, Marshal.SizeOf(addr));
            status = Bluez.hci_le_set_scan_parameters(sockFd, 0, 0x10, 0x10, 0, 0, 1000);
            var filter = new Hci_filter
            {
                type_mask = 0x00000010,
                event_mask = 0x4000000000000000ul,
                opcode = 0
            };
            status = Linux.setsockopt(sockFd, Consts.SOL_HCI, Consts.HCI_FILTER, ref filter, Marshal.SizeOf(filter));
            status = Bluez.hci_le_set_scan_enable(sockFd, 1, 0, 1000);
            watcher = new Thread(() =>
                        {
                            while (shouldRun)
                            {
                                var bytes = new byte[44];
                                Reflect.Call("Mono.Unix.Native.Syscall, Mono.Posix", "recv", sockFd, bytes, (ulong)bytes.Length, 0);
                                foreach (var raw in reader.Read(bytes, offset: 16))
                                {
                                    var parsed = (SimpleData)raw;
                                    parsed.Time = DateTime.UtcNow;
                                    OnHealthEvent?.Invoke(this, parsed);
                                }
                            }
                        })
            {
                IsBackground = true,
                Name = "Watcher"
            };
            watcher.Start();
        }

        public event DataHandler OnHealthEvent;

        public void Dispose()
        {
            shouldRun = false;
            watcher.Interrupt();
        }
    }
}