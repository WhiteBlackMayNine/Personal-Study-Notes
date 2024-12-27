---
tags:
  - 科学/Unity/ARPG/Enemy/EnemyCombatControl
created: 2024-08-14
---

---
# 关联知识点



---
# 说明

- 继承 [[CharacterCombatBase]]
- 敌人的攻击行为由 *行为树* 指派实现的
	- AI 的攻击指令是由 AI管理器 指派的，非AI自身的行为
- 行为树脚本中 去获取这个脚本
	- 然后调用需要的函数
# 变量

```C#
[SerializeField] private bool _attackCommand;//攻击指令是否激活
public bool GetAttackCommand() => _attackCommand;//获取AI攻击指令状态
```
# 生命周期函数

- `_canAttackInput` 是否允许攻击输入 
	- 设置为 `true`
- 调用 `EnemyManager` 中 `GetMainPlayer()` 函数
	- 得到当前的玩家

```C#
private new void Start()
{
	_canAttackInput = true;
	_currentEnemy = EnemyManager.MainInstance.GetMainPlayer();
}

private void OnEnable()
{
	GameEventManager.MainInstance.AddEventListening<Transform>("敌人死亡",OnEnemyDead);            
}

private void OnDisable()
{
	GameEventManager.MainInstance.RemoveEvent<Transform>("敌人死亡", OnEnemyDead);
}
```
# 函数
## 是否允许接受攻击指令 `CheckAIAttackState()`

- 检测当前 AI 的自身状况 ***是否允许接收攻击指令***
- 如果当前处于
	- 受击
	- 格挡
	- 攻击
	- 被处决
	- 已经指派了攻击
- 返回 `false`

```C#
private bool CheckAIAttackState()
{
	if (_animator.AnimationAtTag("Hit"))
	{
		return false;
	}
	if (_animator.AnimationAtTag("Parry"))
	{
		return false;
	}
	if (_animator.AnimationAtTag("Attack"))
	{
		return false;
	}
	if (_animator.AnimationAtTag("FinishHit"))
	{
		return false;
	}
	if (_attackCommand)
	{
		return false;
	}

	return true;
}
```
## 攻击指令状态 `SetAttackCommand()`

^db6c93

- 设置攻击指令的状态
- 先调用 `CheckAIAttackState()` 函数，检测 ***是否允许接收攻击指令***
- 如果不可以 接受攻击指令 （`CheckAIAttackState()` 返回为 `false`）
	- 调用 `ResetAttackCommand()` 函数重置 攻击指令
- 可以接受，就将 `_attackCommand` 的值设置为传入的参数

- [[EnemyManager]] 中的协程会去调用

```C#
public void SetAttackCommand(bool command)
{
	//通过管理器 EnemyManager 调用这个函数
	//判断自身情况
	if (!CheckAIAttackState())
	{
		ResetAttackCommand();
		return;
	}

	_attackCommand = command;
}

```
## 重置攻击指令 `ResetAttackCommand()`

- 将 `_attackCommand` 设置为 `false`

```C#
private void ResetAttackCommand()
{
	_attackCommand = false;
}
```
## AI普通攻击 `AIBaseAttackInput()`

- 判断当前是否可以进行攻击输入

- 调用 [[CharacterCombatBase]] 中的 `ChangeComboData()` 与 `ExecuteComboAction()` 函数
- 设置 脚本对象上挂载的基础连招，然后执行动作

- [[AIComboAction#^0f23e9|AIComboAction中调用了这个函数]] 

```C#
public void AIBaseAttackInput()
{
	if (!_canAttackInput)
	{
		return;
	}
	ChangeComboData(_baseCombo);
	ExecuteComboAction();
}
```
## 处决时暂停攻击 `StopAllAction()`

^b8fea7

- 在 `EnemyManager` 中 `StopAllActiveUnit()` 函数会调用这个函数
- 两个函数都是用来完成 ——> 玩家处决敌人时，敌人停止攻击
- [[PlayerCombat]] 中，`CharacterFinishAttackInput()` 函数会去调用 `EnemyManager.MainInstance.StopAllActiveUnit()`
- 实现功能

- 如果 被指派了攻击指令
	- 重置攻击指令
- 如果当前动画处于 `Attack` 中（正在进行攻击）
	- 播放默认动画（待机动画）
	- 然后重置攻击指令

```C#
public void StopAllAction()
{
	if (_attackCommand)
	{
		ResetAttackCommand();
	}
	if (_animator.AnimationAtTag("Attack"))
	{
		_animator.Play("ldie", 0, 0f);
		ResetAttackCommand();
	}
}
```
## 敌人死亡 `OnEnemyDead()`

- 用来完成 敌人死亡后移除当前敌人
- 添加监听

- 如果死亡的敌人是自己
	- 重置攻击指令
	- 重置连招信息
	- 当前是否允许攻击输入 设置为 `false`

```C#
	private void OnEnemyDead(Transform enemy)
	{
		if(enemy == this.transform)
		{
			ResetAttackCommand();
			ResetComboInfo();
			_canAttackInput = false;
		}
	}
```
# 总结

- 敌人的攻击 由 行为树 和 `EnemyManager`中的协程 来完成的
- 在 行为树脚本中（`AIComboAction`）中，会去调用 `AIBaseAttackInput()` 函数进行 *基础攻击*
- `OnEnemyDead()` 添加到 *事件监听* 中，在 `PlayerCombo` 中也会将 *一个函数* 添加到 *事件监听* 中
	- 在 [[EnemyHealthControl]] 中，监听生命值，如果小于0，就呼叫 *这个事件*

- 在 `EnemyManager` 的协程中会去调用 `SetAttackCommand()` 函数
	- `enemyCombatControl.SetAttackCommand(true);`

---
# 源代码

![[EnemyCombatControl.cs]]

---