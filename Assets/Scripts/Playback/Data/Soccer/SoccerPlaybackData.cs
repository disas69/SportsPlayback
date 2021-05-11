using System.Collections.Generic;
using UnityEngine;

namespace Sports.Playback.Data.Soccer
{
    public class SoccerPlaybackData : PlaybackData
    {
        public List<TrackedObject> TrackedObjects { get; }
        public BallData BallData { get; private set; }

        public SoccerPlaybackData(int frameCount) : base(frameCount)
        {
            TrackedObjects = new List<TrackedObject>();
        }

        public void AddTrackedObject(TrackedObject trackedObject)
        {
            TrackedObjects.Add(trackedObject);
        }

        public void AddBallData(Vector3 position, float speed)
        {
            BallData = new BallData(position, speed);
        }
    }
}