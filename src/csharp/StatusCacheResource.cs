using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyStatusServer
{
    class StatusCacheResource
    {
        private static StatusCache cacheInstance;

        private StatusCacheResource() { }

        public static void init()
        {
            createInstanceIfNeeded();
        }

        public static StatusCacheReader getReader()
        {
            createInstanceIfNeeded();
            return cacheInstance;
        }

        public static StatusCacheWriter getWriter()
        {
            createInstanceIfNeeded();
            return cacheInstance;
        }

        private static void createInstanceIfNeeded()
        {
            if( cacheInstance == null)
            {
                cacheInstance = new StatusCache();
            }
        }
    }
}
