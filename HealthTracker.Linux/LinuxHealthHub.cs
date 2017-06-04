using HealthTracker.API;
using System;
using System.Runtime.InteropServices;
using Mono.Unix.Native;
using System.Threading;

namespace HealthTracker.Linux
{
    public class LinuxHealthHub : IHealthHub
    {
private bool shouldRun = true;
private readonly Thread watcher; 

        public LinuxHealthHub()
        {
            var devId = Bluez.hci_get_route(IntPtr.Zero);
            Console.WriteLine(devId);
            var sockFd = Linux.socket(Consts.AF_BLUETOOTH, Consts.SOCK_RAW | Consts.SOCK_CLOEXEC, Consts.BTPROTO_HCI);
            Console.WriteLine(sockFd);
            var addr = new Sockaddr_hci
            {
                hci_family = Consts.AF_BLUETOOTH,
                hci_dev = (ushort)devId,
                hci_channel = Consts.HCI_CHANNEL_RAW
            };
            var status = Linux.bind(sockFd, ref addr, Marshal.SizeOf(addr));
            Console.WriteLine(status);
            status = Bluez.hci_le_set_scan_parameters(sockFd, 0, 0x10, 0x10, 0, 0, 1000);
            Console.WriteLine(status);

            var filter = new Hci_filter
            {
                type_mask = 0x00000010,
                event_mask = 0x4000000000000000ul,
                opcode = 0
            };

            status = Linux.setsockopt(sockFd, Consts.SOL_HCI, Consts.HCI_FILTER, ref filter, Marshal.SizeOf(filter));
            Console.WriteLine(status);
            status = Bluez.hci_le_set_scan_enable(sockFd, 1, 0, 1000);
            Console.WriteLine(status);

            status = Linux.setsockopt(sockFd, Consts.SOL_HCI, Consts.HCI_FILTER, ref filter, Marshal.SizeOf(filter));
            Console.WriteLine("fuck " + status);


watcher = new Thread(() => 
            {
            while (shouldRun)
            {
                var bytes = new byte[44];
                Syscall.recv(sockFd, bytes, (ulong)bytes.Length, 0);
  var offset = 16;
                const DeviceKind kind = DeviceKind.Scale;
            var ts = new DateTimeOffset();
            var weightKg = BitConverter.ToUInt16(new[] { bytes[5+offset], bytes[4+offset] }, 0) / 10f;
            var finished = bytes[6+offset] != 255 && bytes[7+offset] != 255;
            OnHealthEvent?.Invoke(this, new SimpleData
            {
                Kind = kind,
                Time = ts,
                Data = weightKg,
                Unit = Unit.kg,
                Status = finished ? DataKind.Final : DataKind.Transitional
            });
          }}) { IsBackground = true, Name = "Watcher" };
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
