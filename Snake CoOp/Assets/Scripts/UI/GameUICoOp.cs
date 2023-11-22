using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeCoOp.UI
{
    public class GameUICoOp : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private TextMeshProUGUI scoreText;
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
