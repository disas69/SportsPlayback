using System.Threading.Tasks;
using Sports.Playback.Data;

namespace Sports.Playback.DataProcessor
{
    public interface IPlaybackDataProcessor<T1, T2> where T2 : PlaybackData
    {
        Task<T2[]> Process(T1 data);
    }
}