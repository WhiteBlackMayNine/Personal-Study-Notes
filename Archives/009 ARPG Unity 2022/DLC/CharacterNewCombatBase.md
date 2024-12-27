---
tags:
  - 科学/Unity/ARPG/DLC/CharacterNewCombatBase
created: 2024-08-16
---

---
# 关联知识点



---
# 说明

- 新的攻击输入采用的是 接口
- 如果希望使用事件的话 就按照老的去修改
- 这里只写了一部分功能，如果需要更多功能，可以把老攻击输入的拿过来稍加修改就能用
# 变量

```C#
protected Animator _animator;//获取脚本组件

[SerializeField, Header("ComboData")] protected CharacterComboData _comboData;

protected Transform _me;


[SerializeField, Header("攻击检测")] protected float _detectionRang;

[SerializeField] private LayerMask _enemyLayer;

protected Transform _enemy;

protected float _attackColdTime;
protected bool _applyAttackInput;
```

- `_comboData` 要挂载的招式
- `_me` 挂载脚本的对象
- `_detectionRang` 攻击检测的范围
- `_enemyLayer` 敌人的层级
- `_enemy` 当前的敌人
- `_attackColdTime` 攻击冷却时间
- `_applyAttackInput` 是否允许攻击输入
# 生命周期函数

```C#
private void Awake()
{
	_me = this.transform;
	_animator = this.GetComponent<Animator>();
	ResetAttackInput();
}
```

# 函数
## 动画事件 `NewATK()`

^74059d

- 与老攻击输入不同的是，会根据传入的 `index` 来选择不同的 [[CharacterNewComboData#^d50fcb|受击信息]] 进行处理

```C#
    public void NewATK(int index)
    {
        TriggerDamage(index);
    }
```
## 伤害触发 `TriggerDamage()`

- 动画触发了 `NewATK` 这个事件，那么就会执行`TriggerDamage`这个函数
- 判断是否附近有敌人，通过 `GetUnits()` 函数 获取
- 判断敌人的 角度、距离 是否符合触发伤害的条件

- `if (GetUnits().Length != 0)`
	- 当前 `GetUnits().Length` 敌人列表长度不为0
	- `if (_enemy == null || _enemy != GetUnits()[0].transform)`
		- 当前敌人 是否为空 或者 当前敌人不为 敌人列表 中的第一个（也就是 敌人检测时 距离最近的 敌人）
		- `if (_enemy != GetUnits()[0].transform != _me)`
			- 先判断当前敌人是否为 敌人列表 中的第一个，然后判断 是否为 当前脚本挂载对象
			- `_enemy = GetUnits()[0].transform;`
				- 将 当前敌人 设置为 敌人列表 中的第一个（距离最近的哪一个）

- 进行 `foreach` 循环，判断是否符合触发伤害的条件
	- 如果符合，就获取 该循环元素的 `IDamaged` 接口（ [[CharacterNewHealthBase]] 中的接口）
		- 调用接口的 `CharacterNormalDamaged` 方法
			- 传入 受击动画名、格挡动画名、伤害、攻击者位置、伤害类型

- 关于为什么要判断是否符合伤害触发的条件
	- 在 DLC 中，并 [[CharacterNewHealthBase]] 中并没有像 老的一样，判断了 被攻击是不是自己
	- 在这里去判断是否符合伤害触发的条件，只有被攻击的那个对象符合
	- 其他对象都是不符合的
	- 这样，在攻击一个对象的时候，其他对象不会跟着一起播放受击动画

```C#
private void TriggerDamage(int index)
{
	//获取当前一个锁定的敌人
	if (GetUnits().Length != 0)
	{
		if (_enemy == null || _enemy != GetUnits()[0].transform)
		{
			if (_enemy != GetUnits()[0].transform != _me)
			{
				_enemy = GetUnits()[0].transform;
			}
		}
	}

	if (GetUnits().Length != 0)
	{
		foreach (var e in GetUnits())
		{
			//判断敌人的角度是否小于0.85
			if (Vector3.Dot(DevelopmentToos.DirectionForTarget(e.transform, _me), _me.forward) > 0.85f)
			{
				continue;
			}
			//判断敌人的距离是否小于3
			if (DevelopmentToos.DistanceForTarget(e.transform, _me) > 3f)
			{
				continue;
			}
			if (e.transform.TryGetComponent(out IDamaged damage))
			{
				//Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)
				//防止动画的index输入错误 ComboDamageInfos 只有三个，但输入 5
				//那么就去最小值 3
				damage.CharacterNormalDamaged(_comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].HitName,
					_comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].ParryName,
					_comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)].Damage,
					_me, _comboData.ComboDamageInfos[Mathf.Min(index, _comboData.ComboDamageInfos.Count - 1)]._damageType);

			}
		}
	}
}
```
## 得到敌人 `GetUnits()`

- 利用 `Physics.OverlapSphere` 获取一定范围内的所有敌人
- 这段代码获取了一定范围内的所有 碰撞器，并存储到一个 Collider 类型的数组身上
	- 当 `GetUnits()` 被当作一个数组使用时，实际上是在引用 **这个函数返回的数组**

```C#
private Collider[] GetUnits()
{
	return Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
   _enemyLayer, QueryTriggerInteraction.Ignore);
}
```
## 攻击动作的执行 `ComboActionExecute()`

- `_attackColdTime` 设置为 `_comboData` 的攻击冷却时间
- 播放 `_comboData.ActionName` 这个名字的攻击动画
- 调用计时器进行攻击冷却，冷却结束后执行 `ResetAttackInput`
	- 将 `_applyAttackInput` 设置为 `true`
- [[PlayerNewCombat#^8cafd5|PlayerNewCombat]] 中会去调用

```C#
protected void ComboActionExecute()
{
	_attackColdTime = _comboData.ActionColdTime;

	_animator.CrossFade(_comboData.ActionName, 0.025f, 0, 0f);

	GameTimeManager.MainInstance.TryGetTimer(_attackColdTime, ResetAttackInput);

	_applyAttackInput = false;
}
```
## 更新下一个动作 `SetComboData()`

- 可能有的动作没派生动作
- 在执行这些动作时有失误点击了右键
- 那么就可能造成空引用
- 这里判断一下

- `_comboData` 替换为 传入的 `comboData`

```C#
protected void SetComboData(CharacterComboData comboData)
{
	if (comboData != null)
	{
		_comboData = comboData;
	}
}
```
## 更新攻击输入 `ResetAttackInput()`

- 是否允许攻击输入 ——> 设置为 `true`

```C#
protected void ResetAttackInput()
{
	_applyAttackInput = true;
}
```

---
# 源代码

![[CharacterNewCombatBase.cs]]

---