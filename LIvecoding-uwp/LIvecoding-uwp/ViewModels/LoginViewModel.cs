using System;
using GalaSoft.MvvmLight;
using LIvecoding_uwp.Configuration;
using LivecodingApi.Model;
using LivecodingApi.Services;

namespace LIvecoding_uwp.Infrastructure
{
    public class LoginViewModel : ViewModelBase
    {
        #region Fields

        private IReactiveLivecodingApiService _livecodingApiService;

        #endregion

        #region Constructor

        public LoginViewModel(IReactiveLivecodingApiService livecodingApiService)
        {
            _livecodingApiService = livecodingApiService;

            Authenticate();
        }

        #endregion

        #region Methods

        public void Authenticate()
        {
            _livecodingApiService.Login(AuthConstants.ClientId, AuthConstants.ClientSecret, new[] { AuthenticationScope.Read })
                .Subscribe((result) =>
                {
                    // TODO
                },
                (error) =>
                {
                    throw new Exception();
                });
        }

        #endregion
    }
}