using System;
using System.Threading;
using HealthTracker.API;

namespace HealthTracker.Mac
{
    public class MacHealthHub : IHealthHub
    {
        private Thread thread;

        public MacHealthHub()
        {
            thread = new Thread(Rand);
            thread.Start();
        }

        private void Rand()
        {
            var rnd = new Random();
            while (true)
            {
                var isFinal = rnd.Next(1, 100) > 50;
                OnHealthEvent?.Invoke(this, new SimpleData
                {
                    Data = (int)(rnd.NextDouble() * 100),
                    Kind = DeviceKind.Scale,
                    Origin = 42,
                    Status = isFinal ? DataKind.Final : DataKind.Transitional,
                    Time = DateTime.Now,
                    Unit = Unit.kg
                });
                Thread.Sleep(5 * 100);
            }
        }

        public event DataHandler OnHealthEvent;

        public void Dispose()
        {
            thread.Abort();
        }
    }
}