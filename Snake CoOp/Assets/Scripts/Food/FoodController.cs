using UnityEngine;
using SnakeCoOp.Snake;

namespace SnakeCoOp.Food
{

    public class FoodController : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private GameObject foodPrefab;
        [SerializeField] private SnakeController snake;
        #endregion ------------------

        #region --------- Private Variables ---------
        private GameObject food;
        private Vector2Int foodPosition;
        private const int width = 20;
        private const int height = 20;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        void Start()
        {
            SpawnFood();
        }

        private void Update() {
            if(food != null && food.transform.position == snake.transform.position)
            {
                Destroy(food);
                snake.IncreaseSnakeSize();
                SpawnFood();
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
               foodPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            } while (snake.GetFullSnakeSize().IndexOf(foodPosition) != -1);

            food = Instantiate(foodPrefab);
            food.transform.position = new Vector2(foodPosition.x, foodPosition.y);
        }
        #endregion ------------------
    }
}
