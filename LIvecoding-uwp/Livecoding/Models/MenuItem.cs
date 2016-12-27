using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Livecoding.UWP.Models
{
    public abstract class MenuItem
    {
        public Symbol Icon { get; set; }
        public string Name { get; set; }
        public MenuItemType Type { get; set; }
    }
}
