using System;
using UnityEngine.Events;

namespace LastDay
{
    /// <summary>
    /// Manually updated timer class 
    /// </summary>
    [System.Serializable]
    public class Timer // yo cheers damian
    {
        public float startTime = 0;
        public float endTime = 0;

        [UnityEngine.SerializeField]
        private float currentTime;
        public UnityEvent onFinish = new UnityEvent();

        public float CurrentTime => currentTime;

        public bool IsFinished => currentTime >= endTime;
        public bool IsStarting => currentTime <= startTime;

        public float CurrentTimeNormalized => currentTime / endTime;

        public Timer(float endTime = 1, float startTime = 0, float currentTime = 0)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.currentTime = currentTime;
        }

        /// <summary>
        /// Update the timer value
        /// </summary>
        /// <param name="deltaTime"></param>
        public void UpdateTimer(float deltaTime)
        {
            if (currentTime > endTime)
                currentTime = startTime;
            else
                currentTime += deltaTime;

            if (IsFinished)
                onFinish?.Invoke();
        }

        /// <summary>
        /// Manually reset the timer to it's start time
        /// </summary>
        public void ResetTimer()
        {
            currentTime = 0;
        }

        /// <summary>
        /// Set the timer position within range of min and max (inclusive)
        /// </summary>
        /// <param name="value">New value for the timer</param>
        public void SetTimerPosition(float value)
        {
            currentTime = UnityEngine.Mathf.Clamp(value, startTime, endTime);
        }

        public void SetTimerPositionNormalized(float value)
        {
            currentTime = endTime * value;
        }

        public override String ToString()
        {
            return $"Timer: {currentTime}; {startTime} | {endTime}";
        }
    }

}
