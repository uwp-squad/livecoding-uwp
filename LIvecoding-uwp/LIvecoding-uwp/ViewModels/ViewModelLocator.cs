using GalaSoft.MvvmLight.Ioc;
using LIvecoding_uwp.Models;
using Microsoft.Practices.ServiceLocation;

namespace LIvecoding_uwp.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IRepository, MenuRepository>();
            //SimpleIoc.Default.Register<IRepository, LiveStreamRepository>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<LiveStreamViewModel>();
        }

        public LoginViewModel _loginViewModel
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        public LiveStreamViewModel _liveStreamViewModel
        {
            get { return ServiceLocator.Current.GetInstance<LiveStreamViewModel>(); }
        }
    }
}
