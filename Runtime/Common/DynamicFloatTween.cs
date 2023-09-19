using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DJM.CoreServices.Common
{
    /// <summary>
    /// Represents a dynamic tween for floating-point values using DOTween.
    /// </summary>
    public sealed class DynamicFloatTween
    {
        private Tween _tween;
        private readonly float _durationPerUnit;
        
        /// <summary>
        /// Gets the current floating-point value of the tween.
        /// </summary>
        public float Value { get; private set; }
        
        /// <summary>
        /// Gets the current floating-point value target of the tween.
        /// </summary>
        public float TargetValue { get; private set; }

        /// <summary>
        /// Event triggered whenever the value of the tween updates.
        /// </summary>
        public event Action<float> OnValueUpdate;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicFloatTween"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial floating-point value.</param>
        /// <param name="durationPerUnit">The duration it takes for the value to change by one unit.</param>
        public DynamicFloatTween(float value, float durationPerUnit)
        {
            Value = value;
            TargetValue = Value;
            _durationPerUnit = durationPerUnit;
        }

        /// <summary>
        /// Updates the target value for the tween and begins the tween animation.
        /// </summary>
        /// <param name="target">The target floating-point value.</param>
        public void SetTarget(float target)
        {
            if(Mathf.Approximately(target, TargetValue)) return;
            
            _tween?.Kill();
            var duration = _durationPerUnit * Mathf.Abs(target - Value);
            _tween = DOTween
                .To(()=> Value, x=> Value = x, target, duration)
                .OnUpdate(() => OnValueUpdate?.Invoke(Value));
        }
        
        public async Task SetTargetAsync(float target)
        {
            SetTarget(target);
            if(!_tween.IsPlaying()) return;
            await _tween.AsyncWaitForCompletion();
        }

        public void Stop() => _tween?.Kill();
    }
}