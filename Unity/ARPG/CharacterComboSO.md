---
tags:
  - "#科学/Unity/ARPG/CharacterComboSO"
created: ""
更新:
---

---
# 关联知识点

[[CharacterComboDataSO]] [[PlayerCombatControl]]

---
# 说明

- 利用 ScriptObject 完成动作信息配置 模块
- 以后使用的时候拿模块出来配一下就行
- 这个脚本写的是连招表
	- 需要另一个脚本（`CharacterComboDataSO`）配置的招式搭配用
# 变量

- `[SerializeField]private List<CharacterComboDataSO> _allComboData = new List<CharacterComboDataSO>();`
	- 用来配置连招表信息
# 函数

## 获取动作 `TryGetOneComboAction`

```C#
public string TryGetOneComboAction(int index)
{
	if(_allComboData.Count == 0)
	{
		return null;
	}

	return _allComboData[index].ComboName;
}
```
## 获取受击动画 `TryGetOneHitAction`

```C#
public string TryGetOneHitAction(int index,int hitName)
{
	if (_allComboData.Count == 0)
	{
		return null;
	}

	if(_allComboData[index].GetComboMaxHitandParryCount() == 0)
	{
		return null;
	}

	return _allComboData[index].ComboHitName[hitName];
}
```
## 获取格挡动画 `TryGetOneParryAction`

```C#
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
 
	return _allComboData[index].ComboHitName[parryName];
}
```
## 获取伤害 `TryGetComboDamage`

```C#
public float TryGetComboDamage(int index)
{
	if(_allComboData.Count == 0)
	{
		return 0f;
	}

	return _allComboData[index].Damage;
}
```
## 获取招式冷却时间 `TryGetComboColdTime`
```C#
public float TryGetComboColdTime(int index)
{
	if (_allComboData.Count == 0)
	{
		return 0f;
	}

	return _allComboData[index].ColdTime;
}
```
## 获取位置偏移量 `TryGetComboPositionOffset`

```C#
public float TryGetComboPositionOffset(int index)
{
	if (_allComboData.Count == 0)
	{
		return 0f;
	}

	return _allComboData[index].ComboPositionOffset;
}
```
## 获取最大受击/格挡数 `TryGetHitAndParryMaxCount`

```C#
public float TryGetHitAndParryMaxCount(int index) => _allComboData[index].GetComboMaxHitandParryCount();
```
## 获取连招数量 `TryGetComboMaxCount`

```C#
public float TryGetComboMaxCount() => _allComboData.Count;
```
# 图片

- ![[CharacterComboSO.png]]

---
# 源代码

![[CharacterComboSO.cs]]

---