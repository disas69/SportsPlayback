using Framework.Editor;
using Sports.Playback.View.Soccer;
using UnityEditor;

namespace Sports.Assets.Scripts.Playback.Editor
{
    [CustomPropertyDrawer(typeof(View))]
    public class ViewPropertyDrawer : StorageItemPropertyDrawer<View, ViewStorage>
    {
    }
}