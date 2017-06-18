using System;

namespace HealthTracker.API
{
    public interface IDataEvent
    {
        DeviceKind Kind { get; }
        ulong Origin { get; }
        DateTimeOffset Time { get; }
        double Data { get; }
        Unit Unit { get; }
        DataKind Status { get; }
    }
}