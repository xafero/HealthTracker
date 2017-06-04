using System;
using System.Linq;

namespace HealthTracker.API
{
    public static class Reflect
    {
        public static object Call(string type, string method, params object[] args)
        {
            var typ = Type.GetType(type);
            var meth = typ.GetMethods().Last(m => m.Name == method);
            return meth.Invoke(null, args);
        }
    }
}
