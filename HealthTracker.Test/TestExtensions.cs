using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace HealthTracker.Test
{
    public static class TestExtensions
    {
        public static byte[] ToBytes(this string hex) => SoapHexBinary.Parse(hex).Value;
    }
}