using HealthTracker.API;
using System;

namespace HealthTracker.Web
{
    public static class HealthHubFactory
    {
        public static IHealthHub CreateHub()
        {
            Type hubType;
            var isApple = Environment.GetEnvironmentVariable("Apple_PubSub_Socket_Render") != null;
            if (isApple)
                hubType = Type.GetType("HealthTracker.Mac.MacHealthHub, HealthTracker.Mac");
            else if (Environment.OSVersion.Platform == PlatformID.Unix)
                hubType = Type.GetType("HealthTracker.Linux.LinuxHealthHub, HealthTracker.Linux");
            else
                hubType = Type.GetType("HealthTracker.WinRT.WindowsHealthHub, HealthTracker.WinRT");
            return (IHealthHub)Activator.CreateInstance(hubType);
        }
    }
}