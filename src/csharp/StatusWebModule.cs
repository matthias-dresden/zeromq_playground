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
            Get["/cachesize"] = _ => cache.getSize().ToString(); 
            Get["/"] = Get["/hello"] = _ => "Hello World! Ask me something";
            Get["/clients/list"] = _ => String.Join( ", ", cache.getKnownKeys().ToArray());
            Get["/clients/{id}"] = parameters => 
            { return "You requested info for " + 
                parameters.id + ", it is: " + cache.getItem( parameters.id).Available; };
        }
    }
}
