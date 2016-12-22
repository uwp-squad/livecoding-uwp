using Livecoding.UWP.Constants;
using Livecoding.UWP.Models;
using Livecoding.UWP.Services;
using Microsoft.Practices.ServiceLocation;
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
        #region Fields

        private IHamburgerMenuService _hamburgerMenuService;

        #endregion

        #region Constructor

        public MainPage()
        {
            this.InitializeComponent();

            _hamburgerMenuService = ServiceLocator.Current.GetInstance<IHamburgerMenuService>();

            HamburgerMenuControl.ItemsSource = _hamburgerMenuService.MenuItems.Where(item => item.Type == MenuItemType.Main);
            HamburgerMenuControl.OptionsItemsSource = _hamburgerMenuService.MenuItems.Where(item => item.Type == MenuItemType.Options);
        }

        #endregion

        #region Events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                // Initialize hamburger menu service
                InitializeHamburgerNavigationService();
                _hamburgerMenuService.NavigateTo(ViewConstants.Livestreams);
            }
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            ContentFrame.Navigate(menuItem.PageType);
        }

        #endregion

        #region Methods

        private void InitializeHamburgerNavigationService()
        {
            _hamburgerMenuService.SetHamburgerMenuElement(HamburgerMenuControl);
            _hamburgerMenuService.SetFrameElement(ContentFrame);

            _hamburgerMenuService.Configure(ViewConstants.Stream, typeof(StreamPage));
            _hamburgerMenuService.Configure(ViewConstants.Livestreams, typeof(LivestreamsPage));
        }

        #endregion
    }
}