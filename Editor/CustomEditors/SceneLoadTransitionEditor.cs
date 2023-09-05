using UnityEditor;

namespace DJM.CoreUtilities.Editor
{
    [CustomEditor(typeof(SceneLoadTransitionConfig))]
    internal sealed class SceneLoadTransitionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();

            var property = serializedObject.FindProperty("sceneTransitionCanvasPrefab");
            var transitionCanvas = property.objectReferenceValue as SceneTransitionCanvas;

            if (transitionCanvas != null) return;
            
            EditorGUILayout.HelpBox("sceneTransitionCanvasPrefab requires a prefab!", MessageType.Error);
        }
    }
}