using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using LIvecoding_uwp.Views;
using LivecodingApi.Services;
using Microsoft.Practices.ServiceLocation;

namespace LIvecoding_uwp.Infrastructure
{
    public class ViewModelLocator
    {
        #region Constructor

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register Services
            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                var navigationService = CreateNavigationService();
                SimpleIoc.Default.Register(() => navigationService);
            }

            if (!SimpleIoc.Default.IsRegistered<IReactiveLivecodingApiService>())
            {
                var livecodingApiService = new ReactiveLivecodingApiService();
                SimpleIoc.Default.Register<IReactiveLivecodingApiService>(() => livecodingApiService);
            }

            // Register ViewModels
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        #endregion

        #region Methods

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            navigationService.Configure("Login", typeof(LoginPage));
            navigationService.Configure("Main", typeof(MainPage));

            return navigationService;
        }

        #endregion

        #region ViewModels

        public LoginViewModel Login
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        #endregion
    }
}
