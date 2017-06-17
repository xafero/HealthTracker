using HealthTracker.API;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace HealthTracker.Core
{
    public class DebugScaleReader : ScaleReader
    {
        private readonly TextWriter hciTrace = new StreamWriter(File.OpenWrite("hci.log"))
        {
            AutoFlush = true
        };

        public override IEnumerable<IDataEvent> Read(byte[] bytes, int offset = 0)
        {
            hciTrace.WriteLine(new SoapHexBinary(bytes));
            return base.Read(bytes, offset);
        }
    }
}