using UnityEngine;
using SnakeCoOp.Snake;
using SnakeCoOp.Grid;

namespace SnakeCoOp.Food
{

    public class FoodController : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private GridController gridController;
        [SerializeField] private GameObject foodPrefab;
        [SerializeField] private SnakeController snake;
        #endregion ------------------

        #region --------- Private Variables ---------
        private GameObject food;
        private Vector2Int foodPosition;
        private float foodTimer = 0f;
        private float maxFoodTimer = 5f;
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
                        if (food.transform.position == snake.transform.position)
                        {
                            Destroy(food);
                            snake.IncreaseSnakeSize();
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
        #endregion ------------------

        #region --------- Private Methods ---------
        public void SpawnFood()
        {
            do
            {
                foodPosition = new Vector2Int(Random.Range(0, gridController.GetGridWidth()), Random.Range(0, gridController.GetGridHeight()));
            } while (snake.GetFullSnakeSize().IndexOf(foodPosition) != -1);

            food = Instantiate(foodPrefab);
            food.transform.position = new Vector2(foodPosition.x, foodPosition.y);
        }
        #endregion ------------------
    }
}
