using NHibernate.Cfg;
using NHibernate;
using System.Collections.Generic;
using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Cfg.MappingSchema;

namespace HealthTracker.Data
{
    public static class Database
    {
        internal static Configuration Config { get; }
        private static ISessionFactory SessionFactory { get; }
        private static HbmMapping Mapping { get; }

        static Database()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(new List<Type> { typeof(PersonMap), typeof(MeasurementMap) });
            Mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            Config = new Configuration();
            Config.AddDeserializedMapping(Mapping, null);
            SessionFactory = Config.Configure().BuildSessionFactory();
        }

        internal static ISession OpenSession() => SessionFactory.OpenSession();

        public static void Init()
        {
            // NO-OP! Just load class...
        }
    }
}