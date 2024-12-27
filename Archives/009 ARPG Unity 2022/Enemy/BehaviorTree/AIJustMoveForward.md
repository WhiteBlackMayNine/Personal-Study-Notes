---
tags:
  - 科学/Unity/ARPG/Enemy/BehaviorTree/AIJustMoveForward
created: 2024-08-14
---

---
# 关联知识点



---
# 说明

- 这个节点用来解决 AI 被指派攻击行为后 走向玩家
- 而 [[AIFreeMovementAction]] 是 AI 自由移动
	- 没有被指派攻击行为前，一直执行这个节点
- 而这个节点，只在 被指派攻击行为后 执行

- 整个行为树做的都不是很好，建议就是看个大概，别深究

# 变量

```C#
private EnemyMovementControl _enemyMovementControl;
```
# 函数
## `OnAwake()`

- 获取组件

```C#
public override void OnAwake()
{
	_enemyMovementControl = GetComponent<EnemyMovementControl>();
}
```
## `OnUpdate()`

- 被指派攻击后，如果玩家与敌人距离大于 1.5
- 调用 [[EnemyMovementControl#^d1e1d5|SetApplyMovement]] 函数，执行动作（朝着玩家走）
- 这个 1.5f 为 [[AIAttackDistanceCondition]] 中设置的 攻击距离
- 当小于 1.5f 时，由于执行这个节点，就代表已经被指派了攻击行为
	- 在行为树的逻辑中，就会去执行 [[AIComboAction]] 去进行攻击

```C#
public override TaskStatus OnUpdate()
{
	if (DevelopmentToos.DistanceForTarget(EnemyManager.MainInstance.GetMainPlayer(),
		_enemyMovementControl.transform) > 1.5f)
	{
		_enemyMovementControl.SetApplyMovement(true);
		_enemyMovementControl.SetAnimatorMovementValue(0f, 1f);
		return TaskStatus.Running;
	}

	return TaskStatus.Success;
}
```
# 总结

- ![[AIJustMoveForward 行为树图片.png]]
- 当 AI 被指派攻击行为时，执行这个节点，走向玩家
- 如果玩家与这个 AI 执行的距离少于1.5f（在`AIAttackDistanceCondition`设置的攻击距离）
	- 就去会执行 AIComboAction

---
# 源代码

![[AIJustMoveForward.cs]]

---