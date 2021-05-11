using System;
using System.Threading.Tasks;
using Sports.Playback.Data;
using Sports.Playback.DataProvider;
using Sports.Playback.DataProcessor;

namespace Sports.Playback.Engine
{
    public abstract class PlaybackEngine<T1, T2> : IDisposable where T2 : PlaybackData
    {
        private const int DelayMilliseconds = 1000;

        private readonly IPlaybackDataProvider<T1> _dataProvider;
        private readonly IPlaybackDataProcessor<T1, T2> _dataProcessor;

        private bool _isPlaying;

        public PlaybackModel<T2> Model { get; }

        public PlaybackEngine(int fps, IPlaybackDataProvider<T1> dataProvider, IPlaybackDataProcessor<T1, T2> dataProcessor)
        {
            _dataProvider = dataProvider;
            _dataProcessor = dataProcessor;

            Model = new PlaybackModel<T2>(fps, true);
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