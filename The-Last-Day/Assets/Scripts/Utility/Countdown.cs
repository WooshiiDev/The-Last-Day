using System;
using UnityEngine;
using UnityEngine.Events;

namespace LastDay
{
    /// <summary>
    /// Manually updated timer class which counts down instead of up
    /// </summary>
    [System.Serializable]
    public class Countdown
    {
        public float startTime = 0;
        public float endTime = 0;

        [UnityEngine.SerializeField]
        private float currentTime;
        public UnityEvent onFinish = new UnityEvent();

        public float CurrentTime => currentTime;

        public bool IsFinished => currentTime <= endTime;
        public bool IsStarting => currentTime >= startTime;

        public float CurrentTimeNormalized => currentTime / endTime;

        public Countdown(float startTime = 60, float currentTime = 60, float endTime = 0)
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
            if (currentTime < endTime)
                currentTime = startTime;
            else
                currentTime -= deltaTime;

            if (IsFinished)
                onFinish?.Invoke();
        }

        /// <summary>
        /// Manually reset the timer to it's start time
        /// </summary>
        public void ResetTimer()
        {
            currentTime = startTime;
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

        public string GetTime()
        {
            //string time = Mathf.Floor(currentTime).ToString();
            string minutes = Mathf.Floor(currentTime / 60).ToString("00");
            string seconds = Mathf.Floor(currentTime % 60).ToString("00");

            return string.Format("{0}:{1}", minutes, seconds);
        }
    }

}
