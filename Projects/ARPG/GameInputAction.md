---
tags:
  - "#科学/Unity/ARPG/GameInputAction"
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
	- ` public bool Parry => _gameInputAction.Player.Parry.phase == InputActionPhase.Performed;`
	- `public bool LAttack => _gameInputAction.Player.LAttack.triggered;`

---
# 源代码

- 更新时间为 2024/6/5
- ![[GameInputManager.cs]]

---