﻿using Livecoding.UWP.Models;
using Livecoding.UWP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Services.Store.Engagement;

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
        private string _currentPageKey;

        #endregion

        #region Properties

        public List<MenuItem> MenuItems { get; private set; }

        #endregion

        #region Constructor

        public HamburgerMenuService()
        {
            MenuItems = new List<MenuItem>
            {
                new NavigationMenuItem
                {
                    Symbol = Symbol.Home,
                    Name = "Livestreams",
                    Type = MenuItemType.Main,
                    PageType = typeof(LivestreamsPage)
                },
                new NavigationMenuItem
                {
                    Symbol = Symbol.Play,
                    Name = "Watch",
                    Type = MenuItemType.Main,
                    PageType = typeof(StreamPage)
                }
            };

            if (StoreServicesFeedbackLauncher.IsSupported())
            {
                var feedbackMenuItem = new ActionMenuItem
                {
                    Glyph = "\uE939",
                    Name = "Feedback",
                    Type = MenuItemType.Options,
                    Action = async () =>
                    {
                        await StoreServicesFeedbackLauncher.GetDefault().LaunchAsync();

                        var pageType = GetPageTypeByKey(_currentPageKey);
                        ResetSelectedItem(pageType);
                    }
                };
                MenuItems.Add(feedbackMenuItem);
            }
        }

        #endregion

        #region Public methods

        public void SetFrameElement(Frame frame)
        {
            _frame = frame;
            _frame.Navigated += (sender, e) => TryResetCurrentPageKey();
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
            var pageType = GetPageTypeByKey(pageKey);

            // No page type => no navigation
            if (pageType == null)
            {
                return;
            }

            _currentPageKey = pageKey;

            // Navigate and reset selected item of the hamburger menu
            _frame.Navigate(pageType);
            ResetSelectedItem(pageType);
        }

        #endregion

        #region Private methods

        private Type GetPageTypeByKey(string pageKey)
        {
            _pageTypes.TryGetValue(pageKey, out var pageType);
            return pageType;
        }

        private void TryResetCurrentPageKey()
        {
            if (_frame.CurrentSourcePageType != null)
            {
                foreach (var kv in _pageTypes)
                {
                    if (kv.Value == _frame.CurrentSourcePageType)
                    {
                        _currentPageKey = kv.Key;
                    }
                }
            }
        }

        private void ResetSelectedItem(Type pageType)
        {
            // Retrieve item in the hamburger menu that will be selected
            _hamburgerMenu.SelectedItem = MenuItems.First(menuItem =>
            {
                return menuItem is NavigationMenuItem navigationMenuItem &&
                       navigationMenuItem.PageType == pageType;
            });
            _hamburgerMenu.SelectedOptionsItem = null;
        }

        #endregion
    }
}
