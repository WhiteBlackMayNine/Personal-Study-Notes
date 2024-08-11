---
tags:
  - "#科学/Unity/ARPG/相机/TP_CameraControl"
created: 2024-06-20
---

---
# 关联知识点



---
# 说明

- 创建一个空物体 TP_Camera
- 主摄像机是放到 TP_Camera 下的
	- 也就是它的子物体
- 两个脚本（TP_CameraControl 和 CameraCollider）都是放到 TP_Camera 上的
- 记得在目标（也就是玩家）上创建一个 空物体 lookTarget
# 变量

- `float _cameraSpeed`
	- 相机的移动速度
- `Vector2 _cameraVerticalMaxAnagle;`
	- 相机的垂直最大角度
		- Vector2 类型 可以储存两个值（x 与 y）
- `float _cameraSmoothSpeed;`
	- 设置相机平滑**旋转**的时间
- `Transform _lookTarget`
	- 相机目标（也就是要看着 玩家 ）
- `float _positionOffset`
	- 设置相机的偏移量，控制相机与目标的距离
	- 也就是 TP_Camera 与 目标 的距离
- `float _positionSmoothTime;`
	- 设置相机平滑**移动**的时间
- `Vector2 _input;`
	- 用来接受鼠标输入的值
- `Vector3 _cameraRotationAngle;`
	- 相机旋转的角度
- `Vector3 _smoothDampVelocity = Vector3.zero;`
	- `UpdateCameraRotation` 函数里面需要，就写一个
# 函数

## 得到鼠标输入 `CameraInput`

- 水平移动，以 Y轴 为基准去左右看
	- 也就是 `.y` 加上 鼠标移动的 `.x`
- 垂直移动，以 X轴 为基准去上下看
	- 也就是 `.x` 加上 鼠标移动的 `.y`
  ```C#
  private void CameraInput()
{
	//intput.y 就是水平移动/左右移动 以Y轴为基准左右看
	_input.y += GameInputManager.MainInstance.CameraLook.x;

	//上下看，获取鼠标的Y轴，左右则是鼠标的X轴
	_input.x -= GameInputManager.MainInstance.CameraLook.y;

	//限制一下范围
	_input.x = Mathf.Clamp(_input.x,_cameraVerticalMaxAnagle.x,
		_cameraVerticalMaxAnagle.y);
}
	```
## 相机旋转 `UpdateCameraRotation`

- Vector3.SmoothDamp
	- **实现向量值平滑插值**
	- 用于计算两个向量之间的平滑过渡值
  ```C#
  private void UpdateCameraRotation()
{
	_cameraRotationAngle = Vector3.SmoothDamp(_cameraRotationAngle, new Vector3(_input.x, _input.y, 0f),
		ref _smoothDampVelocity, _cameraSmoothSpeed);
	transform.eulerAngles = _cameraRotationAngle;
}
	```
## 相机位置移动 `CameraPosition`

```C#
  private void CameraPosition()
{
	//以当前看着的目标为基准，往后偏移一段距离(由 _positionOffset 决定)
	//var newPosition = _lookTarget.position + (-transform.forward * _positionOffset);
	//加了处决看着敌人的功能用下面的 没加就用上面的
	var newPosition = ((((_isFinish) ? _currentLookTarget.position + _currentLookTarget.up * 0.7f :
	_currentLookTarget.position) + (-transform.forward * _positionOffset)));
	this.transform.position = Vector3.Lerp(this.transform.position, newPosition, _positionSmoothTime);
}
```
# 生命周期函数

## Awake()

```C#
private void Awake()
{
	_lookTarget = GameObject.FindWithTag("CameraLookTarget").transform;
}
```
## Start()

```C#
private void Start()
{
	Cursor.visible = false;//隐藏光标
	Cursor.lockState = CursorLockMode.Locked;
}
```
## Update()

```C#
private void Update()
{
	CameraInput();
}
```
## LateUpdate()

```C#
private void LateUpdate()
{
	UpdateCameraRotation();
	CameraPosition();
}
```
# 更新
## 变量

- `Transform _currentLookTarget`
	- 用于 处决时相机看着敌人
- `bool _isFinish`
	- 用来判断当前是否处于处决状态
## 函数
### CameraInput 得到鼠标输入

- 鼠标移动时
	- 水平移动为 X轴
		- `GameInputManager.MainInstance.CameraLook.x`
	- 竖直移动为 Y轴
		- `GameInputManager.MainInstance.CameraLook.y`
- 相机的旋转 
	- 左右转动 为 Y轴 转
		- `_input.y`
	- 上下转动 为 X轴 转
		- `_input.x`
- 因此
	- 鼠标水平移动 ——> 相机左右转动
		- `_input.y += GameInputManager.MainInstance.CameraLook.x;`
	- 鼠标竖直移动 ——> 相机上下转动
		- `_input.x -= GameInputManager.MainInstance.CameraLook.y;`

