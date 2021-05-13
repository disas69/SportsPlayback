using Framework.Editor;
using Sports.Playback.View.Soccer;
using UnityEditor;

namespace Sports.Assets.Scripts.Playback.Editor
{
    [CustomEditor(typeof(ViewStorage))]
    public class ViewStorageEditor : StorageEditor<View, ViewStorage>
    {
        protected override void DrawItem(SerializedProperty itemName, SerializedProperty item, int index)
        {
            base.DrawItem(itemName, item, index);
            var settings = item.FindPropertyRelative("SpawnerSettings");
            EditorGUILayout.PropertyField(settings.FindPropertyRelative("ObjectPrefab"));
            EditorGUILayout.PropertyField(settings.FindPropertyRelative("PoolCapacity"));
        }
    }
}