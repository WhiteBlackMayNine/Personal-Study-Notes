---
tags:
  - "#科学/Unity/ARPG/Player/PlayerCombat"
created: 2024-06-20
---

---
# 关联知识点

[[CharacterComboSO]] [[CharacterComboDataSO]]

---
# 说明

- 用于角色攻击逻辑的处理
- 需要另外两个 （`CharacterComboSO CharacterComboDataSO`）连招脚本配置信息
# 变量

- `CharacterComboSO _baseCombo`
	- 角色基础攻击连招表（左键）
- `CharacterComboSO _changeCombo`
	- 角色变招攻击连招表（右键）
- `CharacterComboSO _currentCombo`
	- 当前使用的连招
- `int _currentComboIndex`
	- 当前连招的索引
- `float _maxColdTime`
	- 攻击的最大冷却时间
- `bool _canAttackInput`
	- 是否允许进行攻击输入
- `int _hitIndex`
	- 受击索引值
- `int _currentComboCount`
	- 当前 基础攻击的次数 （也就是左键）
	- 用于变招逻辑
# 函数
## 角色攻击相关函数
### 是否允许攻击输入 `CanBaseAttackInput`

```C#
private bool CanBaseAttackInput()
{
	if (!_canAttackInput)
	{
		return false;
	}

	if (_animator.AnimationAtTag("Parry"))
	{
		return false;
	}

	if (_animator.AnimationAtTag("Hit"))
	{
		return false;
	}

	return true;
}
```
### 普通攻击输入 `CharacterBaseAttackInput`

^edf3bb

- 判断是否可以进行攻击输入
	- 如果不可以就返回
- 判断输入的是左键还是右键
	- 如果是左键
		- 判断 `_currentCombo` 是否为空 **或** `_currentCombo` 不为基础连招表
			- 调用 `ChangeComboData` 函数 将 `_currentCombo` 改为 基础连招表
		- 调用 `ExecuteComboAction` 函数 执行动作
	- 如果是右键
		- 判断基础连招是否大于2（也就是 轻攻击 两下后才能 变招）
			- 调用 `ChangeComboData` 函数 将 `_currentCombo` 改为 变招连招表
			- 利用 `switch` 判断 基础攻击的次数 
				- 将 `_currentComboIndex` 赋予不同的值
		- 如果不大于2 就返回
		- 调用 `ExecuteComboAction` 函数 执行动作
			- 记得 `_currentComboCount = 0;` 设置为0
				- 否则会出现 连招顺序出错

```C#
private void CharacterBaseAttackInput()
{
	if (!CanBaseAttackInput())
	{
		return;
	}

	if (GameInputManager.MainInstance.LAttack)
	{
		if(_currentCombo == null || _currentCombo != _baseCombo)
		{
			ChangeComboData(_baseCombo);
		}
		ExecuteComboAction();
	}else if (GameInputManager.MainInstance.RAttack)
	{
		if(_currentComboCount >= 2)
		{
			ChangeComboData(_changeCombo);
			switch (_currentComboCount)
			{
				case 2:
					_currentComboIndex = 0;
					break;
				case 3:
					_currentComboIndex = 1;
					break;
				case 4:
					_currentComboIndex = 2;
					break;
				case 5:
					_currentComboIndex = 3;
					break;
			}
		}
		else
		{
			return;
		}
		ExecuteComboAction();
		_currentComboCount = 0;
	}
}
```
### 改变组合技 `ChangeComboData`

- 如果 `_currentCombo` 不为 传进来的 连招表
	- 那么就 赋值给 `_currentCombo`
	- 并且调用 `ResetComboInfo` 函数 重置一些下

```C#
private void ChangeComboData(CharacterComboSO characterComboSO)
{
	if(_currentCombo != characterComboSO)
	{
		_currentCombo = characterComboSO;
		ResetComboInfo();
	}
}
```
### 执行动作 `ExecuteComboAction`

- 如果 `_currentCombo` 为 基础攻击 ，那么就让 `_currentComboCount` +1
	- 用于计数
- 如果 当前连招索引值 = 连招的最大数量
	- 说明已经执行到了最后一个动作
	- 将索引值重置
