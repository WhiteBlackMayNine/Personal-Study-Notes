using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;

public class PlayerCombat : MonoBehaviour
{
    #region 变量相关

    private Animator _animator;

    #region 攻击/受击相关

    [SerializeField, Header("角色连招表")] private CharacterComboSO _baseCombo;
    [SerializeField, Header("角色变招表")] private CharacterComboSO _changeCombo;
    private CharacterComboSO _currentCombo;//当前使用的连招
    private int _currentComboIndex;//当前连招的索引值
    private float _maxColdTime;//连招的最大冷却时间
    private bool _canAttackInput;//是否允许进行攻击输入
    private int _hitIndex;//受击索引值
    private int _currentComboCount;

    #endregion


    #endregion

    #region 函数相关

    #region 角色攻击

    #region 是否允许攻击输入
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

    #region 普通攻击输入

    private void CharacterBaseAttackInput()
    {
        if (!CanBaseAttackInput())
        {
            return;
        }

        if (GameInputManager.MainInstance.LAttack)
        {
            if(_currentCombo == null || _currentCombo != _baseCombo)
            {
                ChangeComboData(_baseCombo);
            }
            ExecuteComboAction();
        }else if (GameInputManager.MainInstance.RAttack)
        {
            if(_currentComboCount >= 2)
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

    #region 改变组合技

    private void ChangeComboData(CharacterComboSO characterComboSO)
    {
        if(_currentCombo != characterComboSO)
        {
            _currentCombo = characterComboSO;
            ResetComboInfo();
        }
    }

    #endregion

    #region 执行动作
    private void ExecuteComboAction()
    {
        _currentComboCount += (_currentCombo == _baseCombo) ? 1 : 0;
        _hitIndex = 0;
        
        if(_currentComboIndex == _currentCombo.TryGetComboMaxCount())
        {
            //如果执行到了最后一个动作
            _currentComboIndex = 0;
        }
        _maxColdTime = _currentCombo.TryGetComboColdTime(_currentComboIndex);
        _animator.CrossFadeInFixedTime(_currentCombo.TryGetOneComboAction(_currentComboIndex), 0.15f, 0, 0);
        GameTimeManager.MainInstance.TryGetTimer(_maxColdTime, UpdataComboInfo);
        _canAttackInput = false;
    }

    #endregion

    #region 更新连招动画信息

    private void UpdataComboInfo()
    {
        _currentComboIndex++;
        _maxColdTime = 0f;
        _canAttackInput = true;
    }

    #endregion

    #region 重置连招动画信息

    private void ResetComboInfo()
    {
        _currentComboIndex = 0;
        _maxColdTime = 0f;
        _hitIndex = 0;
    }

    private void OnEndAttack()
    {
        //移动后从头开始进行攻击
        if (_animator.AnimationAtTag("Motion") && _canAttackInput)
        {
            ResetComboInfo();
        }
    }

    #endregion

    #endregion

    #region 生命周期函数

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        _canAttackInput = true;
        _currentCombo = _baseCombo;
    }

    private void Update()
    {
        CharacterBaseAttackInput();
        OnEndAttack();
    }

    #endregion

    #endregion
}
