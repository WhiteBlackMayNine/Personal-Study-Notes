---
tags:
  - "#科学/Unity/ARPG/Base/CharacterCombatBase"
created: 2024-08-15
---

---
# 关联知识点



---
# 说明

- 玩家和AI他们的攻击事件触发、伤害触发也是相同的
- 基础组合技也是差不多的
- 组合技信息也是相似的
- 把差不多的都提取到基类里面
# 变量

```C#
protected Animator _animator;

#region 攻击/受击相关

[SerializeField, Header("角色连招表")] protected CharacterComboSO _baseCombo;
[SerializeField, Header("角色变招表")] protected CharacterComboSO _changeCombo;
protected CharacterComboSO _currentCombo;//当前使用的连招

protected int _currentComboIndex;//当前连招的索引值
protected float _maxColdTime;//连招的最大冷却时间 
protected bool _canAttackInput;//是否允许进行攻击输入
protected int _hitIndex;//受击索引值

protected int _currentComboCount;//当前连招数量

#endregion

#region 敌人检测
//一些函数需要这个变量

[SerializeField, Header("当前敌人")] protected Transform _currentEnemy;//当前敌人位置 用于敌人检测中

#endregion

#region 处决 暗杀

[SerializeField, Header("处决")] protected CharacterComboSO _finishCombo;
[SerializeField, Header("暗杀")] protected CharacterComboSO _assassinationCombo;

//处决/暗杀  防止在普通攻击为结算前进行处决时抛出的 超出索引值 问题
protected int _FinishComboIndex;//处决单独用这个处理，各干个
protected bool _canFinish;//是否可以进行处决 比如当生命值/体力值低于一定值时

#endregion
```
## 攻击/受击相关

- `_maxColdTime` 
	- 其实就是这个招式的冷却时间，过了冷却时间才能执行下一个动作
	- 防止出现动画、函数不匹配 比如动画还没结束但函数已经触发，变量改变的情况
- `_currentComboCount`
	- 当前进行了几个基础连招，用于子类 [[PlayerCombat]] 进行 变招、处决
## 处决 暗杀

- `_FinishComboIndex`
	- 如果使用的是 `_currentComboIndex`
	- 那么执行处决/暗杀相关的动作时，再去使用普通连招
	- 索引值很有可能就超过了最大范围
		- 或者出现什么离奇的错误
	- 因此这里给处决单独设置了一个索引值
# 生命周期函数

```C#
protected virtual void Awake()
{
	_animator = this.GetComponent<Animator>();
}

protected void Start()
{
	_canAttackInput = true;
	_currentCombo = _baseCombo;
}

protected virtual void Update()
{
	LookTargetOnAttack();//攻击时看着敌人
	MatchPosition();//进行位置匹配
	OnEndAttack();
}
```
# 函数
## 位置同步
### `MatchPosition()`

^1eae54

- 如果当前敌人 `_currentEnemy` 为空，返回
- 如果 `_animator` 不存在 为空，返回

- `_animator` 的动画标签为 `Attack`
	- 如果当前动画执行程度大于 0.35f （35%），返回
	- 如果 挂载脚本对象 与 当前敌人 之间的距离大于 2f，返回
		- `_currentEnemy` 为当前敌人
		- `transform` 为挂载脚本对象
	- 如果 `_animator` 不在匹配状态 且 不在动画过渡中
		- 进行位置匹配

- `_animator.MatchTarget` 方法 参数
- `_currentEnemy.position + (-transform.forward * _currentCombo.TryGetComboPositionOffset(_currentComboIndex))`
	- 匹配的目标位置
	- 为 当前敌人的位置，往后移 *一定偏差量*
		-  `_currentCombo.TryGetComboPositionOffset` *具体偏差量*为 招式动画中 设置
- `Quaternion.identity`
	- 匹配的目标旋转
	- 这里使用的是恒等四元数，不旋转
- ` AvatarTarget.Body`
	- 应用动画的目标部位
	- 这里选定的部位为 `Body`
