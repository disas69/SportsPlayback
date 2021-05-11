using Sports.Playback.Data.Soccer;
using Sports.Playback.DataProcessor;
using Sports.Playback.DataProvider;

namespace Sports.Playback.Engine.Soccer
{
    public class SoccerPlaybackEngine : PlaybackEngine<string, SoccerPlaybackData>
    {
        public SoccerPlaybackEngine(int fps, IPlaybackDataProvider<string> dataProvider, IPlaybackDataProcessor<string, SoccerPlaybackData> dataProcessor) 
            : base(fps, dataProvider, dataProcessor)
        {
        }
    }
}