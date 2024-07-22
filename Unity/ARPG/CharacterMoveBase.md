---
tags:
  - "#科学/Unity/ARPG/CharacterMoveBase"
created: 2024-06-20
更新:
---

---
# 关联知识点

[[PlayerMovementControl]]

---
# 说明

- 主要功能都是需要自己写的，这里只是大概写个总结
# 大致思路

- 包括**地面检测** ， 与 **重力相关** 两大部分
- 角色具体的旋转移动之类的在另外一个脚本中（PlayerMovement）
# 地面检测
## 地面检测变量相关

- `bool _characterIsGround`
	- 角色是否在地面上
- `LayerMask _whatIsGround`
	- 地面的层级
- `float _detectionPositionOffset`
	- 检测球的偏移量
- `float _detectionRang`
	- 检测的范围
	- 也就是检测球的半径
## 地面检测函数相关

### 进行地面检测

- 检测角色是否在地面上
- 先创建一个中心点，这个点的位置是 当前角色位置的 Y轴 减去 设定的偏移量
	- X轴 Z轴 不需要进行更改
- 再利用 `Physics.CheckSphere` 检测并返回一个布尔值
	- 中心点就是创建的那个，然后依次输入 范围、层级、触发器相关
```C#
  protected bool GroundDetection()
{
//创建一个中心点
	var detecttionPostion = new Vector3(this.transform.position.x,
		this.transform.position.y -   _detectionPositionOffset,this.transform.position.z);

//利用 API ——> Physics.CheckSphere 来返回一个布尔值
	return Physics.CheckSphere(detecttionPostion, _detectionRang, _whatIsGround,                           QueryTriggerInteraction.Ignore);
}
```
### 进行坡道检测

- 用来检测角色现在是否在爬坡
```C#
	private Vector3 SlopReserDirection(Vector3 moveDirection)
{
	//进行射线检测
	if (Physics.Raycast(transform.position + (transform.up * .5f), direction:                           Vector3.down, out var hit,_characterController.height * .85f, _whatIsGround,                    QueryTriggerInteraction.Ignore))
	{
		if (Vector3.Dot(Vector3.up, hit.normal) != 0)
		//点积等于0，说明两个向量是垂直的，只要不是0那么就不垂直   浮点值不会完全相等，只会无限接近
		{
			return moveDirection = Vector3.ProjectOnPlane(vector: moveDirection,                                   hit.normal);
		}
	}
	return moveDirection;
}
```
### 更新角色移动方向
```C#
  private void UpdateCharacterMoveDirecion(Vector3 dirction)
{
	//先进行坡道检测，获取处理后的角色移动方向
	_moveDirection = SlopReserDirection(dirction);
	//用于更新角色的Y轴  也就是垂直降落相关
	_characterController.Move(_moveDirection * Time.deltaTime);
}
```

---
# 重力检测
## 重力检测变量相关

- 重力常量
	- `readonly float CharacterGravity = -9.8f;`
- 更新角色的Y轴速度
	- `float _characterVerticalVelocity;`
	- `Vector3 _characterVerticalDirection;`
		- 这个变量用来改变角色的Y轴速度，具体值由`_characterVerticalVelocity`更新
- 垂直下落最大速度
	- 用来模拟空气阻力所带来的影响
	- `float _characterVerticalMaxVelocity = 54f;`
- 下落的时间间隔
	- `float _fallOutDeltaTime;`
- 下落的等待总时间
	- `float _fallOutTime = 0.15f;`
- 是否启用重力
	- `bool _isEnableGravity;`
## 重力检测函数相关

### 激活重力

- 搭配 事件管理器 用
```C#
private void EnableCharacterGravity(bool enable)
{
	_isEnableGravity = enable;//修改

	//如果启用(true)重力，那么就将角色垂直速度设置为-2 ，如果不启用(false)则为0f
	_characterVerticalVelocity = (enable) ? -2f : 0f;
}
```
### 设置角色重力

- 先进行地面检测，判断角色是否在地面上
	- 如果在地面上
		- 将角色的下落间隔时间重置一下并将角色的垂直速度固定
			- 如果不固定的话，当再次下落（比如坠落）时，初始速度会（因为一直再累加）
	- 如果不在地面上
		- 判断角色的下落间隔时间的大小
			- 如果大于0就开始递减
			- 如果小于0就播放下落动画
		- 判断角色的下落速度是否小于最大下落速度
			- 小于的话就递加
```C#
  private void SetCharacterGravity()
{
	//先进行地面检测
	_characterIsGround = GroundDetection();

	if (_characterIsGround)
	{
		//如果角色在地面上，那么需要重置 _fallOutDeltaTime
		_fallOutDeltaTime = _fallOutTime;

		if (_characterVerticalVelocity < 0f)
		{
			//如果垂直速度小于0，那么就说明之前有过下落
			//在地面时将速度固定为-2f 否则会一直累加 待再次下落时初始速度会非常大
			_characterVerticalVelocity = -2f;
		}
	}
	else
	{
		//如果角色不在地面上

		if (_fallOutDeltaTime > 0f)
		{
			_fallOutDeltaTime -= Time.deltaTime;
			//等待0.15s 也就是让角色在从较低的高度差下落
			//一般都是下楼梯
		}
		else
		{
			//如果小于0了，那么就说明需要播放下落动画了
			//在此处播放下落动画
		}

		if (_characterVerticalVelocity < _characterVerticalMaxVelocity && _isEnableGravity)
		{
			_characterVerticalVelocity += Time.deltaTime * CharacterGravity;
			//更新角色垂直速度  时间 * 加速度
		}
	}
}
```
### 更新角色重力

 - 如果不启用重力，直接返回
```C#
   private void UpdateCharacterGravity()
{
	if (!_isEnableGravity)
	{
		//如果不启用(false)重力 则直接返回
		return;
	}

	//X Z 都是角色移动进行改变的，因此此处仅改变Y的值
	_characterVerticalDirection.Set(0, _characterVerticalVelocity, 0);
	_characterController.Move(_characterVerticalDirection * Time.deltaTime);
}
```
# 生命周期函数
## `Awake()`

```C#
  protected virtual void Awake()
{
	//获取组件
	_characterController = this.GetComponent<CharacterController>();
	_animator = this.GetComponent<Animator>();
}
```
## `Start()`

```C#
  protected virtual void Start()
{
	_fallOutDeltaTime = _fallOutTime;
	_isEnableGravity = true;
}
```
## `Update()`

```C#
  protected virtual void Update()
{
	SetCharacterGravity();
	UpdateCharacterGravity();
}
```
## `OnAnimatorMove()`

```C#
	private void OnAnimatorMove()
{
	_animator.ApplyBuiltinRootMotion();
	UpdateCharacterMoveDirecion(_animator.deltaPosition);
}
```


---
# 源代码

![[CharacterMoveBase.cs]]

---