using HealthTracker.API;
using HealthTracker.Core;
using HealthTracker.Data;
using HealthTracker.Web.Controllers;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static HealthTracker.Web.WebUtils;

namespace HealthTracker.Web
{
    public class Global : HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Global).Name);

        private static PersonRepository persons;
        private static IHealthHub hub;
        private static CalculateHub calc;

        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            SetCurrentDir(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"));
            var root = Environment.CurrentDirectory;
            var user = Environment.UserName;
            var machine = Environment.MachineName;
            log.Debug($"Starting app in '{root}' under '{user}' on '{machine}'...");
            Database.Init();
            // Start logic
            persons = new PersonRepository();
            var person = persons.List().LastOrDefault();
            if (person == null)
            {
                persons.Save(new Person
                {
                    Birthday = DateTime.Now,
                    FirstName = "Test",
                    LastName = "User",
                    Height = 175,
                    Sex = Sex.Male
                });
                person = persons.List().Last();
            }
            hub = HealthHubFactory.CreateHub();
            hub.OnHealthEvent += DataController.OnHealthEvent;
            calc = new CalculateHub(person);
            hub.OnHealthEvent += calc.OnRawHealthEvent;
            calc.OnHealthEvent += DataController.OnHealthEvent;
            // ASP.NET
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log.Debug("App started.");
        }

        protected void Application_End()
        {
            log.Debug("Stopping app...");
            hub.Dispose();
            log.Debug("App stopped.");
        }
    }
}