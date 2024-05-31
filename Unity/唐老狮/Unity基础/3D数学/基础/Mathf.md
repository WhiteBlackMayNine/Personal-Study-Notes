---
tags:
  - 科学/Unity/Unity基础/3D数学/基础/Mathf
created: 2024-03-23
课时: "2"
来源: https://www.taikr.com/course/1190/task/38674/show
---

---
# 关联知识点



---
# 知识点

## `Mathf`和`Math`

- `Math`是 C# 中封装好的用于数学计算的**工具类
	- 位于`System`命名空间中
- `Mathf`是Unity中封装好的用于数学计算的**工具结构体
	- 位于unityEngine命名空间中
- 他们都是提供来用于进行数学相关计算的
## 他们的区别

- `Mathf`和`Math`中的相关方法几乎一样
- `Math`是 C# 自带的工具类 主要就提供一些数学相关计算方法
- `Mathf`是unity专门封装的，不仅包含`Math`中的方法，还多了一些适用于游戏开发的方法
- 所以我们在进行unity游戏开发时//使用Mathf中的方法用于数学计算即可
## Mathf中的常用方法（一般计算一次）

### 圆周率`PI`

- `Mathf.PI
### 取绝对值

- `Mathf.Abs(数值)`
### 向上取整

- `Mathf.CeilToInt(数值)`
### 向下取整

- `Mathf.FloorToInt(数值)`
### 钳制函数

- `Mahtf.Clamp(进行计算的值,最小值,最大值)`
	- 若计算的值小于最小值，则取最小值
	- 若计算的值大于最大值，则取最大值
	- 若计算的值在范围中，则取自己的值
### 获取最大值

- `Mathf.Max(传入的数)`
### 获取最小值

- `Mathf.Min(传入参数)`
### 一个数的 n 次幂

- `Mathf.Pow(数值,几次幂)`
### 四舍五入

- `Mathf.RoundToInt(数值)`
### 获取平方根

- `Mathf.Sqrt(数值)`
### 判断是否为 2 的 n 次方

- `Mathf.IsPowerOfTwo(数值)`
	- 返回值为布尔值
### 判断正负数

- `Mathf.Sign(数值)`
	- 正数返回 1
	- 负数返回 -1
## Mathf中的常用方法

- 一般不停计算
### 插值运算`Lerp

- 得到的值 = 开始的值 + （结束的值 - 开始的值）* 时间
	- `result = start + ( end -start ) * t
#### Lerp函数公式

- `result = Mathf.Lerp(start, end, t);
	- t为插值系数，取值范围为 0~1 
#### 插值运算用法一

- 每帧改变`start`的值
	- 变化速度先快后慢，位置无限接近，**但是不会得到`end`位置
	- 变速运动
#### 插值运算用法二

- 每帧改变`t`的值
	- 变化速度匀速，位置每帧接近，当`t >= 1`时，得到结果
	- `time += Time.delataTime;`
	- 匀速直线运动
- 插值系数 甚至可以写为`Time.delataTime * Speed`
### 平滑地改变角度值

- `Mathf.SmoothDampAngle(current, target, onUpdate, smoothTime,maxSpeed,deltaTime);`
- [[Unity 知识点#^42a029|使用场景]]
#### 参数说明

- -`current`
	- 当前的起始角度值
- `target`
	- 期望达到的目标角度值
- `onUpdate`
	- 是一个可传入的回调函数
	- 它在每一帧更新时被调用，并接收插值结果作为参数
- `smoothTime`
	- 完成平滑过渡所需的时间
- `maxSpeed`
	- 在`smoothTime`内允许的最大变化速度
	- 其默认值为`Mathf.Infinity`，即没有速度限制
- `deltaTime`
	- 自上一帧以来的时间差
	- 通常使用`Time.deltaTime`即可
### 限制范围

- `Mathf.Clamp(value,min,max)
	- 想要限制的数值  设定的最小值 最大值
	- 这个方法会确保`value`不会小于`min`也不会大于`max`
	- 如果`value`超出了这个范围，`Mathf.Clamp`会返回边界值；如果`value`在范围内，则返回`value`本身
### 四象限反正切值

- `Mathf.Atan2(x,y)
	- 参数：入两个点的坐标值  
	- 返回值：一个介于`[-π， π]`的弧度值
		- 代表从正X轴到点(x, y)的逆时针角度
- 说人话：**计算两点之间的角度**
	- **极其重要 十分常用**
- [[Unity 知识点#^114e25|作用补充]]

### 注意

- 这种匀速移动，当time>=1时，我改变了目标位置后，它会直接瞬移到我们的目标位置
- 解决
	- `if(nowTarget != target.position){nowTarget =target.position;time =0;startPos = B.position;}time += Time.deltaTime;B.position =Vector3.Lerp(startPos, nowTarget, time);
	- 进行位置判断，把时间和开始位置进行重置

---
# 源代码

![[Mathf 知识点.cs]]

---
# 练习题

![[Mathf 练习题.cs]]

---


