using Livecoding.UWP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Livecoding.UWP.Views
{
    public sealed partial class MainPage : Page
    {
        #region Constructor

        public MainPage()
        {
            this.InitializeComponent();

            var mainItems = GetMainItems();

            HamburgerMenuControl.ItemsSource = mainItems;
            HamburgerMenuControl.SelectedItem = mainItems.FirstOrDefault();
            HamburgerMenuControl.OptionsItemsSource = GetOptionsItems();
        }

        #endregion

        #region Events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                ContentFrame.Navigate(typeof(LivestreamsPage));
            }
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            ContentFrame.Navigate(menuItem.PageType);
        }

        #endregion

        #region Methods

        private List<MenuItem> GetMainItems()
        {
            return new List<MenuItem>
            {
                new MenuItem { Icon = Symbol.Home, Name = "Livestreams", PageType = typeof(LivestreamsPage) }
            };
        }

        private List<MenuItem> GetOptionsItems()
        {
            return new List<MenuItem>
            {

            };
        }

        #endregion
    }
}