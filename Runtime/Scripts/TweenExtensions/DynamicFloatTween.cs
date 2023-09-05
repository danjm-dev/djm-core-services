using DG.Tweening;
using UnityEngine;

namespace DJM.CoreUtilities.TweenExtensions
{
    /// <summary>
    /// Represents a dynamic tween for floating-point values using DOTween.
    /// </summary>
    public sealed class DynamicFloatTween
    {
        private Tween _tween;
        
        /// <summary>
        /// Gets the current floating-point value of the tween.
        /// </summary>
        public float Value { get; private set; }
        
        /// <summary>
        /// Indicates whether the tween has reached its target value.
        /// </summary>
        public bool AtTarget { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicFloatTween"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial floating-point value.</param>
        public DynamicFloatTween(float value)
        {
            Value = value;
            AtTarget = true;
        }
        
        /// <summary>
        /// Updates the target value for the tween and begins the tween animation.
        /// </summary>
        /// <param name="target">The target floating-point value.</param>
        /// <param name="durationPerUnit">The duration it takes for the value to change by one unit.</param>
        public void SetTarget(float target, float durationPerUnit)
        {
            AtTarget = false;
            if(_tween is not null) DOTween.Kill(_tween);
            
            var duration = durationPerUnit * Mathf.Abs(target - Value);
            
            _tween = DOTween
                .To(()=> Value, x=> Value = x, target, duration)
                .OnComplete(() => AtTarget = true);
        }
    }
}