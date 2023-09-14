using DJM.CoreServices.Attributes;
using UnityEditor;
using UnityEngine;

namespace DJM.CoreServices.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
    internal sealed class ConditionalFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //get the attribute data
            var condHAtt = (ConditionalFieldAttribute)attribute;
            //check if the propery we want to draw should be enabled
            var enabled = GetConditionalHideAttributeResult(condHAtt, property);
 
            //Enable/disable the property
            var wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
 
            //Check if we should draw the property
            if (!condHAtt.HideInInspector || enabled)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
 
            //Ensure that the next property that is being drawn uses the correct settings
            GUI.enabled = wasEnabled;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var condHAtt = (ConditionalFieldAttribute)attribute;
            var enabled = GetConditionalHideAttributeResult(condHAtt, property);
 
            if (!condHAtt.HideInInspector || enabled)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }

            //The property is not being drawn
            //We want to undo the spacing added before and after the property
            return -EditorGUIUtility.standardVerticalSpacing;
        }
        
        private bool GetConditionalHideAttributeResult(ConditionalFieldAttribute condHAtt, SerializedProperty property)
        {
            var enabled = true;
            //Look for the sourcefield within the object that the property belongs to
            var propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
            var conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
            var sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
 
            if (sourcePropertyValue != null)
            {
                enabled = sourcePropertyValue.boolValue;
            }
            else
            {
                Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
            }
 
            return enabled;
        }
    }
}