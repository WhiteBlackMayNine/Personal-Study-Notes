---
tags:
  - 科学/Unity/ARPG/Player/PlayerHealth
created: 2024-08-12
---

---
# 关联知识点

[[CharacterHealthBase]]

---
# 说明

- 继承 `CharacterHealthBase`
- 完成玩家的生命值信息相关的脚本
	- 比如 受伤、格挡
# 生命周期函数

```C#
protected override void Awake()
{
	base.Awake();
	_characterHealthInfo = ScriptableObject.Instantiate(_healthInfo);
}

protected override void Update()
{
	base.Update();
	PlayerParryInput();
}
```

- `_characterHealthInfo = ScriptableObject.Instantiate(_healthInfo);`
	- 这句代码就是创建对象自己的 [[CharacterHealthBase#^249d6e|生命值信息文件]]

# 函数
## 角色受击逻辑 `CharacterHitAction()`

- 用于处理 当玩家被攻击后，执行相应的受击/格挡动画

- 如果玩家正在格挡，并且伤害值小于一定值（如果超过，就代表是一个破防攻击，直接扣除生命值）
	- 格挡 ——> [[GameInputManager]] 中设置为 一直按空格
- 那么就去播放格挡动画，并在 [[GamePoolManager]] 中调用一个对象，播放格挡声音
- 否则就去播放受击动画，然后播放受击声音
- 最后调用 `TakeDamage()`

- `TakeDamage()` 传入两个参数
- 第一个为 `damage` 伤害值
- 第二个为 `animator.GetBool(AnimationID.ParryID)`
	- 即 是否进行格挡

- 在 [[CharacterHealthInfo]] 中 `Damage()` 函数有两个参数
	- 第一个为 `damage` 伤害 第二个为 `bool hasParry = false`
	- 内部逻辑会根据 `hasParry` 来判断是否进行了防御
	- 再去造成相应的伤害
	
```C#
protected override void CharacterHitAction(float damage, string hitName, string parryName)
{
	if (animator.AnimationAtTag("Finish"))//如果玩家在处决敌人，那么不接受任何伤害信息
	{
		return;
	}


	if (animator.GetBool(AnimationID.ParryID) && damage < 30f)
	{
		animator.Play(parryName, 0, 0f);
		GamePoolManager.MainInstance.TryGetOnePoolItem("BLOCKSound", transform.position,Quaternion.identity);
	}
	else
	{
		animator.Play(hitName, 0, 0f);
		GamePoolManager.MainInstance.TryGetOnePoolItem("HITSound", transform.position, Quaternion.identity);

	}
	TakeDamage(damage, animator.GetBool(AnimationID.ParryID));
}
```
## 格挡输入 `PlayerParryInput()`

- 核心代码为 `animator.SetBool(AnimationID.ParryID, GameInputManager.MainInstance.Parry)`
- 如果一直按着空格，那么就把 `AnimationID.ParryID` 设置为 `true`
	- `AnimationID.ParryID` 在 `CharacterHitAction()` 用于判断是否进行格挡

- 如果处于受击动画且当前动画的播放进度小于0.35f（35%）
	- 返回
- 如果当前处于被处决动画中
	- 返回

```C#
private void PlayerParryInput()
{
	if (animator.AnimationAtTag("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.35f)
	{
		return;//受击后，动画播放超过一定时间后才能允许格挡
	}
	if (animator.AnimationAtTag("FinishHit"))
	{
		return;
	}
	animator.SetBool(AnimationID.ParryID, GameInputManager.MainInstance.Parry);//按住空格为true
}
```
# 总结

- 主要功能都写在了基类里面
- 这里只是把 玩家 独有的内容进行重写
	- 比如说格挡输入

---
# 源代码

![[PlayerHealth.cs]]

---