using System;
using System.IO;
using System.Web.Mvc;

namespace HealthTracker.Web
{
    public static class WebUtils
    {
        public static void SetCurrentDir(string root)
        {
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            Environment.CurrentDirectory = root;
        }

        public static void GetMvcVersion(out string version, out string runtime)
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;
            version = mvcName.Version.Major + "." + mvcName.Version.Minor;
            runtime = isMono ? "Mono" : ".NET";
        }
    }
}