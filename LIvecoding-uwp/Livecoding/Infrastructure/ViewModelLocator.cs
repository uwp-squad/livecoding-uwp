using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Livecoding.UWP.Services;
using Livecoding.UWP.ViewModels;
using Livecoding.UWP.Views;
using LivecodingApi.Services;
using Microsoft.Practices.ServiceLocation;

namespace Livecoding.UWP.Infrastructure
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

            if (!SimpleIoc.Default.IsRegistered<IHamburgerMenuService>())
            {
                var hamburgerMenuService = new HamburgerMenuService();
                SimpleIoc.Default.Register<IHamburgerMenuService>(() => hamburgerMenuService);
            }

            // Register ViewModels
            SimpleIoc.Default.Register<LivestreamsViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<StreamViewModel>();
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

        public LivestreamsViewModel Livestreams
        {
            get { return ServiceLocator.Current.GetInstance<LivestreamsViewModel>(); }
        }

        public LoginViewModel Login
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        public StreamViewModel Stream
        {
            get { return ServiceLocator.Current.GetInstance<StreamViewModel>(); }
        }

        #endregion
    }
}
