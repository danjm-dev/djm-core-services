using DJM.CoreServices.Attributes;
using UnityEditor;
using UnityEngine;

namespace DJM.CoreServices.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ConditionalShowPropertyAttribute))]
    internal sealed class ConditionalShowPropertyDrawer: PropertyDrawer
    {
        private const string NoMatchingPropertyWarningMessage = 
            "Attempting to use a {0} but no matching conditional property found: {1}";
        private const string WrongPropertyTypeWarningMessage = 
            "Attempting to use a {0} but conditional property not of type bool: {1}";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var showAttribute = (ConditionalShowPropertyAttribute) attribute;
            if (!GetShowProperty(property, showAttribute)) return;
            EditorGUI.PropertyField(position, property, label, true);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var showAttribute = (ConditionalShowPropertyAttribute) attribute;
            if (!GetShowProperty(property, showAttribute)) return -EditorGUIUtility.standardVerticalSpacing;
            return EditorGUI.GetPropertyHeight(property, label);
        }

        private static bool GetShowProperty
        (
            SerializedProperty property, 
            ConditionalShowPropertyAttribute attribute
        )
        {
            var conditionalProperty = property.serializedObject.FindProperty(attribute.ConditionalPropertyName);
            
            if (conditionalProperty is null)
            {
                Debug.LogWarning(string.Format
                (
                    NoMatchingPropertyWarningMessage,
                    typeof(ConditionalShowPropertyAttribute),
                    attribute.ConditionalPropertyName)
                );
                return true;
            }

            if (conditionalProperty.propertyType != SerializedPropertyType.Boolean)
            {
                Debug.LogWarning(string.Format
                (
                    WrongPropertyTypeWarningMessage, 
                    typeof(ConditionalShowPropertyAttribute),
                    attribute.ConditionalPropertyName)
                );
                return true;
            }

            return conditionalProperty.boolValue;
        }
    }
}