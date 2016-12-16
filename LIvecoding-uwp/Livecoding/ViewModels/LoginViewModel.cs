using System;
using GalaSoft.MvvmLight;
using Livecoding.UWP.Configuration;
using LivecodingApi.Model;
using LivecodingApi.Services;
using GalaSoft.MvvmLight.Views;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.Security.Credentials;
using Livecoding.UWP.Constants;

namespace Livecoding.UWP.ViewModels
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
        }

        #endregion

        #region Public Methods

        public void TryAuthenticate()
        {
            // Retrieve token and do not login if we are already connected (token already set)
            RetrieveUserToken();
            if (!string.IsNullOrWhiteSpace(_livecodingApiService.Token))
            {
                _navigationService.NavigateTo("Main");
                return;
            }

            // Login if there is no current user already logged
            _livecodingApiService.Login(AuthConstants.ClientId, AuthConstants.ClientSecret, new[] { AuthenticationScope.Read })
                .Subscribe(async (result) =>
                {
                    // Cache access token in Password vault
                    if (result.HasValue && result.Value)
                    {
                        SaveUserToken();

                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                        {
                            _navigationService.NavigateTo("Main");
                        });
                    }
                    else
                    {
                        // TODO : throw and handle exception
                    }
                },
                (error) =>
                {
                    throw new Exception();
                });
        }

        #endregion

        #region Private Methods

        private void RetrieveUserToken()
        {
            try
            {
                PasswordCredential credential = null;
                var vault = new PasswordVault();
                var credentialList = vault.FindAllByResource(LoginConstants.AppResource);

                if (credentialList.Count > 0)
                {
                    if (credentialList.Count == 1)
                    {
                        credential = credentialList[0];
                    }
                    else
                    {
                        // TODO
                        // When there are multiple usernames, retrieve the default username. 
                        // If one doesn't exist, then display UI to have the user select a default username.
                        throw new NotImplementedException();
                    }
                }

                credential.RetrievePassword();
                _livecodingApiService.Token = credential.Password;
            }
            catch
            {
                _livecodingApiService.Token = null;
            }
        }

        private void SaveUserToken()
        {
            _livecodingApiService.GetCurrentUser().Subscribe((user) =>
            {
                var vault = new PasswordVault();
                vault.Add(new PasswordCredential(LoginConstants.AppResource, user.Username, _livecodingApiService.Token));
            });
        }

        #endregion
    }
}