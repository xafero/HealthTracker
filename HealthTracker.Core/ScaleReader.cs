using HealthTracker.API;
using System;
using System.Collections.Generic;

namespace HealthTracker.Core
{
    public class ScaleReader
    {
        public IEnumerable<IDataEvent> Read(byte[] bytes, int offset = 0)
        {
            var deviceMode = bytes[offset + 11];
            var isHighPrecision = deviceMode % 10 == 2;
            var weightDiv = isHighPrecision ? 100f : 10f;
            var weight = BitConverter.ToUInt16(new[] { bytes[offset + 5], bytes[offset + 4] }, 0);
            var sensor = BitConverter.ToUInt16(new[] { bytes[offset + 7], bytes[offset + 6] }, 0);
            var state = bytes[offset + 8];
            var quality = (state & 0b0010) == 0 ? DataKind.Transitional
                : (state & 0b1111) != 0b0010 ? DataKind.Final : DataKind.Unknown;
            yield return new SimpleData { Data = weight / weightDiv, Unit = Unit.kg, Kind = DeviceKind.Scale, Status = quality };
            if (quality == DataKind.Final)
                yield return new SimpleData { Data = sensor, Unit = Unit.ohm, Kind = DeviceKind.Scale, Status = quality };
        }
    }
}