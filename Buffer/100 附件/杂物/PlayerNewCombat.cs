using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;
using Combat;

//这个是 DLC 动作脚本
//跟老的很像
//这里写的就是简化版了 老版的一些东西没有写上去
//如果有需要可以往上写
//需要把老的脚本移除了，不然报错
public class PlayerNewCombat : CharacterNewCombatBase
{
    #region 变量相关

    private bool _hasLastComboAction;//是否有上一个动作

    #endregion


    #region 生命周期函数

    private void Update()
    {
        PlayerAttackInput();
    }

    #endregion

    #region 函数相关


    #region 角色攻击输入

    private void PlayerAttackInput()
    {
        if (_applyAttackInput)
        {
            if (GameInputManager.MainInstance.LAttack)//按的是左键，左键代表执行正常的连招
            {
                UpdateNextLAttackCombo();
            }
            else if (GameInputManager.MainInstance.RAttack)//按的是右键，右键代表执行派生动作
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

    #region 更新下一个动作（左键）

    private void UpdateNextLAttackCombo()
    {
        //更新下一个动作
        if (_hasLastComboAction)
        {
            SetComboData(_comboData.NextCombo);
        }//如果没有这个 if 语句 那么点击左键 第一个动作永远不会触发 点击左键后就直接赋值为第二个动作
        ComboActionExecute();
        _hasLastComboAction = true;
    }

    #endregion

    #region 更新下一个动作（右键）

    private void UpdateNextRAttackCombo()
    {
        //更新下一个动作
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
