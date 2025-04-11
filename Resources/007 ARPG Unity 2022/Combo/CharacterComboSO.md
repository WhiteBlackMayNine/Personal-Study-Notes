---
tags:
  - "#科学/Unity/ARPG/Combo/CharacterComboSO"
created: 2024-06-20
---

---
# 关联知识点

[[CharacterComboDataSO]] 

---
# 说明

- 利用  [[Unity 知识点#^afe6ee|ScriptableObject]]  完成动作信息配置 模块
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
 
	return _allComboData[index].ComboParryName[parryName];
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
# 更新

- CharacterComboSO 创建出来的是一个连招表，而 CharacterComboDataSO 则是一个单独的连招
	- 需要把 CharacterComboDataSO 创建出来的连招 挂载到 CharacterComboSO 的连招表上
- 外部调用时，会传入当前执行的动作是第几个（传入的索引）
- CharacterComboSO 内的函数会根据索引，取出 `_allComboData[index].` 索引对应的招式
	- 然后根据函数不同执行不同的逻辑

---
# 源代码

![[CharacterComboSO.cs]]

---