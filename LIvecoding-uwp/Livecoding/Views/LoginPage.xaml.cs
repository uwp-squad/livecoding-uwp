using Livecoding.UWP.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Livecoding.UWP.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            Loaded += (sender, e) =>
            {
                ServiceLocator.Current.GetInstance<LoginViewModel>().TryAuthenticate();
            };
        }
    }
}
