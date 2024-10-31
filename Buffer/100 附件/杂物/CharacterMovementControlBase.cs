using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB_FGT.Movement
{
    [RequireComponent(typeof(CharacterController))]

    public abstract class CharacterMovementControlBase : MonoBehaviour
    {
        protected CharacterController _control;
        protected Animator _animator;


        //地面检测
        protected bool _characterIsOnGround; //角色是否在地面上
        [SerializeField, Header("地面检测")] protected float _groundDetectionPositionOffset;//检测的偏移量
        [SerializeField] protected float _detectionRang;//检测的范围
        [SerializeField] protected LayerMask _whatisGround;//检测的层级

        //重力相关
        protected readonly float CharacterGravity = -9.8f;//设置重力
        protected float _characterVerticalVelocity;//用来更新Y轴速度，可以应用与重力和跳跃高度
        protected float _fallOutDeltaTime;
        protected float _fallOutTime = 0.15f;//防止下楼梯时播放跌落动画
        protected readonly float _characterVerticalMaxVelocity = 54f;//角色在低于这个值才运行运用重力
        protected Vector3 _characterVerticalDirection;//角色的Y轴移动方向 因为是通过Move函数实现重力
                                                     //所以把characterVeritcalVelocity 应用到这个向量的Y值里面去更新


        protected virtual void Awake()
        {
            _control = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            _fallOutDeltaTime = _fallOutTime;
        }
        private void Update()
        {
            SetCharacterGracity();
            UpdateCharaterGravity();
        }

        //进行地面检测
        private bool GroundDetection()
        {
            //这个就是检测的中心点
            var detectionPosition = new Vector3(transform.position.x,
                y:transform.position.y - _groundDetectionPositionOffset,transform.position.z);

            return Physics.CheckSphere(detectionPosition, _detectionRang,_whatisGround,
                QueryTriggerInteraction.Ignore);
        }

        //重力
        private void SetCharacterGracity()
        {
            _characterIsOnGround = GroundDetection();

            if (_characterIsOnGround)
            {
                /* 
                 * 1.如果角色现在在地面上，需要重置这个 FallOutTime
                 * 2.重置角色的垂直速度 
                */
                _fallOutDeltaTime = _fallOutTime;

                if (_characterVerticalVelocity < 0f)
                {
                    _characterVerticalVelocity = -2f;//如果这里不将速度固定死，那么会一直累计在第二次下落或者在高处下落时，
                                                    //下落速度会很快，不会由快到慢  固定到-2时，第二次下落时会从-2开始
                                                    //如果不固定，在地面的时候就会进行累计，此时再跌落下落速度会很快
                }
            }
            else
            {
                //不在地面
                if (_fallOutDeltaTime > 0)
                {
                    _fallOutDeltaTime -= Time.deltaTime;//等待0.15s 用来帮助角色从较低的高低差下落  比如下楼梯
                }
                else
                {
                    //小于0，说明角色还没有落地，那么可能不是下楼梯 那么就有必要播放下落动画
                    //0.15s 可以根据需求去更改
                }

                if (_characterVerticalVelocity < _characterVerticalMaxVelocity)
                {
                    _characterVerticalVelocity += CharacterGravity * Time.deltaTime;
                }
            }
        }

        private void UpdateCharaterGravity()
        {
            _characterVerticalDirection.Set(newX: 0, newY: _characterVerticalVelocity, newZ: 0);//x y 是角色移动负责控制的
                                                                                              //不是重力控制的
            _control.Move(motion: _characterVerticalDirection * Time.deltaTime);
        }

        //坡道检测
        private Vector3 SlopResetDirection(Vector3 moveDirection)
        {
            //检测角色是否在坡道上移动
            //防止角色在下坡过程中由与速度过快而导致变成弹力球
            if (Physics.Raycast(transform.position + (transform.up * .5f),direction: Vector3.down, out var hit,
                _control.height * .85f,_whatisGround, QueryTriggerInteraction.Ignore))
            {
                if (Vector3.Dot(lhs: Vector3.up, rhs: hit.normal) != 0)//点积等于0，说明两个向量是垂直的，只要不是0那么就不垂直
                                                                       //浮点值不会完全相等，只会无限解进
                {
                    return moveDirection = Vector3.ProjectOnPlane(vector: moveDirection, hit.normal);
                }
            }
            return moveDirection;
        }

        private void OnDrawGizmos()
        {
            //这个就是检测的中心点
            var detectionPosition = new Vector3(this.transform.position.x, this.transform.position.y
                - _groundDetectionPositionOffset, this.transform.position.z);
            Gizmos.DrawWireSphere(detectionPosition, _detectionRang);
        }
    }
}