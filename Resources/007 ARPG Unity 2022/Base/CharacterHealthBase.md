---
tags:
  - "#科学/Unity/ARPG/Base/CharacterHealthBase"
created: 2024-08-12
---

---
# 关联知识点

[[IHealth]]

---
# 说明

- 用来控制 角色 的生命值信息相关的内容
- 包括且不限于  受伤函数、格挡、看向、造成伤害
# 变量

```C#
protected Animator animator;

protected Transform _currentAttack;//当前的攻击者，也就是谁打的

[SerializeField, Header("角色生命值模板")] protected CharacterHealthInfo _healthInfo;

protected CharacterHealthInfo _characterHealthInfo;
```

- `_healthInfo` 与 `_characterHealthInfo` ^249d6e
- 因为本项目是使用 ScriptableObject 创建 资产文件 来控制生命值信息的
- 如果多个对象同时使用一个 资产文件 ，那么他们的生命值信息就是共享的
	- 其中一个对象 扣除了 20 点 HP，那么其他对象也会被扣除 20点 HP
- 所以创建了这两个变量
	- 在 玩家 / 角色 的生命脚本的生命周期函数 `Awkae()`中（[[EnemyHealthControl]] 与 [[PlayerHealth]]）
	- `_characterHealthInfo = ScriptableObject.Instantiate(_healthInfo);`
- 即 利用 `_healthInfo` 来为 `_characterHealthInfo` 实例化 一个 资产文件
- 这样每个对象用的资产文件就是不一样的了，就没有共享的问题
# 生命周期函数

```C#
protected virtual void Awake()
{
	animator = this.GetComponent<Animator>();
}

protected virtual void Start()
{
	_characterHealthInfo.InitCharacterHealthInfo();
}

protected virtual void Update()
{
	OnHitParryLookAttacker();
}

protected virtual void OnEnable()
{
	GameEventManager.MainInstance.AddEventListening<float, string, string, Transform, Transform>("触发伤害", OnCharacterHitEventHandler);

	//处决
	GameEventManager.MainInstance.AddEventListening<string, Transform, Transform>("触发处决", OnCharacterFinishEventHandler);
	GameEventManager.MainInstance.AddEventListening<float, Transform>("生成伤害", TriggerDamageEventHandler);

}

protected virtual void OnDisable()
{
	GameEventManager.MainInstance.RemoveEvent<float, string, string, Transform, Transform>("触发伤害", OnCharacterHitEventHandler);
	GameEventManager.MainInstance.RemoveEvent<string, Transform, Transform>("触发处决", OnCharacterFinishEventHandler);
	GameEventManager.MainInstance.RemoveEvent<float, Transform>("生成伤害", TriggerDamageEventHandler);
}
```
## `Start()`

- `_characterHealthInfo.InitCharacterHealthInfo();`
	- [[CharacterHealthInfo]] 中的初始化函数
		- 用来获取信息
	- 这里是 `Start()` 中进行的初始化，而实例化是在 玩家/敌人 脚本中的 `Awake()` 中实例化的
# 函数
## 角色受伤行为 `CharacterHitAction`

- 用来处理角色受到伤害后的逻辑
- 每个角色的受伤逻辑都是不同的
- 这里基类写的就是 虚函数
- 让子类去重写这个函数，完成自己的受伤逻辑

```C#
protected virtual void CharacterHitAction(float damage, string hitName, string parryName)
{
	//让子类去重写这个函数
	//不同的对象 受伤行为可能不同

	//这个函数用来处理 执行动画 的逻辑
}
```
## 检测攻击者 `SettingAttakcer()`

- 这个函数 用来处理 寻找攻击者（也就是玩家）逻辑
	- 如果 玩家打的是敌人
	- 如果 敌人打的是玩家，那么寻找的攻击者就是这个敌人
- 这个函数会用在 `OnHitParryLookAttacker()` `OnCharacterHitEventHandler()` `OnCharacterFinishEventHandler`
	- 受击/格挡 看向
	- 受伤事件
	- 处决

