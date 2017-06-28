using System.Web.Mvc;
using System.Collections.Generic;
using HealthTracker.API;
using log4net;
using HealthTracker.Data;

namespace HealthTracker.Web.Controllers
{
    public class DataController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataController).Name);
        private static readonly IList<IDataEvent> events = new List<IDataEvent>();

        private static readonly MeasurementRepository measures;

        static DataController()
        {
            measures = new MeasurementRepository();
        }

        public static void OnHealthEvent(IHealthHub hub, IDataEvent data)
        {
            log.Debug($"{data.Time} {data.Origin} {data.Data} {data.Unit} {data.Kind} {data.Status}");
            if (data.Status == DataKind.Final)
            {
                var measure = new Measurement(data);
                measures.Save(measure);
                return;
            }
            events.Add(data);
        }
    }
}