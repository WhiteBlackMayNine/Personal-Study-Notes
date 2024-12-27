using GGG.Tool;
using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterComboData;

//�µĹ���������õ��� �ӿ� 
//���ϣ��ʹ���¼��Ļ� �Ͱ����ϵ�ȥ�޸�
public abstract class CharacterNewCombatBase : MonoBehaviour
{
    #region �������

    protected Animator _animator;

    [SerializeField, Header("ComboData")] protected CharacterComboData _comboData;

    protected Transform _me;//���ؽű��Ķ���


    [SerializeField, Header("�������")] protected float _detectionRang;//��Χ

    [SerializeField] private LayerMask _enemyLayer;//���˵Ĳ㼶

    protected Transform _enemy;//��ǰ�ĵ���

    protected float _attackColdTime;
    protected bool _applyAttackInput;

    #endregion

    #region �������ں���

    private void Awake()
    {
        _me = this.transform;
        _animator = this.GetComponent<Animator>();
        ResetAttackInput();
    }

    #endregion

    #region �������

    #region �����¼�

    //��ͬ�� �ϵĶ�������
    //��� �������� ���ݶ��������и��£�ͨ��������ȡ��
    //����˵ һ������ �а˸��������� ��ô�Ϲ�������Ҫ���˸��ܻ���񵲣�����˸�ȫ��ͬ�������˺�����ͬ
    //��������˸������� ��һ�� �ڶ����߸� �ڰ˸� �ֱ���Ҫһ����ͬ���ܻ���񵲵��죩
    //��ô���µĹ������룬ֻ��Ҫд�������� ��һ����indexΪ0����������ʱ���� int��
    //�ڶ������߸���index Ϊ1 �ڰ˸�Ϊ3
    //��Ӧ�ľ����¶������ �� ComboDamageInfo ��� List �������������ֱ��Ӧ�� 0 1 2
    //�� ComboDamageInfo ��д��Ϣ
    //ͨ�������¼� ATK �� index ȥ ComboDamageInfo ������Ӧ�����˶������񵲡��˺� 

    public void NewATK(int index)
    {
        TriggerDamage(index);
    }

    #endregion

    #region �˺�����

    private void TriggerDamage(int index)
    {
        //��ȡ��ǰһ�������ĵ���
        if (GetUnits().Length != 0)
        {
            if (_enemy == null || _enemy != GetUnits()[0].transform)
            {
                if (_enemy != GetUnits()[0].transform != _me)
                {
                    _enemy = GetUnits()[0].transform;
                }
            }
        }
        if (GetUnits().Length != 0)
        {
            foreach (var e in GetUnits())
            {
                //�жϵ��˵ĽǶ��Ƿ�С��0.85
                if (Vector3.Dot(DevelopmentToos.DirectionForTarget(e.transform, _me), _me.forward) > 0.85f)
                {
                    continue;
                }
                //�жϵ��˵ľ����Ƿ�С��3
                if (DevelopmentToos.DistanceForTarget(e.transform, _me) > 3f)
                {
                    continue;
                }
                if (e.transform.TryGetComponent(out IDamaged damage))
                {
                    //Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)
                    //��ֹ������index������� ComboDamageInfos ֻ�������������� 5
                    //��ô��ȥ��Сֵ 3
                    damage.CharacterNormalDamaged(_comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].HitName,
                        _comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].ParryName,
                        _comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].Damage,
                        _me, _comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)]._damageType);

                }
            }
        }
    }

    #endregion

    #region �õ�����

    private Collider[] GetUnits()
    {
        return Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
       _enemyLayer, QueryTriggerInteraction.Ignore);
    }

    #endregion

    #region ��д�ӿ�

    //public void CharacterNormalDamaged(string hitName, string parryName, float damage, Transform attacker, DamageType damageType)
    //{
    //    throw new System.NotImplementedException();
    //}

    #endregion

    #region ����������ִ��

    protected void ComboActionExecute()
    {
        _attackColdTime = _comboData.ActionColdTime;

        _animator.CrossFade(_comboData.ActionName, 0.025f, 0, 0f);

        GameTimeManager.MainInstance.TryGetTimer(_attackColdTime, ResetAttackInput);

        _applyAttackInput = false;
    }

    #endregion

    #region ������һ������

    protected void SetComboData(CharacterComboData comboData)
    {
        //�����еĶ���û��������
        //��ִ����Щ����ʱ��ʧ�������Ҽ�
        //��ô�Ϳ�����ɿ�����
        //�����ж�һ��
        if (comboData != null)
        {
            _comboData = comboData;
        }
    }

    #endregion

    #region ���¹�������

    protected void ResetAttackInput()
    {
        _applyAttackInput = true;
    }

    #endregion

    #endregion
}