```C#
private void SettingAttakcer(Transform attacker)
{
	//这个函数 用来处理 寻找攻击者（也就是玩家）逻辑
	if (_currentAttack == null || _currentAttack != attacker)
	{
		_currentAttack = attacker;
	}
}
```
## 伤害检测 `TakeDamage()`

- 传入参数 `float damage` 与 `bool hasParry = false`
	- 前者是造成的伤害
	- 后者是 是否进行格挡
- 调用 `_characterHealthInfo.Damage(damage, hasParry);` 进行扣除体力值与生命值
- 该函数用于 子类覆写的 `CharacterHitAction` 中

```C#
protected virtual void TakeDamage(float damage,bool hasParry = false)
{
	_characterHealthInfo.Damage(damage, hasParry);
}
```
## 受伤事件 `OnCharacterHitEventHandler()`

^ac56a6

- 传入参数 `float damage, string hitName, string parryName, Transform attacker,Transform self`
	- 分别为 造成伤害 受伤动画名 格挡动画名 攻击者位置 当前位置（指的是被攻击者）
- 通过事件管理器去添加监听
	- 外部需要时会呼叫这个事件并调用这个函数

- 判断 传入的被攻击者位置 是不是自己的位置
	- 如果不是，说明被攻击的对象不是自己
		- 返回，不做处理（~~打的又不是自己干什么要去做逻辑处理~~)
- 调用 `SettingAttakcer()`函数 和 `CharacterHitAction()`
	- 前者去设置当前攻击者（也就是设置看向）
	- 后者是子类重写的受伤函数，用来播放受伤动画

```C#
private void OnCharacterHitEventHandler(float damage, string hitName, string parryName, Transform attacker,
Transform self)
{
	//这个函数 用来处理 被玩家攻击后 逻辑
	//被攻击后，检测被攻击者是不是自己，调用其他受伤函数

	//传入 造成的伤害、受伤动画名、格挡动画名、以及攻击者位置（判断是不是玩家打的），以及自己的位置
	//可能有多个敌人，但玩家只打了一个，self 判断被攻击者是不是自己

	//如果传进来的 self 不是自己，那么不执行事件
	if (self != this.transform)
	{
		return;
	}

	SettingAttakcer(attacker);//玩家正在攻击该敌人

	CharacterHitAction(damage, hitName, parryName);//调用动画执行函数，执行动画
}
```
### 受伤大致逻辑一览

- 在 [[CharacterCombatBase]] 脚本中，有动画事件 `ATK()` 和 伤害触发 `TriggerDamager()`
- 动画执行时触发了动画事件后
	- 动画事件函数中 会去调用 `TriggerDamager()`函数
- 在 `TriggerDamager()`函数 中，会去呼叫事件，相应的也就调用`OnCharacterHitEventHandler()`函数
	- 并传入 *伤害值 受伤动画名 格挡动画名 攻击者 当前被攻击者* 
		- 这五个参数中，*当前被攻击者* 对应的就是 传入参数`Transform self`
- 在 `OnCharacterHitEventHandler()` 函数中又去调用了 `SettingAttakcer()` `CharacterHitAction()` 函数
	- `SettingAttakcer()` 设置了 `_currentAttack` 为 *攻击者* （`TriggerDamager()`传入的参数）
		- 在`OnHitParryLookAttacker()`函数中
			- 就会使用 `this.transform.Look(_currentAttack.transform.position, 50f);`
			- 设置 *当前看向*了
	- `CharacterHitAction();` 传入了 *伤害值 受伤动画名 格挡动画名*
		- 会根据自身情况，判断是受伤还是格挡，然后扣除相应的数值
		- （假设子类已经重写完毕的情况下）
## 受击/格挡 看向 `OnHitParryLookAttacker()`

- 如果当前攻击者`_currentAttack` 为空
	- 返回不做处理
	- ~~没人打为什么要去看~~
- 如果 `_currentAttack` 不为空，也就是有人攻击了
	- 判断现在动画的标签是不是在 Hit 或者 Parry 标签下，同时 当前动画的播放状态小于 0.5f （50%）
		- 两个标签分别对应着 受击 和 格挡
- `this.transform.Look(_currentAttack.transform.position, 50f);`
	- 看向当前攻击者

