using HealthTracker.API;
using HealthTracker.Remote;

namespace HealthTracker.Web
{
    public static class HealthHubFactory
    {
        public static IHealthHub CreateHub() => new RemoteHealthHub();
    }
}