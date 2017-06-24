using System;
using System.IO;

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
    }
}