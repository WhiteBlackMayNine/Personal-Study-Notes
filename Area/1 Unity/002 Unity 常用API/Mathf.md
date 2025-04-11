---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Mathf"
created: 2024-04-03
---

---
# 关联知识点



---
# 知识点

## 静态变量
### 度到弧度换算常量

- `Mathf.Deg2Rad
### 弧度到度换算常量

- `print(Mathf.Rad2Deg+ ",");
### 正无穷大的表示形式

- `print(Mathf.Infinity+"");
### 负无穷大的表示形式

- `print(Mathf.NegativeInfinity + "");
### PI

- `print(Mathf.PI);
## 静态函数
### 绝对值

- `Mathf.Abs( 值 )
### 反余弦

- `Mathf.Acos(1)
	- 以弧度为单位
### 最大整数

- `Mathf.Floor(2.74f)`
	- 小于或等于 2.74 的最大整数
	- 返回值为 浮点型
- `Mathf.FloorToInt(2.74f)
	- 返回值为 整型
### 线性插值

- `Mathf.Lerp( a,b,0.5f)
	- 参数 a 和 b 之间，按照 参数 t 进行线性插值
	- t 范围为 0~1
		- 大于1 按照 1 计算
		- 小于0 按照 0 计算
- `Mathf.LerpUnclamped(1, 2, -0.5f)
	- 与上面的区别
		- t 可以在任何范围内
			- t为负数
				- 会返回一个比 a 还小的值
			- t大于1
				- 会返回一个比 b 还大的值
## 倒计时

- `Mathf.MoveTowards(float current, float target, float maxDelta)`
	- **让当前值（current）向目标值（target）靠近，但确保每步变化量不超过 maxDelta

---
# 源代码

![[No12_Mathf.cs]]

---