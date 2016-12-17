using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Livecoding.UWP.Services
{
    public interface ISubNavigationService
    {
        void SetFrameElement(Frame frame);
        void Configure(string key, Type pageType);
        void NavigateTo(string pageKey);
    }

    public class SubNavigationService : ISubNavigationService
    {
        #region Fields

        private Frame _frame;
        private Dictionary<string, Type> _pageTypes = new Dictionary<string, Type>();

        #endregion

        #region Methods

        public void SetFrameElement(Frame frame)
        {
            _frame = frame;
        }

        public void Configure(string key, Type pageType)
        {
            _pageTypes.Add(key, pageType);
        }

        public void NavigateTo(string pageKey)
        {
            Type pageType;
            _pageTypes.TryGetValue(pageKey, out pageType);

            _frame.Navigate(pageType);
        }

        #endregion
    }
}
