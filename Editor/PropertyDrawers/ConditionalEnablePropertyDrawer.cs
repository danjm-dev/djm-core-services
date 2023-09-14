using DJM.CoreServices.Attributes;
using UnityEditor;
using UnityEngine;

namespace DJM.CoreServices.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ConditionalEnablePropertyAttribute))]
    internal sealed class ConditionalEnablePropertyDrawer : PropertyDrawer
    {
        private const string NoMatchingPropertyWarningMessage = 
            "Attempting to use a {0} but no matching conditional property found: {1}";
        private const string WrongPropertyTypeWarningMessage = 
            "Attempting to use a {0} but conditional property not of type bool: {1}";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var enableAttribute = (ConditionalEnablePropertyAttribute) attribute;
            
            var originalEnabledState = GUI.enabled;
            
            GUI.enabled = GetEnableProperty(property, enableAttribute);
            EditorGUI.PropertyField(position, property, label, true);
            
            GUI.enabled = originalEnabledState;
        }

        private static bool GetEnableProperty
        (
            SerializedProperty property, 
            ConditionalEnablePropertyAttribute attribute
        )
        {
            var conditionalProperty = property.serializedObject.FindProperty(attribute.ConditionalPropertyName);
            
            if (conditionalProperty is null)
            {
                Debug.LogWarning(string.Format
                (
                    NoMatchingPropertyWarningMessage,
                    typeof(ConditionalEnablePropertyAttribute),
                    attribute.ConditionalPropertyName)
                );
                return true;
            }

            if (conditionalProperty.propertyType != SerializedPropertyType.Boolean)
            {
                Debug.LogWarning(string.Format
                (
                    WrongPropertyTypeWarningMessage,
                    typeof(ConditionalEnablePropertyAttribute),
                    attribute.ConditionalPropertyName)
                );
                return true;
            }

            return conditionalProperty.boolValue;
        }
    }
}