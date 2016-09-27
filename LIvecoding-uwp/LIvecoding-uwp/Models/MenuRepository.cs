using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LIvecoding_uwp.Models
{
    public class MenuRepository:IRepository
    {
        public IEnumerable<T> Get<T>() where T : class, new()
        {
            var items = new ObservableCollection<MenuItem>();
            items.Add(new MenuItem() { Icon = Symbol.ContactInfo, Name = "Profile", PageType = typeof(Views.LoginPage) });
            items.Add(new MenuItem() { Icon = Symbol.Find, Name = "Search", PageType = typeof(Views.LoginPage) });
            items.Add(new MenuItem() { Icon = Symbol.Calendar, Name = "Schedule", PageType = typeof(Views.LoginPage) });
            items.Add(new MenuItem() { Icon = Symbol.Video, Name = "Vodeos", PageType = typeof(Views.LoginPage) });
            items.Add(new MenuItem() { Icon = Symbol.People, Name = "Following", PageType = typeof(Views.LoginPage) });
            items.Add(new MenuItem() { Icon = Symbol.More, Name = "About", PageType = typeof(Views.LoginPage) });
            items.Add(new MenuItem() { Icon = Symbol.ClosePane, Name = "Logout", PageType = typeof(Views.LoginPage) });
            return (IEnumerable <T>) items;
        }

        public T GetById<T>(int id) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
