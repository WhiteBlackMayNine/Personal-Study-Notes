---
tags:
  - "#科学/Unity/ARPG/CameraControl/CameraCollider"
created: 2024-06-20
---
****---
# 关联知识点

[[TP_CameraControl]]

---
# 说明

- 创建一个空物体 TP_Camera
- 主摄像机是放到 TP_Camera 下的
	- 也就是它的子物体
- 两个脚本（TP_CameraControl 和 CameraCollider）都是放到 TP_Camera 上的
- 记得在目标（也就是玩家）上创建一个 空物体 lookTarget
- 作用
	- 防止撞到墙而产生 bug
	- 通过射线检测
		- 在 TP_Camera 这个地方往后发射一条射线
		- 只要这个射线检测到障碍物
		- 那么这个相机就会往前移动
		- 如果移动超过最小值，那么就 失效了
			- 那么就穿过障碍物
	- 为确保摄像机的碰撞生效
		- 主摄像机  的 Z轴 应当为 负的（-0.1），归一化后也就是负的（-1）
			- 也就是让摄像机 在 TP_Camera 的后面
		- 否则 摄像机 的碰撞会失效
			- 因为此时摄像机会跑到 TP_Camera 的前面（相机移动）
- ![[CameraCollider 摄像机.png]]
# 变量

- `Vector2 _maxDistanceOffset;
	- 最大 / 最小的偏移量
	- 这个是 摄像机 与 空物体 TP_Camera 之间的距离
- `LayerMask _whatIsWall;`
	- 障碍物层级
- `float _detectionDistance;`
	- 射线长度
- `float _colliderSomoothTime;`
	- 碰撞移动平滑时间
- `Vector3 _originPosition;`
	- 初始位置
- `float _originOffset;`
	- 初始的偏移量
- `Transform _mainCamera;`
	- 主摄像机的位置
# 函数

## 更新相机碰撞 `UpdateCameraCollider`

- 如果不转换为世界坐标，那么射线的方向就是斜着的
```C#
  private void UpdateCameraCollider()
{
	//需要转换为世界坐标  因为前面(Start函数中)获取的是本地坐标
	Vector3 _distanceDistance = this.transform.TransformPoint(_originPosition * _detectionDistance);

	//进行射线检测
	if(Physics.Linecast(this.transform.position, _distanceDistance,out var hit,_whatIsWall,
		QueryTriggerInteraction.Ignore))
	{
		//_maxDistanceOffset 存储的两个值，一个为最大值(这里为y)，一个为最小值(这里为x)
		_originOffset = Mathf.Clamp(hit.distance * 0.8f, _maxDistanceOffset.x, _maxDistanceOffset.y);
	}
	else
	{
		_originOffset = _maxDistanceOffset.y;
	}

	_mainCamera.localPosition = Vector3.Lerp(_mainCamera.localPosition, _originPosition * (_originOffset -1), 
		_colliderSomoothTime);
}
```
# 生命周期函数

```C#
private void Awake()
{
	_mainCamera = Camera.main.transform;
}

private void Start()
{
	//相机会根据TP_CameraControl中的偏移量为参照物 归一化 使得相机初始位置在后面而不是前面（Z需要在0以下）
	_originPosition = this.transform.localPosition.normalized;
	_originOffset = _maxDistanceOffset.y;
}

private void LateUpdate()
{
	UpdateCameraCollider();
}
```
# 更新
## 作用

- 这个脚本主要用于更新相机碰撞
	- 当玩家跟一堵墙很近的时候，移动视角时很有可能导致摄像机卡在这堵墙里面
	- 写这个脚本，就是让 摄像机 在碰撞到墙后，往前移动一段距离，如果距离过近就直接跑到墙后面去
	- 而不是卡在墙里面
- 挂载在 TP_Camera 物体上
## 生命周期函数
### Awake()  

- 获取摄像机组件
### Start()

- `_originPosition = this.transform.localPosition.normalized;`
	- 得到最初的位置
	- 归一化 TP_Camera 的 Z轴必须小于0（-0.01）
		- 归一化之后就是 -1
		- 如果大于0或者为0，归一化之后就不是 -1
			- 这样 `_originPosition` 的值就是负的
		- 在函数中 `_distanceDistance` 、 `_mainCamera.localPosition`
			- 都需要 `_originPosition` 的值为负
			- 毕竟要实现摄像机在玩家后面

- `_originOffset = _maxDistanceOffset.y;`
	- 最初偏移量为 `_maxDistanceOffset` 的 y值
		- 在 Inspector 窗口中设置
## `LateUpdate()`

- 更新函数 `UpdateCameraCollider();`
## 函数

- 函数中，下面的 if 语句使用的是 `Physics.Linecast`
- 这里得到的 Vector3 类型 的 `_distanceDistance` 就是 射线检测 的终点
- 在生命周期函数 `Start()` 中，获取的 `_originPosition` 是本地坐标系下的
	- 在这里要设置为世界坐标系
- `_detectionDistance` 为变量
	- 在 Inspector 窗口上设置的 检测距离
- *设置的 射线检测 **终点** 为 **初始位置 \* 检测距离**

```C#
Vector3 _distanceDistance = this.transform.TransformPoint(_originPosition * _detectionDistance);
```

- 进行射线检测
- TP_Camera 当前位置为其他，`_distanceDistance` 为终点
- 检测在这一段距离中，是否有其他物体
	- `_whatIsWall` 在 Inspector 窗口上设置层级

```C#
if(Physics.Linecast(this.transform.position, _distanceDistance,out var hit,_whatIsWall,QueryTriggerInteraction.Ignore))
```

- 如果在其中有其他物体（射线检测到了）
- 那么就将 `_originOffset` 设置为 `hit.distance * 0.8f`
	- 让摄像机（`_mainCamera.localPosition`）往前移动一定距离
- 使用 API `Mathf.Clamp` 将 `_originOffset`  钳制一下
- 

```C#
_originOffset = Mathf.Clamp(hit.distance * 0.8f, _maxDistanceOffset.x, _maxDistanceOffset.y);
```

- 如果没有其他物体
- 就让 `_originOffset` 去等于默认值

```C#
_originOffset = _maxDistanceOffset.y;
```

- 最后设置相机的位置

```C#
_mainCamera.localPosition = Vector3.Lerp(_mainCamera.localPosition, _originPosition * (_originOffset -0.1f), _colliderSomoothTime);
```
## 碰撞检测的实现

- 核心功能实现是 `UpdateCameraCollider()`
- 在 TP_Camera 中 是 `_positionOffset` 来控制 TP_Camera 这个物体与目标的距离
	- 目标 也就是 玩家
- 而摄像机却是以 `_maxDistanceOffset` 来控制 Main Camera 与 TP_Camera 的距离
- 这两个变量，导致 TP_Camera 、Main Camera 、目标 三者之间都有间隔
- 而在 `UpdateCameraCollider()` 函数中
- 如果 射线检测 检测到了物体，就会赋值给 `_originOffset`
	- 让摄像机往前移动一点距离

- 简单来说
	- 射线击中障碍物，获取碰撞点，这个点就是相机不会被遮挡的位置
	- 但是距离墙面太近了，所以在这个点 的基础上
	- 让相机以这个点为基础，再往前移动一点

- 如果出现 摄像机 穿过墙到墙后面去了
- 就是这是距离被卡死了，限制没做好
	- `_maxDistanceOffset`，还有 TP_Camera 的 `_positionOffset``
- 或者是
	- 判断一下，如果相机碰撞的那个前后移动
	- 往前移动已经 到最小值了
	- 就不让相机移动

---
# 源代码

![[CameraCollider.cs]]

---