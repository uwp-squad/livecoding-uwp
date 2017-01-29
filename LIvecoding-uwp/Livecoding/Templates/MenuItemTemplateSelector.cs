using Livecoding.UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Livecoding.UWP.Templates
{
    public class MenuItemTemplateSelector : DataTemplateSelector
    {
        #region Properties

        public DataTemplate SymbolMenuItemTemplate { get; set; }
        public DataTemplate GlyphMenuItemTemplate { get; set; }

        #endregion

        #region Methods

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (container is FrameworkElement && item != null && item is MenuItem)
            {
                if (item is SymbolMenuItem)
                {
                    return SymbolMenuItemTemplate;
                }

                if (item is GlyphMenuItem)
                {
                    return GlyphMenuItemTemplate;
                }
            }

            return base.SelectTemplateCore(item, container);
        }

        #endregion
    }
}
