---
tags:
  - "#科学/Unity/ARPG/Player/PlayerMovement"
created: 2024-06-05
---



---
# 关联知识点

[[CharacterMoveBase]]

---
# 说明

- 主要功能都是需要自己写的，这里只是大概写个总结
# 变量
## 角色旋转

```C#
private float _rotationAngle;
private Transform _mainCamera;
private float _angleVelocity = 0f;
[SerializeField, Header("角色旋转")] private float _smoothTime;
```
## 转身跑

```C#
private Vector3 _characterTargetDirection;//角色目标朝向
```
## 控制快速跑与走路

```C#
private float _runcount = 0;
private float _walkcount = 0;
```
## 脚步声

```C#
private float _nextFootTime;
[SerializeField,Header("脚步声")] private float _slowFootTime;
[SerializeField] private float _fastFootTime;
```
# 函数
## 控制角色的旋转

```C#
 private void CharacterRotationControl()
        {
            if (!_characterIsGround)
            {
                return;
            }

            if (_animator.GetBool(AnimationID.HasInputID))
            {
                //如果有输入，那么获取旋转量
                _rotationAngle = Mathf.Atan2(GameInputManager.MainInstance.Movement.x, GameInputManager.MainInstance.Movement.y)
                    * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            }

            if (_animator.GetBool(AnimationID.HasInputID) && _animator.AnimationAtTag("Motion"))
            {
                //角色以Y轴为基准去旋转的
                this.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(this.transform.eulerAngles.y,
                    _rotationAngle, ref _angleVelocity, _smoothTime) ;

                //相当于Z轴经过 以Y轴为基准的旋转后 重新赋值 ，得到目标的移动朝向，也就是要移动的方向
                _characterTargetDirection = Quaternion.Euler(0, _rotationAngle,0) * Vector3.forward;
            }

            _animator.SetFloat(AnimationID.DetalAngleID, DevelopmentToos.GetDeltaAngle(this.transform,
                _characterTargetDirection.normalized));

        }
```

## 更新动画

```C#
        private void UpdateAnimation()
        {
            if (!_characterIsGround)
            {
                return;
            }

            _animator.SetBool(AnimationID.HasInputID, GameInputManager.MainInstance.Movement != Vector2.zero);

            if (_animator.GetBool(AnimationID.HasInputID))
            {
                if (GameInputManager.MainInstance.Run)
                {
                    _animator.SetBool(AnimationID.RunID, true);

                    _runcount++;
                }

                if (GameInputManager.MainInstance.Walk)
                {
                    _animator.SetBool(AnimationID.WalkID, true);

                    _walkcount++;
                }

                if (_animator.GetBool(AnimationID.RunID))
                {

                    if (_runcount % 2 == 0)
                    {
                        _animator.SetBool(AnimationID.RunID, false);
                    }
                }

                if (_animator.GetBool(AnimationID.WalkID))
                {

                    if (_walkcount % 2 == 0)
                    {
                        _animator.SetBool(AnimationID.WalkID, false);
                    }
                }

                _animator.SetFloat(AnimationID.MovementID, (_animator.GetBool(AnimationID.RunID) ? 2f : 
                    GameInputManager.MainInstance.Movement.sqrMagnitude), 
                    0.25f,Time.deltaTime);
                SetCharacterFootSound();
            }
            else
            {
                _animator.SetFloat(AnimationID.MovementID, 0f, 0.25f, Time.deltaTime);

                if(_animator.GetFloat(AnimationID.MovementID) < 0.2f)
                {
                    _animator.SetBool(AnimationID.RunID, false);
                    _animator.SetBool(AnimationID.WalkID, false);
                }
            }
        }
```
## 播放脚步声

```C#
private void PlayFootSound()
{
	GamePoolManager.MainInstance.TryGetOnePoolItem("FOOTSound", this.transform.position, Quaternion.identity);
	_nextFootTime = (_animator.GetFloat(AnimationID.MovementID) > 1.1f) ? _fastFootTime : _slowFootTime;
}
```
## 设置角色脚步声

```C#
private void SetCharacterFootSound()
{
	if(_characterIsGround && _animator.AnimationAtTag("Motion") && 
		_animator.GetFloat(AnimationID.MovementID) > 0.5f)
	{
		_nextFootTime -= Time.deltaTime;
		if(_nextFootTime < 0f)
		{
			PlayFootSound();
		}
	}
	else
	{
		_nextFootTime = 0f;
	}
}
```
# 转身跑原理

