using UnityEngine;

namespace Sports.Playback.Data.Soccer
{
    // Order: TeamNumber,TrackingID,ShirtNumber,X-Position,Y-Position,Speed;
    public class TrackedObject
    {
        public int TeamNumber { get; }
        public int TrackingID { get; }
        public int ShirtNumber { get; }
        public Vector3 Position { get; }
        public float Speed { get; }

        public TrackedObject(int teamNumber, int trackingId, int shirtNumber, Vector3 position, float speed)
        {
            TeamNumber = teamNumber;
            TrackingID = trackingId;
            ShirtNumber = shirtNumber;
            Position = position;
            Speed = speed;
        }
    }
}