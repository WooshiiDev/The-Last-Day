using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LastDay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public Timer worldTimer;
        public float timeTillVolcano;

        private void OnEnable()
        {
            instance = this;

            // Start the timer until world reset
            worldTimer = new Timer(timeTillVolcano);
        }

        private void Update()
        {
            // Update timer
            worldTimer.UpdateTimer(Time.deltaTime);

            // If timer is complete, reset world
            if (worldTimer.IsFinished)
            {
                ResetWorld();
            }
        }

        public void ResetWorld()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


