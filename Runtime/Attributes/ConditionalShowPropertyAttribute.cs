using System;
using UnityEngine;

namespace DJM.CoreUtilities.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ConditionalShowPropertyAttribute : PropertyAttribute
    {
        public string ConditionalPropertyName;
        public ConditionalShowPropertyAttribute(string conditionalPropertyName)
        {
            ConditionalPropertyName = conditionalPropertyName;
        }
    }
}