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
        [SerializeField] private SoccerMatchView _view;
        [SerializeField] private int _dataFPS;
        [HideInInspector] [SerializeField] private string _data;

        private void Start()
        {
            _view.Initialize();
            Play();
        }

        protected override PlaybackEngine<string, SoccerPlaybackData> CreateEngine()
        {
            return new SoccerPlaybackEngine(_dataFPS,
                new LocalFilePlaybackDataProvider(Application.streamingAssetsPath + _data.Replace("Assets/StreamingAssets", string.Empty)),
                new SoccerPlaybackDataProcessor());
        }

        protected override PlaybackVisualizer<SoccerPlaybackData> CreateVisualizer()
        {
            return new SoccerPlaybackVisualizer(Engine.Model, _view);
        }
    }
}