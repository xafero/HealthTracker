using HealthTracker.API;
using System;

namespace HealthTracker.Data
{
    public class Measurement
    {
        public virtual Guid Id { get; set; }
        public virtual DeviceKind Kind { get; set; }
        public virtual DateTime TimeStamp { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual double Value { get; set; }

        public Measurement()
        {
        }

        public Measurement(IDataEvent data)
        {
            Kind = data.Kind;
            Unit = data.Unit;
            Value = data.Data;
            TimeStamp = data.Time.UtcDateTime;
        }
    }
}