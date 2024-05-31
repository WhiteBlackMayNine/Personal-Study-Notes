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


        //������
        protected bool _characterIsOnGround; //��ɫ�Ƿ��ڵ�����
        [SerializeField, Header("������")] protected float _groundDetectionPositionOffset;//����ƫ����
        [SerializeField] protected float _detectionRang;//���ķ�Χ
        [SerializeField] protected LayerMask _whatisGround;//���Ĳ㼶

        //�������
        protected readonly float CharacterGravity = -9.8f;//��������
        protected float _characterVerticalVelocity;//��������Y���ٶȣ�����Ӧ������������Ծ�߶�
        protected float _fallOutDeltaTime;
        protected float _fallOutTime = 0.15f;//��ֹ��¥��ʱ���ŵ��䶯��
        protected readonly float _characterVerticalMaxVelocity = 54f;//��ɫ�ڵ������ֵ��������������
        protected Vector3 _characterVerticalDirection;//��ɫ��Y���ƶ����� ��Ϊ��ͨ��Move����ʵ������
                                                     //���԰�characterVeritcalVelocity Ӧ�õ����������Yֵ����ȥ����


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

        //���е�����
        private bool GroundDetection()
        {
            //������Ǽ������ĵ�
            var detectionPosition = new Vector3(transform.position.x,
                y:transform.position.y - _groundDetectionPositionOffset,transform.position.z);

            return Physics.CheckSphere(detectionPosition, _detectionRang,_whatisGround,
                QueryTriggerInteraction.Ignore);
        }

        //����
        private void SetCharacterGracity()
        {
            _characterIsOnGround = GroundDetection();

            if (_characterIsOnGround)
            {
                /* 
                 * 1.�����ɫ�����ڵ����ϣ���Ҫ������� FallOutTime
                 * 2.���ý�ɫ�Ĵ�ֱ�ٶ� 
                */
                _fallOutDeltaTime = _fallOutTime;

                if (_characterVerticalVelocity < 0f)
                {
                    _characterVerticalVelocity = -2f;//������ﲻ���ٶȹ̶�������ô��һֱ�ۼ��ڵڶ�����������ڸߴ�����ʱ��
                                                    //�����ٶȻ�ܿ죬�����ɿ쵽��  �̶���-2ʱ���ڶ�������ʱ���-2��ʼ
                                                    //������̶����ڵ����ʱ��ͻ�����ۼƣ���ʱ�ٵ��������ٶȻ�ܿ�
                }
            }
            else
            {
                //���ڵ���
                if (_fallOutDeltaTime > 0)
                {
                    _fallOutDeltaTime -= Time.deltaTime;//�ȴ�0.15s ����������ɫ�ӽϵ͵ĸߵͲ�����  ������¥��
                }
                else
                {
                    //С��0��˵����ɫ��û����أ���ô���ܲ�����¥�� ��ô���б�Ҫ�������䶯��
                    //0.15s ���Ը�������ȥ����
                }

                if (_characterVerticalVelocity < _characterVerticalMaxVelocity)
                {
                    _characterVerticalVelocity += CharacterGravity * Time.deltaTime;
                }
            }
        }

        private void UpdateCharaterGravity()
        {
            _characterVerticalDirection.Set(newX: 0, newY: _characterVerticalVelocity, newZ: 0);//x y �ǽ�ɫ�ƶ�������Ƶ�
                                                                                              //�����������Ƶ�
            _control.Move(motion: _characterVerticalDirection * Time.deltaTime);
        }

        //�µ����
        private Vector3 SlopResetDirection(Vector3 moveDirection)
        {
            //����ɫ�Ƿ����µ����ƶ�
            //��ֹ��ɫ�����¹����������ٶȹ�������±�ɵ�����
            if (Physics.Raycast(transform.position + (transform.up * .5f),direction: Vector3.down, out var hit,
                _control.height * .85f,_whatisGround, QueryTriggerInteraction.Ignore))
            {
                if (Vector3.Dot(lhs: Vector3.up, rhs: hit.normal) != 0)//�������0��˵�����������Ǵ�ֱ�ģ�ֻҪ����0��ô�Ͳ���ֱ
                                                                       //����ֵ������ȫ��ȣ�ֻ�����޽��
                {
                    return moveDirection = Vector3.ProjectOnPlane(vector: moveDirection, hit.normal);
                }
            }
            return moveDirection;
        }

        private void OnDrawGizmos()
        {
            //������Ǽ������ĵ�
            var detectionPosition = new Vector3(this.transform.position.x, this.transform.position.y
                - _groundDetectionPositionOffset, this.transform.position.z);
            Gizmos.DrawWireSphere(detectionPosition, _detectionRang);
        }
    }
}