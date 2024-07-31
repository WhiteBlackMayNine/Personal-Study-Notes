---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/概述与导入/导入InputSystem
created: 2024-07-29
更新:
---

---
# 关联知识点



---
# InputSystem包导入

- PackageManager中导入 Input System
## 步骤

- PackageManager ——> 选择 Unity 注册的包 ——> 搜索 InputSystem
- 安装后会弹出一个弹窗，大致意思就是
	- 是否启用新输入系统，启用 Yes 然后禁用老输入系统
# 选择开启哪一种输入模式

- 选择 InputSystem和 老InputManager 的启用情况
- File ——> Build setting ——>Player Setting ——> Other ——> Active Input Handling
- 可以同时启用也可以只启用其中之一
- 每次启用后会重启 Unity

---
