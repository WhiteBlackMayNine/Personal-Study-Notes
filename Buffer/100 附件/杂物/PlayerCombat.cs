using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;
using Combat;
using System;

public class PlayerCombat : CharacterCombatBase
{
    #region �������

    #region ���˼�����

    //����ͨ�� EnemyManager ���м�����

    private Vector3 _detectionDirection;//��ⷽ��
    [SerializeField, Header("�������")] private float _detectionRang;//��Χ
    [SerializeField] private float _detectionDistance;//����
    [SerializeField] private LayerMask _enemyLayer;//���˵Ĳ㼶
    private Collider[] units;

    private Transform _cameraGameobject;//��ȡ���λ�� ���ڵ��˼��(��һ��)��

    #endregion

    #endregion

    #region �������ں���

    protected override void Awake()
    {
        base.Awake();

        //��ȡ���λ�� ���ڵ��˼��(��һ��)��
        if (Camera.main != null)
        {
            _cameraGameobject = Camera.main.transform;
        }
    }

    protected override void Update()
    {
        base.Update();
        //ʹ���˵ڶ��ַ�Χ��⣬��һ�����μ����غ�����ע����
        //UpdateDetectionDirection();

        //����һ�ͷ�������ѡ��һ
        //GetOneEnemyUnitWayOne();
        GetOneEnemyUnitWayTwo();
        //ClearEnemyWayOne();
        ClearEnemyWayTwo();

        CharacterBaseAttackInput();
        //���� Ϊʲô���ڸ���� Update���� ����Ϊ����EnemyCombat ����CharacterBaseAttackInput 
        //����Ϊ���ڵ�ȥ���ã������� Mono �е� Update �е��� Ҳ�Ͳ���д�����е� Update ��
        //���Ǿ�ֻ��д������� Update ��

        //CheackEnemyIsDie();
        CharacterFinishAttackInput();
        CharacterAssassinationInput();
    }

    private void OnEnable()
    {
        GameEventManager.MainInstance.AddEventListening<bool>("�����", EnableFinishEventHandle);
        GameEventManager.MainInstance.AddEventListening<Transform>("��������", CheckRemoveEnemy);
    }

    private void OnDisable()
    {
        GameEventManager.MainInstance.RemoveEvent<bool>("�����", EnableFinishEventHandle);
        GameEventManager.MainInstance.RemoveEvent<Transform>("��������", CheckRemoveEnemy);
    }

    private void FixedUpdate()
    {
        //ʹ���˵ڶ��ַ�Χ��⣬��һ�����μ����غ�����ע����
        //DetectionTarget();

        //����һ�ͷ�������ѡ��һ
        //GetNearUnitWayOne();
        GetNearUnitWayTwo();
    }

    #endregion

    #region �������

    #region ��ɫ����

    #region �Ƿ�������������
    private bool CanBaseAttackInput()
    {
        if (!_canAttackInput)
        {
            return false;
        }

        if (_animator.AnimationAtTag("Parry"))
        {
            return false;
        }

        if (_animator.AnimationAtTag("Hit"))
        {
            return false;
        }

        return true;
    }
    #endregion

    #region ��ͨ��������

    protected override void CharacterBaseAttackInput()
    {
        base.CharacterBaseAttackInput();

        if (!CanBaseAttackInput())
        {
            return;
        }

        if (GameInputManager.MainInstance.LAttack)
        {
            if (_currentCombo == null || _currentCombo != _baseCombo)
            {
                ChangeComboData(_baseCombo);
            }
            ExecuteComboAction();
        }
        else if (GameInputManager.MainInstance.RAttack)
        {
            if (_currentComboCount >= 2)
            {
                ChangeComboData(_changeCombo);
                switch (_currentComboCount)
                {
                    case 2:
                        _currentComboIndex = 0;
                        break;
                    case 3:
                        _currentComboIndex = 1;
                        break;
                    case 4:
                        _currentComboIndex = 2;
                        break;
                    case 5:
                        _currentComboIndex = 3;
                        break;
                }
            }
            else
            {
                return;
            }
            ExecuteComboAction();
            _currentComboCount = 0;
        }
    }

