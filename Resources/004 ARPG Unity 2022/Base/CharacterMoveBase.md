---
tags:
  - "#科学/Unity/ARPG/Base/CharacterMoveBase"
created: 2024-08-12
---

---
# 关联知识点



---
# 说明

- 主要功能都是需要自己写的，这里只是大概写个总结
- 脚本挂载在根物体上
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
# 更新
## 地面相关
### 地面检测 `GroundDetection()`

- 利用 `Physics.CheckSphere` API
- 检测一个圆形的范围内，是否接触到了地面

- 一般情况下，模型的根对象就是在模型的最下方中心处
- `detecttionPostion` 的值就是 最下方中心处（双脚之间）
- 但为了以防万一，`this.transform.position.y - _detectionPositionOffset`
- 当根对象不在玩家的最下方中心处，就利用 `_detectionPositionOffset`
- 来调整 `detecttionPostion` 的 Y轴
### 坡道检测 `SlopReserDirection()`

- 进行射线检测
	- 以 根位置向上0.5f 为起点（`transform.position + (transform.up * .5f)`
	- 方向垂直向下 `Vector3.down`
	- 检测最大距离为 `_characterController.height * .85f`
		- Character Controller 组件中的 Height
		- 碰撞器的高 \* 0.85f
	- 检测的层级为 `_whatIsGround`
- 如果打到了物体
	- 用 `Vector3.Dot` 检测 `hit.normal` 与 `Vector3.up` 的点乘
	- 只要不是 0 ，就代表着两个之间存在角度
		- 点积等于0，说明两个向量是垂直的，只要不是0那么就不垂直   浮点值不会完全相等，只会无限接近
	- 即 斜坡
- 点乘不为0
	- 利用 `Vector3.ProjectOnPlane` 更新 `moveDirection`
- 如果没有打到物体
	- 直接返回 传进来的方向向量

- `hit.normal`
	- 一个三维向量，表示射线与物体表面的交点处的法线方向
	- 当一个射线（例如从摄像机发射的光线）与场景中的物体相交时
	- `hit.normal` 提供了该物体表面在该点的法线向量
	- 这个向量垂直于物体表面，并且指向物体外

- `Vector3.Dot`
	- **计算两个向量的点积，结果是两个向量的长度相乘再乘以它们夹角的余弦值**
		- 得到的是一个标量值
	- 如果点积为正，则说明两个向量之间的夹角小于90度；如果为负，则说明夹角大于90度
	- 对于已经标准化（长度为1）的两个向量来说，它们的点积等于它们夹角的余弦值
		- 如果两个向量完全相同，点积为1；完全相反，则为-1；如果垂直，则为0

 - `Vector3.ProjectOnPlane`
	 - **将一个向量投影到由法线定义的平面上，得到的是原始向量在平面上的垂直投影向量**

- 坡道检测
	- ![[ARPG CharacterMoveBase 坡道检测.png]]

- 如果移动动画，勾选了 Root Transform Position(XZ) 的 Bake Into Pose
	- 导致 移动时根位置不会动
- 这个函数就会和 `UpdateCharacterMoveDirecion` 一起作用
	- 或者是原本的作用就是这个
- 去移动 玩家 的根位置 
	- ![[ARPG CharacterMoveBase 动画根位置由 Character 组件 .Move API 控制.png]]
### 更新移动方向 `UpdateCharacterMoveDirecion`

^a791de

- 进行坡道检测，获取处理后的角色移动方向
	- `_moveDirection = SlopReserDirection(dirction);`
- 调用 `.Move` API 移动角色根位置
	- `_characterController.Move(_moveDirection * Time.deltaTime);`
## 重力相关
### 激活重力 `EnableCharacterGravity()`

- 外部需要传入参数 `enable`
- 修改 是否启用重力 `_isEnableGravity`
	- `_isEnableGravity = enable;`
- 如果启用(true)重力，那么就将角色垂直速度设置为-2 ，如果不启用(false)则为0f
	- `_characterVerticalVelocity = (enable) ? -2f : 0f;`