- 获取攻击间隔时间
- 利用 `.CrossFadeInFixedTime` API 来执行动作
	- `.TryGetOneComboAction` 来获取要执行动作的名字
- 调用一个计时器，将冷却时间与 `UpdataComboInfo` 传进去
- `_canAttackInput = false;
	- 在播放动画时不能进行攻击
	- 需要等计时器计时结束后，调用 `UpdataComboInfo`
		- `_canAttackInput = true;` 时才能继续攻击
		- 也就是攻击间隔冷却

```C#
private void ExecuteComboAction()
{
	_currentComboCount += (_currentCombo == _baseCombo) ? 1 : 0;
	_hitIndex = 0;
	
	if(_currentComboIndex == _currentCombo.TryGetComboMaxCount())
	{
		//如果执行到了最后一个动作
		_currentComboIndex = 0;
	}
	_maxColdTime = _currentCombo.TryGetComboColdTime(_currentComboIndex);
	_animator.CrossFadeInFixedTime(_currentCombo.TryGetOneComboAction(_currentComboIndex), 0.15f, 0, 0);
	GameTimeManager.MainInstance.TryGetTimer(_maxColdTime, UpdataComboInfo);
	_canAttackInput = false;
}
```
### 更新连招动画信息 `UpdataComboInfo`

- 攻击索引 +1
- 最大冷却时间重置
- 是否允许攻击输入 ——> 允许

```C#
private void UpdataComboInfo()
{
	_currentComboIndex++;
	_maxColdTime = 0f;
	_canAttackInput = true;
}
```
### 重置连招动画信息 `ResetComboInfo`

- `ResetComboInfo()`
	- 当前连招索引、冷却时间、受击索引 都设置为0
- `OnEndAttack()`
	- 如果角色移动了，那么调用 `ResetComboInfo()`
		- 也就是如果移动了，就要从头开始攻击
	- 如果不希望有这个功能，那么就注释掉这个函数就行

```C#
private void ResetComboInfo()
{
	_currentComboIndex = 0;
	_maxColdTime = 0f;
	_hitIndex = 0;
}

private void OnEndAttack()
{
	//移动后从头开始进行攻击
	if (_animator.AnimationAtTag("Motion") && _canAttackInput)
	{
		ResetComboInfo();
	}
}
```
# 生命周期函数

```C#
private void Awake()
{
	_animator = this.GetComponent<Animator>();
}

private void Start()
{
	_canAttackInput = true;
	_currentCombo = _baseCombo;
}

