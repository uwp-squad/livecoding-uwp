using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livecoding.UWP.Constants
{
    public static class LoginConstants
    {
        /// <summary>
        /// Name of the resource for Password Vault
        /// </summary>
        public const string AppResource = "Livecoding UWP";

        /// <summary>
        /// Lifetime of the Access Token to be able to retrieve data from the API (= 10 hours)
        /// </summary>
        public const double TokenLifetime = 10 * 60 * 60;
    }
}
