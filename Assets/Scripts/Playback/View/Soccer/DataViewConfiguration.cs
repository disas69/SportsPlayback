using System;
using System.Collections.Generic;
using Framework.Utils.Math;
using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    [Serializable]
    public class TeamConfig
    {
        public int Team;
        public View View;
    }

    [Serializable]
    public class Bounds
    {
        public MinMaxFloatValue X;
        public MinMaxFloatValue Z;
    }

    [CreateAssetMenu(fileName = "DataViewConfiguration", menuName = "Configuration/DataViewConfiguration")]
    public class DataViewConfiguration : ScriptableObject
    {
        public List<TeamConfig> Teams = new List<TeamConfig>();
        public List<int> IgnoredTrackingID = new List<int>();
        public Bounds Bounds;

        public string GetViewName(int team)
        {
            var config = Teams.Find(c => c.Team == team);
            if (config != null)
            {
                return config.View.Name;
            }

            return string.Empty;
        }

        public bool IsIgnored(int trackingID)
        {
            return IgnoredTrackingID.Contains(trackingID);
        }

        public bool IsInBounds(Vector3 position)
        {
            return Bounds.X.Contains(position.x) && Bounds.Z.Contains(position.z);
        }
    }
}