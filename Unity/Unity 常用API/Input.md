---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Input"
created: 2024-04-02
---

---
# 关联知识点

[[输入相关 Input]]
按键 Input 可以自己修改

---
# 知识点

## 键盘连续检测（移动）

- 一般用于处理移动
- WASD
### 水平方向轴值

- `Input.GetAxis("Horizontal")
- -1 ~ 1
- 一般是 按下按键后 过一段时间开始运动
### 垂直方向轴值

- `Input.GetAxis("Vertical")
- -1 ~ 1
- 一般是 按下按键后 过一段时间开始运动
### 边界轴值

- `Input.GetAxisRaw("Horizontal")
- `Input.GetAxisRaw("Vertical")
- -1 0 1
- 一般是 按下按键就运动
## 鼠标连续检测（移动）
### 水平移动增量

- `Input.GetAxis("Mouse X")
### 水平移动增量

- `Input.GetAxis("Mouse Y")
## 鼠标连续检测（事件）

- `Input.GetButton( 0 / 1 / 2)
	- 也可以填入 `Fire1 / Fire2
	- 也可以填入其他按键名称
## 间隔检测（事件）

- 一般用于检测事件
- 且一般与 `if` 语句连用
### 按下后检测一次

- `Input.GetButtonDown()
	- **指定的虚拟按钮**
- `Input.GetKeyDown(KeyCode.Q)` ——> 按下Q键
	- 键盘
### 抬起后检测一次

- `Input.GetButtonUp()
	- **指定的虚拟按钮**
- `Input.GetKeyUp()`
	- 键盘
### 任意按键

- `Input.anyKeyDown
### 鼠标左右键

- `Input.GetMouseButton( 8 / 1 / 2 )

---
# 源代码

![[No8_Input.cs]]

---