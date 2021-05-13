using System;
using Framework.Spawn;
using System.Collections.Generic;
using Sports.Playback.Camera;
using Sports.Playback.Data.Soccer;
using Sports.Playback.View.Soccer.TrackedObjects;
using Sports.Playback.View.Soccer.TrackedObjects.Actors;
using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    public class SoccerMatchView : MonoBehaviour, IDisposable
    {
        private readonly List<TrackedObjectView> _trackedObjects = new List<TrackedObjectView>();
        private readonly List<ViewSpawnConfig> _views = new List<ViewSpawnConfig>();

        private TrackedObjectView _ball;

        [SerializeField] private CameraController _camera;
        [SerializeField] private Transform _viewRoot;
        [SerializeField] private ViewStorage _viewStorage;
        [SerializeField] private DataViewConfiguration _configuration;

        private void Awake()
        {
            for (var i = 0; i < _viewStorage.Items.Count; i++)
            {
                var view = _viewStorage.Items[i];
                var spawner = new GameObject($"Spawner [{view.Name}]").AddComponent<Spawner>();
                spawner.transform.SetParent(transform);
                spawner.Activate(view.SpawnerSettings);

                _views.Add(new ViewSpawnConfig { View = view.Name, Spawner = spawner });
            }
        }

        public void Initialize()
        {
            _ball = GetSpawner("Ball").Spawn<TrackedObjectView>();
            _ball.transform.SetParent(_viewRoot);

            _camera.SetTarget(_ball.transform);
            _camera.Activate(true);
        }

        public void Interpolate(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            UpdateBall(t, current, next);
            UpdateActors(t, current, next);
        }

        private void UpdateBall(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            if (next != null)
            {
                var speed = Mathf.Lerp(current.BallData.Speed, next.BallData.Speed, t);
                _ball.SetSpeed(speed);

                var position = Vector3.Lerp(current.BallData.Position, next.BallData.Position, t);
                if (_configuration.IsInBounds(position))
                {
                    _ball.SetPosition(position);
                }
            }
            else
            {
                _ball.SetSpeed(current.BallData.Speed);
                _ball.SetPosition(current.BallData.Position);
            }
        }

        private void UpdateActors(float t, SoccerPlaybackData current, SoccerPlaybackData next)
        {
            for (var i = 0; i < current.TrackedObjects.Count; i++)
            {
                var view = GetView(current.TrackedObjects[i]);
                if (view != null)
                {
                    if (next != null)
                    {
                        var speed = Mathf.Lerp(current.TrackedObjects[i].Speed, next.TrackedObjects[i].Speed, t);
                        view.SetSpeed(speed);

                        var position = Vector3.Lerp(current.TrackedObjects[i].Position, next.TrackedObjects[i].Position, t);
                        if (_configuration.IsInBounds(position))
                        {
                            view.SetPosition(position);
                        }
                    }
                    else
                    {
                        view.SetSpeed(current.TrackedObjects[i].Speed);
                        view.SetPosition(current.TrackedObjects[i].Position);
                    }
                }
            }
        }

        private TrackedObjectView GetView(TrackedObject trackedObject)
        {
            if (_configuration.IsIgnored(trackedObject.TrackingID))
            {
                return null;
            }

            var view = _trackedObjects.Find(t => t.TrackingID == trackedObject.TrackingID);
            if (view != null)
            {
                return view;
            }

            var viewName = _configuration.GetViewName(trackedObject.TeamNumber);
            if (!string.IsNullOrEmpty(viewName))
            {
                var actor = GetSpawner(viewName).Spawn<SoccerActorView>();
                actor.transform.SetParent(_viewRoot);
                actor.SetTarget(_ball.transform);
                actor.Setup(trackedObject);

                _trackedObjects.Add(actor);

                return actor;
            }

            return null;
        }

        private Spawner GetSpawner(string view)
        {
            var config = _views.Find(v => v.View == view);
            if (config != null)
            {
                return config.Spawner;
            }

            Debug.LogError($"Can't get a spawner config with view name: {view}");
            return null;
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