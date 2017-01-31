using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;
using Energy;

namespace EnergyStatusServer
{
    class StatusCache : StatusCacheReader, StatusCacheWriter
    {
        private ConcurrentDictionary<String, Energy.Status> cache = new ConcurrentDictionary<String, Energy.Status>();

        public StatusCache( )
        {

        }

        public Status getItem(String key)
        {
            Status statusRet = new Status();
            if (cache.TryGetValue(key, out statusRet))
            {
                return statusRet;
            }
            return statusRet;
        }

        public int getSize()
        {
            return cache.Count();
        }

        public List<String> getKnownKeys()
        {
            var ret = new List<String>();
            foreach( var key in cache.Keys)
            {
                ret.Add(key);
            }
            return ret;
        }

        public void insertItem(String key, Status value)
        {
            cache.AddOrUpdate(key, value,
                        (newKey, existingValue) => { return value; });
        }
    }
}
