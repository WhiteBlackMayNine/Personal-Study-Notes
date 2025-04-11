---
tags:
  - 科学/Unity/ARPG/Enemy/EnemyMovementControl
created: 2024-08-14
---

---
# 关联知识点

[[AIJustMoveForward]] [[AIFreeMovementAction]] [[CharacterMoveBase]]

---
# 说明

- 敌人的移动是通过 行为树 来实现的

- 在 Animator 中，敌人的移动动画是使用混合树控制的八方向
	- ![[EnemyMovementControl 敌人 Animator八方向移动.png]]
	- ![[EnemyMovementControl 敌人Animator八方向移动 Inspector窗口.png]]
# 变量

- `_applyMovement`
	- 是否可以进行移动
# 生命周期函数

```C#
protected override void Start()
{
	base.Start();
	SetApplyMovement(true);
}


protected override void Update()
{
	base.Update();
	LookTargetDirection();
	DrawDirection();
}
```
# 函数
## 看着目标 `LookTargetDirection()`

- 敌人在移动的时候才去看着玩家
- [[EnemyManager]] 里的 `GetMainPlayer()` 方法获取玩家位置

```C#
private void LookTargetDirection()
{
	//只希望AI在移动的时候才看着玩家的方向
	//通过动画的 Tag 来控制 
	if (_animator.AnimationAtTag("Motion"))
	{
		transform.Look(EnemyManager.MainInstance.GetMainPlayer().position, 500f);
	}
}
```
## 是否允许移动 `SetApplyMovement()`

^d1e1d5

- 用于外部调用
- 外部调控能否进行移动

```C#
public void SetApplyMovement(bool apply)
{
	_applyMovement = apply;
}
```
## 设置动画移动 `SetAnimatorMovementValue()`

^375757

- `HasInputID` 在 Animator 中，敌人从静止状态过渡到移动混合树时，Conditions 为 `HasInput = true`
- 外部传入 `horizontal` `vertical` 分别对应着 上方图片中 Inspector 窗口上的两个参数
- 修改这两个参数的值，让敌人执行不同的动作
 
```C#
public void SetAnimatorMovementValue(float horizontal, float vertical)
{
	if (_applyMovement)
	{
		_animator.SetBool(AnimationID.HasInputID, true);
		_animator.SetFloat(AnimationID.HorizontalID, horizontal, 0.2f, Time.deltaTime);
		_animator.SetFloat(AnimationID.VerticalID, vertical, 0.2f, Time.deltaTime);
	}
	else
	{
		_animator.SetBool(AnimationID.HasInputID, false);
		_animator.SetFloat(AnimationID.HorizontalID, 0f, 0.2f, Time.deltaTime);
		_animator.SetFloat(AnimationID.VerticalID, 0f, 0.2f, Time.deltaTime);
	}
}
```
## 是否激活角色控制器 `EnableCharacterController()`

^819f32

- [[EnemyManager]] 中，删除敌人时（`RemovedEnemyUnit`） 调用
- 失活 角色控制器

```C#
public void EnableCharacterController(bool enable)
{
	_characterController.enabled = enable;
}
```
# 总结

- 敌人的具体移动需要使用 行为树 来进行实现
- 这个脚本主要是 *提供函数方法* 供 *行为树脚本* 调用，完成移动

---
# 源代码

![[EnemyMovementControl.cs]]

---