- Animator 参数——> DetalAngle 
- 原理
	- 以当前面朝向为基准（也就是Z轴的朝向）与目标轴（转向后的朝向）
	- 把这两个朝向做一个增量角的计算
	- 得到角色 转的幅度 的增量角是多少
- 步骤
	- 先获取目标朝向 `_characterTargetDirection`
	- 将以 Y轴 为基准的旋转 乘以 `Vector3.forward` 后赋值给 `_characterTargetDirection`
		- 此时得到的便是 移动方向
	- 将增量叫进行计算后赋值给参数条件
- 相当于Z轴经过 以Y轴为基准的旋转后 重新赋值 ，得到目标的移动朝向，也就是要移动的方向
	- `_characterTargetDirection = Quaternion.Euler(0, _rotationAngle,0) * Vector3.forward;`
	- `_animator.SetFloat(AnimationID.DetalAngleID, DevelopmentToos.GetDeltaAngle(this.transform,_characterTargetDirection.normalized));`
- 动画可能会有些偏差
	- 旋转平滑度可以调高一些
		- 如果过低（也就是旋转过快）的话，会导致按键与动画的旋转量不匹配而造成旋转过多 / 少
	- 可以改一下动画信息
# 更新
## 控制角色的旋转 `CharacterRotationControl()`

- 判断角色是否在地面上

- 获取 `AnimationID.HasInputID` 的值，是否有输入
	- 如果有，获取旋转量
		- `_rotationAngle = Mathf.Atan2(GameInputManager.MainInstance.Movement.x, GameInputManager.MainInstance.Movement.y) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
- `Mathf.Atan2(GameInputManager.MainInstance.Movement.x, GameInputManager.MainInstance.Movement.y)` ^72630c
	- 使用`Mathf.Atan2`函数来计算两个数值 （`.x .y`）的 反正切值（`arctan`），返回一个介于`-π`到`π`之间的弧度值
		- `arctan` 返回的就是一个角度
		- 表示从原点到该点的向量与正Y轴之间的角度
		- 这个角度可以用来确定物体在二维空间中的方向
	- `GameInputManager.MainInstance.Movement.x, GameInputManager.MainInstance.Movement.y)`
		- 通过 GameInputAction 获取到的玩家移动输入的方向向量的两个分量 `.x` `.y`
			- 往 前 / 后 走 ——> `.y` 增加 / 减少
			- 往 右 / 左 走 ——> `.y` 增加 / 减少
			- 可以理解为 ——> 从上往下俯视角色，以中心点为原点，前后走就是 Y轴，左右走就是 X轴
- `* Mathf.Rad2Deg` 
	- 将计算出的弧度值转换为度数
	- 得到一个介于-180到180之间的角度值
- 此时，这两段代码就已经完成了角色的旋转（得到了 旋转量）
	- `+ _mainCamera.eulerAngles.y`
		- 与摄像机当前的欧拉角中的 Y轴 角度相加（相机的左右旋转）
			- [[TP_CameraControl#^e3a8aa|相机旋转]] 左手定则，相机想要左右旋转，就是 Y轴转动
				- 简单理解
					- 物体是太阳，相机是地球，左右旋转，就是地球绕着太阳公转
					- 也就是 Y轴 的角度
		- 得到物体相对于摄像机的旋转角度
- **摄像机的移动也就可以影响到了物体的移动**

- 如果当前有按键输入，且当前动画处于 `Motion` 移动中
	- `this.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(this.transform.eulerAngles.y, _rotationAngle, ref _angleVelocity, _smoothTime);`
- `Mathf.SmoothDampAngle` 平滑地将一个角度从一个值过渡到另一个值
	- 从 当前的欧拉角 转到 *旋转量* `_rotationAngle` ，平滑时间为 `_smoothTime`
	- `_angleVelocity` 一个引用参数，用于存储当前的速度值
- 乘以 `Vector3.up` （0，1，0） 将得到的旋转 应用到物体的旋转上（ Y轴 ），同时保持物体的其他轴不变
	- 将角度限制在垂直方向上的变化，即只改变物体绕 Y 轴的旋转角度
	- 使用的向量乘法为 *点乘*
- 物体旋转也就做完了


- 转身跑
	- `_characterTargetDirection = Quaternion.Euler(0, _rotationAngle, 0) * Vector3.forward;`
		- `Quaternion.Euler` 创建一个四元数
			- 创建一个 Y轴 为旋转量 `_rotationAngle` 的 四元数
			- ——> 物体旋转完成之后的 Y轴角度
		- `* Vector3.forward` 
			- 四元数 乘以 向量 ——> 相当于 这个向量 绕着 四元数 得到一个 新的向量
			- ![[PlayerMove 转身跑向量乘以四元数 示意图.png]]
				- 备注
					- `Vector3.forward` 绕着 `Quaternion.Euler(0, _rotationAngle, 0)` 旋转后
					- 得到 `_characterTargetDirection`
					- 大致画了一下，假设都是正确的
	- `DevelopmentToos.GetDeltaAngle(this.transform, _characterTargetDirection.normalized)`
		- 调用工具包，将 归一化后的 `_characterTargetDirection` 传进去
		- 返回值就是所要的增量角了
- 大致逻辑
	- 获取到物体旋转之后的方向 `_characterTargetDirection`（目标朝向）
	- 跟现在的位置做一个增量角计算，并 传给 `AnimationID.DetalAngleID`
	- 如果增量角大于 Animator 中设置的数值，就去播放转身跑动画
### 总结

- 其实控制角色移动没有那么复杂

- 利用 `Mathf.Atan2` 得到角色移动的旋转角度，然后 `* Mathf.Rad2Deg` 转换为角度
- 然后 `+ _mainCamera.eulerAngles.y` 这样相机旋转的时候，就可以控制角色的旋转了
	- 使用 `Mathf.SmoothDampAngle` *将当前欧拉角*`this.transform.eulerAngles` 转变为 *目标旋转量*`_rotationAngle`
- 之后，获取旋转之后的目标朝向 `_characterTargetDirection`
	- 其实就是获取 旋转之后的面朝向（ Z轴 ）
	- 所以需要 乘个 `Vector.forward`
- 最后，使用工具包 `DevelopmentToos.GetDeltaAngle` 把当前位置 和 归一化后的目标朝向 `_characterTargetDirection`
	- 进行 增量角计算
- 得到的增量角赋值给 `AnimationID.DetalAngleID`
	- 在 Unity 的 Animator 中，会根据参数 `DetalAngle` 的值 判断是否播放 转身跑动画
## 更新动画 `UpdateAnimation()`

- 更新 `AnimationID` 的成员变量的值 ——> 更新 Animator 中的参数
- ——> 根据动画连线的限制条件 去 播放不同动画

- `_animator.SetBool(AnimationID.HasInputID, GameInputManager.MainInstance.Movement != Vector2.zero);`
	- 只要 WASD 有输入，GameInputManager.MainInstance.Movement 的值就不会为 `Vector2.zero`

- `_animator.SetFloat(AnimationID.MovementID, (_animator.GetBool(AnimationID.RunID) ? 2f : GameInputManager.MainInstance.Movement.sqrMagnitude), 0.25f, Time.deltaTime)
	- 判断，是否按下了 快步跑
	- 如果按下，给 `MovementID` 设置为 2
	- 如果没有，就给 `MovementID` 设置为  `GameInputManager.MainInstance.Movement.sqrMagnitude`
		- WASD 输入的向量的 *平方长度* 
			- **向量平方长度是向量各分量平方和的结果，而不是向量长度的平方**
			- 向量a = (x, y)，其平方长度为  `x^2 + y^2` 
	- 调用 `SetCharacterFootSound()` 播放脚步声

