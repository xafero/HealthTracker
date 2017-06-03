using System;

namespace HealthTracker.API
{
    public struct SimpleData : IDataEvent
    {
        public DeviceKind Kind { get; set; }
        public ulong Origin { get; set; }
        public DateTimeOffset Time { get; set; }
        public float Data { get; set; }
        public Unit Unit { get; set; }
        public DataKind Status { get; set; }
    }
}