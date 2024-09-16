---
tags:
  - 科学/Unity/ARPG/Enemy/EnemyHealthControl
created: 2024-08-13
---

---
# 关联知识点

[[CharacterHealthBase]]

---
# 生命周期函数

```C#
protected override void Awake()
{
	base.Awake();

	//利用 Instantiate 创建一个新的 
	//如果不这么做，就会导致多个敌人共用一个生命配置模板时 生命值、体力值 是相通的
	_characterHealthInfo = ScriptableObject.Instantiate(_healthInfo);

	EnemyManager.MainInstance.AddEnemyUnit(this.gameObject);
}
```

- `EnemyManager.MainInstance.AddEnemyUnit(this.gameObject);`
- EnemyHealthControl 是挂载到 敌人对象上的
- 在 [[EnemyManager]]  `Awake()` 中添加这个敌人
# 函数
## 角色受击 `CharacterHitAction`

- `_characterHealthInfo.StrengthFull` [[CharacterHealthInfo]] 中，判断体力值是否归0了
- `_characterHealthInfo.CurrentHP` 角色当前生命值
- *激活处决* 这个事件在 [[PlayerCombat]]

```C#
protected override void CharacterHitAction(float damage, string hitName, string parryName)
{
	if (_characterHealthInfo.StrengthFull && damage < 30f)
	{
		//格挡
		//如果敌人不在攻击状态下
		if (!animator.AnimationAtTag("Attack"))
		{
			animator.Play(parryName, 0, 0f);
			TakeDamage(damage, true);
			//播放音效
			GamePoolManager.MainInstance.TryGetOnePoolItem("BLOCKSound", this.transform.position, Quaternion.identity);


			//如果 _characterHealthInfo.StrengthFull = false
			//即体力值被清空 那么就需要通知玩家，现在可以进行处决
			if (!_characterHealthInfo.StrengthFull)
			{
				GameEventManager.MainInstance.CallEvent<bool>("激活处决", true);
			}
		}
	}
	else
	{
		//当生命值低于一定值时，那么就需要通知玩家，现在可以进行处决
		if (_characterHealthInfo.CurrentHP < 20f)
		{
			GameEventManager.MainInstance.CallEvent<bool>("激活处决", true);
		}
		//受击
		animator.Play(hitName, 0, 0f);
		//播放音效
		GamePoolManager.MainInstance.TryGetOnePoolItem("HITSound", this.transform.position, Quaternion.identity);
		TakeDamage(damage);
	}
}
```
## 造成伤害 `TakeDamage()`

^d820c9

- 如果当前生命值小于等于
- 调用事件 *敌人死亡*
- 调用 死亡动画 函数
- 在 `EnemyManager` 中，删除这个对象
- 最后将 [[EnemyMovementControl]] 脚本是否
	- 避免脚本内 调用 角色控制器 `.Move` 报错
	 
```C#
protected override void TakeDamage(float damage,bool hasParry = false)
{
	base.TakeDamage(damage, hasParry);
	if (_characterHealthInfo.CurrentHP <= 0)
	{
		GameEventManager.MainInstance.CallEvent("敌人死亡", transform);
		PlayDeadAnimation();
		EnemyManager.MainInstance.RemovedEnemyUnit(this.gameObject);
		this.gameObject.GetComponent<EnemyMovementControl>().enabled = false;
	}
}
```
## 死亡动画 `PlayDeadAnimation()`

- 重写基类的
- 但没有写什么额外内容

```C#
protected override void PlayDeadAnimation()
{
	base.PlayDeadAnimation();
	DevelopmentToos.WTF(this.gameObject.name + "已死亡");
}
```
# 总结

- 主要的功能都写在了基类
- 这里只是对 敌人对象 的一些逻辑进行单独处理
	- ~~玩家和敌人的操作肯定不一样的~~

---
# 源代码

![[EnemyHealthControl.cs]]

---