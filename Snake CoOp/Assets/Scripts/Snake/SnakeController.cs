using UnityEngine;

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
            transform.eulerAngles = new Vector3(0, 0, GetDirectionAngle(moveDirection) - 90);
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

        private float GetDirectionAngle(Vector2Int direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if(angle<0)
            {
                angle += 360;
            }

            return angle;
        }
        #endregion ------------------

        #region --------- Public Methods ---------
        #endregion ------------------
    }
}

