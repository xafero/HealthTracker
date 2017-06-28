using HealthTracker.API;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace HealthTracker.Data
{
    public class MeasurementMap : ClassMapping<Measurement>
    {
        public MeasurementMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.Kind, a => a.Type<EnumStringType<DeviceKind>>());
            Property(x => x.TimeStamp);
            Property(x => x.Unit, a => a.Type<EnumStringType<Unit>>());
            Property(x => x.Value);
        }
    }
}