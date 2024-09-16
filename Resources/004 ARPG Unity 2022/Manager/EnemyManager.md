---
tags:
  - 科学/Unity/ARRG/Manager/EnemyManager
created: 2024-08-15
---

---
# 关联知识点



---
# 说明

- *敌人对象* 的 **总管理器** ，用来控制所有 *敌人对象*
- 创建两个列表
	- 添加 / 删除 / 初始化 敌人
- [[2 协同程序|协程]] 去指派 敌人的攻击指令
# 变量

```C#
private Transform _mainPlayer;//获取玩家位置信息

/// <summary>
/// 获取玩家位置信息
/// </summary>
/// <returns></returns>
public Transform GetMainPlayer() => _mainPlayer;


//这个列表用来保存未被激活的敌人
[SerializeField] private List<GameObject> _allEnemies = new List<GameObject>();

//这个列表用来保存激活的敌人
[SerializeField] private List<GameObject> _activeEnemies = new List<GameObject>();

private bool _closeAttackCommandCoroutine;//这个变量用于在外部控制关闭协程

private WaitForSeconds _waitTime;
```

- `_mainPlayer` 与 `GetMainPlayer()` 用来获取 玩家 的位置
	- 在生命周期函数 `Awake()` 中获取

- `_allEnemies` 与 `_activeEnemies`
	- 前者为 当前所有敌人，后者为 当前所有存活敌人
	- 但一个敌人对象死亡后，会从 `_activeEnemies` 删除
	- 协程会根据 `_activeEnemies` 来派发攻击指令
- `_closeAttackCommandCoroutine`
	- 外部可以调用函数 `CloseAttackCommandCoroutine()`
	- 来停止 协程
- `_waitTime`
	- 协程中的等待时间
# 生命周期函数

```C#
protected override void Awake()
{
	base.Awake();
	_mainPlayer = GameObject.FindGameObjectWithTag("Player").transform;
	_waitTime = new WaitForSeconds(6f);
}

private void Start()
{
	InitActiveEnemy();
	if (_activeEnemies.Count > 0)
	{
		_closeAttackCommandCoroutine = false;
	}

	//协程会牵扯到Update的执行逻辑，或者内存的泄漏
	StartCoroutine(EnableEnemyUnitAttackCommand());
}

private void OnDestroy()
{
	StopAllCoroutines();
}
```
## `Awake()`

- `WaitForSeconds` 是 Unity3D 中用于等待一段时间的类
## `Start()`

- 初始化敌人
- 如果当前存活敌人数量大于0
	- `_closeAttackCommandCoroutine` 设置为 `false`
	- 不关闭协程
- 开启协程
# 函数
## 添加 / 删除 敌人
### `AddEnemyUnit()`

- 外部需要传入参数 `GameObject enemy`
	- 一个 *敌人对象*
- 判断 `_allEnemies` 是否有这个敌人对象
- 如果没有，就添加进去

```C#
public void AddEnemyUnit(GameObject enemy)
{
	if (!_allEnemies.Contains(enemy))
	{
		_allEnemies.Add(enemy);
}
```
### `RemovedEnemyUnit()`

- 外部需要传入参数 `GameObject enemy`
	- 一个 *敌人对象*
- 如果当前 `_activeEnemies` 包含了这个 *敌人对象*
- 获取 `EnemyMovementControl` 类型的变量
	- 调用 [[EnemyMovementControl#^819f32|EnableCharacterController 函数]] 禁用 角色控制器
- 然后在 `_activeEnemies` 队列中删除这个 *敌人对象*

```C#
public void RemovedEnemyUnit(GameObject enemy)
{
	if (_activeEnemies.Contains(enemy))
	{
		EnemyMovementControl enemyMovementControl;
		if (enemy.TryGetComponent(out enemyMovementControl))
		{
			enemyMovementControl.EnableCharacterController(false);
		}
		_activeEnemies.Remove(enemy);
	}
}
```
## 初始化敌人 `InitActiveEnemy()`

- 利用 `foreach` 遍历 `_allEnemies` 所有敌人对象
- 如果当前遍历的敌人对象处于激活状态 `e.activeSelf`
- 获取 `EnemyMovementControl` 类型的变量
	- 调用 [[EnemyMovementControl#^819f32|EnableCharacterController 函数]] 激活 角色控制器
- 往 `_activeEnemies` 列表中添加这个 *敌人对象*

```C#
private void InitActiveEnemy()
{
	foreach (var e in _allEnemies)
	{
		if (e.activeSelf)
		{
			EnemyMovementControl enemyMovementControl;
			if (e.TryGetComponent(out enemyMovementControl))
			{
				enemyMovementControl.EnableCharacterController(true);
			}
			_activeEnemies.Add(e);
		}
	}
}
```
## 处决时敌人停止攻击 `StopAllActiveUnit()`

^2ad492

- 执行某些动作时让其他单位停止动作
- 也可以通过在玩家上使用枚举来代表玩家的不同状态
- 利用枚举进行监测

- 遍历每一个 `_activeEnemies` 当前存活敌人列表 中的元素
- 获取当前 *遍历元素* 身上的 `EnemyCombatControl` 脚本对象
- 调用 [[EnemyCombatControl#^b8fea7|StopAllAction 函数]] 
	- 重置攻击指令 ——> 暂停攻击

```C#
public void StopAllActiveUnit()
{
	//让当前所有激活的单位 停止动作 或者 攻击动作
	foreach (var e in _activeEnemies)
	{
		if (e.TryGetComponent(out EnemyCombatControl enemyCombatControl))
		{
			enemyCombatControl.StopAllAction();
		}
	}
}
```
## 协程 `EnableEnemyUnitAttackCommand()`

- 如果当前没有存活的敌人
	- 关闭协程

- `while` 循环 当前存活敌人的数量大于0
	- 先判断是否关闭了协程
	- 取一个随机值
		- 如果这个随机值小于当前存活角色的数量
		- 创建一个变量，并将其赋予 `_activeEnemies[index]`
		- 如果这个变量能获取到`EnemyCombatControl`组件
			- 调用 [[EnemyCombatControl#^db6c93|SetAttackCommand]] 设置攻击状态
				- 指派攻击指令
- 等待 `_waitTime` 时间

- 简而言之
	- 随机取一个 *敌人对象*
	- 给这个 *敌人对象* 指派攻击动作

```C#
IEnumerator EnableEnemyUnitAttackCommand()
{
	if (_activeEnemies == null)
	{
		yield break;//关闭协程.
	}
	if (_activeEnemies.Count == 0)
	{
		yield break;//关闭协程
	}

	while (_activeEnemies.Count > 0)
	{
		if (_closeAttackCommandCoroutine)
		{
			yield break;//关闭协程
		}

		var index = Random.Range(0, _activeEnemies.Count);

		if (index < _activeEnemies.Count)
		{
			GameObject _temp = _activeEnemies[index];
			if (_temp.TryGetComponent(out EnemyCombatControl enemyCombatControl))
			{
				enemyCombatControl.SetAttackCommand(true);
			}
		}
		yield return _waitTime;
	}

	yield break;//关闭协程
}
```
# 总结

- 管理器，用于管理 所有敌人对象 的 添加删除初始化
- 利用协程来指派某一个敌人对象的攻击指令
- 以及 （玩家）处决敌人时所有敌人停止攻击

---
# 源代码

![[EnemyManager.cs]]

---