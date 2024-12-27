---
tags:
  - "#科学/Unity/ARPG/总结/敌人AI总结"
created: 2024-08-15
---

---
# 关联知识点



---
# 说明

敌人不能像玩家一样，去通过 按键 来控制状态机的切换 ~~敌人又没有键盘~~
想要敌人动起来，就需要使用 行为树 （Behavior Tree） 来控制敌人的行动
# 行为树

行为树节点分别为 [[AIAttackCommandCondition]] [[AIAttackDistanceCondition]] [[AIJustMoveForward]][[AIFreeMovementAction]]  [[AIComboAction]]

五个节点内部会去调用 [[EnemyCombatControl]] [[EnemyMovementControl]] 里面的函数去完成逻辑判断

在 [[EnemyManager]] 中会利用 *协程* 派发攻击指令

- 整个行为树做的都不是很好，建议就是看个大概，别深究

---
