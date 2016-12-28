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
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp;
using Microsoft.Practices.ServiceLocation;
using Livecoding.UWP.Models;
using System.Collections.Generic;

namespace Livecoding.UWP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Fields

        private IReactiveLivecodingApiService _livecodingApiService;
        private INavigationService _navigationService;

        private IObjectStorageHelper _localObjectStorageHelper;

        #endregion

        #region Properties

        private bool _loginFailed;
        public bool LoginFailed
        {
            get { return _loginFailed; }
            private set { _loginFailed = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand TryAuthenticateCommand { get; }

        #endregion

        #region Constructor

        public LoginViewModel(
            IReactiveLivecodingApiService livecodingApiService,
            INavigationService navigationService)
        {
            _livecodingApiService = livecodingApiService;
            _navigationService = navigationService;

            _localObjectStorageHelper = ServiceLocator.Current.GetInstance<IObjectStorageHelper>(ServiceLocatorConstants.LocalObjectStorageHelper);

            TryAuthenticateCommand = new RelayCommand(TryAuthenticate);
        }

        #endregion

        #region Command Methods

        public async void TryAuthenticate()
        {
            if (LoginFailed)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    LoginFailed = false;
                });
            }

            // Do not login if we are already connected (token already set)
            // Retrieve token and username from previous connections
            string username = RetrieveUserTokenAndUsername();

            if (!string.IsNullOrWhiteSpace(_livecodingApiService.Token) && !string.IsNullOrWhiteSpace(username))
            {
                // Check if token has expired
                var userConnectionProfile = await RetrieveConnectionProfileByUsernameAsync(username);
                if (userConnectionProfile.ExpirationDate > DateTime.Now)
                {
                    _navigationService.NavigateTo(ViewConstants.Main);
                    return;
                }
            }

            // Login if there is no current user already logged
            _livecodingApiService.Login(AuthConstants.ClientId, AuthConstants.ClientSecret, new[] { AuthenticationScope.Read })
                .Subscribe(async (result) =>
                {
                    if (result.HasValue && result.Value)
                    {
                        HandleLoginSuccess();
                    }
                    else
                    {
                        await HandleLoginFailedAsync();
                    }
                },
                async (error) =>
                {
                    await HandleLoginFailedAsync();
                });
        }

        #endregion

        #region Private Methods

        private string RetrieveUserTokenAndUsername()
        {
            string username = null;

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
                username = credential.UserName;
            }
            catch
            {
                _livecodingApiService.Token = null;
            }

            return username;
        }

        private void SaveUserToken(string username)
        {
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(LoginConstants.AppResource, username, _livecodingApiService.Token));
        }

        private async Task HandleLoginFailedAsync()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                LoginFailed = true;
            });
        }

        private void HandleLoginSuccess()
        {
            _livecodingApiService.GetCurrentUser().Subscribe(async (user) =>
            {
                // Cache access token in Password vault
                SaveUserToken(user.Username);

                // Save user information (connection profile) in local storage
                var userConnectionProfile = new UserConnnectionProfile
                {
                    User = user,
                    ExpirationDate = DateTime.Now.AddSeconds(LoginConstants.TokenLifetime)
                };
                await AddUserConnectionProfileAsync(userConnectionProfile);

                // Navigate to main page
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    _navigationService.NavigateTo(ViewConstants.Main);
                });
            });
        }

        #endregion

        #region Manage User Connection Profiles

        private async Task<Dictionary<string, UserConnnectionProfile>> RetrieveDictionaryOfConnectionProfilesAsync()
        {
            var dictionaryOfUserConnectionProfiles = new Dictionary<string, UserConnnectionProfile>();
            if (await _localObjectStorageHelper.FileExistsAsync(LocalStorageConstants.UserConnectionProfiles))
            {
                dictionaryOfUserConnectionProfiles = await _localObjectStorageHelper
                    .ReadFileAsync(LocalStorageConstants.UserConnectionProfiles, dictionaryOfUserConnectionProfiles);
            }

            return dictionaryOfUserConnectionProfiles;
        }

        private async Task<UserConnnectionProfile> RetrieveConnectionProfileByUsernameAsync(string username)
        {
            // Retrieve a connection profile by username
            var dictionaryOfUserConnectionProfiles = await RetrieveDictionaryOfConnectionProfilesAsync();
            return dictionaryOfUserConnectionProfiles[username];
        }

        private async Task AddUserConnectionProfileAsync(UserConnnectionProfile userConnectionProfile)
        {
            // Set the connection profiles inside the list of all existing profiles
            var dictionaryOfUserConnectionProfiles = await RetrieveDictionaryOfConnectionProfilesAsync();
            dictionaryOfUserConnectionProfiles[userConnectionProfile.User.Username] = userConnectionProfile;

            // Save the dictionary in local storage
            await _localObjectStorageHelper.SaveFileAsync(LocalStorageConstants.UserConnectionProfiles, dictionaryOfUserConnectionProfiles);
        }

        #endregion
    }
}