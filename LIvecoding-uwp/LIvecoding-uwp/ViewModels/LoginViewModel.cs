using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LIvecoding_uwp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace LIvecoding_uwp.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        private IRepository repo;
        public ICommand chargePageCommand { get; set; }
        private ObservableCollection<MenuItem> menuItems;
        public ObservableCollection<MenuItem> _menuItems {
            get
            {
                return menuItems;
            }
            set
            {
                RaisePropertyChanged();
            }
        }

        //constructor
        public LoginViewModel(IRepository menuRepo)
        {
            repo = menuRepo;
            chargePageCommand = new RelayCommand(ChargerPersonne);
        }

        internal void ChargerPersonne()
        {
            _menuItems = (ObservableCollection<MenuItem>)repo.Get<MenuItem>();
        }
    }
}
