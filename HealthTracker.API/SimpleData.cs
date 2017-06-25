using System;

namespace HealthTracker.API
{
    public struct SimpleData : IDataEvent
    {
        public DeviceKind Kind { get; set; }
        public string Origin { get; set; }
        public DateTimeOffset Time { get; set; }
        public double Data { get; set; }
        public Unit Unit { get; set; }
        public DataKind Status { get; set; }
    }
}