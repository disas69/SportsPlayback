using UnityEngine;

namespace Sports.Playback.Data.Soccer
{
    // Order: X-Position,Y-Position,Z-Position,BallSpeed,[ClickerFlags]
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