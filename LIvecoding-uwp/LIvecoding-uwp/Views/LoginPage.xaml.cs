using LIvecoding_uwp.Configuration;
using LivecodingApi.Model;
using LivecodingApi.Services;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LIvecoding_uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            Authenticate();
        }

        public void Authenticate()
        {
            // TODO : Make a global service
            var service = new ReactiveLivecodingApiService();

            service.Login(AuthConstants.ClientId, AuthConstants.ClientSecret, new[] { AuthenticationScope.Read })
                .Subscribe((result) =>
                {
                    // TODO
                },
                (error) =>
                {
                    // TODO
                });
        }
    }
}
