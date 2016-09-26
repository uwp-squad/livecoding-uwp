using GalaSoft.MvvmLight.Ioc;
using LIvecoding_uwp.Models;
using LIvecoding_uwp.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIvecoding_uwp.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<IRepository, MenuRepository>();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        public LoginViewModel _loginViewModel
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }
    }
}
