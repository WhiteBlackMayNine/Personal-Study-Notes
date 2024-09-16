---
tags: 
created: 2024-08-15
---
---
# 关联知识点



---
# 说明

- 工具包类
- 提供了 **线性插值、计算方向、距离、角度等，以及打印日志** 的功能
# 方法
## 不受帧数影响的Lerp

```C#
public static float UnTetheredLerp(float time = 10f)  
{  
    return 1 - Mathf.Exp(-time * Time.deltaTime);  
}
```
## 取目标方向(返回一个标量)

```C#
public static Vector3 DirectionForTarget(Transform target, Transform self)  
{  
    return (self.position - target.position).normalized;  
}
```
## 返回于目标之间的距离

```C#
public static float DistanceForTarget(Transform target, Transform self)  
{  
    return Vector3.Distance(self.position, target.position);  
}
```
## 获取增量角

```C#
public static float GetDeltaAngle(Transform currentDirection, Vector3 targetDirection)  
{  
    //当前角色朝向的角度  
    float angleCurrent = Mathf.Atan2(currentDirection.forward.x, currentDirection.forward.z) * Mathf.Rad2Deg;
    
    //目标方向的角度也就是希望角色转过去的那个方向的角度  
    float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;  
  
    return Mathf.DeltaAngle(angleCurrent, targetAngle);  
}
```
## 计算当前朝向于目标方向之间的夹角

```C#
public static float GetAngleForTargetDirection(Transform target, Transform self)  
{  
    return Vector3.Angle(((self.position - target.position).normalized), self.forward);  
}
```
## 限制一个值或者度数在-360-360之间

```C#
public static float ClampValueOn360(float f)  
{  
    f %= 360f;  
    if (f < 0)  
        f += 360;  
  
    return f;  
}
```
## 计算当前点和目标点之间的位置

```C#
public static Vector3 TargetPositionOffset(Transform target, Transform self, float time)  
{  
    var pos = target.transform.position;  
    return Vector3.MoveTowards(self.position, pos, UnTetheredLerp(time));  
}
```
## 打印日志

```C#
public static void WhatThisF(object message)  
{  
    Debug.LogFormat($"日志内容:<color=#ff0000> --->   {message}   <--- </color>");  
}
```

---
# 源代码

![[DevelopmentToos.cs]]

---