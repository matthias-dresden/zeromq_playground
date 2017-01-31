using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyStatusServer
{
    interface StatusCacheWriter
    {
        void insertItem(byte[] key, Energy.Status value);
    }
}
