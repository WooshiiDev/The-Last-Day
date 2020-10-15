using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LastDay
{
    public class UserInterfaceManager : MonoBehaviour
    {
        public TextMeshProUGUI timerDisplay;
        public Image endFadeImage;

        // Update is called once per frame
        void Update()
        {
            timerDisplay.text = GameManager.instance.WorldTimer.GetTime();

            if (GameManager.instance.WorldTimer.CurrentTime <= 5)
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

