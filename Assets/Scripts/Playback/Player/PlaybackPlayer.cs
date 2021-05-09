using Sports.Playback.DataProcessor;
using Sports.Playback.DataProvider;
using Sports.Playback.Engine;
using UnityEngine;

namespace Sports.Playback.Player
{
    public class PlaybackPlayer : MonoBehaviour
    {
        private PlaybackEngine _engine;

        [SerializeField] private string _data;

        private void Awake()
        {
            var dataProvider = new FilePlaybackDataProvider(Application.dataPath.Replace("Assets", _data));

            _engine = new PlaybackEngine(25, dataProvider, new DefaultPlaybackDataProcessor());
            _engine.Start();
        }

        private void OnDestroy()
        {
            _engine.Stop();
            _engine.Dispose();
        }
    }
}
