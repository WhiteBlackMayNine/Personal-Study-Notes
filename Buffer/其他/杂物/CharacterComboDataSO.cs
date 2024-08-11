using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ComboData",menuName = "Create/Character/ComboData" ,order = 0)]
public class CharacterComboDataSO : ScriptableObject
{
    #region 变量相关

    [SerializeField] private string _comboName;
    [SerializeField] private string[] _comboHitName;
    [SerializeField] private string[] _comboParryName;
    [SerializeField] private float _damage;
    [SerializeField] private float _coldTime;
    [SerializeField] private float _comboPositionOffset;

    public string ComboName => _comboName;
    public string[] ComboHitName => _comboHitName;
    public string[] ComboParryName => _comboParryName;
    public float Damage => _damage;
    public float ColdTime => _coldTime;
    public float ComboPositionOffset => _comboPositionOffset;

    #endregion

    public float GetComboMaxHitandParryCount ()=> _comboHitName.Length;

}