- `new MatchTargetWeightMask(Vector3.one, 0f)`
	- 权重掩码
	- 用于控制不同部位的匹配程度
	- 这里创建了一个新的 `MatchTargetWeightMask`实例
	- 其中 `Vector3.one` 表示所有部位都有相同的权重
	- 0f 表示没有权重
	- 这代表 动画将在所有部位上均匀地应用
- 0
	- 开始匹配的时间偏移量，以秒为单位
	- 这里设置为0，表示动画立即开始匹配
- 0.35f
	- 匹配持续时间，以秒为单位、
	- 这里设置为0.35秒，表示动画将持续匹配 0.35 秒

```C#
protected virtual void MatchPosition()
{
	if (_currentEnemy == null)
	{
		return;
	}

	if (!_animator)
	{
		return;
	}

	//如果敌人在播放死亡动画时 突然往前移动了
	//就是因为在播放攻击动画时，被玩家给打死了，触发了攻击事件，进行了位置匹配
	//可以把 _animator.MatchTarget 给注释了

	if (_animator.AnimationAtTag("Attack"))
	{
		//对动画时间归一化 0 — 1 的一个值
		var timer = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

		//播放进度大约 35% 返回
		if (timer > 0.35f)
		{
			return;
		}

		//距离过大 返回
		if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 2f)
		{
			return;
		}

		//不在匹配状态  同时不在过渡中
		if (!_animator.isMatchingTarget && !_animator.IsInTransition(0))
		{
			_animator.MatchTarget(_currentEnemy.position + (-transform.forward * _currentCombo.TryGetComboPositionOffset(_currentComboIndex)),
			Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), 0, 0.35f);
		}
	}
}
```
### `RunningMatch()`

^468c75

- 用于 在子类 [[PlayerCombat]] 重写的 `MatchPosition()` 中
- 完成 处决 / 暗杀 的位置匹配

- 使用的参数与 `MatchPosition()` 相同
- 招式、匹配开始时间、匹配持续时间（`endTime`）可以由外部传入

```C#
protected void RunningMatch(CharacterComboSO comboSO, int index, float startTime = 0f,
	float endTime = 0.01f)
{
	//不在匹配状态  同时不在过渡中
	if (!_animator.isMatchingTarget && !_animator.IsInTransition(0))
	{
		_animator.MatchTarget(_currentEnemy.position + (-transform.forward * comboSO.TryGetComboPositionOffset(index)), Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), startTime, endTime);
	}
}
```
## 动画事件 `ATK()`

^054214

- 这个函数用于触发动画事件
- ![[CharacterComboBase 动画事件ATK.png]]

- 触发动画事件后
- 调用 `TriggerDamager()` 造成伤害
- 调用 `UpdateHitIndex()` 更新受伤索引值
- 调用 [[GamePoolManager]] 播放声音

```C#
protected void ATK()
{
	TriggerDamager();
	UpdateHitIndex();
	GamePoolManager.MainInstance.TryGetOnePoolItem("ATKSound", this.transform.position, Quaternion.identity);
}
```
## 伤害触发 `TriggerDamager()`

- 当前敌人 为空
	- 返回

- `DevelopmentToos.DirectionForTarget(transform, _currentEnemy)`
	- 返回值为 `(self.position - target.position).normalized`
	- 计算两个物体之间的方向向量
		- 然后归一化，从而得到一个单位向量
	- 这个单位向量表示从自身物体指向目标物体的方向
- 将 当前位置的 `transform.forward` 与 所得到的单位向量 进行点积 `Vector3.Dot` 处理
	- 确保敌人处于可触发伤害的距离和角度

- 判断距离
	- `DevelopmentToos.DistanceForTarget(_currentEnemy, transform)`
		- 返回值为 两者之间的距离
	- 如果大于 1.3f 
		- 返回

- 如果 处于 动画标签为 `Attack` 的动画下
	- 呼叫事件，并传入相应参数
	- `_currentCombo.TryGetComboDamage(_currentComboIndex)`
		- 当前连招表 `_currentCombo` 正在执行的连招 `_currentComboIndex` 的造成伤害 `TryGetComboDamage`
	- `_currentCombo.TryGetOneHitAction(_currentComboIndex, _hitIndex)`
		- 当前连招表 `_currentCombo` 正在执行的连招 `_currentComboIndex` 的第几个 `_hitIndex`受击动画 `TryGetOneHitAction`
	- `_currentCombo.TryGetOneParryAction(_currentComboIndex, _hitIndex)`
		- 当前连招表 `_currentCombo` 正在执行的连招 `_currentComboIndex` 的第几个 `_hitIndex`格挡动画 `TryGetOneParryAction`
	- `transform`
		- 当前位置
	- `_currentEnemy`
		- 当前敌人（也就是当前的被攻击者）

