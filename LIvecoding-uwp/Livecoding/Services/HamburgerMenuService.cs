using Livecoding.UWP.Models;
using Livecoding.UWP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace Livecoding.UWP.Services
{
    public interface IHamburgerNavigationService
    {
        void SetFrameElement(Frame frame);
        void Configure(string key, Type pageType);
        void NavigateTo(string pageKey);
    }

    public interface IHamburgerMenuService : IHamburgerNavigationService
    {
        List<MenuItem> MenuItems { get; }

        void SetHamburgerMenuElement(HamburgerMenu hamburgerMenu);
    }

    public class HamburgerMenuService : IHamburgerMenuService
    {
        #region Fields

        private Frame _frame;
        private Dictionary<string, Type> _pageTypes = new Dictionary<string, Type>();
        private HamburgerMenu _hamburgerMenu;

        #endregion

        #region Properties

        public List<MenuItem> MenuItems { get; private set; }

        #endregion

        #region Constructor

        public HamburgerMenuService()
        {
            MenuItems = new List<MenuItem>
            {
                new MenuItem
                {
                    Icon = Symbol.Home,
                    Name = "Livestreams",
                    PageType = typeof(LivestreamsPage),
                    Type = MenuItemType.Main
                },
                new MenuItem {
                    Icon = Symbol.Play,
                    Name = "Watch",
                    PageType = typeof(StreamPage),
                    Type = MenuItemType.Main
                }
            };
        }

        #endregion

        #region Methods

        public void SetFrameElement(Frame frame)
        {
            _frame = frame;
        }

        public void SetHamburgerMenuElement(HamburgerMenu hamburgerMenu)
        {
            _hamburgerMenu = hamburgerMenu;
        }

        public void Configure(string key, Type pageType)
        {
            _pageTypes.Add(key, pageType);
        }

        public void NavigateTo(string pageKey)
        {
            _pageTypes.TryGetValue(pageKey, out var pageType);
            _frame.Navigate(pageType);

            _hamburgerMenu.SelectedItem = MenuItems.First(menuItem => menuItem.PageType == pageType);
        }

        #endregion
    }
}
