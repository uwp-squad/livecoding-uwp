using GalaSoft.MvvmLight.Ioc;
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
            SimpleIoc.Default.Register<IReactiveLivecodingApiService, ReactiveLivecodingApiService>();

            // Register ViewModels
            SimpleIoc.Default.Register<LoginViewModel>();
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
