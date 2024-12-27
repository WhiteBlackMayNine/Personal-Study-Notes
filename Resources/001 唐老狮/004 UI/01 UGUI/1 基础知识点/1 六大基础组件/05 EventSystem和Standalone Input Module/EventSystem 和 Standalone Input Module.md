---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-11
---

---
# 关联知识点



---
# EventSystem组件用来干啥

- Event System意思是事件系统
- 它是用于管理玩家的输入事件并分发给各UI控件
- 它是事件逻辑处理模块
- 所有的UI事件都通过EventSystem组件中轮询检测并做相应的执行
- 它类似一个中转站，和许多模块一起共同协作
- 如果没有它，所有点击、拖曳等等行为都不会被响应
# EventSystem组件参数

- First Selected
	- 首先选择的游戏对象，可以在游戏一开始时 设置 默认选择
		- ——> 游戏一开始，就会选择一个默认UI
- Send Navigation Events
	- 是否允许导航事件（移动/按下/取消）
		- ——> 勾选后，可以使用 WASD（或者小键盘的上下左右）切换选择的UI
		- ——> 回车 相当于 触发 UI
- Drag Threshold
	- 拖拽操作的阈值（移动多少像素算拖拽）
# Standalone Input Module组件用来干啥的

- Standalone Input Module意思是独立输入模块
- 它主要针对处理 鼠标/键盘/控制器/触屏 （新版Unity）的输入
- 输入的事件通过 EventSystem 进行分发
- 它依赖于 EventSystem 组件，他们两缺一不可
# Standalone Input Module组件参数

- Horizontal Axis
	- 水平轴按钮对应的热键名（该名字对应Input管理器）
- Vertical Axis
	- 垂直轴按钮对应的热键名（该名字对应Input管理器）
- Submit Button
	- 提交（确定）按钮对应的热建名（该名字对应Input管理器）
- Cancel Button
	- 取消按钮对应的热建名（该名字对应lpput管理器）
- Input Actions Per Second
	- 每秒允许键盘/控制器输入的数量
- Repeat Delay
	- 每秒输入操作重复率生效前的延迟时间
- ForceModule Active
	- 是否强制模块处于激活状态

- 除此之外
	- 在 Inspector 窗口最下面，还有一个调试窗口
	- 打开后可以看见 鼠标位置、偏移量、当前选择目标等
# 注意

- 确保 Canvas 中的 Graphic Raycaster 组件已经激活

---