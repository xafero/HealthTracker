using HealthTracker.Data;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static HealthTracker.Web.WebUtils;

namespace HealthTracker.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            SetCurrentDir(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"));
            Database.Init();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}