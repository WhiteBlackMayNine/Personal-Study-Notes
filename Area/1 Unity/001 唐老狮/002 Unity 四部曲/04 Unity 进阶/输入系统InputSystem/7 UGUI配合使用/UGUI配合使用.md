---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/UGUI配合使用
created: 2024-07-31
更新:
---

---
# 关联知识点



---
# InputSystem对UI的支持

- 新输入系统 InputSystem 不支持 IMGUI(GUI) 
	- 注意
		- 编辑器代码不受影响
- 如果当前激活的是 InputSystem，那么 OnGUI中的输入判断相关内容不会被触发
- 你必须要选择 Both 或者只激活老输入系统 InputManager 才能让 OnGuI 中内容有用
- 新输入系统支持 UGUI
	- 但是需要使用新输入系统输入模块
		- Input system UI Input Module
# UGUI中的新输入系统输入模块参数相关

- 如有需要，自行查看文档或视频
# VR相关中使用新输入系统注意事项

- 如果想在VR项目中使用新输入系统配合UGUI使用
- 需要在 Canvas 对象上添加 Tracked Device Raycaster 组件
# 多人游戏使用多套UI

- 如果同一设备上的多人游戏，每个人想要使用自己的一套独立UI
- 需要将 EventSystem 中的 EventSystem 组件替换为 Multiplayer Event System 组件
- 与Eventsystem组件不同，可以在场景中同时激活多个 MultiplayerEventsystem
- 这样，您可以有多个玩家
- 每个玩家都有自己的 InputSystemuIInputModule 和 MultiplayerEventsystem 组件
- 每个玩家都可以有自己的一组操作来驱动自己的UI实例
- 如果您正在使用 PlayerInput 组件
- 还可以设置 PlayerInput 以自动配置玩家的 InputSystemUIInputModule以使用玩家的操作
- MultilayerEventSystem 组件的属性与事件系统中的属性相同
- 此外，MultiplayerEventsystem 组件还添加了一个 playerRoot 属性
- 您可以将其设置为一个游戏对象
- 该游戏对象包含此事件系统应在基层次结构中处理的所有UI可选择项
## 说人话

- EventSystem 组件替换为 Multiplayer Event System 组件
	- 并且 添加 新输入系统模块 UI
		- Input System Ul input Module (Script)
# 0n - Screen组件相关

- On - Screen 组件可以模拟 UI 和用户操作的交互
## 0n - Screen Button

* 按钮交互
## 0n - Screen Stick

- 摇杆交互
# 更多内容

- 可查看官方文档了解更多新输入系统和UI配合使用的相关内容
- [官方文档](ttps://docs.unity3d.com/Packages/com.unity.inputsystem@1.2/manual/uIsupport.html)

---
