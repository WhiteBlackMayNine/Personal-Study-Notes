---
tags:
  - "#科学/Unity/ARPG/PlayerCombatControl"
created: ""
更新:
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


---
# 源代码

![[PlayerCombat.cs]]

---