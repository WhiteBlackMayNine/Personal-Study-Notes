using System;
using AnimatorParameters;
using General;
using Manage.Input_System;
using Settings.Unilts.ExpandClass;
using Settings.Unilts.Tools.DevelopmentTool;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        #region 变量
        
        private Animator _animator;
        
        private Rigidbody2D _rb2d;
        
        private SpriteRenderer _renderer;
        
        private PhysicsCheck _physicsCheck;
        
        private CapsuleCollider2D _collider;
        
        
        
        public Vector2 movementDetection;//移动方向
        
        [SerializeField,Header("基本移动")]
        public float movementSpeed;//移动速度
        public float jumpForce;//跳跃时 给 Y轴 施加的力
        
        private float WalkSpeed => movementSpeed / 2;//走路时的速度
        
        private Vector2 _originalOffset;//碰撞体原始的偏移量

        private Vector2 _originalSize;//碰撞器原始的尺寸大小
        
        public float hurtForce;
        
        [SerializeField,Header("状态")]
        public bool isHurt;
        
        public bool isDead;
        
        public bool isAttack;
        
        [SerializeField,Header("物理材质")]
        public PhysicsMaterial2D jumpMaterial;
        public PhysicsMaterial2D walkMaterial;

        #endregion
        

        #region 生命周期函数

        private void Awake()
        {
            #region 获取组件

            _animator = GetComponent<Animator>();
            _rb2d = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _physicsCheck = GetComponent<PhysicsCheck>();
            _collider = GetComponent<CapsuleCollider2D>();

            #endregion

            #region 修改碰撞体大小

            _originalOffset = _collider.offset;
            _originalSize = _collider.size;

            #endregion

        }

        private void Update()
        {
            movementDetection = GameInputManage.MainInstance.Movement;

            CharacterFlip();
            SquatChangeCollider();
            Jump();
            Walk();
            PlayerAttack();
            ChangeMaterial();
        }

        

        public void FixedUpdate()
        {
            Move();
        }

        #endregion
        
        #region 函数

        private void Move()
        {
            if(_animator.AnimationAtTag("Walk")) return;
            if(_animator.AnimationAtTag("Squat")) return;
            if(!isHurt&& !isAttack)
            {
                _rb2d.velocity = new Vector2(movementDetection.x * movementSpeed, _rb2d.velocity.y);
            }
        }

        private void Jump()
        {
            if (!_physicsCheck.isGround) return;
            if (isHurt || isAttack) return;
            if (GameInputManage.MainInstance.Jump)
            {
                _rb2d.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
            }

        }

        private void Walk()
        {
            if(isHurt || isAttack) return;
            if(!_physicsCheck.isGround) return;
            if(_animator.AnimationAtTag("Squat")) return;
            if(GameInputManage.MainInstance.Walk)
            {
                _rb2d.velocity = new Vector2(movementDetection.x * WalkSpeed, _rb2d.velocity.y);
            }
        }
        
        private void PlayerAttack()
        {
            if (isHurt) return;
            if (GameInputManage.MainInstance.Attack)
            {
                _animator.SetTrigger(AnimationID.AttackID);
                isAttack = true;
            }

        }
        
        /// <summary>
        /// 修改下蹲时碰撞器大小
        /// </summary>
        private void SquatChangeCollider()
        {
            if (_physicsCheck.isGround && GameInputManage.MainInstance.Squat)
            {
                //蹲下时 修改碰撞体的大小与中心偏移量
                _collider.offset = new Vector2(-0.05f, 0.8f);
                _collider.size = new Vector2(0.7f, 1.5f);
            }
            else
            {
                //还原
                _collider.offset = _originalOffset;
                _collider.size = _originalSize;
            }
        }
        
        /// <summary>
        /// 角色翻转
        /// </summary>
        private void CharacterFlip()
        {
            //人物翻转
            // int faceDie = movementDetection.x < 0 ? -1 : 1;
            // transform.localScale = new Vector3(faceDie, 1, 1);
            // _renderer.flipX = movementDetection.x switch
            // {
            //
            //     > 0 => false,
            //     < 0 => true,
            //     _ => _renderer.flipX
            // };
            
            int faceDir = (int)transform.localScale.x;

            if (movementDetection.x > 0)
                faceDir = 1;
            if (movementDetection.x < 0)
                faceDir = -1;

            //人物翻转
            transform.localScale = new Vector3(faceDir, 1, 1);
            
        }

        public void GetHurt(Transform attacker)
        {
            isHurt = true;
            _rb2d.velocity = Vector2.zero;
            var dir = new Vector2((transform.position.x - attacker.position.x),0).normalized;
            _rb2d.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        }

        public void PlayerDead()
        {
            isDead = true;
            GameInputManage.MainInstance.GameInputAction.Gameplay.Disable();
        }

        private void ChangeMaterial()
        {
            _collider.sharedMaterial = _physicsCheck.isGround ? walkMaterial : jumpMaterial ;
        }
        
        
        #endregion

        
    }
}
