---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/PlayerInput/认识PlayerInput
created: 2024-07-31
更新:
---

---
# 关联知识点



---
# PlayerInput 是什么

- PlayerInput 是 InputSystem 提供的
- 专门用于接受玩家输入来处理自定义逻辑的组件
## 主要工作原理

- 配置输入文件（InputActions文件）
- 通过 PlayerInput 关联配置文件，它会自动解析该配置文件
- 关联对应的 响应函数 ，处理对应逻辑
## 好处

- 不需要自己进行相关输入的逻辑书写
- 通过配置文件即可配置想要监听的对应行为
- 让我们专注于输入事件触发后的逻辑处理
## 简而言之

- 之前学习了配置文件与配置文件生成代码，然后去 new 出一个对象
- 然后进行逻辑的处理
- PlayerInput 的出现，让 生成代码、去 new 对象，的步骤都可以省略
- 只需要通过 PlayerInput 去关联一个输入配置文件，然后直接去书写处理逻辑就行了
- 把工作重心完全放在了输入事件触发后的逻辑处理上
# 添加 PlayerInput 组件

- 选择任意对象
	- 一般为一个玩家对象
- 为其添加 PlayerInput 组件
# PlayerInput 参数相关
## Actions

* 行为
* 一套输入动作和玩家相关联，帮助我们监听一些按键的输入
### Default Control Scheme

- 默认启用哪一个控制方案
### Default Actions Map

- 默认启用哪一个行为映射方案
## UI Input Moudle

- 关联 新输入系统的 UI 模块
- 但程序一般不会管这个
## Camera

- 关联摄像机，当分屏设置时才需修改此选项
- 双人游戏，屏幕一分为二
## Behavior

- 如何通知游戏对象上执行对应逻辑
### SendMessage

- 将逻辑脚本挂载在和 PlayerInput 同一对象上，会通过 SendMessage 通知执行对应函数
### BroadcastMessage

- 将逻辑脚本挂载在其自身或子对象上
- 会通过 BroadcastMessage 通知执行对应函数
### Invoke UnityEvent Actions

- 通过拖拽脚本关联函数指明想要执行的函数逻辑
### Invoke CSharp Events

- 通过 CSharp事件 监听处理对应逻辑，通过获取 Playerlnput 进行事件监听

---
