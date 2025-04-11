---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/PlayerInput/PlayerInputManager
created: 2024-07-31
更新:
---

---
# 关联知识点



---
# PlayerInputManager 的作用

- PlayerInputManager 组件主要是用于管理本地多人输入的输入管理器
	- 做多人游戏的
	- 会自动启用（并检测）不同的设备输入
		- 键盘控制一个，摇杆控制一个
		- 由谁创建的，谁控制
		- 一句话总结
			- 按设备去创建对象
	- 需要在配置文件中提前写好
- 它主要管理玩家加入和离开
# 组件添加及参数相关
## 组件添加

- 在空对象上，添加组件 PlayerInputManager
## 参数相关
### Notification Behavior

- 当玩家进入时 PlayerinputManager 如何通知关联的对象
- 它的工作方式和 Playerlnput 相同
### Join Behavior

- 玩家加入的机制
#### Join Players When Button ls Pressed

- 当有新设备加入按下任意键或者没有任何玩家时按下任意键
#### **Join Players When Join Action IsTriggered**

- 当有新设备加入按下指定按键触发玩家加入
- **一般用这个**
### Join Players Manually

- 不要自动加入玩家，需要自己手动加入玩家
### Player Prefab

- 挂载 PlayerInput 组件的游戏对象
- **必须是有 PlayerInput 组件的游戏对象**
	- 否则会报错
### Joining Enabled By Default

- 启用后，新加玩家按照 JoinBehavior 的规则加入
### Limit Number Of Players

- 启用后，可以限制加入游戏的玩家数量
### Max Player Count

- 允许参加游戏的最大玩家数
### Enable Split Screen

- 如果启用，会自动为每个对象分配可用屏幕区域的一部分，用于多人分屏游戏
- 注意
	- 在 PlayerInput 脚本中，需要将摄像机拖入到 Camera 中进行关联
#### Maintain Aspect Ratio

- 假值使游戏能够生成屏幕区域，其纵横比与细分屏幕时的屏幕分辨率不同
### Set Fixed Number

- 如果该值大于零，则 PlayerInputManager 始终将屏幕分割为固定数量的矩形
- 而不考虑实际的玩家数量
#### Screen Rectangle

- 可用于分配播放器拆分屏幕的规范化屏幕矩形
# PlayerInputManager使用

- 这里主要讲的是 Invoke CSharp Events 模式下
- 利用代码进行监听
## 获取 PlayerInputManager

- `PlayerInputManager.instance`
## 玩家加入时

```C#
PlayerInputManager.instance.onPlayerJoined += (playerInput) => {   };
```
## 玩家离开时

```C#
PlayerInputManager.instance.onPlayerLeft += (playerInput) => {   };
```

---