private void Update()
{
	CharacterBaseAttackInput();
	OnEndAttack();
}
```

# 角色变招逻辑
## 位置

- 写在 `CharacterBaseAttackInput` 中
## 代码

- 变量 `int _currentComboCount` 在每次攻击时累加一次，用于计数
	- 如果放在 `if(GameInputManager.MainInstance.RAttack)` 中计数（也就是攻击输入函数）
		- 可能导致该变量不累计或提前累计
	- 因此，应当放到 `ExecuteComboAction` 函数中
- 如果有多个变招，可以利用 `switch` 来完成
	- 如果只有一个的话
	  ```C#
		  if(_currentComboCount == 2)
		  {
			  ChangeComboData(传入变招);
			  
		  }
		  ExecuteComboAction();
		```
- `if(_currentComboCount >= 2)`
	- 防止一直按右键导致一直播放变招
	- 如果希望这个功能，就删掉 `if` 语句就行
- 如果 左键 右键 各一套连招的话
	- 那么就是按照以上逻辑写，然后套动画就行
## 总体逻辑

- `_changeCombo` 是 CharacterComboSO 类型变量，为连招表，里面有多个（至少一个）连招
- 当按下右键后，将当前 *连招表* 替换成 *变招的连招表*
- 利用 `switch` 根据 `_currentComboCount` 的值 给 `_currentComboIndex` 赋值 后 执行动作
- 此时的 *索引值* 对应的连招表是 *变招连招表* ，执行的也就是 **变招** 了
# 恻步/滑步攻击（伪代码）

- 如果按下了 左键/右键/自己设置的键 则调用 `ChangeComboData` 改一下连招表
- 给 `_currentComboIndex` 赋个值 后 执行动作 然后放入 `Updata` 更新
# 连招库

- 也可以单独创建一个连招库，里面存放着所有的连招
- 每次攻击利用 `Random` 随机赋值给`_currentComboIndex`
	- 或者 划定取值范围 后赋值
# 更新 
## 是否允许攻击输入 `CanBaseAttackInput()`

- 如果 不允许攻击输入 `!_canAttackInput`
	- 返回
- 如果 当前动画为为 受击 或者 格挡
	- 返回

```C#
private bool CanBaseAttackInput()
{
	if (!_canAttackInput)
	{
		return false;
	}

	if (_animator.AnimationAtTag("Parry"))
	{
		return false;
	}

	if (_animator.AnimationAtTag("Hit"))
	{
		return false;
	}

	return true;
}
```
## 普通攻击输入 `CharacterBaseAttackInput()`

- 判断 是否允许攻击输入

- 如果点击了左键
	- `_currentCombo == null || _currentCombo != _baseCombo`
		- 判断 当前的连招表 是否为空、或者是 当前连招表 不是 基础攻击连招表
	- `ChangeComboData(_baseCombo)` [[CharacterCombatBase#^8ca4c7|改变组合技]]，更改为 基础攻击连招表
	- 更改完之后，调用 [[CharacterCombatBase#^2f5c94|ExecuteComboAction]] 函数，执行动作
- 如果点击了右键 ——> 要执行派生动作
	- `ChangeComboData(_changeCombo)` 将组合技更改为 ——> 特殊组合技（派生攻击连招表）
	- 根据 进行派生攻击 之前，使用了多少次基础攻击
		- 右键 之前 点击了多少次 左键
	- 给 `_currentComboIndex` 赋值
	- 执行动作
	- `_currentComboCount = 0` 归0

```C#
protected override void CharacterBaseAttackInput()
{
	base.CharacterBaseAttackInput();

	if (!CanBaseAttackInput())
	{
		return;
	}

	if (GameInputManager.MainInstance.LAttack)
	{
		if (_currentCombo == null || _currentCombo != _baseCombo)
		{
			ChangeComboData(_baseCombo);
		}
		ExecuteComboAction();
	}
	else if (GameInputManager.MainInstance.RAttack)
	{
		if (_currentComboCount >= 2)
		{
			ChangeComboData(_changeCombo);
			switch (_currentComboCount)
			{
				case 2:
					_currentComboIndex = 0;
					break;
				case 3:
					_currentComboIndex = 1;
					break;
				case 4:
					_currentComboIndex = 2;
					break;
				case 5:
					_currentComboIndex = 3;
					break;
			}
		}
		else
		{
			return;
		}
		ExecuteComboAction();
		_currentComboCount = 0;
	}
}
```
## 位置同步 `MatchPosition()`

- 玩家中的位置同步，重写了父类中的 [[CharacterCombatBase#^1eae54|位置同步]]
- 这里多出了 处决 与 暗杀 的位置同步

- 处决
	- `transform.rotation = Quaternion.LookRotation(-_currentEnemy.forward);`
		- 因为处决是在敌人的前方进行的 所以 看向敌人的后方
	- `RunningMatch(_finishCombo, _FinishComboIndex);`
		- 调用 [[CharacterCombatBase#^468c75|RunningMatch]] 进行位置匹配
- 暗杀
	- `transform.rotation = Quaternion.LookRotation(_currentEnemy.forward);`
		- 因为暗杀是在敌人的后方进行的 所以 看向敌人的前方
	- `RunningMatch(_assassinationCombo, _FinishComboIndex);`
		- 调用 `RunningMatch` 进行位置匹配

```C#
protected override void MatchPosition()
{
	base.MatchPosition();

	if (_animator.AnimationAtTag("Finish"))
	{
		//因为处决是在敌人的前方进行的 所以 看向敌人的后方
		transform.rotation = Quaternion.LookRotation(-_currentEnemy.forward);
		RunningMatch(_finishCombo, _FinishComboIndex);
	}
	else if (_animator.AnimationAtTag("Assassination"))
	{
		//因为暗杀是在敌人的后方进行的 所以 看向敌人的前方
		transform.rotation = Quaternion.LookRotation(_currentEnemy.forward);
		RunningMatch(_assassinationCombo, _FinishComboIndex);
	}
}
```
## 敌人检测
### 第一种检测 射线检测

- `Physics.SphereCast` 去获取敌人
- 通过自己的按键和摄像机的移动，决定 `_detectionDirection`
	- ——> 通过自己的按键去选择敌人
#### 更新球形检测方向 `UpdateDetectionDirection()`

- `_detectionDirection = (_cameraGameobject.forward * GameInputManager.MainInstance.Movement.y) +(_cameraGameobject.right * GameInputManager.MainInstance.Movement.x);

