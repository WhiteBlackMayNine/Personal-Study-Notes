using Manage;
using UnityEngine;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        #region 变量

        public Vector2 movementDetection;
        
        public float movementSpeed;
        
        private Rigidbody2D _rb2d;
        
        private SpriteRenderer _renderer;

        #endregion
        

        #region 生命周期函数

        void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            movementDetection = GameInputManager.MainInstance.Movement;
        }

        public void FixedUpdate()
        {
            Move(movementDetection);
        }

        #endregion
        
        #region 函数

        private void Move(Vector2 movement)
        {
            _rb2d.velocity = new Vector2(movement.x * movementSpeed, _rb2d.velocity.y);
            
            //人物翻转
            // int faceDie = movement.x < 0 ? -1 : 1;
            // transform.localScale = new Vector3(faceDie, 1, 1);
            if(movement.x > 0)
                _renderer.flipX = false;
            if(movement.x < 0)
                _renderer.flipX = true;
        }
        
        #endregion

        
    }
}
