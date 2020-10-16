using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LastDay
{
    public class UserInterfaceManager : MonoBehaviour
    {
        public TextMeshProUGUI timerDisplay, scoreDisplay;
        public Image endFadeImage;
        private GameManager game;

        private void Start()
        {
            game = GameManager.instance;
        }

        // Update is called once per frame
        void Update()
        {
            // Get and display time remaining
            timerDisplay.text = game.WorldTimer.GetTime();
            // Get and display score
            scoreDisplay.text = game.Score.ToString();

            if (game.WorldTimer.CurrentTime <= 5)
                StartCoroutine(EndFade(0.2f));
        }

        // Fade an image over time
        public IEnumerator EndFade(float fadeSpeed)
        {
            Color fadeColour = endFadeImage.color;
            float fadeAmount;

            while (endFadeImage.color.a < 1)
            {
                fadeAmount = fadeColour.a + (fadeSpeed * Time.deltaTime);
                fadeColour = new Color(fadeColour.r, fadeColour.g, fadeColour.b, fadeAmount);
                endFadeImage.color = fadeColour;
                yield return null;
            }
        }
    }
}

