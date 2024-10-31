---
tags:
  - "#科学/Unity/ARPG/Manager/GameInputAction"
created: 2024-06-05
---
 
---
# 关联知识点



---
# 事先准备

- 兼容 InputAcion
	- 需要现在包资产中下载 InputSystem 包 
	- 然后在 Edit 中 设置兼容新旧版本的 Input
	- ![[ARPG_InputAcion_设置兼容.png]]
- 创建一个 InputAction 文件，且勾选 Generate C# Class
	- 勾选后会自动创建一个脚本
	- 这个脚本一般不需要管
# 配置信息

- 在 InputAction 文件中 配置信息
	- ![[ARPG_InputAcion_InputAction配置信息.png]]
# 代码脚本

- 写成单例模式
- 这里使用了工具包
## 生命周期函数

- 变量
	- `private GameInputAction _gameInputAction;`
	- GameInputAction 为 InputAction 创建出的关联 C# 脚本的名字
### Awake函数

- 使用 `??=` 来 new 一个对象
	- `_gameInputAction ??= new GameInputAction();`
### OnEnalbe 与 OnDisable()

- 分别启用与禁用
	- 启用
		- `_gameInputAction.Enable();`
	- 禁用
		- `_gameInputAction.Disable();`

## 关联信息

- 将 InputAction 中的资产与脚本中的变量关联
	- `public Vector2 Movement => _gameInputAction.Player.Movement.ReadValue<Vector2>();`
	- `public bool Parry => _gameInputAction.Player.Parry.phase == InputActionPhase.Performed;`
	- `public bool LAttack => _gameInputAction.Player.LAttack.triggered;`
# 更新

- Unity四部曲 ——> Unity进阶 ——> 输入系统InputSystem

- 这个项目中
	- 利用代码将 InputAction 中的按键与变量进行关联
- 当 按下了这个按键后，就会返回一个布尔值（且是通过 `triggered` 返回的布尔值）给关联的变量
	- 或者是 读取输入的值 `ReadValue<Vector2>()`
- 这样做，就可以通过 GameInputManager 实例化后，调用变量，就可以得知 某一个按键是否被按下
	- 或者是 鼠标、键盘 的移动

- 继承单例类

---
# 源代码

![[GameInputManager.cs]]

---