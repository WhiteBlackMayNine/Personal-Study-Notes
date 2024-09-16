---
tags:
  - 科学/Unity/ARPG/Enemy/BehaviorTree/AIAttackDistanceCondition
created: 2024-08-14
---

---
# 关联知识点

[[EnemyCombatControl]]

---
# 说明

- 这个节点来判断距离
- 变量的值需要在 行为树 Inspector 窗口中设置

- 整个行为树做的都不是很好，建议就是看个大概，别深究

# 变量

- 获取组件
- 设置 AI 的攻击距离

```C#
private EnemyCombatControl _enemyCombatControl;
[SerializeField] private float _attackDistance;
```
# 函数
## `OnAwake`

```C#
public override void OnAwake()
{
	_enemyCombatControl = GetComponent<EnemyCombatControl>();
}
```
## `OnUpdate`

- 如果玩家和敌人之间的距离小于 `_attackDistance` 所设置的距离
- 就返回 `TaskStatus.Success`

```C#
public override TaskStatus OnUpdate()
{
	return (DevelopmentToos.DistanceForTarget(EnemyManager.MainInstance.GetMainPlayer(),
		_enemyCombatControl.transform) > _attackDistance ? TaskStatus.Success : TaskStatus.Failure);
}
```

---
# 源代码

![[AIAttackDistanceCondition.cs]]

---