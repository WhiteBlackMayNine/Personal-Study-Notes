using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;


namespace Movement
{
    public class PlayerMovement : CharacterMoveBase
    {
        #region 变量相关

        #region 角色旋转

        private float _rotationAngle;
        private Transform _mainCamera;
        private float _angleVelocity = 0f;
        [SerializeField, Header("角色旋转")] private float _smoothTime;

        #endregion

        #region 转身跑

        private Vector3 _characterTargetDirection;//角色目标朝向

        #endregion

        #region 控制快速跑与走路

        private float _runcount = 0;
        private float _walkcount = 0;

        #endregion

        #region 脚步声

        private float _nextFootTime;
        [SerializeField,Header("脚步声")] private float _slowFootTime;
        [SerializeField] private float _fastFootTime;

        #endregion

        #endregion

        #region 生命周期函数

        protected override void Awake()
        {
            base.Awake();

            //获取摄像机父对象的位置(也就是 TP_Camera)
            _mainCamera = Camera.main.transform.root.transform;
        }

        protected override void Update()
        {
            base.Update();
            CharacterRotationControl();
        }

        private void LateUpdate()
        {
            UpdateAnimation();
            CharacterRotationControl();
        }

        #endregion

        #region 函数相关

        /// <summary>
        /// 控制角色的旋转
        /// </summary>
        private void CharacterRotationControl()
        {
            if (!_characterIsGround)
            {
                return;
            }

            if (_animator.GetBool(AnimationID.HasInputID))
            {
                //如果有输入，那么获取旋转量
                _rotationAngle = Mathf.Atan2(GameInputManager.MainInstance.Movement.x, GameInputManager.MainInstance.Movement.y)
                    * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            }

            if (_animator.GetBool(AnimationID.HasInputID) && _animator.AnimationAtTag("Motion"))
            {
                //角色以Y轴为基准去旋转的
                this.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(this.transform.eulerAngles.y,
                    _rotationAngle, ref _angleVelocity, _smoothTime) ;

                //相当于Z轴经过 以Y轴为基准的旋转后 重新赋值 ，得到目标的移动朝向，也就是要移动的方向
                _characterTargetDirection = Quaternion.Euler(0, _rotationAngle,0) * Vector3.forward;
            }

            _animator.SetFloat(AnimationID.DetalAngleID, DevelopmentToos.GetDeltaAngle(this.transform,
                _characterTargetDirection.normalized));

        }


        /// <summary>
        /// 更新动画
        /// </summary>
        private void UpdateAnimation()
        {
            if (!_characterIsGround)
            {
                return;
            }

            _animator.SetBool(AnimationID.HasInputID, GameInputManager.MainInstance.Movement != Vector2.zero);

            if (_animator.GetBool(AnimationID.HasInputID))
            {
                if (GameInputManager.MainInstance.Run)
                {
                    _animator.SetBool(AnimationID.RunID, true);

                    _runcount++;
                }

                if (GameInputManager.MainInstance.Walk)
                {
                    _animator.SetBool(AnimationID.WalkID, true);

                    _walkcount++;
                }

                if (_animator.GetBool(AnimationID.RunID))
                {

                    if (_runcount % 2 == 0)
                    {
                        _animator.SetBool(AnimationID.RunID, false);
                    }
                }

                if (_animator.GetBool(AnimationID.WalkID))
                {

                    if (_walkcount % 2 == 0)
                    {
                        _animator.SetBool(AnimationID.WalkID, false);
                    }
                }

                _animator.SetFloat(AnimationID.MovementID, (_animator.GetBool(AnimationID.RunID) ? 2f : 
                    GameInputManager.MainInstance.Movement.sqrMagnitude), 
                    0.25f,Time.deltaTime);
                SetCharacterFootSound();
            }
            else
            {
                _animator.SetFloat(AnimationID.MovementID, 0f, 0.25f, Time.deltaTime);

                if(_animator.GetFloat(AnimationID.MovementID) < 0.2f)
                {
                    _animator.SetBool(AnimationID.RunID, false);
                    _animator.SetBool(AnimationID.WalkID, false);
                }
            }
        }

        /// <summary>
        /// 播放脚步声
        /// </summary>
        private void PlayFootSound()
        {
            GamePoolManager.MainInstance.TryGetOnePoolItem("FOOTSound", this.transform.position, Quaternion.identity);
            _nextFootTime = (_animator.GetFloat(AnimationID.MovementID) > 1.1f) ? _fastFootTime : _slowFootTime;
        }

        /// <summary>
        /// 设置角色脚步声
        /// </summary>
        private void SetCharacterFootSound()
        {
            if(_characterIsGround && _animator.AnimationAtTag("Motion") && 
                _animator.GetFloat(AnimationID.MovementID) > 0.5f)
            {
                _nextFootTime -= Time.deltaTime;
                if(_nextFootTime < 0f)
                {
                    PlayFootSound();
                }
            }
            else
            {
                _nextFootTime = 0f;
            }
        }

        #endregion
    }
}