- *触发伤害* 、*生成伤害* 事件在 [[CharacterHealthBase]] 脚本中

```C#
protected void TriggerDamager()
{
	//要确保有目标
	if (_currentEnemy == null)
	{
		return;
	}
	//要确保敌人处于我们可触发伤害的距离和角度
	//这个是角度
	if (Vector3.Dot(transform.forward, DevelopmentToos.DirectionForTarget(transform, _currentEnemy)) < 0.9f)
	{
		return;
	}
	//这个是距离
	if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 1.3f)
	{
		return;
	}
	//去呼叫事件中，调用触发伤害这个函数
	if (_animator.AnimationAtTag("Attack"))
	{
		GameEventManager.MainInstance.CallEvent("触发伤害", _currentCombo.TryGetComboDamage(_currentComboIndex),_currentCombo.TryGetOneHitAction(_currentComboIndex, _hitIndex),	_currentCombo.TryGetOneParryAction(_currentComboIndex, _hitIndex),transform, _currentEnemy);
		  //伤害值 受伤动画名 格挡动画名 攻击者 当前被攻击者
		  //这个写完基本就不用动了
		  //这里传入的受伤动画是单个片段
	}
	else
	{
		//这里就是处决、暗杀等其他动作  需要调用另外的函数
		//处决是一个完整的被处决动作，导致在动作期间可能会有多次触发伤害

		GameEventManager.MainInstance.CallEvent("生成伤害", _finishCombo.TryGetComboDamage(_FinishComboIndex)
			, _currentEnemy);
	}
}
```
## 攻击看着目标 `LookTargetOnAttack()`

- 如果当前没有敌人 或者 当前 玩家与目标之间距离过大
	- 返回

- 当前处于的动画的标签为 `Attack`，且当前的播放进度小于 0.5f （50%）
	- 看向目标

```C#
protected void LookTargetOnAttack()
{
	//要确保有一个敌人目标
	if (_currentEnemy == null)
	{
		return;
	}
	//确保玩家与目标之间的距离要小于一定距离
	if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 5f)
	{
		return;
	}
	//获取当前动画状态的详细信息
	if (_animator.AnimationAtTag("Attack") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
	{
		transform.Look(_currentEnemy.position, 1000f);
	}
}
```
## 基础攻击 `CharacterBaseAttackInput()`

- 让子类去重写这个函数
## 更新连招动画信息
### `UpdataComboInfo()`

- 更新连招信息
	- 当前连招的索引
	- 冷却事件
	- 是否可以进行攻击输入
- 增加 `_currentComboIndex`
- 如果 `_currentComboIndex` 已经到了当前连招表的最大数量
	- 执行到最后一个动作
- `_currentComboIndex` 归0，从头开始

```C#
protected virtual void UpdataComboInfo()
{
	_currentComboIndex++;

	if (_currentComboIndex == _currentCombo.TryGetComboMaxCount())
	{
		//如果执行到了最后一个动作
		_currentComboIndex = 0;
	}
	_maxColdTime = 0f;
	_canAttackInput = true;
}
```
### `UpdateHitIndex()`

- 播放受伤动画主要是利用 `.Play()`
	- 传进动画名字字符串进行播放
- 但 `_hitIndex` 在攻击时不去累计
	- 可能会去报空引用和超出索引的错误
		- 前者是因为 [[CharacterComboSO]] 所创建的文件中
			- Combo Hit 列表中没有这个索引值所对应的受伤动
		- 后者是因为 当前攻击动作还没有结束，但 `_currentComboIndex++` 已经执行
			- 导致 `_hitIndex++` 超出索引
			- 需要改连招的冷却时间

