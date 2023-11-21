using UnityEngine;
using SnakeCoOp.Grid;
using SnakeCoOp.Snake;
using SnakeCoOp.Food;

namespace SnakeCoOp.Powerup
{
    public class PowerupController : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private GridController gridController;
        [SerializeField] private SnakeController snake;
        [SerializeField] private FoodController food;
        [SerializeField] private GameObject[] powerUpPrefabs;
        #endregion ------------------

        #region --------- Private Variables ---------
        private GameObject powerUp = null;
        private Vector2Int powerUpPosition;
        private int powerupIndex = 0;
        private float powerupTimer = 0f;
        private float maxPowerupTimer = 5f;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        private void Update()
        {
            if (GameManager.Instance.GetState() == State.ALIVE)
            {
                powerupTimer += Time.deltaTime;
                if (powerUp != null)
                {
                    print(powerupTimer);
                    if (powerupTimer >= maxPowerupTimer)
                    {
                        powerupTimer -= maxPowerupTimer;
                        Destroy(powerUp);
                    }
                    else
                    {
                        if (powerUp.transform.position == snake.transform.position)
                        {
                            powerupTimer -= maxPowerupTimer;
                            Destroy(powerUp);
                        }
                    }
                }
            }
            else
            {
                Destroy(powerUp);
            }
        }
        #endregion ------------------

        #region --------- Public Methods ---------
        public void SpawnPowerup()
        {
            if (powerUp != null)
            {
                return;
            }

            powerUp = GetRandomPowerUp();

            do
            {
                powerUpPosition = new Vector2Int(Random.Range(0, gridController.GetGridWidth()), Random.Range(0, gridController.GetGridHeight()));
            } while ((snake.GetFullSnakeSize().IndexOf(powerUpPosition) != -1) && powerUpPosition != food.GetFoodPosition());

            powerUp = Instantiate(powerUpPrefabs[powerupIndex]);
            powerUp.transform.position = new Vector2(powerUpPosition.x, powerUpPosition.y);
            powerupTimer = 0f;
        }
        #endregion ------------------

        #region --------- Private Methods ---------

        private GameObject GetRandomPowerUp()
        {
            int randVal = Random.Range(0, 100);

            if (randVal <= 20)
            {
                powerupIndex = Random.Range(0, 3);
                return powerUpPrefabs[powerupIndex];
            }

            return null;
        }
        #endregion ------------------
    }
}