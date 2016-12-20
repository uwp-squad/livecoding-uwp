using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Livecoding.UWP.Controls
{
    public sealed class LivecodingMediaTransportControls : MediaTransportControls
    {
        public LivecodingMediaTransportControls()
        {
            DefaultStyleKey = typeof(LivecodingMediaTransportControls);
        }
    }
}
