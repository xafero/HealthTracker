using HealthTracker.API;
using System;
using System.Runtime.InteropServices;

namespace HealthTracker.Linux
{
    public class LinuxHealthHub : IHealthHub
    {
        public LinuxHealthHub()
        {
            //    Thread.Sleep(10 * 1000);

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
                type_mask = (ushort)0x00000010,
                event_mask = 0x4000000000000000ul,
                opcode = 0
            };

            status = Linux.setsockopt(sockFd, Consts.SOL_HCI, Consts.HCI_FILTER, ref filter, Marshal.SizeOf(filter));
            Console.WriteLine(status);
            status = Bluez.hci_le_set_scan_enable(sockFd, 1, 0, 1000);
            Console.WriteLine(status);

            status = Linux.setsockopt(sockFd, Consts.SOL_HCI, Consts.HCI_FILTER, ref filter, Marshal.SizeOf(filter));
            Console.WriteLine("fuck " + status);

            int counter = 0;
            while (true)
            {
                var buffer = new byte[1024];
                Linux.recvfrom(sockFd, ref buffer, buffer.Length, 0, IntPtr.Zero, 0);
                Console.WriteLine(BitConverter.ToString(buffer));


                counter++;
                if (counter > 10)
                    break;
            }

            OnHealthEvent?.Invoke(this, new SimpleData { Data = 42 });
        }

        public event DataHandler OnHealthEvent;

        public void Dispose()
        {
        }
    }
}