- 动画执行完后 就会切换到默认动作或者是行走动作（不会是受击和格挡动画了）
- 但不能去写 `else{ _currentAttack = null; }`
- 如果写了，那么在玩家攻击敌人时，第一下攻击敌人不会去看向玩家了
	- 因为在 `OnHitParryLookAttacker()` 在 `Update` 中更新，在敌人执行受击/格挡动画前
	- `OnHitParryLookAttacker()` 就已经被调用了
	- 此时 敌人的动画标签不是 Hit 或者 Parry，`if` 语句就会直接走 `else`
	- 导致第一下攻击敌人不会去看玩家
- 备注：本项目中 玩家第一个攻击有两个攻击动作
	- 这里说的第一下攻击，是指 *第一个攻击中的第一个攻击动作*

```C#
private void OnHitParryLookAttacker()
{
  
	if (_currentAttack == null)
	{
		return;
	}

	if (animator.AnimationAtTag("Hit") || animator.AnimationAtTag("Parry")
			&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
	{
		Debug.LogError("111");
		this.transform.Look(_currentAttack.transform.position, 50f);
	}
	else
	{
		Debug.LogWarning(111);
		_currentAttack = null;
	}
}
```
## 处决相关
### `OnCharacterFinishEventHandler()`

^ee7bbc

- 通过事件管理器去添加监听
	- 外部需要时会呼叫这个事件并调用这个函数

- 如果被攻击者不是自己
	- 不做处理
- 调用 `SettingAttakcer()`
- 播放传进来的 受伤动画（传进来的就是 被处决动画）
### `TriggerDamageEventHandler()`

- 通过事件管理器去添加监听
	- 外部需要时会呼叫这个事件并调用这个函数
- 一般跟 `OnCharacterFinishEventHandler()` 的事件一起呼叫并调用

- 如果被攻击者不是自己
	- 不做处理
- 调用 `TakeDamage()` 造成伤害
- 调用 [[GamePoolManager|对象池]] 拿一个对象进行播放 受击音效
### 代码示例

```C#
private void OnCharacterFinishEventHandler(string hitName, Transform attacker, Transform self)
{
	if (self != transform)
	{
		return;
	}

	SettingAttakcer(attacker);
	animator.Play(hitName);
}

private void TriggerDamageEventHandler(float damage, Transform self)
{
	if (self != transform)
	{
		return;
	}

	TakeDamage(damage);

	GamePoolManager.MainInstance.TryGetOnePoolItem("HITSound", transform.position, Quaternion.identity);

}
```
## 当前目标死亡后移除当前目标

- 具体内容在其他脚本中
- 这里仅写了大致思路
- 推荐使用第二个
### 第一种

- 在 [[PlayerCombat]] 的Update中调用 `CheackEnemyIsDie()`函数去检测敌人生命值，有没有死亡
	- 但这样做有些浪费资源，毕竟是每一帧都去检测
- 通过接口 IHealth 来监听目标是否死亡
	- `public bool OnDie() => _characterHealthInfo.IsDie;`
		- `CharacterHealthInfo` 函数中
			- `bool IsDie => _isDie;` ` bool _isDie => (_currentHP <= 0f);`
-  `CheackEnemyIsDie()`会去检测敌人是否死亡
	- 如果死亡，那么 `_currentEnemy`置空
	- 即 玩家的当前敌人为空（PlayerCombat 脚本挂载在玩家身上，`_currentEnemy`指的也就是攻击的敌人）

- 注意
	- 第一种仅写了代码，但更多的内容没有写上去（包括失活敌人移动脚本，播放死亡动画）
	- 可以把第二种的那些代码拿上来

```C#
//在Update中调用CheackEnemyIsDie 去检测生命值，有没有死亡
//但这样做有些浪费资源，毕竟是每一帧都去检测
//通过接口 IHealth 来监听目标生命值

public bool OnDie() => _characterHealthInfo.IsDie;
//PlayerCombat 中还有 CheackEnemyIsDie();
```
### 第二种

