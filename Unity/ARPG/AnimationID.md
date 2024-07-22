---
tags:
  - "#科学/Unity/ARPG/AnimationID"
created: 2024-06-12
更新: 2024-06-13
---

---
# 关联知识点



---
# 使用API

- `Animator.StringToHash("参数名")`
# 代码实例

- `public static readonly int MovementID = Animator.StringToHash("Movement");`
	- `static`
		- 代表无论创建多少个该类的实例，变量的副本仅有一个
	- `readonly`
		- 只读
			- 值 初始化后不可改变
	- `int`
		- **哈希码** 
		- 这个**哈希码** 在Animator控制器中代表特定的动画参数
		- 使得在编程时可以通过数字索引而不必每次都使用字符串
		- 进而优化性能和减少错误
	- `.StringToHash`
		- **字符串参数转换为哈希值（整数ID）**
		- 这在动画状态机中用于高效地引用状态或参数
		- 其返回值是一个整型的**哈希码**
	- `"Movement"`
		- Animator 的参数名

# 外部引用

- `animator.GetFloat(AnimationID.MovementID)`
	- 获取到 Unity 中 Movement 参数 的具体的值

---
# 源代码

![[AnimationID.cs]]

---