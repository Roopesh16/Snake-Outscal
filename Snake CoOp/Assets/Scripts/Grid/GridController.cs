using UnityEngine;

namespace SnakeCoOp.Grid
{
    public class GridController : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private Transform grid;
        #endregion ------------------

        #region --------- Private Variables ---------
        private int width;
        private int height;
        #endregion ------------------

        #region --------- Serialized Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        private void Awake()
        {
            width = (int)grid.localScale.x;
            height = (int)grid.localScale.y;
        }
        #endregion ------------------

        #region --------- Public Methods ---------
        public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
        {
            if (gridPosition.x < 0)
            {
                gridPosition.x = width - 1;
            }

            if (gridPosition.x > width - 1)
            {
                gridPosition.x = 0;
            }

            if (gridPosition.y < 0)
            {
                gridPosition.y = height - 1;
            }

            if (gridPosition.y > height - 1)
            {
                gridPosition.y = 0;
            }

            return gridPosition;
        }

        public int GetGridWidth()
        {
            return width;
        }

        public int GetGridHeight()
        {
            return height;
        }
        #endregion ------------------
    }
}
