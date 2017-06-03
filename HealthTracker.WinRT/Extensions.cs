using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace HealthTracker.WinRT
{
    public static class Extensions
    {
        public static string ToHex(this byte[] bytes) => (new SoapHexBinary(bytes)).ToString();
    }
}