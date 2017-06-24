using HealthTracker.API;
using HealthTracker.Core;
using log4net;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace HealthTracker.Linux
{
    public class LinuxHealthHub : IHealthHub
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LinuxHealthHub).Name);

        private static readonly ScaleReader reader = new ScaleReader();

        private bool shouldRun = true;
        private readonly Thread watcher;

        public LinuxHealthHub()
        {
            log.Debug($"Hub initialising...");
            var devId = Bluez.hci_get_route(IntPtr.Zero);
            log.Debug($"Device ID = {devId}");
            var sockFd = Linux.socket(Consts.AF_BLUETOOTH, Consts.SOCK_RAW | Consts.SOCK_CLOEXEC, Consts.BTPROTO_HCI);
            log.Debug($"Socket FD = {sockFd}");
            var addr = new Sockaddr_hci
            {
                hci_family = Consts.AF_BLUETOOTH,
                hci_dev = (ushort)devId,
                hci_channel = Consts.HCI_CHANNEL_RAW
            };
            var status = Linux.bind(sockFd, ref addr, Marshal.SizeOf(addr));
            log.Debug($"Socket bind => {status}");
            status = Bluez.hci_le_set_scan_parameters(sockFd, 0, 0x10, 0x10, 0, 0, 1000);
            log.Debug($"Bluetooth LE scan parameters => {status}");
            var filter = new Hci_filter
            {
                type_mask = 0x00000010,
                event_mask = 0x4000000000000000ul,
                opcode = 0
            };
            status = Linux.setsockopt(sockFd, Consts.SOL_HCI, Consts.HCI_FILTER, ref filter, Marshal.SizeOf(filter));
            log.Debug($"Socket options => {status}");
            status = Bluez.hci_le_set_scan_enable(sockFd, 1, 0, 1000);
            log.Debug($"Bluetooth LE scan enable => {status}");
            watcher = new Thread(() =>
                        {
                            while (shouldRun)
                            {
                                var bytes = new byte[44];
                                Reflect.Call("Mono.Unix.Native.Syscall, Mono.Posix", "recv", sockFd, bytes, (ulong)bytes.Length, 0);
                                log.Debug($"Got {bytes.Length} bytes!");
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
            log.Debug("Watcher started.");
        }

        public event DataHandler OnHealthEvent;

        public void Dispose()
        {
            log.Debug($"Hub disposing...");
            shouldRun = false;
            watcher.Interrupt();
            log.Debug($"Watcher interrupted.");
        }
    }
}