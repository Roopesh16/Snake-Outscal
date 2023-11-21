using System;
using System.Collections.Generic;
using SnakeCoOp.Grid;
using UnityEngine;

namespace SnakeCoOp.Snake
{
    public class SnakeController : MonoBehaviour
    {
        private enum State
        {
            ALIVE,
            DEAD
        }

        #region --------- Serialized Variables ---------
        [SerializeField] private GameObject snakeBody;
        [SerializeField] private GridController gridController;
        #endregion ------------------

        #region --------- Private Variables ---------
        private State state;
        private Vector2Int gridPosition;
        private Vector2Int moveDirection;
        private float movementTimer = 0f;
        private const float maxMoveTimer = 0.3f;
        private int snakeBodyCount = 0;
        private List<Vector2Int> snakeBodyList;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        private void Awake()
        {
            snakeBodyList = new List<Vector2Int>();
        }
        private void Start()
        {
            gridPosition = new Vector2Int(10, 10);
            moveDirection = new Vector2Int(0, 1);
        }

        private void Update()
        {
            if (state == State.ALIVE)
            {
                InputHandler();
                UpdateSnakePosition();
            }
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
                gridPosition = gridController.ValidateGridPosition(gridPosition);

                if (snakeBodyList.Count >= snakeBodyCount + 1)
                {
                    snakeBodyList.RemoveAt(snakeBodyList.Count - 1);
                }

                transform.position = new Vector2(gridPosition.x, gridPosition.y);
                transform.eulerAngles = new Vector3(0, 0, GetDirectionAngle(moveDirection) - 90);

                AddSnakeBody();
                CheckGameOver();
            }

        }

        private void CheckGameOver()
        {
            for (int i = 0; i < snakeBodyList.Count; i++)
            {
                if (gridPosition == snakeBodyList[i])
                {
                    state = State.DEAD;
                    GameOver();
                    break;
                }
            }
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

        private void GameOver()
        {
            print("Game Over");
        }

        private void AddSnakeBody()
        {
            for (int i = 0; i < snakeBodyList.Count; i++)
            {
                Vector2 bodyPosition = snakeBodyList[i];
                GameObject body = Instantiate(snakeBody);
                body.transform.SetParent(transform);
                body.transform.position = new Vector2(bodyPosition.x, bodyPosition.y);
                body.transform.eulerAngles = transform.eulerAngles;
                Destroy(body, maxMoveTimer);
            }
        }
        #endregion ------------------

        #region --------- Public Methods ---------
        public Vector2Int GetSnakePosition()
        {
            return gridPosition;
        }

        public void IncreaseSnakeSize()
        {
            snakeBodyCount++;
        }

        public List<Vector2Int> GetFullSnakeSize()
        {
            List<Vector2Int> snakeFullBodyList = new List<Vector2Int>() { gridPosition };
            snakeFullBodyList.AddRange(snakeBodyList);
            return snakeFullBodyList;
        }
        #endregion ------------------
    }
}

