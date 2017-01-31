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
        private ConcurrentDictionary<byte[], Energy.Status> cache = new ConcurrentDictionary<byte[], Energy.Status>();

        public StatusCache( )
        {

        }

        public Status getItem(byte[] key)
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

        public void insertItem(byte[] key, Status value)
        {
            cache.AddOrUpdate(key, value,
                        (newKey, existingValue) => { return value; });
        }
    }
}
