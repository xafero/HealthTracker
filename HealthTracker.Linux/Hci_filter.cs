using System.Runtime.InteropServices;

namespace HealthTracker.Linux
{
    [StructLayout(LayoutKind.Sequential)]
    struct Hci_filter
    {
        public ushort type_mask;
        public ulong event_mask;
        public ushort opcode;

        /*	public UInt32 type_mask;
            public UInt64 event_mask;
            public UInt16 opcode;  */
    }
}