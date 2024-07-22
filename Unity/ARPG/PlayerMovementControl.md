---
tags:
  - "#科学/Unity/ARPG/PlayerMovement"
created: 2024-06-05
更新:
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



---
# 源代码

![[PlayerMovement.cs]]

---