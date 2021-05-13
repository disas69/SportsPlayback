using Framework.Editor;
using Sports.Playback.Player.Soccer;
using UnityEditor;

namespace Sports.Assets.Scripts.Playback.Editor
{
    [CustomEditor(typeof(SoccerPlaybackPlayer))]
    public class SoccerPlaybackPlayerEditor : CustomEditorBase<SoccerPlaybackPlayer>
    {
        protected override void DrawInspector()
        {
            base.DrawInspector();
            DrawDefaultInspector();

            var data = serializedObject.FindProperty("_data");
            var dataAsset = EditorGUILayout.ObjectField("Data Asset", AssetDatabase.LoadAssetAtPath<DefaultAsset>(data.stringValue), typeof(DefaultAsset), false);
            data.stringValue = AssetDatabase.GetAssetPath(dataAsset);
        }
    }
}