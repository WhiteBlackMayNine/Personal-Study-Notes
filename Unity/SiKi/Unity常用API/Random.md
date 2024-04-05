---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Component/Random"
created: 2024-04-03
---

---
# 关联知识点



---
# 知识点

## 静态变量

- `Random.rotation
	- 随机出的旋转数是
		- 以四元数形式表示
- `Random.rotation.eulerAngles
	- 四元数转换成欧拉角
- `Quaternion.Euler(Random.rotation.eulerAngles
	- 欧拉角转四元数
- `Random.value
	- 随机出`[0,1]`之间的浮点数
- `Random.insideUnitCircle
	- 在（-1，-1）~（1,1）范围内随机生成的一个 Vector2
- `Random.state
	- 当前随机数生成器的状态
## 静态函数

- `Random.Range(0,4)
	- 在区间`[0,4)`
	- 整形重载包含左 Min ，不包含右 Max
- `Random.Range(0, 4f)
	- 在区间`[0,4)`
	- （浮点形重载包含左 Min ，包含右 Max 
- `Random.InitState(1);
	- 用于控制随机数生成器行为的方法

---
# 源代码

![[No13_Random.cs]]

---