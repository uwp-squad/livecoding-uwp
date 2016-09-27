using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivecodingApi;
using LivecodingApi.Services;
using LivecodingApi.Model;
using System.Collections.ObjectModel;

namespace LIvecoding_uwp.Models
{
    public class LiveStreamRepository : IRepository
    {
        public IEnumerable<T> Get<T>() where T : class, new()
        {
            var _liveStreams = new ObservableCollection<LiveStream>();
            _liveStreams.Add(new LiveStream()
            {
                Description="Integration MVVM",
                IsLive=true,
                Language="C#",
                Title="MVVM",
                UserUrl="Aritide Lavri"
            });
            _liveStreams.Add(new LiveStream()
            {
                Description = "Integration MVVM",
                IsLive = true,
                Language = "C#",
                Title = "MVVM",
                UserUrl = "André"
            });
            _liveStreams.Add(new LiveStream()
            {
                Description = "Integration MVVM",
                IsLive = true,
                Language = "C#",
                Title = "MVVM",
                UserUrl = "David"
            });


            return (IEnumerable<T>)_liveStreams;
        }

        public T GetById<T>(int id) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
