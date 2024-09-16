---
tags:
  - 科学/Unity/ARPG/Enemy/BehaviorTree/AIAttackCommandCondition
created: 2024-08-14
---

---
# 关联知识点



---
# 说明

- 这个节点用于检测敌人是否指派了攻击指令
- 在行为树里 搭配 Inverter 进行判断

- 整个行为树做的都不是很好，建议就是看个大概，别深究

# 变量

- 获取组件

```C#
private EnemyCombatControl _enemyCombatControl;
```
# 函数

```C#
public override void OnAwake()
{
	_enemyCombatControl = GetComponent<EnemyCombatControl>();
}

public override TaskStatus OnUpdate()
{
	return (_enemyCombatControl.GetAttackCommand() ? TaskStatus.Success : TaskStatus.Failure);
}
```
## `OnUpdate()`

- 调用 [[EnemyCombatControl]] 中的 `GetAttackCommand()` 函数，获取脚本中的攻击指令
- 如果 对象 被指派了攻击指令，`_enemyCombatControl.GetAttackCommand()` 所获得的值为 `true`
	- 返回的值为 `TaskStatus.Success`
- 如果没有被指派攻击指令，那么返回的值就是 `TaskStatus.Failure`

- ![[AIAttackCommandCondition 行为树图片 自由移动.png]]
- 行为树进行自由移动时，返回的值为 `TaskStatus.Failure` 才能进行
	- 也就是 没有指派攻击指令

- ![[AIAttackCommandCondition 行为树图片 攻击行为.png]]
- 进行 攻击行为时，返回的值为 `TaskStatus.Success` 才能进行
	- 也就是 指派了攻击指令

```C#
public override TaskStatus OnUpdate()
{
	return (_enemyCombatControl.GetAttackCommand() ? TaskStatus.Success : TaskStatus.Failure);
}
```

---
# 源代码

![[AIAttackCommandCondition.cs]]

---