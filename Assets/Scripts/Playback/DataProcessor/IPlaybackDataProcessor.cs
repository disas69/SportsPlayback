using System.Threading.Tasks;
using Sports.Playback.Data;

namespace Sports.Playback.DataProcessor
{
    public interface IPlaybackDataProcessor
    {
        Task<PlaybackData> Process(string data);
    }
}
