using Sports.Playback.Data;
using Sports.Playback.Engine;
using UnityEngine;

namespace Sports.Playback.Player
{
    public abstract class PlaybackPlayer<T1, T2> : MonoBehaviour where T2 : PlaybackData
    {
        private bool _isPlaying;

        protected PlaybackEngine<T1, T2> Engine { get; private set; }
        protected PlaybackVisualizer<T2> Visualizer { get; private set; }

        private void Awake()
        {
            Engine = CreateEngine();
            Visualizer = CreateVisualizer();

            Application.targetFrameRate = 60;
        }

        protected abstract PlaybackEngine<T1, T2> CreateEngine();
        protected abstract PlaybackVisualizer<T2> CreateVisualizer();

        public void Play()
        {
            _isPlaying = true;
            Engine.Start();
        }

        public void Stop()
        {
            _isPlaying = false;
            Engine.Stop();
        }

        private void Update()
        {
            if (_isPlaying)
            {
                Visualizer.Update();
            }
        }

        private void OnDestroy()
        {
            if (_isPlaying)
            {
                Stop();
            }

            Engine.Dispose();
        }
    }
}