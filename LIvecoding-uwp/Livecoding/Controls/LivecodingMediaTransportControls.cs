using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Livecoding.UWP.Controls
{
    public sealed class LivecodingMediaTransportControls : MediaTransportControls
    {
        #region Dependency Properties

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(LivecodingMediaTransportControls), new PropertyMetadata(null));

        #endregion

        #region Constructor

        public LivecodingMediaTransportControls()
        {
            DefaultStyleKey = typeof(LivecodingMediaTransportControls);
        }

        #endregion
    }
}
