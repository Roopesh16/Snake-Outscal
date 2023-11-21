using UnityEngine;
using SnakeCoOp.Grid;
using SnakeCoOp.Snake;

namespace SnakeCoOp.Powerup
{
    public class PowerupController : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private GridController gridController;
        [SerializeField] private GameObject[] powerUpPrefabs;
        [SerializeField] private SnakeController snake;
        #endregion ------------------

        #region --------- Private Variables ---------
        private GameObject powerUp;
        private Vector2Int powerUpPosition;
        private int powerupIndex = 0;
        private float powerupTimer = 0f;
        private float maxPowerupTimer = 5f;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        void Start()
        {
            SpawnPowerup();
        }

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
                        SpawnPowerup();

                    }
                    else
                    {
                        if (powerUp.transform.position == snake.transform.position)
                        {
                            powerupTimer -= maxPowerupTimer;
                            Destroy(powerUp);
                            snake.IncreaseSnakeSize();
                            SpawnPowerup();
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
        #endregion ------------------

        #region --------- Private Methods ---------
        public void SpawnPowerup()
        {
            do
            {
                powerUpPosition = new Vector2Int(Random.Range(0, gridController.GetGridWidth()), Random.Range(0, gridController.GetGridHeight()));
            } while (snake.GetFullSnakeSize().IndexOf(powerUpPosition) != -1);

            powerUp = Instantiate(powerUpPrefabs[powerupIndex]);
            powerUp.transform.position = new Vector2(powerUpPosition.x, powerUpPosition.y);
        }
        #endregion ------------------
    }
}