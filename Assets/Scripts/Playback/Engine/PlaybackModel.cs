using System.Collections.Generic;
using Sports.Playback.Data;
using UnityEngine;

namespace Sports.Playback.Engine
{
    public class PlaybackModel
    {
        private const int BufferTimeSeconds = 3;

        public int FPS { get; }
        public int FrameBuffer { get; }
        public LinkedList<PlaybackData> Data { get; }
        public LinkedListNode<PlaybackData> Frame { get; private set; }

        public PlaybackModel(int fps)
        {
            FPS = fps;
            FrameBuffer = BufferTimeSeconds * FPS;
            Data = new LinkedList<PlaybackData>();
        }

        public void Append(PlaybackData playbackData)
        {
            Data.AddLast(playbackData);

            if (Frame == null)
            {
                Frame = Data.First;
            }

            Debug.Log($"Added frame: {playbackData.FrameCount}");
        }

        public bool IsEnoughFrames()
        {
            if (Frame == null)
            {
                return false;
            }

            var current = Frame.Value;
            var last = Data.Last.Value;

            return last.FrameCount - current.FrameCount >= FrameBuffer;
        }

        public bool Next()
        {
            if (Frame.Next != null)
            {
                Frame = Frame.Next;
                return true;
            }

            return false;
        }

        public bool Previous()
        {
            if (Frame.Previous != null)
            {
                Frame = Frame.Previous;
                return true;
            }

            return false;
        }
    }
}