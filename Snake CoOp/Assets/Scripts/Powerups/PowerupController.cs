using UnityEngine;
using SnakeCoOp.Grid;
using SnakeCoOp.Snake;
using SnakeCoOp.Food;

namespace SnakeCoOp.Powerup
{
    public class PowerupController : MonoBehaviour
    {
        private enum PowerupType
        {
            SHIELD,
            SPEED_BOOST,
            SCORE_DOUBLE
        }

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
        private PowerupType powerupType;
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
                            switch(powerupType)
                            {
                                case PowerupType.SHIELD: print("Shield");
                                    StartCoroutine(snake.ActivateShield());
                                    break;
                                case PowerupType.SPEED_BOOST: print("Speed");
                                StartCoroutine(snake.ActivateSpeedBoost());
                                    break;
                                // case PowerupType.SCORE_DOUBLE:DoubleScore();
                                //     break;
                            }
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

            if (randVal <= 100)
            {
                // powerupIndex = Random.Range(0, 3);
                powerupIndex = 1;
                switch(powerupIndex)
                {
                    case 0:powerupType = PowerupType.SHIELD;
                        break;
                    case 1:powerupType = PowerupType.SPEED_BOOST;
                        break;
                    case 2:powerupType = PowerupType.SCORE_DOUBLE;
                        break;
                }
                return powerUpPrefabs[powerupIndex];
            }

            return null;
        }
        #endregion ------------------
    }
}