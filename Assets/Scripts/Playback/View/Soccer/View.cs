using System;
using Framework.Spawn;
using Framework.Tools.Data;
using UnityEngine;

namespace Sports.Playback.View.Soccer
{
    [Serializable]
    public class View : StorageItem
    {
        [HideInInspector]
        public SpawnerSettings SpawnerSettings;
    }
}
