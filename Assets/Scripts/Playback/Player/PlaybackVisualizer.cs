using Sports.Playback.Engine;
using UnityEngine;

namespace Sports.Playback.Player
{
    public class PlaybackVisualizer
    {
        private readonly float _frameTime;
        private readonly PlaybackModel _model;

        private float _time;
        private Transform _target;

        public PlaybackVisualizer(PlaybackModel model, Transform target)
        {
            _frameTime = 1f / model.FPS;
            _model = model;

            _target = target;
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

        private void NextFrame(float delta)
        {
            _model.Next();
            _time = delta;
        }

        private void Interpolate(float t)
        {
            var current = _model.Frame;
            var next = current.Next;

            if (next != null)
            {
                _target.position = Vector3.Lerp(current.Value.BallData.Position, next.Value.BallData.Position, t);
            }
            else
            {
                _target.position = current.Value.BallData.Position;
            }
        }
    }
}