- 如果没有按键输入，将 `MovementID` 设置为 0
	- 并且把 `RunID` 和 `WalkID` 设置为 `false`
## 设置角色脚步声 `SetCharacterFootSound()`

- 如果当前角色 处于地面上、当前播放动画标签为 `Motion` 、`MovementID` > 0.5 （确保在移动状态下）
	- `_nextFootTime -= Time.deltaTime` 让 *下一次脚步声时间* **逐帧减少**
	- 当 `_nextFootTime` 的值为 0 时
		- 调用 `PlayFootSound()` 进行播放声音
- 如果不在地面，或者不在移动状态下
	- 就是停止移动了，比如 攻击、受击、格挡、或者是 不走了
	- `_nextFootTime = 0f`
	- 下一次移动直接播放声音
## 播放脚步声 `PlayFootSound()`

- 调用 [[GamePoolManager]] 播放 `FOOTSound` 脚步声
- 判断是否点击了 走步
- 如果是 ——> `_nextFootTime = _walkFootTime` 将下一次脚步声时间 更改为 `_walkFootTime`
- 如果不是 ——> 
	- `_nextFootTime = (_animator.GetFloat(AnimationID.MovementID) > 1.1f) ? _fastFootTime : _slowFootTime;`
	- 根据 `MovementID` 的值判断当前是 *快步跑* 还是 *正常跑* 然赋予相应的值 `_fastFootTime` 与 `_slowFootTime`

---
# 源代码

![[PlayerMovement.cs]]

---