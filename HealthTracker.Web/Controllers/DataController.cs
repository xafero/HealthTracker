using System.Web.Mvc;
using HealthTracker.API;
using log4net;

namespace HealthTracker.Web.Controllers
{
    public class DataController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataController).Name);

        public static void OnHealthEvent(IHealthHub hub, IDataEvent data)
        {
            log.Debug($" {hub} {data.Data} {data.Time} {data.Unit} ");
			// throw new NotImplementedException();
        }

        /*
         *  private void Hub_OnHealthEvent(IHealthHub hub, IDataEvent data)
        {
            if (!currentMeasure.IsHandleCreated)
                return;
            currentMeasure.BeginInvoke((Action)(() =>
            {
                currentMeasure.Text = $"{data.Data} {data.Unit} ({(data.Status == DataKind.Final ? "!" : "~")})";
            }));
        }   */
    }
}