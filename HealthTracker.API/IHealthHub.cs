using System;

namespace HealthTracker.API
{
    public interface IHealthHub : IDisposable
    {
        event DataHandler OnHealthEvent;
    }

    public delegate void DataHandler(IHealthHub hub, IDataEvent data);
}