using UnityEngine;

namespace Sports.Playback.Data.Soccer
{
    public class BallData
    {
        public Vector3 Position { get; }
        public float Speed { get; }

        public BallData(Vector3 position, float speed)
        {
            Position = position;
            Speed = speed;
        }
    }
}