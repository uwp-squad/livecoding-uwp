using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LIvecoding_uwp.Models;
using LivecodingApi.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LIvecoding_uwp.ViewModels
{
    public class LiveStreamViewModel:ViewModelBase
    {
        private IRepository repo=new LiveStreamRepository();
        public ICommand chargePageCommand { get; set; }
        private ObservableCollection<LiveStream> video;
        public ObservableCollection<LiveStream> _videos
        {
            get
            {
                return video;
            }
            set
            {
                video = value;
                RaisePropertyChanged();
            }
        }

        //constructor
        public LiveStreamViewModel()
        {
            //repo = videoRepo;
            chargePageCommand = new RelayCommand(ChargerStream);
        }

        internal void ChargerStream()
        {
            _videos = (ObservableCollection<LiveStream>)repo.Get<LiveStream>();
        }
    }
}