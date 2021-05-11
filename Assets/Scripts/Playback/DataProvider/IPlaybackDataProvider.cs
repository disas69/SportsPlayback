using System;
using System.Threading.Tasks;

namespace Sports.Playback.DataProvider
{
    public interface IPlaybackDataProvider<T> : IDisposable
    {
        bool IsEnd { get; }
        Task<T> Get();
    }
}
