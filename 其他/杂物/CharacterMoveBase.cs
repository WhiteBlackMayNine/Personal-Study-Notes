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
        protected Vector3 _moveDirection;//��ɫ�ƶ�����

        #region ��������ر���

        protected bool _characterIsGround;//��ɫ�Ƿ��ڵ�����
        [SerializeField, Header("������")] protected LayerMask _whatIsGround;//����Ĳ㼶
        [SerializeField] protected float _detectionPositionOffset;//ƫ����
        [SerializeField] protected float _detectionRang;//���ķ�Χ

        #endregion

        #region ������ر���

        protected readonly float CharacterGravity = -9.8f;

        protected float _characterVerticalVelocity;//���½�ɫ��Y���ٶ�

        protected float _characterVerticalMaxVelocity = 54f;//��ɫ��ֱ��������ٶȣ�����ģ�����������������Ӱ��

        protected float _fallOutDeltaTime;//���ڸ��½�ɫ�������ٶ� ���ٽ�ɫ�����ʱ����
                                          //��_fallOutDeltaTime����0ʱ����ʾ��ɫ���ڵȴ�����(Ҳ���������ʱ���ڻص����治�Ქ�����䶯��)��
                                          //��_fallOutDeltaTimeС�ڵ���0ʱ����ʾ��ɫ�Ѿ���ػ�����Ҫ�������䶯����

        protected float _fallOutTime = 0.15f;//����ĵȴ���ʱ��  ���������/�� ��/¥�� ʱ����ʹ��

        //������������ı��ɫ��Y���ٶȣ�����ֵ��_characterVerticalVelocity ����
        protected Vector3 _characterVerticalDirection;

        protected bool _isEnableGravity;//�Ƿ���������

        #endregion

        #region �������ں���

        protected virtual void Awake()
        {
            //��ȡ���
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
        //    GameEventManager.MainInstance.AddEventListening<bool>("��������", EnableCharacterGravity);
        //}

        //private void OnDisable()
        //{
        //    GameEventManager.MainInstance.RemoveEvent<bool>("��������", EnableCharacterGravity);
        //}

        #endregion

        #region ��������غ���

        //�༭ģʽ�µĻ�ͼ����
        private void OnDrawGizmos()
        {
            //������Ǽ������ĵ�
            var detectionPosition = new Vector3(this.transform.position.x, this.transform.position.y
                - _detectionPositionOffset, this.transform.position.z);
            Gizmos.DrawWireSphere(detectionPosition, _detectionRang);
        }

        /// <summary>
        /// ���е�����
        /// </summary>
        /// <returns></returns>
        protected bool GroundDetection()
        {
            //����һ�����ĵ�
            var detecttionPostion = new Vector3(this.transform.position.x,
                this.transform.position.y - _detectionPositionOffset, this.transform.position.z);

            //���� API ����> Physics.CheckSphere ������һ������ֵ
            return Physics.CheckSphere(detecttionPostion, _detectionRang, _whatIsGround, QueryTriggerInteraction.Ignore);
        }

        /// <summary>
        /// �����µ����
        /// </summary>
        /// <param name="moveDirection">��ɫ��ǰ���ƶ�����</param>
        /// <returns>�����������ƶ�����</returns>
        private Vector3 SlopReserDirection(Vector3 moveDirection)
        {
            //�������߼��
            if (Physics.Raycast(transform.position + (transform.up * .5f),Vector3.down, out var hit,
             _characterController.height * .85f, _whatIsGround, QueryTriggerInteraction.Ignore))
            {
                if (Vector3.Dot(Vector3.up, hit.normal) != 0)
                //�������0��˵�����������Ǵ�ֱ�ģ�ֻҪ����0��ô�Ͳ���ֱ   ����ֵ������ȫ��ȣ�ֻ�����޽ӽ�
                {
                    return moveDirection = Vector3.ProjectOnPlane(vector: moveDirection, hit.normal);
                }
            }
            return moveDirection;
        }

        /// <summary>
        /// ���½�ɫ�ƶ�����
        /// </summary>
        /// <param name="dirction">�ƶ�����</param>
        private void UpdateCharacterMoveDirecion(Vector3 dirction)
        {
            //�Ƚ����µ���⣬��ȡ�����Ľ�ɫ�ƶ�����
            _moveDirection = SlopReserDirection(dirction);
            _characterController.Move(_moveDirection * Time.deltaTime);//���ڸ��½�ɫ��Y��  Ҳ���Ǵ�ֱ�������
        }

        #endregion

        #region ������غ���

        /// <summary>
        /// ��������
        /// </summary>
        private void EnableCharacterGravity(bool enable)
        {
            _isEnableGravity = enable;//�޸�

            //�������(true)��������ô�ͽ���ɫ��ֱ�ٶ�����Ϊ-2 �����������(false)��Ϊ0f
            _characterVerticalVelocity = (enable) ? -2f : 0f;
        }

        /// <summary>
        /// ���ý�ɫ����
        /// </summary>
        private void SetCharacterGravity()
        {
            //�Ƚ��е�����
            _characterIsGround = GroundDetection();

            if (_characterIsGround)
            {
                //�����ɫ�ڵ����ϣ���ô��Ҫ���� _fallOutDeltaTime
                _fallOutDeltaTime = _fallOutTime;

                if (_characterVerticalVelocity < 0f)
                {
                    //�����ֱ�ٶ�С��0����ô��˵��֮ǰ�й�����
                    //�ڵ���ʱ���ٶȹ̶�Ϊ-2f �����һֱ�ۼ� ���ٴ�����ʱ��ʼ�ٶȻ�ǳ���
                    _characterVerticalVelocity = -2f;
                }
            }
            else
            {
                //�����ɫ���ڵ�����

                if (_fallOutDeltaTime > 0f)
                {
                    _fallOutDeltaTime -= Time.deltaTime;
                    //�ȴ�0.15s Ҳ�����ý�ɫ�ڴӽϵ͵ĸ߶Ȳ�����
                    //һ�㶼����¥��
                }
                else
                {
                    //���С��0�ˣ���ô��˵����Ҫ�������䶯����
                    //�ڴ˴��������䶯��
                }

                if (_characterVerticalVelocity < _characterVerticalMaxVelocity && _isEnableGravity)
                {
                    _characterVerticalVelocity += Time.deltaTime * CharacterGravity;
                    //���½�ɫ��ֱ�ٶ�  ʱ�� * ���ٶ�
                }

            }
        }

        /// <summary>
        /// ���½�ɫ����
        /// </summary>
        private void UpdateCharacterGravity()
        {
            if (!_isEnableGravity)
            {
                //���������(false)���� ��ֱ�ӷ���
                return;
            }

            //X Z ���ǽ�ɫ�ƶ����иı�ģ���˴˴����ı�Y��ֵ
            _characterVerticalDirection.Set(0, _characterVerticalVelocity, 0);
            _characterController.Move(_characterVerticalDirection * Time.deltaTime);
        }

        #endregion

    }
}

