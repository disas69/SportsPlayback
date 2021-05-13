using Framework.Spawn;
using Framework.Utils.Math;
using Sports.Playback.Data.Soccer;
using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    public class TrackedObjectView : SpawnableObject
    {
        private VectorAverager _averager;
        private Vector3 _lastPosition = Vector3.zero;

        public int TrackingID { get; private set; }
        public float Speed { get; private set; }
        public Vector3 Direction { get; private set; }

        private void Awake()
        {
            _averager = new VectorAverager(0.15f);
        }

        public virtual void Setup(TrackedObject trackedObject)
        {
            TrackingID = trackedObject.TrackingID;
        }

        public virtual void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public virtual void SetPosition(Vector3 position)
        {
            _averager.AddSample(position);
            transform.position = _averager.Value;

            Direction = (_lastPosition.WithY(transform.position.y) - transform.position).normalized;
            _lastPosition = transform.position;
        }
    }
}