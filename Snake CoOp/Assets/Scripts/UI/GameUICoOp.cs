using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeCoOp.UI
{
    public class GameUICoOp : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private TextMeshProUGUI p1ScoreText;
        [SerializeField] private TextMeshProUGUI p2ScoreText;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------
        #region --------- Private Variables ---------
        private int p1Score = 0;
        private int p2Score = 0;
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
        public void IncreaseScore(PlayerType playerType)
        {
            if (playerType == PlayerType.PLAYER_1)
            {
                p1Score += foodScore;
                DisplayP1Text();
            }
            else
            {
                p2Score += foodScore;
                DisplayP2Text();
            }
        }

        public void DecreaseScore(PlayerType playerType)
        {
            if (playerType == PlayerType.PLAYER_1)
            {
                p1Score -= foodScore;
                if (p1Score <= 0)
                    p1Score = 0;
                DisplayP1Text();
            }
            else
            {
                p2Score -= foodScore;
                if (p2Score <= 0)
                    p2Score = 0;
                DisplayP1Text();
            }
        }

        public void DisplayGameOver()
        {
            gameOverScreen.SetActive(true);
        }
        #endregion ------------------
        #region --------- Private Methods ---------
        private void DisplayP1Text()
        {
            p1ScoreText.text = "P1 SCORE : " + p1Score;
        }

        private void DisplayP2Text()
        {
            p2ScoreText.text = "P2 SCORE : " + p2Score;
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
