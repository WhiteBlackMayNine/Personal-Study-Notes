---
tags:
  - "#科学/Unity/ARPG/总结/攻击输入DLC"
created: 2024-08-15
---

---
# 关联知识点



---
# 说明

DLC 跟 原本的攻击输入 逻辑大致都是一样的

区别就是 招式数据文件 与 动画时间NewATK 的更改，让一个攻击动画中的不同攻击动作 可以造成不同的伤害
# 大致逻辑

玩家挂载脚本 [[PlayerNewCombat]]，利用 `PlayerAttackInput()` 函数 输入 左键右键后

调用 `UpdateNextLAttackCombo()` 更新连招，并在函数内部调用 `ComboActionExecute()` 执行动作

执行动作时，会去触发 [[CharacterNewCombatBase#^74059d|动画时间 NewATK]] ，触发之后， NewATK 调用 `TriggerDamage（）`

同时将传入 NewATK的 `index` 传入到 `TriggerDamage()`

`TriggerDamage()` 会调用接口 `IDamaged` 中的方法 `CharacterNormalDamaged`

传入 相应参数后，[[CharacterNewHealthBase#^df515d|接口实现]] 中会去调用 `CharacterHitAction()` 函数 

也就是子类重写的 `CharacterHitAction()` 受击函数

因为 `TriggerDamage（）` 中，会根据传入的 `index` 来去获取对应索引的受伤逻辑（受伤动画、伤害等）

所以调用接口时，传入的信息就是 对应所以的受伤逻辑信息

这样，就完成了 一个攻击动画中不同的攻击动作 可以造成不同的伤害

注意，这里只是大致完成了逻辑，如果需要，可以跟 老攻击输入 相融合


---
