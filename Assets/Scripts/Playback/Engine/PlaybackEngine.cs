using System;
using System.Threading.Tasks;
using Sports.Playback.DataProvider;
using Sports.Playback.DataProcessor;

namespace Sports.Playback.Engine
{
    public class PlaybackEngine : IDisposable
    {
        private const int DelayMilliseconds = 1000;

        private readonly IPlaybackDataProvider _dataProvider;
        private readonly IPlaybackDataProcessor _dataProcessor;

        private bool _isPlaying;

        public PlaybackModel Model { get; }

        public PlaybackEngine(int fps, IPlaybackDataProvider dataProvider, IPlaybackDataProcessor dataProcessor)
        {
            _dataProvider = dataProvider;
            _dataProcessor = dataProcessor;

            Model = new PlaybackModel(fps);
        }

        public async void Start()
        {
            _isPlaying = true;

            while (_isPlaying && !_dataProvider.IsEnd)
            {
                if (!Model.IsEnoughFrames())
                {
                    var data = await _dataProvider.Get();
                    var playbackData = await _dataProcessor.Process(data);

                    Model.Append(playbackData);
                }
                else
                {
                    await Task.Delay(DelayMilliseconds);
                }
            }
        }

        public void Stop()
        {
            _isPlaying = false;
        }

        public void Dispose()
        {
            _dataProvider.Dispose();
        }
    }
}