using Sports.Playback.Data.Soccer;
using Sports.Playback.Engine;
using Sports.Playback.View.Soccer;
using UnityEngine;

namespace Sports.Playback.Player.Soccer
{
    public class SoccerPlaybackVisualizer : PlaybackVisualizer<SoccerPlaybackData>
    {
        private SoccerPlayView _view;

        public SoccerPlaybackVisualizer(PlaybackModel<SoccerPlaybackData> model, SoccerPlayView view) : base(model)
        {
            _view = view;
        }

        protected override void Interpolate(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            if (next != null)
            {
                _view.Target.position = Vector3.Lerp(current.BallData.Position, next.BallData.Position, t);
            }
            else
            {
                _view.Target.position = current.BallData.Position;
            }
        }
    }
}