using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivecodingApi.Model;

namespace Livecoding.UWP.Models
{
    public class UserConnnectionProfile
    {
        public UserPrivate User { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
