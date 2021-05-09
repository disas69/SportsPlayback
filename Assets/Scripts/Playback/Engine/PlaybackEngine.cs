using System;
using Sports.Playback.DataProvider;
using Sports.Playback.DataProcessor;
using UnityEngine;

namespace Sports.Playback.Engine
{
    public class PlaybackEngine : IDisposable
    {
        private readonly PlaybackModel _model;
        private readonly IPlaybackDataProvider _dataProvider;
        private readonly IPlaybackDataProcessor _dataProcessor;

        private bool _isPlaying;

        public PlaybackModel Model => _model;

        public PlaybackEngine(int fps, IPlaybackDataProvider dataProvider, IPlaybackDataProcessor dataProcessor)
        {
            _model = new PlaybackModel(fps);
            _dataProvider = dataProvider;
            _dataProcessor = dataProcessor;
        }

        public async void Start()
        {
            _isPlaying = true;

            while (_isPlaying && !_dataProvider.IsEnd)
            {
                var data = await _dataProvider.Get();
                var playbackData = await _dataProcessor.Process(data);

                Debug.Log(playbackData.Frame);

                _model.Append(playbackData);
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