---
tags:
  - "#Unity"
  - "#BehaviorTree"
created: 2025-04-02
---

---
# 关联知识点



---
# Task 界面

- 可以分成四个不同的主题
	- Composites 复合
	- Decorator 装饰
	- Action 动作
	- Conditional 条件
## Action

- 是最常用的一个节点
- 游戏对象/状态 执行一个动作
## Conditional

- 检查 游戏对象/状态 的条件
	- ~~就相当于 `if - else`~~
- 如果 Conditional 左侧已经满足，那么就不会去执行右侧的节点
## Composites 

* 用来管理字节点的
	* ~~相当于父节点~~

- 一个杆 ——> 逐个执行
- 三个杆 ——> 同步执行
## Decorator

- 本节课不涉及

---
# 源代码



---