- `_cameraGameobject.forward` 摄像机的面朝向（ `Vector3` ）
- `GameInputManager.MainInstance.Movement.y` 玩家的 Y轴 移动量（ [[PlayerMovement#^72630c|往 前/后 走]] ) （ `Vector2` ）
- 两者相乘之后，得到 得到的向量，就会受到 键盘WASD 和 鼠标移动 一起控制
- 这里控制的是 当 玩家前后移动与相机 的变化
- 而后面的那一句则是 当玩家左右移动与相机选择 的变化
- 两者相加，就是 `_detectionDirection` 的方向了（包含了前后左右与其中的相机）
	- ***这个新向量表示了在摄像机视角下，用户输入的水平和垂直移动方向***

- `_cameraGameobject.right` 代表 摄像机右侧的方向向量
	- 由`_cameraGameobject.forward` 和 `_cameraGameobject.up` 进行叉乘运算后得到的

- `_detectionDirection.Set(_detectionDirection.x, 0f, _detectionDirection.z);`
	- 将 `_detectionDirection` 的 y轴的值 归零 ，防止 浮在半空中 或者 遁入到地下

- `_detectionDirection = _detectionDirection.normalized`
	- 归一化 处理
	- 使其成为一个单位向量
		- 长度始终为 1，表示方向时，不会受到长度影响

```C#
private void UpdateDetectionDirection()//更新球形检测方向
{
	_detectionDirection = (_cameraGameobject.forward * GameInputManager.MainInstance.Movement.y) +
		(_cameraGameobject.right * GameInputManager.MainInstance.Movement.x);

	//y轴的值归零防止浮在半空中
	_detectionDirection.Set(_detectionDirection.x, 0f, _detectionDirection.z);

	_detectionDirection = _detectionDirection.normalized;
}
```
#### 进行球形检测 `DetectionTarget()`

- `Physics.SphereCast(transform.position + (transform.up * 0.7f), _detectionRang, _detectionDirection,out var hit, _detectionDistance, 1 << 9, QueryTriggerInteraction.Ignore)`
- `transform.position + (transform.up * 0.7f)` 因为脚本是挂载在 根物体 上的（角色最下方中央）
	- 所有需要 `+ (transform.up * 0.7f)`
- `_detectionRang` 为球形检测的半径
- `_detectionDirection` 球形检测的方向
- `hit` 返回的碰撞体
- `_detectionDistance` 球形检测的最大距离
- `1 << 9` 要检测的层级，Enemy 的层级为 9 
	- [[Unity 知识点#^bc8e6a|左移与层级]]
- `QueryTriggerInteraction.Ignore` 忽略 触发器 的交互
- ![[PlayerCombat 敌人检测第一种球形检测示例图.png]]
```C#
private void DetectionTarget()
{
	//判断检测一下有没有检测到敌人   敌人的图层是第九层  这里的 1<<9 就是检测敌人层级
	if (Physics.SphereCast(transform.position + (transform.up * 0.7f), _detectionRang, _detectionDirection,
		out var hit, _detectionDistance, 1 << 9, QueryTriggerInteraction.Ignore))
	{
		//进入 if 内部，说明已经检测到了敌人
		_currentEnemy = hit.collider.transform;
	}
}
```
### 第二种检测 范围检测

- 以玩家为中心 取自定义的一个半径圆的范围内 获取其中的敌人
- 在当前玩家没有目标时 取距离玩家最近的敌人为目标
- 方法一 ：获取一定范围内的所有敌人，选择一个距离最近的作为目前敌人
	- 当移动时，不会获取目标
- 方法二 ：找一个距离最近的目标 在距离一定范围内时不会改变目标
#### 方法一
##### 得到附近的所有敌人 `GetNearUnitWayOne()`

- 如果当前的 目前敌人 为不空 代表已经有敌人了
	- 返回
- 移动时不去获取
	- 返回
- 获取一定范围内的敌人
	- 利用 `Physics.OverlapSphere` 获取 **一个球形区域内与指定层级有交集的所有碰撞器**
	- 位置为 `transform.position + (transform.up * 0.7f)` 
	- 半径为 `_detectionRang`
	- 层级为 `_enemyLayer`
	- 忽略 触发器交互

```C#
private void GetNearUnitWayOne()
{
	//如果当前的 目前敌人 为不空 代表已经有敌人了
	if (_currentEnemy != null)
	{
		return;
	}
	if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//移动时不去获取
	{
		return;
	}
	//获取一定范围内的敌人
	units = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
		_enemyLayer, QueryTriggerInteraction.Ignore);
}
```
##### 选择距离最近的敌人 `GetOneEnemyUnitWayOne()`

- 如果敌人数组为空 也就是长度为0
	- 则返回
- 如果当前的 目前敌人 为不空 代表已经有敌人了
	- 返回
- 移动时不去获取
	- 返回
- 当前动画标签不是 `Attack`
	- 返回

- 利用冒泡排序选择距离最近的敌人（敌人数量不是很多）
	- `_temp_Enemy` 和 `_distance` 分别为 *敌人对象* 与 *敌人距离* 的 *中间缓存变量*
- 依次去遍历附近的敌人并做比较
	- 调用 `DevelopmentToos.DistanceForTarget` 去计算 *敌人* 与 *玩家* 之间 的距离 `dis`
	- 如果这个距离 `dis` 小于 `_distance`
		- 说明这个 敌人 比 *之前的那个敌人* 与 玩家 之间的距离 **更近**
	- 交换中间变量 `_temp_Enemy = e.transform` `_distance = dis`
- 把最后遍历获取到距离玩家最近的一个敌人当作目标
- `_currentEnemy = _temp_Enemy != null ? _temp_Enemy : _currentEnemy`
	- `_temp_Enemy` 是否不为空 ——> `true` 则 `_currentEnemy = _temp_Enemy`
		- ——> `false` 则 `_currentEnemy = _currentEnemy`
			- 不改变敌人对象

```C#
private void GetOneEnemyUnitWayOne()
{
	//如果敌人数组为空 也就是长度为0 则返回
	if (units.Length == 0)
	{
		return;
	}

	//如果当前的 目前敌人 为不空 代表已经有敌人了 
	if (_currentEnemy != null)
	{
		return;
	}

	if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//移动时不去获取
	{
		return;
	}

	if (!_animator.AnimationAtTag("Attack"))
	{
		return;
	}

	//-----------------------------------------------------------------------

	Transform _temp_Enemy = null;
	var _distance = Mathf.Infinity;//Mathf.Infinity 表示正无穷大

	foreach (var e in units)
	{
		//依次去遍历附近的敌人并做比较，选取距离玩家最近的一个敌人作为当前目标
		var dis = DevelopmentToos.DistanceForTarget(e.transform, transform);
		if (dis < _distance)//冒泡排序进行比较
		{
			_temp_Enemy = e.transform;
			_distance = dis;
		}
	}

	//把最后遍历获取到距离玩家最近的一个敌人当作目标
	_currentEnemy = _temp_Enemy != null ? _temp_Enemy : _currentEnemy;

	//------------------------------------------------------------------------
}
```
##### 清空敌人 `ClearEnemyWayOne()`

- 如果当前 `_currentEnemy` 为空
	- 没有敌人
	- 返回
- 如果当前移动速度大于 0.7
	- `_currentEnemy = null`

```C#
private void ClearEnemyWayOne()
{
	if (_currentEnemy == null)
	{
		return;
	}

	if (_animator.GetFloat(AnimationID.MovementID) > 0.7f)//移动时不去获取
	{
		_currentEnemy = null;
	}
}
```
#### 方法二
##### 得到附近敌人 `GetNearUnitWayTwo()`

- 如果当前的 目前敌人 为不空 代表已经有敌人了 且 距离小于1.5 则返回
	- 不获取附近的敌人

```C#
private void GetNearUnitWayTwo()
{
	//如果当前的 目前敌人 为不空 代表已经有敌人了 且 距离小于1.5 则返回 不更新目标
	if (_currentEnemy != null && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 1.5f)
	{
		return;
	}

	//获取一定范围内的敌人
	units = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRang,
		_enemyLayer, QueryTriggerInteraction.Ignore);
}
```
##### 选择距离最近的敌人 `GetOneEnemyUnitWayTwo()`

- 如果当前的 目前敌人 为不空 代表已经有敌人了 且 距离小于1.5
	- 则返回

```C#
private void GetOneEnemyUnitWayTwo()
{
	//如果敌人数组为空 也就是长度为0 则返回
	if (units.Length == 0)
	{
		return;
	}

	//如果当前的 目前敌人 为不空 代表已经有敌人了 且 距离小于1.5 则返回 不更新目标
	if (_currentEnemy != null && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 1.5f)
	{
		return;
	}

	//-----------------------------------------------------------------------

	Transform _temp_Enemy = null;
	var _distance = Mathf.Infinity;//Mathf.Infinity 表示正无穷大

	foreach (var e in units)
	{
		//依次去遍历附近的敌人并做比较，选取距离玩家最近的一个敌人作为当前目标
		var dis = DevelopmentToos.DistanceForTarget(e.transform, transform);
		if (dis < _distance)//冒泡排序进行比较
		{
			_temp_Enemy = e.transform;
			_distance = dis;
		}
	}

	//把最后遍历获取到距离玩家最近的一个敌人当作目标
	_currentEnemy = _temp_Enemy != null ? _temp_Enemy : _currentEnemy;

	//------------------------------------------------------------------------
}
```
##### 清空敌人 `ClearEnemyWayTwo()`

- 距离大于1.5
	- 返回

```C#
private void ClearEnemyWayTwo()
{
	if (_currentEnemy == null)
	{
		return;
	}

	if(_animator.GetFloat(AnimationID.MovementID) > 0.7f && DevelopmentToos.DistanceForTarget(_currentEnemy, transform) < 3f)
	{
		_currentEnemy = null;
	}
}
```
#### 总结

- 方法二只是在方法一的基础上
	- 移动不去获取敌人，可以视情况添加
- 在 `if` 的判断语句中添加 `DevelopmentToos.DistanceForTarget(_currentEnemy, transform)`
- 判断距离是否在一定范围内
## 处决
### 是否允许执行特殊攻击 `CanSpecialAttack()`

- 处于动画 处决、暗杀
	- 返回 `false`
- 当前敌人为空
	- 返回 `false`
- `_canFinish` 为 `false`
	- 即不允许处决时
	- 返回 `false`
- 当目前攻击次数小于2时，不可以处决
	- 返回 `false`

```C#
private bool CanSpecialAttack()//是否允许执行特殊攻击
{
	if (_animator.AnimationAtTag("Finish"))
	{
		return false;
	}

	if (_animator.AnimationAtTag("Assassination"))
	{
		return false;
	}

	if (_currentEnemy == null)
	{
		return false;
	}

	if (!_canFinish)
	{
		return false;
	}


	if (_currentComboCount < 2) //当目前攻击次数小于2时，不可以处决
	{
		return false;
	}

	return true;
}
```
### 处决处决 `CharacterFinishAttackInput()`

- 判断 是否允许执行特殊攻击

- 如果点击了 F 键（设置的是 F键 处决）
	- 随机在 处决连招表 中 拿一个 *索引*
	- 播放这个 *索引* 所对应的 *处决动画* 
- 呼叫 [[CharacterHealthBase#^ee7bbc|触发处决]] 事件
	- 传入 处决动画的受击动画，当前位置（玩家的位置），当前敌人的位置（被处决者的位置）
- 调用 [[CharacterCombatBase#^6ba9f6|ResetComboInfo]] 重置一下连招信息
- 调用 [[EnemyManager#^2ad492|StopAllActiveUnit]] 让其他敌人单位暂停攻击
- 呼叫事件 [[TP_CameraControl#^af7780|设置相机位置]] 处决的时候，让摄像机看向敌人
- 当前的 基础攻击数 设置为 0
	- `_currentComboCount = 0`
- 将 是否允许进行处决 设置为 `false`
	- `_canFinish = false`

```C#
private void CharacterFinishAttackInput()
{
	if (!CanSpecialAttack())
	{
		return;
	}

	if (GameInputManager.MainInstance.Finish)
	{
		//去播放对应的处决动画
		_FinishComboIndex = Random.Range(0, _finishCombo.TryGetComboMaxCount());
		_animator.Play(_finishCombo.TryGetOneComboAction(_FinishComboIndex));

		//呼叫事件中心，调用敌人注册的处决事件
		GameEventManager.MainInstance.CallEvent("触发处决", _finishCombo.TryGetOneHitAction(_FinishComboIndex, 0), transform, _currentEnemy);

		ResetComboInfo();
		EnemyManager.MainInstance.StopAllActiveUnit();
		GameEventManager.MainInstance.CallEvent<Transform, float>("设置相机位置", _currentEnemy,
			_animator.GetCurrentAnimatorStateInfo(0).length - 1f);
		_currentComboCount = 0;
		_canFinish = false;
	}
}
```
### 激活处决 `EnableFinishEventHandle()`

- 如果当前 `_canFinish = true` 返回
- 否则就将 `_canFinish = apply` 设置为传入的 `apply` 布尔值（传入一遍就是 `true` 目的就是要激活处决）

```C#
    private void EnableFinishEventHandle(bool apply)//激活处决
    {
        if (_canFinish)
        {
            return;
        }

        _canFinish = apply;
    }
```
## 暗杀
### 是否允许进行暗杀 `CanAssassination()`

- 当前没有目标 
	- 返回 `false`
- 距离大于 2
	- 返回 `false`
- 玩家面朝向 与 敌人面朝向 的角度 大于 30度
	- 确保 玩家 在 敌人的身后
	- 返回 `false`
- 当前处于暗杀动画
	- 返回 `false`
- 当前处于处决动画
	- 返回 `false`

- 以上都不符号
	- 返回 `true`

```C#
private bool CanAssassination()
{
	//当前没有目标
	if (_currentEnemy == null)
	{
		return false;
	}

	//距离
	if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 2f)
	{
		return false;
	}

	//角度
	if (Vector3.Angle(transform.forward, _currentEnemy.transform.forward) > 30f)
	{
		return false;
	}

	//当前在暗杀动画中
	if (_animator.AnimationAtTag("Assassination"))
	{
		return false;
	}

	//当前在处决动画中
	if (_animator.AnimationAtTag("Finish"))
	{
		return false;
	}
	
	return true;
}
```
### 暗杀输入 `CharacterAssassinationInput()`

- 判断是否可以进行暗杀
- 剩下的逻辑和 处决输入 相同
	- 区别只是 播放的动画不同

```C#
private void CharacterAssassinationInput()
{
	if (!CanAssassination())
	{
		return;//检测是否可以进行暗杀
	}

	if (GameInputManager.MainInstance.TakeOut)
	{
		//执行暗杀
		_FinishComboIndex = Random.Range(0, _assassinationCombo.TryGetComboMaxCount());//随机取一个索引值
		_animator.Play(_assassinationCombo.TryGetOneComboAction(_FinishComboIndex), 0, 0f);

		//呼叫敌人的被暗杀事件
		GameEventManager.MainInstance.CallEvent("触发处决", _assassinationCombo.TryGetOneHitAction(_FinishComboIndex, 0), transform, _currentEnemy);//这个跟处决的事件是一样的
		ResetComboInfo();
	}
}
```
# 总结

- 核心功能是 *普通攻击输入* 与 *敌人检测*
- 根据点击的左右键，去更改 左右键对应的连招表
	- 然后执行 连招动画
- 动画 会 触发 动画事件
- 动画事件 会 调用 伤害触发函数

---
# 源代码

![[PlayerCombat.cs]]

---