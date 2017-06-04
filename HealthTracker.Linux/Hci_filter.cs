using System.Runtime.InteropServices;

namespace HealthTracker.Linux
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    struct Hci_filter
    {
        public uint type_mask;
        public ulong event_mask;
        public ushort opcode;
    }
}