- 通过事件监听 
- PlayerCombat 、 [[EnemyCombatControl]] 中添加事件（添加监听）
- 同一个事件，但要注册两个函数（PlayerCombat ——> `CheckRemoveEnemy` EnemyCombatControl ——> `OnEnemyDead`）
	- 前者将 玩家脚本中的 `_currentEnemy` 置空
	- 后者将 敌人的攻击信息进行重置
- 在 EnemyHealthControl 中呼叫事件

- [[EnemyHealthControl]] 中 ，`TakeDamage()` 函数重写后
	- 会额外判断当前生命值是否小于0，如果小于0 
	- 就去呼叫事件（ [[EnemyHealthControl#^d820c9|敌人死亡]] ），调用 `PlayDeadAnimation();`
		- 播放死亡动画
	- 调用 `EnemyManager` 去移除这个敌人
	- 并将 `EnemyMovementControl()` 脚本失活
		- 因为脚本上有些函数会去调用 角色控制器，导致警报

```C#
//通过事件监听 
//PlayerCombat 、 EnemyCombatControl 中添加事件
//同一个事件，但要注册两个函数
//在 EnemyHealthControl 中呼叫事件

protected virtual void PlayDeadAnimation()
{
	//如果 处决执行过后 敌人就会死亡然后播放死亡动画
	//那么这里就需要使用 if 语句 
	//否则就会 播放两次 一次为处决 一次为正常死亡
	//FinisHit 为 被处决动画 的标签 敌人被处决时播放这个动作
	if (!animator.AnimationAtTag("FinishHit"))
	{
		animator.SetBool(AnimationID.IsDieID, true);
	}
}
```
#### `PlayDeadAnimation()`

- 用于播放死亡动画
	- `animator.SetBool(AnimationID.IsDieID, true);`

- 如果 处决执行过后 敌人就会死亡然后播放处决的死亡动画
- 那么这里就需要使用 if 语句 
	- `if (!animator.AnimationAtTag("FinishHit"))`
- 否则就会 播放两次 一次为处决 一次为正常死亡
- `FinisHit` 为 被处决动画 的标签 敌人被处决时播放这个动作
### 简而言之

- 方法一
	- 通过在 PlayerCombat 脚本中 `Update` 中去检测敌人生命值是否小于0
		- 然后去置空
- 方法二
	- EnemyHealthControl 脚本 检测敌人生命值是否小于0，如果小于0就去呼叫事件
	- 在 PlayerCombat 脚本 和 EnemyCombatControl 脚本中监听事件
	- 前者置空（玩家上的）当前敌人，后者重置敌人的攻击信息
# 总结

- 主要用于实现 受伤函数、检测攻击者、伤害检测、受伤事件、处决相关的受伤事件、受击/格挡看向、目标死亡后移除

- 受伤函数
	- 让子类去重写，根据子类的具体需要进行更改
	- ***这里主要完成的是动画播放***

- 检测攻击者
	- 用来处理 寻找攻击者
	- 在 受击/格挡看向 、 受伤事件 中被调用
	- 获取攻击者的位置信息，然后去做相应的处理
		- 看向目标等

- 伤害检测
	- 调用 CharacterHealthInfo 中的 `.Damage` 函数，扣除对应的体力值与生命值
	- 虚方法，供子类去重写去完成子类独有的逻辑

- 受伤事件
	- 这个函数才是 被攻击后 *所调用的受伤事件*
	- 判断 被攻击者 是不是自己再去做处理
	- 调用 检测攻击者 和 受伤函数
	- 设置 当前的攻击者（看向攻击者） 与 执行受伤的相应逻辑（播放动画）

- 处决相关的受伤事件
	- 处决不是时时刻刻都可以进行的处决
	- 所以单独写了处决相关的函数，用事件管理器去添加监听
	- 当执行处决时，就去呼叫事件，播放受伤动画 并且 造成伤害

- 受击/格挡看向
	- 受击之后看着 攻击者
	- 放在 Update 中更新

* 当前目标死亡后移除当前目标
	* 第一种
		* 在 Update 中去监听敌人的生命值有没有小于0
		* 如果小于0就清空敌人
	* 第二种
		* 通过事件去移除敌人

---
# 源代码

![[CharacterHealthBase.cs]]

---