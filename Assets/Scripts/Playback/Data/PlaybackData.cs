using UnityEngine;
using System.Collections.Generic;

namespace Sports.Playback.Data
{
    public class PlaybackData
    {
        public int FrameCount { get; }
        public List<TrackedObject> TrackedObjects { get; }
        public BallData BallData { get; private set; }

        public PlaybackData(int frameCount)
        {
            FrameCount = frameCount;
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