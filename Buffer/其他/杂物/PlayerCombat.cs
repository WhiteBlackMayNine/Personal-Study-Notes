using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool;

public class PlayerCombat : MonoBehaviour
{
    #region �������

    private Animator _animator;

    #region ����/�ܻ����

    [SerializeField, Header("��ɫ���б�")] private CharacterComboSO _baseCombo;
    [SerializeField, Header("��ɫ���б�")] private CharacterComboSO _changeCombo;
    private CharacterComboSO _currentCombo;//��ǰʹ�õ�����
    private int _currentComboIndex;//��ǰ���е�����ֵ
    private float _maxColdTime;//���е������ȴʱ��
    private bool _canAttackInput;//�Ƿ�������й�������
    private int _hitIndex;//�ܻ�����ֵ
    private int _currentComboCount;

    #endregion


    #endregion

    #region �������

    #region ��ɫ����

    #region �Ƿ�����������
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

    #region �ı���ϼ�

    private void ChangeComboData(CharacterComboSO characterComboSO)
    {
        if(_currentCombo != characterComboSO)
        {
            _currentCombo = characterComboSO;
            ResetComboInfo();
        }
    }

    #endregion

    #region ִ�ж���
    private void ExecuteComboAction()
    {
        _currentComboCount += (_currentCombo == _baseCombo) ? 1 : 0;
        _hitIndex = 0;
        
        if(_currentComboIndex == _currentCombo.TryGetComboMaxCount())
        {
            //���ִ�е������һ������
            _currentComboIndex = 0;
        }
        _maxColdTime = _currentCombo.TryGetComboColdTime(_currentComboIndex);
        _animator.CrossFadeInFixedTime(_currentCombo.TryGetOneComboAction(_currentComboIndex), 0.15f, 0, 0);
        GameTimeManager.MainInstance.TryGetTimer(_maxColdTime, UpdataComboInfo);
        _canAttackInput = false;
    }

    #endregion

    #region �������ж�����Ϣ

    private void UpdataComboInfo()
    {
        _currentComboIndex++;
        _maxColdTime = 0f;
        _canAttackInput = true;
    }

    #endregion

    #region �������ж�����Ϣ

    private void ResetComboInfo()
    {
        _currentComboIndex = 0;
        _maxColdTime = 0f;
        _hitIndex = 0;
    }

    private void OnEndAttack()
    {
        //�ƶ����ͷ��ʼ���й���
        if (_animator.AnimationAtTag("Motion") && _canAttackInput)
        {
            ResetComboInfo();
        }
    }

    #endregion

    #endregion

    #region �������ں���

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
