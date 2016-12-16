using GalaSoft.MvvmLight;
using LivecodingApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;

namespace Livecoding.UWP.ViewModels
{
    public class StreamViewModel : ViewModelBase
    {
        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }

        private MediaSource _viewingSource;
        public MediaSource ViewingSource
        {
            get { return _viewingSource; }
            set { _viewingSource = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Methods

        public void SelectLivestream(LiveStream stream)
        {
            // TODO
            Title = stream.Title;

            var httpsViewingUrls = stream.ViewingUrls.Where(url => url.StartsWith("https")); // TODO : handle exception if no https streaming url
            ViewingSource = MediaSource.CreateFromUri(new Uri(httpsViewingUrls.First()));
        }

        #endregion
    }
}