- 通过 事件管理器 去 添加监听
- 外部需要的时候就去 呼叫 这个事件，并传入相应的参数
### 设置角色重力 `SetCharacterGravity()`

- `_fallOutTime` 与 `_fallOutDeltaTime`
- `_fallOutTime` 自己设置的一个等待下落总时间
- 而 `_fallOutDeltaTime` 是用来计算下落时间的
- 当玩家离开地面时，就去 计算 `_fallOutDeltaTime -= Time.deltaTime` 
	- 在这段时间内，不去进行下落相关的逻辑处理
	- 进行的是正常的移动逻辑
- 设置这两个变量，是为了 下楼梯 / 斜坡 用的
	- 如果一离开地面就播放下落动画
		- 那么当下楼梯时，很有可能去播放下落动画
	- 所以去等待一段时间再去播放下落动画

- 函数逻辑
	- 先判断是否在地面上
	- 如果在地面上，就重置 `_fallOutDeltaTime` 的值
		- 判断 `_characterVerticalVelocity` 的值是否 小于0
			- 如果是，就说明之前以及下落过了
			- 那么这里就固定为 -2 
	- 如果不在地面上
		- 先判断 `_fallOutDeltaTime` 的值是否大于0
			- 大于0
				- 减去 `Time.deltaTime` 等待下落时间
			- 小于0
				- 播放下落动画
				- 或处理下落的相关逻辑处理
	- 最后判断 垂直速度是否小于最大垂直速度，并且是否开启重力
		- 更新角色垂直速度  时间 * 加速度
		- `_characterVerticalVelocity += Time.deltaTime * CharacterGravity;`

- 为什么在地面上，`_characterVerticalVelocity` 要固定为 -2
	- 如果不去固定
		- 当下落的时候，`_characterVerticalVelocity` 的值就会一直去累加
		- 直到下落速度达到最高速度
	- 只有去固定了，当处于地面上时为 -2
		- 那么 在下落之后，`_characterVerticalVelocity` 为 -2，不会去累加
		- 下一次下落之后，`_characterVerticalVelocity`为 -2 
			- 而不是累加的一个值
### 更新角色重力 `UpdateCharacterGravity()`

- 如果不启用重力 则直接返回
- X轴 Z轴 都是角色移动进行改变的，因此此处仅改变 Y轴 的值
- `_characterVerticalDirection.Set(0, _characterVerticalVelocity, 0);`
- 利用 `.Move` 进行移动
- `_characterController.Move(_characterVerticalDirection * Time.deltaTime);`
	- 跟 坡道检测 更新移动方向 的 `.Move` 相同
	- 如果动画不去移动根位置
	- 就有 `_characterController.Move` 去移动
# 总结

- 这个脚本基类主要来处理最基本的移动逻辑
- 包括 地面检测、坡道检测、更新移动方向、设置重力、激活重力、更新重力

- 地面检测
	- 使用 范围检测 `Physics.CheckSphere` 检测范围内是否有地面
		- 通过 层级 来进行检测，需要在 Inspector 中设置地面层级
- 坡道检测
	- 使用 射线检测 `Physics.Raycast` 检测射线是否打到物体
	- 利用 `Vector3.Dot` 计算是否为垂直关系
	- 通过 `Vector3.ProjectOnPlane` 来得到 玩家移动方向在斜坡的投影向量
		- 从而得到新的移动方向
- 更新移动方向
	- 利用 `.Move` 进行 角色控制器 移动

- 激活重力
	- 使用 事件管理器 去添加监听，外部如果需要修改 是否激活重力
		- 就去呼叫这个事件
- 设置重力
	- 判断是否在地面
	- 然后去判断 `_fallOutDeltaTime` 的值
		- 等待下落时间
	- 判断 垂直速度 是否 小于 最大垂直速度
		- 去更新垂直速度
- 更新重力
	- 利用 `.Move` 进行 角色控制器 更新 Y轴

---
# 源代码

![[CharacterMoveBase.cs]]

---