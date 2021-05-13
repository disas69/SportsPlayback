using Sports.Playback.Data.Soccer;
using Sports.Playback.Engine;
using Sports.Playback.View.Soccer;

namespace Sports.Playback.Player.Soccer
{
    public class SoccerPlaybackVisualizer : PlaybackVisualizer<SoccerPlaybackData>
    {
        private readonly SoccerMatchView _view;

        public SoccerPlaybackVisualizer(PlaybackModel<SoccerPlaybackData> model, SoccerMatchView view) : base(model)
        {
            _view = view;
        }

        protected override void Interpolate(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            _view.Interpolate(t, current, next);
        }
    }
}