    #endregion

    #region λ��ͬ��

    protected override void MatchPosition()
    {
        base.MatchPosition();

        if (_animator.AnimationAtTag("Finish"))
        {
            //��Ϊ�������ڵ��˵�ǰ�����е� ���� ������˵ĺ�
            transform.rotation = Quaternion.LookRotation(-_currentEnemy.forward);
            RunningMatch(_finishCombo, _FinishComboIndex);
        }
        else if (_animator.AnimationAtTag("Assassination"))
        {
            //��Ϊ��ɱ���ڵ��˵ĺ󷽽��е� ���� ������˵�ǰ��
            transform.rotation = Quaternion.LookRotation(_currentEnemy.forward);
            RunningMatch(_assassinationCombo, _FinishComboIndex);
        }
    }

    #endregion

    #endregion

    #region ���˼��

    #region ��һ�ּ�� ���μ��

    //��ȡһ����Χ�ڵ����е��� ȡһ������ĵ�����ΪĿ��

    private void UpdateDetectionDirection()//�������μ�ⷽ��
    {
        _detectionDirection = (_cameraGameobject.forward * GameInputManager.MainInstance.Movement.y) +
            (_cameraGameobject.right * GameInputManager.MainInstance.Movement.x);

        //y���ֵ�����ֹ���ڰ����
        _detectionDirection.Set(_detectionDirection.x, 0f, _detectionDirection.z);

        _detectionDirection = _detectionDirection.normalized;
    }

    private void DetectionTarget()
    {
        //�жϼ��һ����û�м�⵽����   ���˵�ͼ���ǵھŲ�  ����� 1<<9 ���Ǽ����˲㼶
        if (Physics.SphereCast(transform.position + (transform.up * 0.7f), _detectionRang, _detectionDirection,
            out var hit, _detectionDistance, 1 << 9, QueryTriggerInteraction.Ignore))
        {
            //���� if �ڲ���˵���Ѿ���⵽�˵���
            _currentEnemy = hit.collider.transform;
        }
    }

    #endregion

    #region �ڶ��ּ�� ��Χ���

    //�����Ϊ���� ȡ�Զ����һ���뾶Բ�ķ�Χ�� ��ȡ���еĵ���
    //�ڵ�ǰ���û��Ŀ��ʱ ȡ�����������ĵ���ΪĿ��
    //(1)����һ ����ȡһ����Χ�ڵ����е��ˣ�ѡ��һ�������������ΪĿǰ����
    //(2)������ ����һ�����������Ŀ�� �ھ���һ����Χ��ʱ����ı�Ŀ��(��̬����)

    #region ����һ
    //��ȡһ����Χ�ڵ����е��ˣ�ѡ��һ�������������ΪĿǰ����
    //���ƶ�ʱ�������ȡĿ��
    //ʹ��ʱ ����Ѱ뾶���ô�һ�� 

