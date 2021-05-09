using System;
using System.Threading.Tasks;

namespace Sports.Playback.DataProvider
{
    public interface IPlaybackDataProvider : IDisposable
    {
        bool IsEnd { get; }
        Task<string> Get();
    }
}
