using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;
using Combat;

//����� DLC �����ű�
//���ϵĺ���
//����д�ľ��Ǽ򻯰��� �ϰ��һЩ����û��д��ȥ
//�������Ҫ��������д
//��Ҫ���ϵĽű��Ƴ��ˣ���Ȼ����
public class PlayerNewCombat : CharacterNewCombatBase
{
    #region �������

    private bool _hasLastComboAction;//�Ƿ�����һ������

    #endregion


    #region �������ں���

    private void Update()
    {
        PlayerAttackInput();
    }

    #endregion

    #region �������


    #region ��ɫ��������

    private void PlayerAttackInput()
    {
        if (_applyAttackInput)
        {
            if (GameInputManager.MainInstance.LAttack)//������������������ִ������������
            {
                UpdateNextLAttackCombo();
            }
            else if (GameInputManager.MainInstance.RAttack)//�������Ҽ����Ҽ�����ִ����������
            {
                if (!_comboData.HasChildCombo)
                {
                    return;
                }
                UpdateNextRAttackCombo();
            }
        }
    }

    #endregion

    #region ������һ�������������

    private void UpdateNextLAttackCombo()
    {
        //������һ������
        if (_hasLastComboAction)
        {
            SetComboData(_comboData.NextCombo);
        }//���û����� if ��� ��ô������ ��һ��������Զ���ᴥ�� ���������ֱ�Ӹ�ֵΪ�ڶ�������
        ComboActionExecute();
        _hasLastComboAction = true;
    }

    #endregion

    #region ������һ���������Ҽ���

    private void UpdateNextRAttackCombo()
    {
        //������һ������
        if (_hasLastComboAction)
        {
            SetComboData(_comboData.ChildCombo);
        }
        ComboActionExecute();
        _hasLastComboAction = true;
    }

    #endregion

    #endregion
}
