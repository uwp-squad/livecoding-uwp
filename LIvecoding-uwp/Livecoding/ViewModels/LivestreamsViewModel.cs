using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Livecoding.UWP.Services;
using LivecodingApi.Model;
using LivecodingApi.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Livecoding.UWP.ViewModels
{
    public class LivestreamsViewModel : ViewModelBase
    {
        #region Fields

        private IHamburgerMenuService _hamburgerNavigationService;
        private IReactiveLivecodingApiService _livecodingApiService;

        #endregion

        #region Properties

        public ObservableCollection<LiveStream> LiveStreams { get; } = new ObservableCollection<LiveStream>();

        #endregion

        #region Commands

        public ICommand SelectLivestreamCommand { get; }

        #endregion

        #region Constructor

        public LivestreamsViewModel(
            IHamburgerMenuService hamburgerNavigationService,
            IReactiveLivecodingApiService livecodingApiService)
        {
            _hamburgerNavigationService = hamburgerNavigationService;
            _livecodingApiService = livecodingApiService;

            SelectLivestreamCommand = new RelayCommand<LiveStream>(SelectLivestream);

            LoadLivestreams();
        }

        #endregion

        #region Command Methods

        private void SelectLivestream(LiveStream stream)
        {
            ServiceLocator.Current.GetInstance<StreamViewModel>().SelectLivestream(stream);
            _hamburgerNavigationService.NavigateTo("Stream");
        }

        #endregion

        #region Public Methods

        public void LoadLivestreams()
        {
            var paginationRequest = new PaginationRequest();

            _livecodingApiService.GetLiveStreamsOnAir(paginationRequest)
                .Subscribe(async (result) =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        LiveStreams.Clear();

                        foreach (var lv in result.Results)
                        {
                            LiveStreams.Add(lv);
                        }
                    });
                },
                (error) =>
                {
                    throw new Exception();
                });
        }

        #endregion
    }
}
