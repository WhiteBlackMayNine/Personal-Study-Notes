---
tags:
  - "#科学/Unity/ARPG/相机相关"
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



---
# 源代码

![[CameraCollider.cs]]

---