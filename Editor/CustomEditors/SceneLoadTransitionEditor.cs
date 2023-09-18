using DJM.CoreServices.MonoServices.LoadingScreen;
using DJM.CoreServices.Services.SceneLoader;
using UnityEditor;

namespace DJM.CoreServices.Editor.CustomEditors
{
    [CustomEditor(typeof(LoadingScreenConfig))]
    internal sealed class SceneLoadTransitionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // DrawDefaultInspector();
            // EditorGUILayout.Space();
            //
            // var property = serializedObject.FindProperty("transitionCanvasPrefab");
            // var transitionCanvas = property.objectReferenceValue as LoadingScreenService;
            // if (transitionCanvas == null)
            //     EditorGUILayout.HelpBox("transitionCanvasPrefab requires a prefab!", MessageType.Error);
        }
    }
}