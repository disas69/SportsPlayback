using Sports.Playback.DataProcessor;
using Sports.Playback.DataProvider;
using Sports.Playback.Engine;
using UnityEngine;

namespace Sports.Playback.Player
{
    public class PlaybackPlayer : MonoBehaviour
    {
        private PlaybackEngine _engine;
        private PlaybackVisualizer _visualizer;

        [SerializeField] private string _data;
        [SerializeField] private int _dataFPS;
        [SerializeField] private int _targetFPS;
        [SerializeField] private Transform _target;

        private void Awake()
        {
            Application.targetFrameRate = _targetFPS;

            _engine = new PlaybackEngine(_dataFPS, new FilePlaybackDataProvider(Application.dataPath.Replace("Assets", _data)), new DefaultPlaybackDataProcessor());
            _visualizer = new PlaybackVisualizer(_engine.Model, _target);

            _engine.Start();
        }

        private void Update()
        {
            if (_engine.IsPlaying)
            {
                _visualizer.Update();
            }
        }

        private void OnDestroy()
        {
            _engine.Stop();
            _engine.Dispose();
        }
    }
}
