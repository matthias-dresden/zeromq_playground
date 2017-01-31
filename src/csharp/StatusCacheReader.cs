using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyStatusServer
{
    interface StatusCacheReader
    {
        Energy.Status getItem(String key);

        List<String> getKnownKeys();

        int getSize();
    }
}
