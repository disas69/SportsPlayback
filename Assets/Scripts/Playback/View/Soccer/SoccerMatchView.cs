using System;
using Framework.Spawn;
using System.Collections.Generic;
using Sports.Playback.Data.Soccer;
using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    public enum SoccerViewType
    {
        Ball,
        Player1,
        Player2,
        Referee
    }

    [Serializable]
    public class SoccerViewConfig
    {
        public SoccerViewType Type;
        public Spawner Spawner;
    }

    public class SoccerMatchView : MonoBehaviour, IDisposable
    {
        private TrackedObjectView _ball;
        private List<TrackedObjectView> _trackedObjects = new List<TrackedObjectView>();

        [SerializeField] private Transform _viewRoot;
        [SerializeField] private List<SoccerViewConfig> _views;

        public void Interpolate(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            UpdateBall(t, current, next);
            UpdatePlayers(t, current, next);
        }

        private void UpdateBall(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            if (_ball == null)
            {
                _ball = GetSpawner(SoccerViewType.Ball).Spawn<TrackedObjectView>();
                _ball.transform.SetParent(_viewRoot);
            }

            if (next != null)
            {
                var speed = Mathf.Lerp(current.BallData.Speed, next.BallData.Speed, t);
                _ball.SetSpeed(speed);

                var position = Vector3.Lerp(current.BallData.Position, next.BallData.Position, t);
                _ball.SetPosition(position);
            }
            else
            {
                _ball.SetSpeed(current.BallData.Speed);
                _ball.SetPosition(current.BallData.Position);
            }
        }

        private void UpdatePlayers(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            for (var i = 0; i < current.TrackedObjects.Count; i++)
            {
                var view = GetView(current.TrackedObjects[i]);

                if (next != null)
                {
                    var speed = Mathf.Lerp(current.TrackedObjects[i].Speed, next.TrackedObjects[i].Speed, t);
                    view.SetSpeed(speed);

                    var position = Vector3.Lerp(current.TrackedObjects[i].Position, next.TrackedObjects[i].Position, t);
                    view.SetPosition(position);
                }
                else
                {
                    view.SetSpeed(current.TrackedObjects[i].Speed);
                    view.SetPosition(current.TrackedObjects[i].Position);
                }
            }
        }

        private TrackedObjectView GetView(TrackedObject trackedObject)
        {
            var view = _trackedObjects.Find(t => t.TrackingID == trackedObject.TrackingID);
            if (view != null)
            {
                return view;
            }

            if (trackedObject.ShirtNumber > 0)
            {
                var soccerPlayerView = GetSpawner(trackedObject.TeamNumber == 0 ? SoccerViewType.Player1 : SoccerViewType.Player2).Spawn<SoccerPlayerView>();
                soccerPlayerView.SetNumber(trackedObject.ShirtNumber);
                view = soccerPlayerView;

            }
            else
            {
                view = GetSpawner(SoccerViewType.Referee).Spawn<SoccerRefereeView>();
            }

            view.SetID(trackedObject.TrackingID);
            view.transform.SetParent(_viewRoot);

            _trackedObjects.Add(view);

            return view;
        }

        private Spawner GetSpawner(SoccerViewType type)
        {
            return _views.Find(s => s.Type == type).Spawner;
        }

        public void Dispose()
        {
            for (var i = 0; i < _views.Count; i++)
            {
                _views[i].Spawner.Clear();
            }
        }
    }
}