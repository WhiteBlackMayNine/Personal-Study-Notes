---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/导入与概述/InputSystem总结
created: 2024-08-01
更新:
---

---
# 关联知识点



---
# InputManager 和 InputSystem 如何选择
## 老输入系统

- InputManager 通过代码检测 鼠标、键盘、触屏、摇杆等等输入
## 新输入系统 InputSystem 

- 通过配置文件检测各种输入设备的输入
- 新输入系统相对老输入系统更强大、更易用、兼容性更强
- 强大
	- 代码、配置都能用
- 易用
	- 配置更简单
- 兼容性强
	- 可以一次设置好各种设备输入
# 选择建议

- 游戏操作主要通过 UI 进行操的游戏（手游、页游）
	- 选择老输入系统 InputManager
- 游戏操作主要通过 各设备（手柄、鼠标键盘）进行操作的游戏（PC、主机端游）
	- 选择新输入系统InputSystem


---
