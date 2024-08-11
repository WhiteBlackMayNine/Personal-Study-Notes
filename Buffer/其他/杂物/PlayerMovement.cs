using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;


namespace Movement
{
    public class PlayerMovement : CharacterMoveBase
    {
        #region �������

        #region ��ɫ��ת

        private float _rotationAngle;
        private Transform _mainCamera;
        private float _angleVelocity = 0f;
        [SerializeField, Header("��ɫ��ת")] private float _smoothTime;

        #endregion

        #region ת����

        private Vector3 _characterTargetDirection;//��ɫĿ�곯��

        #endregion

        #region ���ƿ���������·

        private float _runcount = 0;
        private float _walkcount = 0;

        #endregion

        #region �Ų���

        private float _nextFootTime;
        [SerializeField,Header("�Ų���")] private float _slowFootTime;
        [SerializeField] private float _fastFootTime;

        #endregion

        #endregion

        #region �������ں���

        protected override void Awake()
        {
            base.Awake();

            //��ȡ������������λ��(Ҳ���� TP_Camera)
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

        #region �������

        /// <summary>
        /// ���ƽ�ɫ����ת
        /// </summary>
        private void CharacterRotationControl()
        {
            if (!_characterIsGround)
            {
                return;
            }

            if (_animator.GetBool(AnimationID.HasInputID))
            {
                //��������룬��ô��ȡ��ת��
                _rotationAngle = Mathf.Atan2(GameInputManager.MainInstance.Movement.x, GameInputManager.MainInstance.Movement.y)
                    * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            }

            if (_animator.GetBool(AnimationID.HasInputID) && _animator.AnimationAtTag("Motion"))
            {
                //��ɫ��Y��Ϊ��׼ȥ��ת��
                this.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(this.transform.eulerAngles.y,
                    _rotationAngle, ref _angleVelocity, _smoothTime) ;

                //�൱��Z�ᾭ�� ��Y��Ϊ��׼����ת�� ���¸�ֵ ���õ�Ŀ����ƶ�����Ҳ����Ҫ�ƶ��ķ���
                _characterTargetDirection = Quaternion.Euler(0, _rotationAngle,0) * Vector3.forward;
            }

            _animator.SetFloat(AnimationID.DetalAngleID, DevelopmentToos.GetDeltaAngle(this.transform,
                _characterTargetDirection.normalized));

        }


        /// <summary>
        /// ���¶���
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
        /// ���ŽŲ���
        /// </summary>
        private void PlayFootSound()
        {
            GamePoolManager.MainInstance.TryGetOnePoolItem("FOOTSound", this.transform.position, Quaternion.identity);
            _nextFootTime = (_animator.GetFloat(AnimationID.MovementID) > 1.1f) ? _fastFootTime : _slowFootTime;
        }

        /// <summary>
        /// ���ý�ɫ�Ų���
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