    //�õ����������е���
    private void GetNearUnitWayOne()
    {
        //�����ǰ�� Ŀǰ���� Ϊ���� �����Ѿ��е�����
        if (_currentEnemy != null)
        {
            return;
        }
        if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//�ƶ�ʱ��ȥ��ȡ
        {
            return;
        }
        //��ȡһ����Χ�ڵĵ���
        units = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
            _enemyLayer, QueryTriggerInteraction.Ignore);
    }

    //ѡ���������ĵ���
    private void GetOneEnemyUnitWayOne()
    {
        //�����������Ϊ�� Ҳ���ǳ���Ϊ0 �򷵻�
        if (units.Length == 0)
        {
            return;
        }

        //�����ǰ�� Ŀǰ���� Ϊ���� �����Ѿ��е����� 
        if (_currentEnemy != null)
        {
            return;
        }

        if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//�ƶ�ʱ��ȥ��ȡ
        {
            return;
        }

        if (!_animator.AnimationAtTag("Attack"))
        {
            return;
        }

        //-----------------------------------------------------------------------

        Transform _temp_Enemy = null;
        var _distance = Mathf.Infinity;//Mathf.Infinity ��ʾ�������

        foreach (var e in units)
        {
            //����ȥ���������ĵ��˲����Ƚϣ�ѡȡ������������һ��������Ϊ��ǰĿ��
            var dis = DevelopmentToos.DistanceForTarget(e.transform, transform);
            if (dis < _distance)//ð��������бȽ�
            {
                _temp_Enemy = e.transform;
                _distance = dis;
            }
        }

        //����������ȡ��������������һ�����˵���Ŀ��
        _currentEnemy = _temp_Enemy != null ? _temp_Enemy : _currentEnemy;

        //------------------------------------------------------------------------
    }

    //��յ���
    private void ClearEnemyWayOne()
    {
        if (_currentEnemy == null)
        {
            return;
        }

        if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//�ƶ�ʱ��ȥ��ȡ
        {
            _currentEnemy = null;
        }
    }

    #endregion

    #region ������
    //��һ�����������Ŀ�� �ھ���һ����Χ��ʱ����ı�Ŀ��
    //��ʱ����


    private void GetNearUnitWayTwo()
    {
        //�����ǰ�� Ŀǰ���� Ϊ���� �����Ѿ��е����� �� ����С��1.5 �򷵻� ������Ŀ��
        if (_currentEnemy != null && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 1.5f)
        {
            return;
        }

        //��ȡһ����Χ�ڵĵ���
        units = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
            _enemyLayer, QueryTriggerInteraction.Ignore);
    }

    //ѡ���������ĵ���
    private void GetOneEnemyUnitWayTwo()
    {
        //�����������Ϊ�� Ҳ���ǳ���Ϊ0 �򷵻�
        if (units.Length == 0)
        {
            return;
        }

        //�����ǰ�� Ŀǰ���� Ϊ���� �����Ѿ��е����� �� ����С��1.5 �򷵻� ������Ŀ��
        if (_currentEnemy != null && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 1.5f)
        {
            return;
        }

        //-----------------------------------------------------------------------

        Transform _temp_Enemy = null;
        var _distance = Mathf.Infinity;//Mathf.Infinity ��ʾ�������

        foreach (var e in units)
        {
            //����ȥ���������ĵ��˲����Ƚϣ�ѡȡ������������һ��������Ϊ��ǰĿ��
            var dis = DevelopmentToos.DistanceForTarget(e.transform, transform);
            if (dis < _distance)//ð��������бȽ�
            {
                _temp_Enemy = e.transform;
                _distance = dis;
            }
        }

        //����������ȡ��������������һ�����˵���Ŀ��
        _currentEnemy = _temp_Enemy != null ? _temp_Enemy : _currentEnemy;

        //------------------------------------------------------------------------
    }

    //��յ���
    private void ClearEnemyWayTwo()
    {
        if (_currentEnemy == null)
        {
            return;
        }

        if (_animator.GetFloat(AnimationID.MovementID) > 0.7f && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 3f)
        {
            _currentEnemy = null;
        }
    }

    #endregion


    #endregion

    #endregion

    #region ��ͼ

    private void OnDrawGizmos()//��ͼ����
    {
        Gizmos.DrawWireSphere(transform.position + (transform.up * 0.7f) +
            (_detectionDirection * _detectionDistance), _detectionRang);
    }
    #endregion

    #region ����

    private bool CanSpecialAttack()//�Ƿ�����ִ�����⹥��
    {
        if (_animator.AnimationAtTag("Finish"))
        {
            return false;
        }

        if (_animator.AnimationAtTag("Assassination"))
        {
            return false;
        }

        if (_currentEnemy == null)
        {
            return false;
        }

        if (!_canFinish)
        {
            return false;
        }


        if (_currentComboCount < 2) //��Ŀǰ��������С��2ʱ�������Դ���
        {
            return false;
        }


        return true;
    }


    private void CharacterFinishAttackInput()
    {
        if (!CanSpecialAttack())
        {
            return;
        }

        if (GameInputManager.MainInstance.Finish)
        {
            //ȥ���Ŷ�Ӧ�Ĵ�������
            _FinishComboIndex = Random.Range(0, _finishCombo.TryGetComboMaxCount());
            _animator.Play(_finishCombo.TryGetOneComboAction(_FinishComboIndex));

            //�����¼����ģ����õ���ע��Ĵ����¼�
            GameEventManager.MainInstance.CallEvent("��������", _finishCombo.TryGetOneHitAction(_FinishComboIndex,
                0), transform, _currentEnemy);

            ResetComboInfo();
            EnemyManager.MainInstance.StopAllActiveUnit();
            GameEventManager.MainInstance.CallEvent<Transform, float>("�������λ��", _currentEnemy,
                _animator.GetCurrentAnimatorStateInfo(0).length - 1f);
            _currentComboCount = 0;
            _canFinish = false;
        }
    }

    private void EnableFinishEventHandle(bool apply)//�����
    {
        if (_canFinish)
        {
            return;
        }

        _canFinish = apply;
    }

    #endregion

    #region ��ɱ

    /// <summary>
    /// �Ƿ��������а�ɱ
    /// </summary>
    /// <returns></returns>
    private bool CanAssassination()
    {
        //��ǰû��Ŀ��
        if (_currentEnemy == null)
        {
            return false;
        }

        //����
        if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 2f)
        {
            return false;
        }

        //�Ƕ�
        if (Vector3.Angle(transform.forward, _currentEnemy.transform.forward) > 30f)
        {
            return false;
        }

        //��ǰ�ڰ�ɱ������
        if (_animator.AnimationAtTag("Assassination"))
        {
            return false;
        }

        //��ǰ�ڴ���������
        if (_animator.AnimationAtTag("Finish"))
        {
            return false;
        }

        return true;

    }

    private void CharacterAssassinationInput()
    {
        if (!CanAssassination())
        {
            return;//����Ƿ���Խ��а�ɱ
        }

        if (GameInputManager.MainInstance.TakeOut)
        {
            //ִ�а�ɱ
            _FinishComboIndex = Random.Range(0, _assassinationCombo.TryGetComboMaxCount());//���ȡһ������ֵ
            _animator.Play(_assassinationCombo.TryGetOneComboAction(_FinishComboIndex), 0, 0f);

            //���е��˵ı���ɱ�¼�
            GameEventManager.MainInstance.CallEvent("��������", _assassinationCombo.TryGetOneHitAction(_FinishComboIndex,
               0), transform, _currentEnemy);//������������¼���һ����
            ResetComboInfo();
        }
    }
    #endregion

    #region Ŀ����������

    #region ����һ
    //CharacterHealthBase �� ��ǰĿ���������Ƴ���ǰĿ�� ����

    private void CheackEnemyIsDie()
    {
        if (_currentEnemy == null)
        {
            return;
        }

        if (_currentEnemy.TryGetComponent(out IHealth health))
        {
            if (_animator.AnimationAtTag("Motion") && health.OnDie() && !_animator.IsInTransition(0))
            {
                //.IsInTransition(0) ����ɫ�����Ƿ�ת��

                //�����ҵĶ���ִ����ϣ�ͬʱ�����Ѿ����������Ҷ������ڹ�����
                //���������û��ִ����ϣ�Ȼ����ĳЩ����������Ҫȥ�жϻ�������ǰĿ��
                //��ô�����ڵ���������ֱ���Ƴ���ǰĿ�꣬��Ҿ�û�취��ȡ����ǰĿ��
                //�ͻᱨ�� ������
                _currentEnemy = null;
            }
        }
    }

    #endregion

    #region ������

    private void CheckRemoveEnemy(Transform enemy)
    {
        if (_currentEnemy == enemy)
        {
            //�����ҵĶ���ִ������ˣ�ͬʱ�������Ѿ�������
            //���Ҷ������ڹ�����
            //���������û��ִ����ϣ�Ȼ������ĳЩ����������Ҫȥ�жϻ���������ǰĿ��
            //��ô�����ڵ���������ֱ�Ӿ��Ƴ���ǰĿ��
            //��ô��Ҿ�û�취��ȡ����ǰĿ��
            //�ͻᱨ������!
            _currentEnemy = null;
            _canFinish = false;
        }
    }

    #endregion

    #endregion

    #endregion
}
