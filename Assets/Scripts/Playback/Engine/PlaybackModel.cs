using System.Collections.Generic;
using Sports.Playback.Data;

namespace Sports.Playback.Engine
{
    public class PlaybackModel
    {
        public int FPS { get; }
        public LinkedList<PlaybackData> Data { get; }

        public PlaybackModel(int fps)
        {
            FPS = fps;
            Data = new LinkedList<PlaybackData>();
        }

        public void Append(PlaybackData playbackData)
        {
            Data.AddLast(playbackData);
        }
    }
}