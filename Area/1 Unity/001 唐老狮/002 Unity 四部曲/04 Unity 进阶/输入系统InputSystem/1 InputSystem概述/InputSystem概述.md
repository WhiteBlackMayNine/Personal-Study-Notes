---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/导入与概述/InputSystem概述
created: 2024-07-29
更新:
---

---
# 关联知识点



---
# InputSystem 是什么

- InputSystem 是 Unity 提供的种新的输入系统
- 最新 InputSystem 的运行环境需要 
	- Unity 2019.4 以上版本 + .NET 4 runtime
- 它相对于老的输入系统更具拓性和可自定义的替代方案
## 说人话

- InputSystem 是 Unity 提供的一套更好用更方便的系统
	- 用于检测键盘、鼠标、手柄、摇杆等等设备输入
# InputSystem 和 InputManager 的区别
## InputManager

- InputManager 是 Unity 的老输入系统
	- [[输入相关 Input]]
## 新老输入系统的区别
### 老输入系统

- 我们需要自己写各种检测代码来判断设备输入，并处理对应逻辑
### 新输入系统

- 不仅可以像老输入系统一样使用，还增加了输入配置的概念
- 新输入系统将输入操作进行封装
	- 让我们可以在 Unity 内进行 **输入配置文件编辑**
	- **我们不需要写代码来判断设备输入，只需要把工作重心放在逻辑处理上**
- **输入配置文件 就是 新老输入系统的区别 最大的区别**
#### 配置文件

- Creat ——> InputActions
	- 本质是个 Json 文件
- 挂载脚本 PlayerInput
	- 将 InputActions挂载
	- 脚本就会解析配置文件，判断是哪一种输入
#### 另一种方法

- 创建一个 [[GameInputManager]]
- 利用 => 将变量进行关联
- 就可以在别的脚本里进行使用
- **具体实现内容在 Unity ——> ARPG 中**
# 总结
## InputSystem是什么

- Unity 提供的新的用于检测玩家设备输入的系统
## InputSystem 和 InputManager 的区别

- InputSystem让我们可以专注于功能逻辑开发
- 不用操心输入检测相关代码

---
