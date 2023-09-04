using System;
using UnityEngine;

namespace DJM.CoreUtilities.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ConditionalEnablePropertyAttribute : PropertyAttribute
    {
        public string ConditionalPropertyName;
 
        public ConditionalEnablePropertyAttribute(string conditionalPropertyName)
        {
            ConditionalPropertyName = conditionalPropertyName;
        }
    }
}