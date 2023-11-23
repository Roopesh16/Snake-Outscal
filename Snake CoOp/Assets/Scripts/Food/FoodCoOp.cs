using UnityEngine;
using SnakeCoOp.Snake;
using SnakeCoOp.Grid;
using SnakeCoOp.UI;
using UnityEngine.Audio;

namespace SnakeCoOp.Food
{
    public class FoodCoOp : MonoBehaviour
    {
        private enum FoodType
        {
            MASS_GAINER,
            MASS_BURNER
        }

        #region --------- Serialized Variables ---------
        [SerializeField] private GridController gridController;
        [SerializeField] private GameObject[] foodPrefabs;
        [SerializeField] private SnakeCoOpController snakeP1;
        [SerializeField] private SnakeCoOpController snakeP2;
        [SerializeField] private GameUICoOp gameUICoOp;
        #endregion ------------------

        #region --------- Private Variables ---------
        private GameObject food;
        private Vector2Int foodPosition;
        private float foodTimer = 0f;
        private float maxFoodTimer = 5f;
        private int foodIndex;
        private FoodType foodType;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        void Start()
        {
            SpawnFood();
        }

        private void Update()
        {
            if (GameManager.Instance.GetState() == State.ALIVE)
            {
                foodTimer += Time.deltaTime;
                if (food != null)
                {
                    if (foodTimer >= maxFoodTimer)
                    {
                        foodTimer -= maxFoodTimer;
                        Destroy(food);
                        SpawnFood();
                    }
                    else
                    {
                        if (food.transform.position == snakeP1.transform.position)
                        {
                            foodTimer -= maxFoodTimer;
                            if (foodType == FoodType.MASS_GAINER)
                            {
                                AudioManager.Instance.PlaySFX(Audio_SFX.FOOD_EAT);
                                snakeP1.IncreaseSnakeSize();
                                gameUICoOp.IncreaseScore(PlayerType.PLAYER_1);
                            }
                            else
                            {
                                AudioManager.Instance.PlaySFX(Audio_SFX.BURNER_EAT);
                                snakeP1.DecreaseSnakeSize();
                                gameUICoOp.DecreaseScore(PlayerType.PLAYER_1);
                            }
                            Destroy(food);
                            SpawnFood();
                        }
                        else if (food.transform.position == snakeP2.transform.position)
                        {
                            foodTimer -= maxFoodTimer;
                            if (foodType == FoodType.MASS_GAINER)
                            {
                                AudioManager.Instance.PlaySFX(Audio_SFX.FOOD_EAT);
                                snakeP2.IncreaseSnakeSize();
                                gameUICoOp.IncreaseScore(PlayerType.PLAYER_2);
                            }
                            else
                            {
                                AudioManager.Instance.PlaySFX(Audio_SFX.BURNER_EAT);
                                snakeP2.DecreaseSnakeSize();
                                gameUICoOp.DecreaseScore(PlayerType.PLAYER_2);
                            }
                            Destroy(food);
                            SpawnFood();
                        }
                    }
                }
            }
            else
            {
                Destroy(food);
            }
        }
        #endregion ------------------

        #region --------- Public Methods ---------
        public Vector2Int GetFoodPosition()
        {
            return foodPosition;
        }
        #endregion ------------------

        #region --------- Private Methods ---------
        private void SpawnFood()
        {
            int randVal = Random.Range(0, 100);

            if (randVal <= 70)
            {
                foodIndex = 0;
                foodType = FoodType.MASS_GAINER;
            }
            else
            {
                foodIndex = 1;
                foodType = FoodType.MASS_BURNER;
            }

            do
            {
                foodPosition = new Vector2Int(Random.Range(0, gridController.GetGridWidth()), Random.Range(0, gridController.GetGridHeight()));
            } while ((snakeP1.GetFullSnakeSize().IndexOf(foodPosition) != -1) && (snakeP2.GetFullSnakeSize().IndexOf(foodPosition) != -1));

            food = Instantiate(foodPrefabs[foodIndex]);
            food.transform.position = new Vector2(foodPosition.x, foodPosition.y);
        }
        #endregion ------------------
    }
}
