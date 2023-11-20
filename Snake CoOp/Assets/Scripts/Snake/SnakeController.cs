using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeCoOp.Snake
{
    public class SnakeController : MonoBehaviour
    {
        #region --------- Serialized Variables ---------
        [SerializeField] private float snakeSpeed;
        #endregion ------------------

        #region --------- Private Variables ---------
        private Vector2 axisValue;
        private Rigidbody2D snakeRb;
        #endregion ------------------

        #region --------- Public Variables ---------
        #endregion ------------------

        #region --------- Monobehavior Methods ---------
        private void Start()
        {
            snakeRb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            axisValue.x = Input.GetAxisRaw("Horizontal");
            axisValue.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            snakeRb.MovePosition(snakeRb.position + axisValue * snakeSpeed * Time.fixedDeltaTime);
        }
        #endregion ------------------

        #region --------- Private Methods ---------
        #endregion ------------------

        #region --------- Public Methods ---------
        #endregion ------------------
    }
}
