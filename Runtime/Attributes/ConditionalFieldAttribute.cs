using System;
using UnityEngine;

namespace DJM.CoreServices.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ConditionalFieldAttribute : PropertyAttribute
    {
        //The name of the bool field that will be in control
        public string ConditionalSourceField = "";
        //TRUE = Hide in inspector / FALSE = Disable in inspector 
        public bool HideInInspector = false;
 
        public ConditionalFieldAttribute(string conditionalSourceField)
        {
            ConditionalSourceField = conditionalSourceField;
            HideInInspector = false;
        }
 
        public ConditionalFieldAttribute(string conditionalSourceField, bool hideInInspector)
        {
            ConditionalSourceField = conditionalSourceField;
            HideInInspector = hideInInspector;
        }
    }
}