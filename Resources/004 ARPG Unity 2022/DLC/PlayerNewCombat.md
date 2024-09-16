---
tags:
  - 科学/Unity/ARPG/DLC/PlayerNewCombat
created: 2024-08-16
---

---
# 关联知识点



---
# 说明

- 这个是 DLC 动作脚本，跟老的很像
- 这里写的就是简化版了 老版的一些东西没有写上去
- 如果有需要可以往上写
- 需要把老的脚本移除了，不然报错
# 变量

- `private bool _hasLastComboAction`
	- 是否有上一个动作
	- 未进行赋值，默认未 `false`
# 生命周期函数

```C#
private void Update()
{
	PlayerAttackInput();
}
```
# 函数
## 角色攻击输入 `PlayerAttackInput()`

^8cafd5

- 按的是左键，左键代表执行 *正常连招*
	- 调用 `UpdateNextLAttackCombo()`
- 按的是右键，右键代表执行 *派生动作*
	- 先判断 当前连招`_comboData` 是否有派生动作
		- 如果有，调用 `UpdateNextRAttackCombo()`

```C#
private void PlayerAttackInput()
{
	if (_applyAttackInput)
	{
		if (GameInputManager.MainInstance.LAttack)
		{
			UpdateNextLAttackCombo();
		}
		else if (GameInputManager.MainInstance.RAttack)
		{
			if (!_comboData.HasChildCombo)
			{
				return;
			}
			UpdateNextRAttackCombo();
		}
	}
}
```
## 更新下一个动作（左键）`UpdateNextLAttackCombo()`

- 更新下一个动作
- 如果没有这个 `if` 语句 那么点击左键 第一个动作永远不会触发 点击左键后就直接赋值为第二个动作


- 如果没有这个 `if` 语句，就会直接 执行 `SetComboData()` 切换到下一个动画
- 第一次 左键的动作 **永远不会触发** 

- 第一个左键执行动画，是没有上一个动作的
- `_hasLastComboAction` 变量的值 初始时没有辅助，所有默认为 `false`‘
- 在第一次按下左键后执行`UpdateNextLAttackCombo()`，`if`语句不执行
- 调用 `ComboActionExecute()` 执行动画之后，再把 `_hasLastComboAction` 设置为 `true`
- 以后 点击左键 执行动画的时候，就会去走 `if` 语句，切换到下一个动画

```C#
private void UpdateNextLAttackCombo()
{
	if (_hasLastComboAction)
	{
		SetComboData(_comboData.NextCombo);
	}
	ComboActionExecute();
	_hasLastComboAction = true;
}
```
## 更新下一个动作（右键）`UpdateNextRAttackCombo()`

- 与 左键 的更新逻辑相同
- 只是 `SetComboData()` 的 连招 为 `ChildCombo` 子动作

```C#
private void UpdateNextRAttackCombo()
{
	if (_hasLastComboAction)
	{
		SetComboData(_comboData.ChildCombo);
	}
	ComboActionExecute();
	_hasLastComboAction = true;
}
```

---
# 源代码

![[PlayerNewCombat.cs]]

---