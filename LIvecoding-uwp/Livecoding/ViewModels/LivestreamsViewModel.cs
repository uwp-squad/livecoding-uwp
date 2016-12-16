using GalaSoft.MvvmLight;
using LivecodingApi.Model;
using LivecodingApi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Livecoding.UWP.ViewModels
{
    public class LivestreamsViewModel : ViewModelBase
    {
        #region Fields

        private IReactiveLivecodingApiService _livecodingApiService;

        #endregion

        #region Properties

        public ObservableCollection<LiveStream> LiveStreams { get; } = new ObservableCollection<LiveStream>();

        #endregion

        #region Constructor

        public LivestreamsViewModel(
            IReactiveLivecodingApiService livecodingApiService)
        {
            _livecodingApiService = livecodingApiService;

            LoadLivestreams();
        }

        #endregion

        #region Methods

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
