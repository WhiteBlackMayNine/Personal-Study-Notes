---
tags:
  - 科学/Unity/ARPG/Health/CharacterHealthInfo
created: 2024-08-14
---

---
# 关联知识点

[[CharacterHealthBaseData]]

---
# 说明

- 需要挂载 CharacterHealthBaseData 所创建的资产文件
- 完成一系列的逻辑处理
# 变量

```C#
private float _currentHP;//当前生命值
private float _currentStrength;//当前体力值
private float _maxHP;//最大生命值
private float _maxStrength;//最大体力值
private bool _strengthFull;//体力值是否充沛
private bool _isDie => (_currentHP <= 0f);//当前生命值小于0 就设置为 true 即死亡
//关联变量
public float CurrentHP => _currentHP;
public float CurrentStrength => _currentStrength;
public float MaxHP => _maxHP;
public float MaxStrength => _maxStrength;
public bool IsDie => _isDie;
public bool StrengthFull => _strengthFull;
```

- `_strengthFull` 如果体力值小于等于0，没有体力值时
	- 这个变量会被设置为 `false` 
- 并且在 其他脚本中，会判断 `_strengthFull` 是否可以激活处决

- [[Unity 知识点#^14520f|这样的书写的好处]] 
# 函数
## 初始化变量 `InitCharacterHealthInfo()`

- 初始化变量
	- 最大生命值为 CharacterHealthBase 里设置的最大生命值
	- 最大体力值为 CharacterHealthBase 里设置的最大体力值
	- 当前生命值为 最大生命值（一开始对象都是满血）
	- 当前体力值为 最大体力值（一开始对象都是满体力）
	- 体力是否充沛 设置为 `true`
## 限制值的大小 `Clmap()`

- 用于造成伤害后的扣除血量/体力值

- 传入五个值 `value` `offValue` `min` `max` `Add`
	- 当前体力/生命值 造成伤害 最小值 最大值 是否为添加
- `Add` 如果为 `true` 那么代表的就是增加生命

```C#
private float Clamp(float value, float offValue, float min, float max, bool Add = false)
{
	return Mathf.Clamp((Add) ? value + offValue : value - offValue, min, max);
	//API的作用是将一个值限制在指定的最小值和最大值之间
}
```
## 造成伤害 `Damage()`

^f881da

- 如果体力充沛且传入的 `hasParry` 为 `true` 
	- 那么就去扣除体力值
	- 如果扣除完之后当前体力值小于等于0
		- 那么就把 `_strengthFull` 设置为 `false`
- 否则就去扣除生命值

```C#
public void Damage(float damage, bool hasParry = false)
{
	//如果体力充沛，那么就需要扣除体力值
	//敌人现在正在攻击动作中，还没打到玩家，却被玩家打到了
	//在体力恢复期间 _strengthFull = flase 时，不去扣除
	if (_strengthFull && hasParry)
	{
		_currentStrength = Clamp(_currentStrength, damage, 0f, _maxStrength);
		//如果体力值被清空了，把_strengthFull设置为false 就是破防状态
		if (_currentStrength <= 0f)
		{
			_strengthFull = false;
		}
	}
	else
	{
		//没有体力值的话 扣除血量
		_currentHP = Clamp(_currentHP, damage, 0f, _maxHP);
	}
}
```
## 恢复生命 / 体力 `AddHP() AddStrength()`

```C#
public void AddHP(float hp)
{
	_currentHP = Clamp(_currentHP, hp, 0f, _maxHP, true);
}

public void AddStrength(float strength)
{
	_currentStrength = Clamp(_currentHP, strength, 0f, _maxStrength, true);
	if (_currentStrength >= _maxStrength)
	{
		_strengthFull = true;
	}
}
```
# 总结

- 创建出资产文件后，需要挂载 CharacterHealthBaseData 的资产文件进行数据配置
- 然后把 CharacterHealthInfo 资产文件挂载到 玩家/敌人 对象身上
- 外部类会去调用 CharacterHealthInfo 脚本内的函数方法，完成生命值处理逻辑

---
# 源代码

![[CharacterHealthInfo.cs]]

---