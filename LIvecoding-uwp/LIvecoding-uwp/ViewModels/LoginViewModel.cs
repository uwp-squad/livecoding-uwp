using System;
using GalaSoft.MvvmLight;
using LIvecoding_uwp.Configuration;
using LivecodingApi.Model;
using LivecodingApi.Services;
using GalaSoft.MvvmLight.Views;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace LIvecoding_uwp.Infrastructure
{
    public class LoginViewModel : ViewModelBase
    {
        #region Fields

        private IReactiveLivecodingApiService _livecodingApiService;
        private INavigationService _navigationService;

        #endregion

        #region Constructor

        public LoginViewModel(
            IReactiveLivecodingApiService livecodingApiService,
            INavigationService navigationService)
        {
            _livecodingApiService = livecodingApiService;
            _navigationService = navigationService;

            Authenticate();
        }

        #endregion

        #region Methods

        public void Authenticate()
        {
            _livecodingApiService.Login(AuthConstants.ClientId, AuthConstants.ClientSecret, new[] { AuthenticationScope.Read })
                .Subscribe(async (result) =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        _navigationService.NavigateTo("Main");
                    });
                },
                (error) =>
                {
                    throw new Exception();
                });
        }

        #endregion
    }
}