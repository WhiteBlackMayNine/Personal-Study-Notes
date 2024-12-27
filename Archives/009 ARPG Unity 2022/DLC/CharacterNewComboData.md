---
tags:
  - 科学/Unity/ARPG/DLC/CharacterNewComboData
created: 2024-08-16
---

---
# 关联知识点



---
# 说明

- 不像老攻击输入那样，有分离数据和实例数据（连招和连招表）
- 这里只有连招，通过在连招上挂载下一个连招，完成类型连招表的功能
- 也可以直接创建一个连招表，然后通过 老攻击输入 那样，通过连招表来完成
- 但代码上需要一些改变，新攻击输入 的 动画事件ATK 是需要传入参数的
# 类 ComboDamageInfo

- 记载的是 受伤相关的参数
	- 伤害类型、伤害、受击动画名字、格挡名字
- 在变量中有一个 ComboDamageInfo 类型的列表 `_comboDamageInfo`
	- 就是用来存储 这个类的对象

- 一个攻击动画，有三个不同的攻击动作（假设分别为 A B C），且希望这三者所对应的受击、伤害都不相同
- 那么就可以在 列表`_comboDamageInfo`添加三个元素，分别对应着（列表的）索引 0，1，2
- 在编辑动画的时候，添加 Event 就可以将 `Int` 栏 写上对应的索引 0，1，2
- 在执行 动画事件ATK 时，会通过 `Int` 的值，去元素 `_comboDamageInfo` 中对应索引的 伤害元素
	- 比如 `Int` 为 1，那么执行动画事件的时候，就有去找在 `_comboDamageInfo` 中索引为 1 的元素 中所对应的信息
- ![[ARPG DLC 新攻击输入 CharacterNewComboData.png]]

```C#
public class ComboDamageInfo
{
	//伤害，受伤动画名，格挡名，还可以加一个伤害类型
	public DamageType _damageType;
	public float Damage;
	public string HitName;
	public string ParryName;
}
```
# 变量

```C#
[SerializeField] private string _actionName;
[SerializeField] private List<ComboDamageInfo> _comboDamageInfo;
[SerializeField] private float _actionColdTime;
[SerializeField] private CharacterComboData _nextCombo;
[SerializeField] private CharacterComboData _childCombo;
```

- `_actionName` 要播放的动作名字
- `_comboDamageInfo` ComboDamageInfo 类型的列表 ^d50fcb
	- 用来存储 连招上不同攻击动作的 所需要的 受击相关信息
- `_actionColdTime` 动作的冷却时间，衔接下一段动作前需要的等待时间
- `_nextCombo` 下一个动作 ，执行完当前动作后的下一个要执行的动作
- `_childCombo` 子动作，就是变招动作
# 属性

- 供外部调用

```C#
//方便外部作为判断条件来判断是否有子动作，或者说是派生动作
public bool HasChildCombo
{
	get
	{
		return _childCombo != null;
	}
}

public string ActionName
{
	get
	{
		return _actionName;
	}
}

public List<ComboDamageInfo> ComboDamageInfos
{
	get
	{
		return _comboDamageInfo;
	}
}

public float ActionColdTime
{
	get
	{
		return _actionColdTime;
	}
}

public CharacterComboData NextCombo
{
	get
	{
		return _nextCombo;
	}
}

public CharacterComboData ChildCombo
{
	get
	{
		return _childCombo;
	}
}
```

---
# 源代码

![[CharacterNewComboData.cs]]

---