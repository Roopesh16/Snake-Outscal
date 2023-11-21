using TMPro;
using UnityEngine;
using System.Collections;

namespace SnakeCoOp.UI
{
    public class GameUI : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private TextMeshProUGUI scoreText;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------
        #region --------- Private Variables ---------
        private int score = 0;
        private int foodScore = 10;
        #endregion ------------------
        #region --------- Monobehavior Methods ---------
        #endregion ------------------
        #region --------- Public Methods ---------
        public void UpdateScore()
        {
            score += foodScore;
            DisplayText();
        }

        public IEnumerator ActivateDoubleScore()
        {
            int maxTime = Random.Range(1, 4);
            foodScore = 20;
            print(foodScore);
            yield return new WaitForSeconds(maxTime);
            foodScore = 10;
            print(foodScore);
            yield return null;
        }
        #endregion ------------------
        #region --------- Private Methods ---------
        private void DisplayText()
        {
            scoreText.text = "SCORE : " + score;
        }
        #endregion ------------------
    }
}
