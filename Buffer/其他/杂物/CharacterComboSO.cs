using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "Create/Character/Combo", order = 0)]
public class CharacterComboSO : ScriptableObject
{
    [SerializeField] private List<CharacterComboDataSO> _allComboData = new List<CharacterComboDataSO>();

    #region �������

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string TryGetOneComboAction(int index)
    {
        if (_allComboData.Count == 0)
        {
            return null;
        }

        return _allComboData[index].ComboName;
    }

    /// <summary>
    /// ��ȡ�ܻ�����
    /// </summary>
    /// <param name="index"></param>
    /// <param name="hitName"></param>
    /// <returns></returns>
    public string TryGetOneHitAction(int index, int hitName)
    {
        if (_allComboData.Count == 0)
        {
            return null;
        }

        if (_allComboData[index].GetComboMaxHitandParryCount() == 0)
        {
            return null;
        }

        return _allComboData[index].ComboHitName[hitName];
    }

    /// <summary>
    /// ��ȡ�񵲶���
    /// </summary>
    /// <param name="index"></param>
    /// <param name="hitName"></param>
    /// <returns></returns>
    public string TryGetOneParryAction(int index, int parryName)
    {
        if (_allComboData.Count == 0)
        {
            return null;
        }

        if (_allComboData[index].GetComboMaxHitandParryCount() == 0)
        {
            return null;
        }

        return _allComboData[index].ComboParryName[parryName];
    }

    /// <summary>
    /// ��ȡ�˺�
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float TryGetComboDamage(int index)
    {
        if (_allComboData.Count == 0)
        {
            return 0f;
        }

        return _allComboData[index].Damage;
    }

    /// <summary>
    /// ��ȡ��ʽ��ȴʱ��
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float TryGetComboColdTime(int index)
    {
        if (_allComboData.Count == 0)
        {
            return 0f;
        }

        return _allComboData[index].ColdTime;
    }

    /// <summary>
    /// ��ȡλ��ƫ����
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float TryGetComboPositionOffset(int index)
    {
        if (_allComboData.Count == 0)
        {
            return 0f;
        }

        return _allComboData[index].ComboPositionOffset;
    }

    /// <summary>
    /// ��ȡ����ܻ�/����
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float TryGetHitAndParryMaxCount(int index) => _allComboData[index].GetComboMaxHitandParryCount();

    /// <summary>
    /// ��ȡ��������
    /// </summary>
    /// <returns></returns>

    public int TryGetComboMaxCount() => _allComboData.Count;

    #endregion
}
