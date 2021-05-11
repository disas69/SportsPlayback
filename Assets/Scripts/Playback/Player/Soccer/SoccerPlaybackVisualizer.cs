using Sports.Playback.Data.Soccer;
using Sports.Playback.Engine;
using UnityEngine;

namespace Sports.Playback.Player.Soccer
{
    public class SoccerPlaybackVisualizer : PlaybackVisualizer<SoccerPlaybackData>
    {
        private Transform _target;

        public SoccerPlaybackVisualizer(PlaybackModel<SoccerPlaybackData> model, Transform target) : base(model)
        {
            _target = target;
        }

        protected override void Interpolate(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            if (next != null)
            {
                _target.position = Vector3.Lerp(current.BallData.Position, next.BallData.Position, t);
            }
            else
            {
                _target.position = current.BallData.Position;
            }
        }
    }
}