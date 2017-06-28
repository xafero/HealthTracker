using HealthTracker.API;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace HealthTracker.Data
{
    public class PersonMap : ClassMapping<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.Sex, a => a.Type<EnumStringType<Sex>>());
            Property(x => x.Height);
            Property(x => x.Birthday);
        }
    }
}