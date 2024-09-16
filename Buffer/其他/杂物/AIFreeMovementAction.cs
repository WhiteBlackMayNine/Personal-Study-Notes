using BehaviorDesigner.Runtime.Tasks;
using Combat;
using GGG.Tool;
using Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFreeMovementAction : Action
{
    #region ע��

    //�ڵ��˶���û�б���ҹ��� ���� û��ȥִ��������Ϊ
    //�ͻ�һֱ��������ڵ� һֱ�ƶ�

    //����ű� AI������״̬�µ� ͳһ���ƶ��ڵ㣬ר�Ź���AI���ƶ�
    //��û�б�ָ�ɹ���ָ��ʱ�� �������õ������ƶ�
    //���ɵ� ����/ǰ���ƶ�
    //���߲���ĳЩ����
    //������ҹ���ʱ����Ҫ�� AI ����

    //AI������ �ȼ�⹥��ָ�� 
    //�������ָ��û�б������ô�ж���Ŀ��ľ��룬  ��Զ����ô�ͳ���Ŀ���ƶ���ȥ
    //���ִﵽһ�������У�����ָ������û�б�����
    //��ô����AI����������������
    //������ָ������ô�˳��ڵ�

    #endregion

    #region �������

    private EnemyMovementControl _enemyMovementControl;
    private EnemyCombatControl _enemyCombatControl;

    //����ҵľ���
    private float DistranceForTarget() => DevelopmentToos.DistanceForTarget(
        EnemyManager.MainInstance.GetMainPlayer(), _enemyMovementControl.transform);

    private int _actionIndex;//������������ͬ��ֵ����ͬ�Ķ���
    private int _lastActionIndex;//��һ��ִ�еĶ���
    private float _actionTimer;//����ʱ�䣬�������Ҫά�ֶ��  �����ǹ̶���Ҳ�����������

    #endregion

    #region �������

    public override void OnAwake()
    {
        _enemyMovementControl = this.GetComponent<EnemyMovementControl>();
        _enemyCombatControl = this.GetComponent<EnemyCombatControl>();
        _lastActionIndex = _actionIndex;
        _actionTimer = 1f;
    }

    public override TaskStatus OnUpdate()
    {
        //�ж������Ƿ�ָ�ɹ���
        if (!_enemyCombatControl.GetAttackCommand())
        {
            //����ȥ�� ����ڵ� ����Ϊ�߼�
            //�������8 ����ǰ��������ң���
            //����С��8������3 ����ƶ�
            //������3һ�� �����ƶ�
            if (DistranceForTarget() > 8f)
            {
                _enemyMovementControl.SetAnimatorMovementValue(0, 1);
            }
            else if (DistranceForTarget() < 8f - 0.1f && DistranceForTarget() > 3f + 0.1f)
            {
                //��һ��ֵ�ڣ������ƶ�
                //0.1f Ϊһ��ƫ��ֵ
                FreeMovement();
                UpdateFreeActionTime();
            }
            else//����̫��ʱ�������ƶ�
            {
                _enemyMovementControl.SetAnimatorMovementValue(0f, -1f);//�����ƶ�
            }

            return TaskStatus.Running;
        }
        else
        {
            //����ȥ�� �˳��߼�
            DevelopmentToos.WTF(this.gameObject.name + "�ѱ�ָ�ɹ�����Ϊ");
        }
        return TaskStatus.Success;
    }

    #region �������ִ��

    private void FreeMovement()
    {
        switch (_actionIndex)
        {
            case 0:  //����Ϊ �����ƶ�
                _enemyMovementControl.SetAnimatorMovementValue(-1f, 0f);
                break;
            case 1:  // �����ƶ�
                _enemyMovementControl.SetAnimatorMovementValue(1f, 0f);
                break;
            case 2:
                _enemyMovementControl.SetAnimatorMovementValue(0f, 0f);
                break;
            case 3:
                _enemyMovementControl.SetAnimatorMovementValue(-1f, 1f);
                break;
            case 4:
                _enemyMovementControl.SetAnimatorMovementValue(1f, 1f);
                break;
                //���ද���Դ�����
        }
    }

    #endregion

    #region ���������������ʱ��
    private void UpdateFreeActionTime()
    {
        if (_actionTimer > 0f)
        { 
            //�����ƶ�ʱ��
            _actionTimer -= Time.deltaTime;
        }

        if (_actionTimer <= 0f)
        {
            //�ƶ�ʱ��С�ڵ���0ʱ  ���¶�������
            UpdateActionIndex();
        }
    }

    #endregion

    #region ������������Ķ�������

    private void UpdateActionIndex()
    {
        //����һ��
        _lastActionIndex = _actionIndex;

        //���¶�������  ע�� Random ������Ҳ�����
        _actionIndex = Random.Range(0, 5);

        //���ʱ��
        _actionTimer = Random.Range(1f, 3f);

        //������º�Ķ�������������һ�εĶ�������
        if (_actionIndex == _lastActionIndex)
        {
            //�����һ��
            _actionIndex = Random.Range(0, 5);
        }
        if (_actionIndex == 3 || _actionIndex == 4)
        {
            //�����⶯����ʱ������޸�
            _actionTimer = 1.5f;
        }
    }

    #endregion


    #endregion

}
