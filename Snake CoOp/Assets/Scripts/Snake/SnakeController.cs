using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeCoOp.Snake
{
    public class SnakeController : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private GameObject snakeBody;
        #endregion ------------------

        #region --------- Private Variables ---------
        private Vector2Int gridPosition;
        private Vector2Int moveDirection;
        private float movementTimer = 0f;
        private const float maxMoveTimer = 0.3f;
        private int snakeBodyCount = 1;
        private List<Vector2Int> snakeBodyList;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        private void Start()
        {
            gridPosition = new Vector2Int(10, 10);
            moveDirection = new Vector2Int(0, 1);

            snakeBodyList = new List<Vector2Int>();
        }

        private void Update()
        {
            InputHandler();
            UpdateSnakePosition();

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
                movementTimer -= maxMoveTimer;
                snakeBodyList.Insert(0, gridPosition);
                gridPosition += moveDirection;

                if (snakeBodyList.Count >= snakeBodyCount + 1)
                {
                    snakeBodyList.RemoveAt(snakeBodyList.Count - 1);
                }

                for (int i = 0; i < snakeBodyList.Count; i++)
                {
                    Vector2Int snakeMovePosition = snakeBodyList[i];
                    GameObject body = Instantiate(snakeBody);
                    body.transform.SetParent(transform);
                    body.transform.position = new Vector2(snakeMovePosition.x, transform.position.y);
                    body.transform.eulerAngles = transform.eulerAngles;
                    Destroy(body, maxMoveTimer);
                }
            }

            transform.position = new Vector2(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetDirectionAngle(moveDirection) - 90);
        }

        private float GetDirectionAngle(Vector2Int direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }
        #endregion ------------------

        #region --------- Public Methods ---------
        public Vector2Int GetSnakePosition()
        {
            return gridPosition;
        }
        #endregion ------------------
    }
}

