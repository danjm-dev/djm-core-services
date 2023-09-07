using DJM.CoreUtilities.SceneManagement;
using UnityEditor;

namespace DJM.CoreUtilities.Editor
{
    [CustomEditor(typeof(SceneTransitionConfig))]
    internal sealed class SceneLoadTransitionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();

            var property = serializedObject.FindProperty("transitionCanvasPrefab");
            var transitionCanvas = property.objectReferenceValue as SceneTransitionCanvas;
            if (transitionCanvas == null)
                EditorGUILayout.HelpBox("transitionCanvasPrefab requires a prefab!", MessageType.Error);
        }
    }
}