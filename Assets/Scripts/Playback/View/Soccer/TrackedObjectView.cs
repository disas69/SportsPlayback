using Framework.Spawn;
using Framework.Utils.Math;
using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    public class TrackedObjectView : SpawnableObject
    {
        private VectorAverager _averager;

        public int TrackingID { get; private set; }
        public float Speed { get; private set; }

        private void Awake()
        {
            _averager = new VectorAverager(0.1f);
        }

        public void SetID(int id)
        {
            TrackingID = id;
        }

        public virtual void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public virtual void SetPosition(Vector3 position)
        {
            _averager.AddSample(position);
            transform.position = _averager.Value;
        }
    }
}