using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LastDay
{
    public class UserInterfaceManager : MonoBehaviour
    {
        private GameManager game;
        public Image characterPortraitImage;
        private Sprite defaultImage;
        public TextMeshProUGUI timerDisplay, scoreDisplay, endScoreDisplay, objectiveDisplay;
        public Image endFadeImage;
        public RectTransform gameOverPanel;
        private string score;

        private void Start()
        {
            defaultImage = characterPortraitImage.sprite;
            game = GameManager.instance;
            gameOverPanel.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            // Get and display time remaining
            timerDisplay.text = game.WorldTimer.GetTime();

            // Get and display score
            score = game.Score.ToString();
            scoreDisplay.text = score;
            endScoreDisplay.text = score;

            objectiveDisplay.text = game.ObjectiveText;
            if (game.currentMiniGame == null) characterPortraitImage.sprite = defaultImage;
            else characterPortraitImage.sprite = game.currentMiniGame.npc.portraitImage;

            if (game.WorldTimer.CurrentTime <= 5)
                StartCoroutine(EndFade(0.2f));

            if (game.GameOver)
            {
                gameOverPanel.gameObject.SetActive(true);
                //Time.timeScale = 0;
            }
        }

        public void PlayAgain()
        {
            //Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Quit()
        {
            //Time.timeScale = 1;
            Application.Quit();
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

