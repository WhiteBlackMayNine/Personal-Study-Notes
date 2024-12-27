---
tags:
  - 科学/Unity/ARPG/Enemy/BehaviorTree/AIComboAction
created: 2024-08-14
---

---
# 关联知识点



---
# 说明

- 写的攻击动作要在这里面执行

- 整个行为树做的都不是很好，建议就是看个大概，别深究

# 变量

```C#
private EnemyCombatControl _enemyCombatControl;
```
# 函数
## `OnAwake`

- 获取组件

```C#
_enemyCombatControl = GetComponent<EnemyCombatControl>();
```
## `OnUpdate`

^0f23e9

- 如果被指派了攻击指令
- 调用 [[EnemyCombatControl]] 中的 `AIBaseAttackInput()` 方法
- 执行攻击动作

```C#
public override TaskStatus OnUpdate()
{
	if (_enemyCombatControl.GetAttackCommand())
	{
		_enemyCombatControl.AIBaseAttackInput();
		return TaskStatus.Running;
	}

	return TaskStatus.Success;
}
```

---
# 源代码

![[AIComboAction.cs]]

---