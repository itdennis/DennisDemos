using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redis4test.Cache
{
    public interface IValueCache
    {
        IList<string> GetValuesByKey(string key);
        void Put(string key, string value);
    }
}
