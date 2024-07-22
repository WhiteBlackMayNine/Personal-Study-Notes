---
tags:
  - "#科学/Unity/ARPG/TP_CameraControl"
created: 2024-06-20
更新:
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
	- 
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
	_input.x = Mathf.Clamp(_input.x, _cameraVerticalMaxAnagle.x,_cameraVerticalMaxAnagle.y);
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
	var newPosition = _lookTarget.position + (-transform.forward * _positionOffset);
	this.transform.position = Vector3.Lerp(this.transform.position, newPosition,         _positionSmoothTime);
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
	Cursor.visible = false;
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

---
# 源代码

![[TP_CameraControl.cs]]

---