using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DJM.CoreUtilities.Editor
{
    [CustomEditor(typeof(SceneLoadTransitionConfig))]
    internal sealed class SceneLoadTransitionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var transition = (SceneLoadTransitionConfig)target;
            
            GUILayout.Space(20f);
            
            GUI.enabled = transition.transitionCanvasPrefab != null;
            if (GUILayout.Button("Verify Transition Canvas Prefab")) OnVerifyCanvasPrefab(transition);
            GUI.enabled = true;
        }

        private static void OnVerifyCanvasPrefab(SceneLoadTransitionConfig transitionConfig)
        {
            var errorMessages = new List<string>();
            var canvasPrefab = transitionConfig.transitionCanvasPrefab;
            
            VerifyCanvasComponent(canvasPrefab, errorMessages);
            if (!transitionConfig.progressBar) VerifyProgressBarComponent(canvasPrefab, errorMessages);
            
            foreach (var message in errorMessages) Debug.LogError(message, transitionConfig);
            Debug.Log(VerificationMessages.VerificationCompleteMessage(errorMessages.Count), transitionConfig);
        }

        private static void VerifyCanvasComponent(GameObject prefab, ICollection<string> errorMessages)
        {
            if (prefab.GetComponent<Canvas>() == null)
            {
                var errorMessage = VerificationMessages.PrefabRequiresComponentOnRootMessage
                (
                    prefab.name, 
                    nameof(Canvas)
                );
                
                errorMessages.Add(errorMessage);
            }

            var canvasGroup = prefab.GetComponent<CanvasGroup>();
            
            if (canvasGroup == null)
            {
                var errorMessage = VerificationMessages.PrefabRequiresComponentOnRootMessage
                (
                    prefab.name, 
                    nameof(CanvasGroup)
                );
                
                errorMessages.Add(errorMessage);
                return;
            }
            
            if (canvasGroup.alpha > 0f)
            {
                errorMessages.Add(VerificationMessages.PrefabRootCanvasGroupAlphaValueMessage(prefab.name));
            }
        }
        
        private static void VerifyProgressBarComponent(GameObject prefab, ICollection<string> errorMessages)
        {
            var progressBarComponent = prefab.GetComponent<ProgressBar>();

            if (progressBarComponent == null)
            {
                var errorMessage = VerificationMessages.PrefabRequiresComponentOnRootMessage
                (
                    prefab.name,
                    nameof(ProgressBar)
                );
                
                errorMessages.Add(errorMessage);
                return;
            }

            if (progressBarComponent.canvasGroup == null)
            {
                var errorMessage = VerificationMessages.PrefabComponentFieldNotAssignedMessage
                (
                    progressBarComponent.name,
                    nameof(ProgressBar),
                    nameof(progressBarComponent.canvasGroup)
                );
                
                errorMessages.Add(errorMessage);
            }
            
            if (progressBarComponent.fillImage == null)
            {
                var errorMessage = VerificationMessages.PrefabComponentFieldNotAssignedMessage
                (
                    progressBarComponent.name,
                    nameof(ProgressBar),
                    nameof(progressBarComponent.fillImage)
                );
                
                errorMessages.Add(errorMessage);
            }
        }
        
        private static class VerificationMessages
        {
            // validation messages
            private const string VerificationMessagePrefix = 
                nameof(SceneLoadTransitionConfig) + " Verification: ";
            private const string PrefabRequiresComponentOnRoot = 
                VerificationMessagePrefix + "{0} prefab requires a {1} component on its root game object.";
            private const string ComponentNotAssigned = 
                VerificationMessagePrefix + "{0} component {1} field has not been assigned in the {2} prefab";
            private const string PrefabRootCanvasGroupAlphaValue = 
                VerificationMessagePrefix + "prefab {0} CanvasGroup component on root gameobject should have alpha value set to 0";
            private const string VerificationComplete = 
                VerificationMessagePrefix + "Verification complete with {0} errors";
            
            internal static string PrefabRequiresComponentOnRootMessage(string prefabName, string missingComponentName)
            {
                return string.Format(PrefabRequiresComponentOnRoot, prefabName, missingComponentName);
            }

            internal static string PrefabComponentFieldNotAssignedMessage(string prefabName, string componentName, string fieldName)
            {
                return string.Format(ComponentNotAssigned, componentName, fieldName, prefabName);
            }

            internal static string PrefabRootCanvasGroupAlphaValueMessage(string prefabName)
            {
                return string.Format(PrefabRootCanvasGroupAlphaValue, prefabName);
            }

            internal static string VerificationCompleteMessage(int issueCont)
            {
                return string.Format(VerificationComplete, issueCont);
            }
        }
    }
}