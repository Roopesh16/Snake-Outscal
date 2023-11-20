using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace SnakeCoOp.Snake
{
    public class SnakeController : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        #endregion ------------------

        #region --------- Private Variables ---------
        private Vector2Int gridPosition;
        private Vector2Int moveDirection;
        private float movementTimer = 0f;
        private const float maxMoveTimer = 0.75f;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        private void Start()
        {
            gridPosition = new Vector2Int(10, 10);
            moveDirection = new Vector2Int(0, 1);
        }

        private void Update()
        {
            InputHandler();
            UpdateSnakePosition();
            transform.position = new Vector2(gridPosition.x, gridPosition.y);
        }
        #endregion ------------------

        #region --------- Private Methods ---------
        private void InputHandler()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (moveDirection.y != -1)
                {
                    moveDirection.x = 0;
                    moveDirection.y = 1;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (moveDirection.y != 1)
                {
                    moveDirection.x = 0;
                    moveDirection.y = -1;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (moveDirection.x != 1)
                {
                    moveDirection.x = -1;
                    moveDirection.y = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (moveDirection.x != -1)
                {
                    moveDirection.x = 1;
                    moveDirection.y = 0;
                }
            }
        }

        private void UpdateSnakePosition()
        {
            movementTimer += Time.deltaTime;

            if (movementTimer >= maxMoveTimer)
            {
                gridPosition += moveDirection;
                movementTimer -= maxMoveTimer;
            }
        }
        #endregion ------------------

        #region --------- Public Methods ---------
        #endregion ------------------
    }
}

