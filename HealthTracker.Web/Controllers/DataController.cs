using System;
using System.Web.Mvc;
using HealthTracker.API;
using Mono.Unix.Native;

namespace HealthTracker.Web.Controllers
{
    public class DataController : Controller
    {
        public static void OnHealthEvent(IHealthHub hub, IDataEvent data)
        {
			throw new NotImplementedException();
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