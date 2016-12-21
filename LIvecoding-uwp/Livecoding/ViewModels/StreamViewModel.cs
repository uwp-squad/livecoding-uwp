using GalaSoft.MvvmLight;
using LivecodingApi.Model;
using LivecodingApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Media.Core;
using Windows.UI.Core;

namespace Livecoding.UWP.ViewModels
{
    public class StreamViewModel : ViewModelBase
    {
        #region Fields

        private IReactiveLivecodingApiService _livecodingApiService;

        #endregion

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(); }
        }

        private MediaSource _viewingSource;
        public MediaSource ViewingSource
        {
            get { return _viewingSource; }
            set { _viewingSource = value; RaisePropertyChanged(); }
        }

        private string _thumbnailUrl;
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { _thumbnailUrl = value; RaisePropertyChanged(); }
        }

        private string _ownerUsername;
        public string OwnerUsername
        {
            get { return _ownerUsername; }
            set { _ownerUsername = value; RaisePropertyChanged(); }
        }

        private string _avatarUrl;
        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set { _avatarUrl = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Constructor

        public StreamViewModel(
            IReactiveLivecodingApiService livecodingApiService)
        {
            _livecodingApiService = livecodingApiService;
        }

        #endregion

        #region Methods

        public void SelectLivestream(LiveStream stream)
        {
            // Reset viewing source
            ViewingSource = null;

            // Set stream basic properties
            Title = stream.Title;
            Description = stream.Description;
            ThumbnailUrl = stream.ThumbnailUrl;

            // Set source of the stream video
            var httpsViewingUrls = stream.ViewingUrls.Where(url => url.StartsWith("https"));
            if (httpsViewingUrls.Any())
            {
                ViewingSource = MediaSource.CreateFromUri(new Uri(httpsViewingUrls.First()));
            }
            else
            {
                // TODO : handle exception
                throw new Exception("This streaming channel does not contain HTTPS viewing url.");
            }

            // Retrieve owner information
            _livecodingApiService.GetUserBySlug(stream.UserSlug)
                .Subscribe(async (user) =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        OwnerUsername = user.Username;
                        AvatarUrl = user.Avatar ?? "/Images/user.png";
                    });
                },
                (error) =>
                {

                });
        }

        #endregion
    }
}
