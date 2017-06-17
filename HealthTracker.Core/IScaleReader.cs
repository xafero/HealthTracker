using HealthTracker.API;
using System.Collections.Generic;

namespace HealthTracker.Core
{
    public interface IScaleReader
    {
        IEnumerable<IDataEvent> Read(byte[] bytes, int offset);
    }
}