using Source.Camera;
using Sports.Playback.Data.Soccer;
using Sports.Playback.DataProcessor.Soccer;
using Sports.Playback.DataProvider;
using Sports.Playback.Engine.Soccer;
using Sports.Playback.View.Soccer;
using Sports.Playback.Engine;
using UnityEngine;

namespace Sports.Playback.Player.Soccer
{
    public class SoccerPlaybackPlayer : PlaybackPlayer<string, SoccerPlaybackData>
    {
        [SerializeField] private string _data;
        [SerializeField] private int _dataFPS;
        [SerializeField] private SoccerMatchView _view;

        private void Start()
        {
            _view.Initialize();
            Play();
        }

        protected override PlaybackEngine<string, SoccerPlaybackData> CreateEngine()
        {
            return new SoccerPlaybackEngine(_dataFPS,
                new LocalFilePlaybackDataProvider(Application.streamingAssetsPath + "/" + _data),
                new SoccerPlaybackDataProcessor());
        }

        protected override PlaybackVisualizer<SoccerPlaybackData> CreateVisualizer()
        {
            return new SoccerPlaybackVisualizer(Engine.Model, _view);
        }
    }
}