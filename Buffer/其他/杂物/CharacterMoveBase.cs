using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;


namespace Movement
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class CharacterMoveBase : MonoBehaviour
    {
        protected CharacterController _characterController;
        protected Animator _animator;
        protected Vector3 _moveDirection;//角色移动方向

        #region 地面检测相关变量

        protected bool _characterIsGround;//角色是否在地面上
        [SerializeField, Header("地面检测")] protected LayerMask _whatIsGround;//地面的层级
        [SerializeField] protected float _detectionPositionOffset;//偏移量
        [SerializeField] protected float _detectionRang;//检测的范围

        #endregion

        #region 重力相关变量

        protected readonly float CharacterGravity = -9.8f;

        protected float _characterVerticalVelocity;//更新角色的Y轴速度

        protected float _characterVerticalMaxVelocity = 54f;//角色垂直下落最大速度，用来模拟空气阻力所带来的影响

        protected float _fallOutDeltaTime;//用于更新角色的下落速度 跟踪角色下落的时间间隔
                                          //当_fallOutDeltaTime大于0时，表示角色正在等待下落(也就是在这个时间内回到里面不会播放下落动作)；
                                          //当_fallOutDeltaTime小于等于0时，表示角色已经落地或者需要播放下落动画。

        protected float _fallOutTime = 0.15f;//下落的等待总时间  这个用于上/下 坡/楼梯 时进行使用

        //这个变量用来改变角色的Y轴速度，具体值由_characterVerticalVelocity 更新
        protected Vector3 _characterVerticalDirection;

        protected bool _isEnableGravity;//是否启用重力

        #endregion

        #region 生命周期函数

        protected virtual void Awake()
        {
            //获取组件
            _characterController = this.GetComponent<CharacterController>();
            _animator = this.GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            _fallOutDeltaTime = _fallOutTime;
            _isEnableGravity = true;
        }

        protected virtual void Update()
        {
            SetCharacterGravity();
            UpdateCharacterGravity();
        }

        private void OnAnimatorMove()
        {
            _animator.ApplyBuiltinRootMotion();
            UpdateCharacterMoveDirecion(_animator.deltaPosition);
        }

        //private void OnEnable()
        //{
        //    GameEventManager.MainInstance.AddEventListening<bool>("激活重力", EnableCharacterGravity);
        //}

        //private void OnDisable()
        //{
        //    GameEventManager.MainInstance.RemoveEvent<bool>("激活重力", EnableCharacterGravity);
        //}

        #endregion

        #region 地面检测相关函数

        //编辑模式下的绘图函数
        private void OnDrawGizmos()
        {
            //这个就是检测的中心点
            var detectionPosition = new Vector3(this.transform.position.x, this.transform.position.y
                - _detectionPositionOffset, this.transform.position.z);
            Gizmos.DrawWireSphere(detectionPosition, _detectionRang);
        }

        /// <summary>
        /// 进行地面检测
        /// </summary>
        /// <returns></returns>
        protected bool GroundDetection()
        {
            //创建一个中心点
            var detecttionPostion = new Vector3(this.transform.position.x,
                this.transform.position.y - _detectionPositionOffset, this.transform.position.z);

            //利用 API ——> Physics.CheckSphere 来返回一个布尔值
            return Physics.CheckSphere(detecttionPostion, _detectionRang, _whatIsGround, QueryTriggerInteraction.Ignore);
        }

        /// <summary>
        /// 进行坡道检测
        /// </summary>
        /// <param name="moveDirection">角色当前的移动方向</param>
        /// <returns>经过处理后的移动方向</returns>
        private Vector3 SlopReserDirection(Vector3 moveDirection)
        {
            //进行射线检测
            if (Physics.Raycast(transform.position + (transform.up * .5f),Vector3.down, out var hit,
             _characterController.height * .85f, _whatIsGround, QueryTriggerInteraction.Ignore))
            {
                if (Vector3.Dot(Vector3.up, hit.normal) != 0)
                //点积等于0，说明两个向量是垂直的，只要不是0那么就不垂直   浮点值不会完全相等，只会无限接近
                {
                    return moveDirection = Vector3.ProjectOnPlane(vector: moveDirection, hit.normal);
                }
            }
            return moveDirection;
        }

        /// <summary>
        /// 更新角色移动方向
        /// </summary>
        /// <param name="dirction">移动方向</param>
        private void UpdateCharacterMoveDirecion(Vector3 dirction)
        {
            //先进行坡道检测，获取处理后的角色移动方向
            _moveDirection = SlopReserDirection(dirction);
            _characterController.Move(_moveDirection * Time.deltaTime);//用于更新角色的Y轴  也就是垂直降落相关
        }

        #endregion

        #region 重力相关函数

        /// <summary>
        /// 激活重力
        /// </summary>
        private void EnableCharacterGravity(bool enable)
        {
            _isEnableGravity = enable;//修改

            //如果启用(true)重力，那么就将角色垂直速度设置为-2 ，如果不启用(false)则为0f
            _characterVerticalVelocity = (enable) ? -2f : 0f;
        }

        /// <summary>
        /// 设置角色重力
        /// </summary>
        private void SetCharacterGravity()
        {
            //先进行地面检测
            _characterIsGround = GroundDetection();

            if (_characterIsGround)
            {
                //如果角色在地面上，那么需要重置 _fallOutDeltaTime
                _fallOutDeltaTime = _fallOutTime;

                if (_characterVerticalVelocity < 0f)
                {
                    //如果垂直速度小于0，那么就说明之前有过下落
                    //在地面时将速度固定为-2f 否则会一直累加 待再次下落时初始速度会非常大
                    _characterVerticalVelocity = -2f;
                }
            }
            else
            {
                //如果角色不在地面上

                if (_fallOutDeltaTime > 0f)
                {
                    _fallOutDeltaTime -= Time.deltaTime;
                    //等待0.15s 也就是让角色在从较低的高度差下落
                    //一般都是下楼梯
                }
                else
                {
                    //如果小于0了，那么就说明需要播放下落动画了
                    //在此处播放下落动画
                }

                if (_characterVerticalVelocity < _characterVerticalMaxVelocity && _isEnableGravity)
                {
                    _characterVerticalVelocity += Time.deltaTime * CharacterGravity;
                    //更新角色垂直速度  时间 * 加速度
                }

            }
        }

        /// <summary>
        /// 更新角色重力
        /// </summary>
        private void UpdateCharacterGravity()
        {
            if (!_isEnableGravity)
            {
                //如果不启用(false)重力 则直接返回
                return;
            }

            //X Z 都是角色移动进行改变的，因此此处仅改变Y的值
            _characterVerticalDirection.Set(0, _characterVerticalVelocity, 0);
            _characterController.Move(_characterVerticalDirection * Time.deltaTime);
        }

        #endregion

    }
}

