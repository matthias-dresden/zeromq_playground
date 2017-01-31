using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace EnergyStatusServer
{
    public class StatusWebModule : NancyModule
    {
        private StatusCacheReader cache;

        public StatusWebModule()
        {
            cache = StatusCacheResource.getReader();
            initRestApi();
        }

        private void initRestApi()
        {
            Get["/"] = _ => "Hello World! " + cache.getSize();
            Get["/hello"] = _ => "Hello World! " + cache.getSize();
        }
    }
}
