using HealthTracker.API;
using HealthTracker.Core;
using HealthTracker.Data;
using HealthTracker.Web.Controllers;
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
        private static PersonRepository persons;
        private static IHealthHub hub;
        private static CalculateHub calc;

        protected void Application_Start()
        {
            SetCurrentDir(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"));
            Database.Init();
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
            // Stop listener
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_DomainUnload;
            // ASP.NET
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            hub.Dispose();
        }
    }
}