- 在 *攻击事件ATK* 中更新
- 即使没有攻击到敌人也要去更新
- 不然会出现  *受击动画* 和 *攻击动画* 不匹配
 
```C#
protected void UpdateHitIndex()
{
	_hitIndex++;
	if (_hitIndex == _currentCombo.TryGetHitAndParryMaxCount(_currentComboIndex))
	{
		_hitIndex = 0;
	}
}
```
## 重置连招动画信息
### `ResetComboInfo()`

^6ba9f6

- 重置 `_currentComboIndex` 当前连招索引
- 最大冷却时间
- `_hitIndex` 受伤索引

- 用于 `ChangeComboData()` 改变组合技

```C#
	protected void ResetComboInfo()
	{
		_currentComboIndex = 0;
		_maxColdTime = 0f;
		_hitIndex = 0;
	}
```
### `OnEndAttack()`

- 如果玩家进行了移动
- 那么就重置连招信息

- 简单来说就是
	- 移动了，就要从头开打
- 如果不希望有这个功能，注释即可

```C#
protected void OnEndAttack()
{
	//移动后从头开始进行攻击
	if (_animator.AnimationAtTag("Motion") && _canAttackInput)
	{
		ResetComboInfo();
	}
}
```
## 改变组合技 `ChangeComboData()`

^8ca4c7

- 将 `_currentCombo` 改变为 传入的连招表

```C#
protected void ChangeComboData(CharacterComboSO characterComboSO)
{
	if (_currentCombo != characterComboSO)
	{
		_currentCombo = characterComboSO;
		ResetComboInfo();
	}
}
```
## 执行动作 `ExecuteComboAction()`

^2f5c94

- 供子类去重写，完成子类独有的需求

- 判断当前连招是否为基础连招
	- 如果是，让 `_currentComboCount` 当前连招数量 加一
- `_currentComboCount` 在 子类中使用

- 执行一次 `ExecuteComboAction()`，就是执行一个动作
- `_hitIndex = 0` 每个动作的受伤索引都是不同的
- 在这里 重新归0，防止 上一个动作的受伤索引 对 这一个（即将进行的动作）产生影响

- 如果当前的动作索引 等于 连招的于最大动作数量
	- 执行到最后一个动作
- `_currentComboIndex = 0` 去执行第一个动作

- 获取最大的冷却时间
	- 这个招式的冷却时间

- `_animator.CrossFadeInFixedTime` 平滑地切换到一个新的动画状态
- `_currentCombo.TryGetOneComboAction(_currentComboIndex)` 为要切换的动画
- `0.15f` 过渡动画的时间
	- 过渡动画将在0.15秒内完成
- `0` 延迟时间
	- 开始过渡动画之前等待的时间
- `0` 混合时间
	- 表示过渡动画的持续时间内，新动画和旧动画之间的混合程度
	- 混合时间越长，新旧动画之间的过渡就越平滑

- 调用 [[GameTimerManager]] 一个计时器，将 `UpdataComboInfo` 传进去
- 把 `_canAttackInput` 设置为 `false`
- 当 `_maxColdTime` 秒过去后，就会调用 `UpdataComboInfo`
- 更新连招信息

```C#
protected virtual void ExecuteComboAction()
{
	_currentComboCount += (_currentCombo == _baseCombo) ? 1 : 0;
	_hitIndex = 0;
	if (_currentComboIndex == _currentCombo.TryGetComboMaxCount())
	{
		//如果执行到了最后一个动作
		_currentComboIndex = 0;
	}

	_maxColdTime = _currentCombo.TryGetComboColdTime(_currentComboIndex);
	_animator.CrossFadeInFixedTime(_currentCombo.TryGetOneComboAction(_currentComboIndex), 0.15f, 0, 0);
	GameTimeManager.MainInstance.TryGetTimer(_maxColdTime, UpdataComboInfo);
	_canAttackInput = false;
}
```
# 总结

- 敌人 和 玩家 连招攻击，都有的函数，写在这里

- 动画事件 `ATK` 用于配置动画时，写在动画的 Event 中调用

- 其他的函数，子类去重写，或者子类在自己的函数中进行调用 去 完成功能

---
# 源代码

![[CharacterCombatBase.cs]]

---