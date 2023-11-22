using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace SnakeCoOp.UI
{
    public class GameUI : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI powerupText;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------
        #region --------- Private Variables ---------
        private int score = 0;
        private int foodScore = 10;
        #endregion ------------------
        #region --------- Monobehavior Methods ---------
        private void Awake()
        {
            quitButton.onClick.AddListener(QuitButton);
            pauseButton.onClick.AddListener(PauseButton);
            resumeButton.onClick.AddListener(ResumeButton);
            resumeButton.gameObject.SetActive(false);
            powerupText.enabled = false;
            gameOverScreen.SetActive(false);
        }
        #endregion ------------------
        #region --------- Public Methods ---------
        public void IncreaseScore()
        {
            score += foodScore;
            DisplayText();
        }

        public void DecreaseScore()
        {
            score -= foodScore;
            if (score <= 0)
                score = 0;
            DisplayText();
        }

        public IEnumerator ActivateDoubleScore()
        {
            int maxTime = Random.Range(1, 4);
            StartCoroutine(DisplayPowerupText(PowerupType.SCORE_DOUBLE, maxTime));
            foodScore = 20;
            yield return new WaitForSeconds(maxTime);
            foodScore = 10;
            yield return null;
        }

        public IEnumerator DisplayPowerupText(PowerupType powerupType, float time)
        {
            powerupText.enabled = true;
            switch (powerupType)
            {
                case PowerupType.SHIELD:
                    powerupText.text = "Shield Activated";
                    break;
                case PowerupType.SPEED_BOOST:
                    powerupText.text = "Speed Boost Activated";
                    break;
                case PowerupType.SCORE_DOUBLE:
                    powerupText.text = "Score Double Activated";
                    break;
            }
            yield return new WaitForSeconds(time);
            powerupText.enabled = false;
            yield return null;
        }

        public void DisplayGameOver()
        {
            gameOverScreen.SetActive(true);
        }
        #endregion ------------------
        #region --------- Private Methods ---------
        private void DisplayText()
        {
            scoreText.text = "SCORE : " + score;
        }

        private void QuitButton()
        {
            Time.timeScale = 1;
            GameManager.Instance.LoadScene(0);
        }

        private void PauseButton()
        {
            pauseButton.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void ResumeButton()
        {
            pauseButton.gameObject.SetActive(true);
            resumeButton.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        #endregion ------------------
    }
}
