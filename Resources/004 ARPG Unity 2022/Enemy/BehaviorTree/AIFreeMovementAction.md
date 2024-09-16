---
tags:
  - 科学/Unity/ARPG/Enemy/BehaviorTree/AIFreeMovementAction
created: 2024-08-14
---

---
# 关联知识点

[[AIJustMoveForward]]

---
# 说明

- 敌人自由移动行为
- 行为树 的一个脚本

- 在敌人对象没有被玩家攻击 或者 没有去执行其他行为
- 就会一直运行这个节点 一直移动

- 整个行为树做的都不是很好，建议就是看个大概，别深究

# 变量

- `_enemyMovementControl` `_enemyCombatControl` 获取组件 
- `DistranceForTarget()` 用于 `Update` 中判断距离
- `_actionIndex` `_lastActionIndex` `_actionTimer` 用在 `UpdateActionIndex()` 中更新随机动作索引
- `_actionTimer` 也会在 `UpdateFreeActionTime()` 中去更新持续时间

```C#
private EnemyMovementControl _enemyMovementControl;
private EnemyCombatControl _enemyCombatControl;

//与玩家的距离
private float DistranceForTarget() => DevelopmentToos.DistanceForTarget(EnemyManager.MainInstance.GetMainPlayer(),enemyMovementControl.transform);

private int _actionIndex;//动作索引，不同的值代表不同的动作
private int _lastActionIndex;//上一次执行的动作
private float _actionTimer;//动作时间，这个动作要维持多久  可以是固定的也可以是随机的
```
# 函数
## `OnAwake()`



```C#
public override void OnAwake()
{
	_enemyMovementControl = this.GetComponent<EnemyMovementControl>();
	_enemyCombatControl = this.GetComponent<EnemyCombatControl>();
	_lastActionIndex = _actionIndex;
	_actionTimer = 1f;
}
```
## 更新随机动作的动作索引 `UpdateActionIndex()`


- `_lastActionIndex` 用于保存未随机赋值前的 `_actionIndex`
- 然后给 `_actionIndex` 和 `_actionTimer` 随机取一个值
- 判断 `_actionIndex` 是否 等于 `_lastActionIndex`
	- 相当于 是否等于 未随机赋值前的 `_actionIndex`
- 如果相当那么就再随机取一次
	- 防止随机到同一个动作
	- 但如果两次都是一个动作就去执行
- 判断 `_actionIndex` 的索引是否为 特殊动作 （自己设置的）
	- 如果是，那么把时间修改一下
	- 或者是去做一些特殊处理

```C#
private void UpdateActionIndex()
{
	//保存一下
	_lastActionIndex = _actionIndex;

	//更新动作索引  注意 Random 左包含右不包含
	_actionIndex = Random.Range(0, 5);

	//随机时间
	_actionTimer = Random.Range(1f, 3f);

	//如果更新后的动作索引等于上一次的动作索引
	if (_actionIndex == _lastActionIndex)
	{
		//再随机一次
		_actionIndex = Random.Range(0, 5);
	}
	if (_actionIndex == 3 || _actionIndex == 4)
	{
		//对特殊动作的时间进行修改
		_actionTimer = 1.5f;
	}
}
```
## 随机动作执行 `FreeMovement()`

 - 利用 `switch` 判断 `_actionIndex` 的值为多少
 - 根据 `_actionIndex` 的值，去调用 [[EnemyMovementControl#^375757|SetAnimatorMovementValue 函数]] 执行动作

```C#
private void FreeMovement()
{
	switch (_actionIndex)
	{
		case 0:  //假设为 往左移动
			_enemyMovementControl.SetAnimatorMovementValue(-1f, 0f);
			break;
		case 1:  // 往右移动
			_enemyMovementControl.SetAnimatorMovementValue(1f, 0f);
			break;
		case 2:
			_enemyMovementControl.SetAnimatorMovementValue(0f, 0f);
			break;
		case 3:
			_enemyMovementControl.SetAnimatorMovementValue(-1f, 1f);
			break;
		case 4:
			_enemyMovementControl.SetAnimatorMovementValue(1f, 1f);
			break;
			//更多动作以此类推
	}
}
```
## 更新随机动作持续时间 `UpdateFreeActionTime()`

- `_actionTimer` 大于0，说明动作还在执行
- 小于0，动作执行完毕，去 *更新动作索引* 调用 `UpdateActionIndex()`

```C#
private void UpdateFreeActionTime()
{
	if (_actionTimer > 0f)
	{ 
		//更新移动时间
		_actionTimer -= Time.deltaTime;
	}

	if (_actionTimer <= 0f)
	{
		//移动时间小于等于0时  更新动作索引
		UpdateActionIndex();
	}
}
```
## `OnUpdate()`

- 调用 [[EnemyCombatControl]] 中的 `GetAttackCommand()` 方法，判断自身目前是否被指派攻击行为
	- 如果被指派，就不去执行这个节点，并返回 `TaskStatus.Success`

- 判断 `DistranceForTarget()` 的值（敌人与玩家之间的距离）
	- 如果大于 8 
		- 就朝着玩家走（接近玩家）
	- 如果小于 8 但大于 3（0.1 为偏差值）
		- 就去自由移动，前后左右的走
		- 更新动作时间
	- 如果小于 3 
		- 距离玩家过近，往后走
- 最后逻辑执行完毕，返回 `TaskStatus.Running`

```C#
public override TaskStatus OnUpdate()
{
	//判断自身是否被指派攻击
	if (!_enemyCombatControl.GetAttackCommand())
	{
		//这里去做 这个节点 的行为逻辑
		//距离大于8 就往前（冲着玩家）走
		//距离小于8但大于3 随机移动
		//距离在3一下 往后移动
		if (DistranceForTarget() > 8f)
		{
			_enemyMovementControl.SetAnimatorMovementValue(0, 1);
		}
		else if (DistranceForTarget() < 8f - 0.1f && DistranceForTarget() > 3f + 0.1f)
		{
			//在一定值内，左右移动
			//0.1f 为一个偏差值
			FreeMovement();
			UpdateFreeActionTime();
		}
		else//距离太近时，往后移动
		{
			_enemyMovementControl.SetAnimatorMovementValue(0f, -1f);//往后移动
		}

		return TaskStatus.Running;
	}
	else
	{
		//这里去做 退出逻辑
		DevelopmentToos.WTF(this.gameObject.name + "已被指派攻击行为");
	}
	return TaskStatus.Success;
}
```
# 总结

- 核心方法为 `OnUpdate()`
- 判断敌人与玩家之间的距离，不同的距离去执行不同的逻辑
	- 更新随机动作索引 `UpdateActionIndex()`
	- 更新随机动作时间 `UpdateFreeActionTime()`
	- 执行随机动作 `FreeMovement()`
- ![[AIFreeMovementAction 行为树图片.png]]

---
# 源代码

![[AIFreeMovementAction.cs]]

---