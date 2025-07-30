using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineFormApp.Utils
{
    public static class NetworkUtils
    {
        public static bool IsOnline()
        {
            var profiles = Connectivity.NetworkAccess;
            var isOnline = profiles == NetworkAccess.Internet;
            return isOnline;
        }
    }
}
