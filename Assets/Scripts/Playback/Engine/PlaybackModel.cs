using System.Collections.Generic;
using Sports.Playback.Data;
using UnityEngine;

namespace Sports.Playback.Engine
{
    public class PlaybackModel<T> where T : PlaybackData
    {
        private const int BufferTimeSeconds = 3;

        private readonly bool _removeUsedFrames;

        public int FPS { get; }
        public int FrameBuffer { get; }
        public LinkedList<T> Data { get; }
        public LinkedListNode<T> Frame { get; private set; }

        public PlaybackModel(int fps, bool removeUsedFrames)
        {
            FPS = fps;
            FrameBuffer = BufferTimeSeconds * FPS;
            Data = new LinkedList<T>();

            _removeUsedFrames = removeUsedFrames;
        }

        public void Append(T[] playbackData)
        {
            for (var i = 0; i < playbackData.Length; i++)
            {
                Data.AddLast(playbackData[i]);
                // Debug.Log($"Added frame: {playbackData[i].FrameCount}. Total: {Data.Count}");
            }

            if (Frame == null)
            {
                Frame = Data.First;
            }
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

                if (_removeUsedFrames)
                {
                    Data.RemoveFirst();
                }

                return true;
            }

            return false;
        }

        public bool Previous()
        {
            if (Frame.Previous != null)
            {
                Frame = Frame.Previous;

                if (_removeUsedFrames)
                {
                    Data.RemoveLast();
                }

                return true;
            }

            return false;
        }
    }
}