- 鼠标的输入值通常是以屏幕中心为原点的
	- 鼠标 向右 / 左 移动，以屏幕中心为原点
		- `GameInputManager.MainInstance.CameraLook.x` 的值为正 / 负
- 而相机的旋转是以世界坐标系的原点为中心的
	- 鼠标向右 / 左移动，是希望相机向左 / 右移动
		- 因为相机左 / 右 移，这样才能看到右 / 左 边的东西
	- 相机向左 / 右 移动，以世界坐标系的原点为中心旋转
		- `_input.x` 的值为负 / 正
	- 也就是
		- `_input.y += GameInputManager.MainInstance.CameraLook.x`
- 同理
	- 鼠标向上 / 下 移动，以屏幕中心为原点
		- `GameInputManager.MainInstance.CameraLook.y;` 的值 为正 / 负
	- 鼠标向上 / 下 移动，是希望相机向下 / 上移动
		- 相机向下 / 上 移动，以世界坐标系的原点为中心的
		- `_input.y` 的值为 负 / 正
	- 也就是
		- `_input.x -= GameInputManager.MainInstance.CameraLook.y;`
- 所以
	- 水平移动 为 `+=`
	- 竖直移动为 `-=`

- 简单理解 相机旋转
	- 把相机与世界坐标中心点想象成太阳和地球的关系
	- 左手定则，*顺时针为正，逆时针为负*
	- ***鼠标移动 和 相机旋转 是反着来的***

- Unity 的旋转采用 左手定则 
	- [[009 Unity 知识点#^b95203|Unity的旋转]]
## UpdateCameraRotation() 相机旋转

- 利用 `Vector3.SmoothDamp` 这个 API 过渡
- 这里的旋转就是相机的旋转
	- 使用的是 `_input.x` `_input.y`
	- 前者对应 *相机在X轴的旋转*，后者对应 *相机在Y轴的旋转*
- 然后将 新的旋转角度 赋给 `transform.eulerAngles`

```C#
_cameraRotationAngle = Vector3.SmoothDamp(_cameraRotationAngle, new Vector3(_input.x, _input.y, 0f),
	ref _smoothDampVelocity, _cameraSmoothSpeed);
	
transform.eulerAngles = _cameraRotationAngle;
```
## CameraPosition() 相机位置移动

- `var newPosition = _lookTarget.position + (-transform.forward * _positionOffset);`
	- `_lookTarget` 在 生命周期函数 的 `Awake()` 中赋值
	- 在场景中，自己给模型手动加一个名为 CameraLookTarger 的空物体
	- 目的是 让相机看着这个物体
- `_lookTarget.position + (-transform.forward * _positionOffset)`
	- 目标对象的位置 减去 偏移量
	- 就是让 最终的位置 在 目标对象位置的后面
- `this.transform.position = Vector3.Lerp(this.transform.position, newPosition, _positionSmoothTime);`
	- 利用 `Vector3.Lerp` 把值赋给 `this.transform.position`


- 由于后面新加了个处决时看着敌人
- 但其实逻辑很简单，先判断是不是处于 处决中，然后选择用哪一个
- 第一层括号
	- `((_isFinish)? _currentLookTarget.position + _currentLookTarget.up * 0.7f : currentLookTarget.position))
	- 如果在处决中，就选择用 `_currentLookTarget.position + _currentLookTarget.up * 0.7f`
		- 当前目标的位置然后在往上移个 0.7
	- 如果不在就用 `currentLookTarget.position`
		- 这样跟一般的相同

```C#
var newPosition = ( ( ( (_isFinish) ? _currentLookTarget.position + _currentLookTarget.up * 0.7f :
        _currentLookTarget.position) + (-transform.forward * _positionOffset) ) );
```
## 处决时相机看着敌人
### SetFinishTarget() 设置处决目标

- 通过事件来监听这个函数
	- `GameEventManager.MainInstance.AddEventListening<Transform,float>("设置相机位置", SetFinishTarget);`
- PlayerCombat 脚本中会去呼叫这个事件
	- 并传进去 `_currentEnemy` 与 `_animator.GetCurrentAnimatorStateInfo(0).length - 1f`
	- 前者是当前的敌人 后者是 获取当前动画长度，然后减去1秒
- 调用 `SetFinishTarget()` 函数时
	- 把 `_currentLookTarget` 设置为传入的 `_currentEnemy`
	- 把是否在处决给设置为真`_isFinish = true`
		- `CameraPosition()` 函数中也就会去使用相应的代码
			- `_currentLookTarget.position + _currentLookTarget.up * 0.7f`
	- 调用的 计时器 计时时间为 `_animator.GetCurrentAnimatorStateInfo(0).length - 1f`
- 计时器会去调用 `ResetTarget()`
### ResetTarget() 重置目标

- 用来重置信息
	- ` _isFinish = false;
	- `_currentLookTarget = _lookTarget;

---
# 源代码

![[TP_CameraControl.cs]]

---