using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "Create/Character/Combo", order = 0)]
public class CharacterComboSO : ScriptableObject
{
    [SerializeField] private List<CharacterComboDataSO> _allComboData = new List<CharacterComboDataSO>();

    #region 函数相关

    /// <summary>
    /// 获取动作
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
    /// 获取受击动画
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
    /// 获取格挡动画
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
    /// 获取伤害
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
    /// 获取招式冷却时间
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
    /// 获取位置偏移量
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
    /// 获取最大受击/格挡数
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float TryGetHitAndParryMaxCount(int index) => _allComboData[index].GetComboMaxHitandParryCount();

    /// <summary>
    /// 获取连招数量
    /// </summary>
    /// <returns></returns>

    public int TryGetComboMaxCount() => _allComboData.Count;

    #endregion
}
