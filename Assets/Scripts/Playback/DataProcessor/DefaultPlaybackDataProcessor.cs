using System.Threading.Tasks;
using Sports.Playback.Data;
using UnityEngine;

namespace Sports.Playback.DataProcessor
{
    public class DefaultPlaybackDataProcessor : IPlaybackDataProcessor
    {
        public async Task<PlaybackData> Process(string data)
        {
            return await Task<PlaybackData>.Factory.StartNew(() => ParseData(data));
        }

        private PlaybackData ParseData(string data)
        {
            var items = data.Split(':');
            var playbackData = new PlaybackData(GetInt(items[0]));

            var trackedObjects = items[1].Split(';');
            for (var i = 0; i < trackedObjects.Length - 1; i++)
            {
                var trackedObjectItems = trackedObjects[i].Split(',');

                var teamNumber = GetInt(trackedObjectItems[0]);
                var trackingID = GetInt(trackedObjectItems[1]);
                var shirtNumber = GetInt(trackedObjectItems[2]);
                var position = GetVector3(trackedObjectItems[3], null, trackedObjectItems[4]);
                var speed = GetFloat(trackedObjectItems[5]);

                playbackData.AddTrackedObject(new TrackedObject(teamNumber, trackingID, shirtNumber, position, speed));
            }

            var ballDataItems = items[2].Split(',');
            var ballPosition = GetVector3(ballDataItems[0], ballDataItems[1], ballDataItems[2]);
            var ballSpeed = GetFloat(ballDataItems[3]);

            playbackData.AddBallData(ballPosition, ballSpeed);

            return playbackData;
        }

        private static int GetInt(string input)
        {
            int.TryParse(input, out var result);
            return result;
        }

        private static float GetFloat(string input)
        {
            float.TryParse(input, out var result);
            return result;
        }

        private static Vector3 GetVector3(string inputX, string inputY, string inputZ)
        {
            var x = GetFloat(inputX) / 100f;
            var y = GetFloat(inputY) / 100f;
            var z = GetFloat(inputZ) / 100f;

            return new Vector3(x, y, z);
        }
    }
}