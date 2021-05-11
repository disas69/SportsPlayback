using Sports.Playback.Data;
using Sports.Playback.Engine;
using UnityEngine;

namespace Sports.Playback.Player
{
    public abstract class PlaybackVisualizer<T> where T : PlaybackData
    {
        private readonly float _frameTime;
        private readonly PlaybackModel<T> _model;

        private float _time;

        public PlaybackVisualizer(PlaybackModel<T> model)
        {
            _frameTime = 1f / model.FPS;
            _model = model;
        }

        public void Update()
        {
            if (_model.Frame == null)
            {
                return;
            }

            if (_time >= _frameTime)
            {
                NextFrame(_time - _frameTime);
            }

            Interpolate(_time / _frameTime);

            _time += Time.deltaTime;
        }

        private void Interpolate(float t)
        {
            Interpolate(t, _model.Frame.Value, _model.Frame.Next?.Value);
        }

        protected abstract void Interpolate(float t, T current, T next);

        private void NextFrame(float delta)
        {
            _model.Next();
            _time = delta;
        }
    }
}