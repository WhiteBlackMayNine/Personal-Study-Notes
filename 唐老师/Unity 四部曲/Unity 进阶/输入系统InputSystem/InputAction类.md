---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/InputAction类
created: 2024-07-30
更新:
---

---
# 关联知识点



---
# 备注

- InputAction为一个挂载的脚本
- 需要搜索并挂载才行
- ![[InputAction.png]]
- 由于声明的 InputAction 不会显示名字
	- 建议使用特性 `[Header("***")]`
	- 添加一个名字
# InputAction是什么

- 顾名思义，InputAction 是 Inputsystem 帮助我们封装的输入动作类
- 它的主要作用，**是不需要我们通过写代码的形式来处理输入**
	- 而是直接在Inspector窗口编辑想要处理的输入类型
- 当输入触发时，我们**只需要把精力花在输入触发后的逻辑处理上**
- 我们在想要用于处理输入动作的类中
- 申明对应的 InputAction 类型的成员变量
	- 注意
		- 需要引用命名空间 UnityEngine.Inputsystem
```C#
public InputAction move;
```

- 写完代码后。Inspectot 窗口上就可以编辑了
	- 编辑的内容就是
	- 这个 InputAction 输入动作是用来监听哪一种输入操作的
# InputAction参数相关
## 点击齿轮
### Actions

- 输入动作设置
- 设置检测哪些输入
- 为之后到底要选择哪一种设备的哪一种数据的筛选
	- 一般情况下，处理按键按下，则选择 Button
		- 获取输入设备的某些指，则选择 Vector
#### Action Type

- 动作类型
##### Value

- 值类型，主要用于**状态连续更改的输入**
	- 例如鼠标的移动，手柄的遥感
- 如果有多个设备绑定这个Action，只会发送其中一个设备（最受控制的）的输入
	- 也就是 `.current` 的那一个设备
##### Button

- 按钮类型，用于每次按下时触发的Action
	- 一般用于键盘、鼠标左右键的点击
##### Pass Through

* 直通类型，和Value一样
* 区别在于如果有多个设备绑定这个Action
	* 会发送所有设备的输入
#### Control Type

- 控制类型
- 在这里选择对应的类型
- 之后在选择对应设备按键相关属性时
- 会根据你选择内容的不同，筛选对应内容
- 这上面显示的内容就是各设备属性的返回值类型
- 当你选择他们后，非选择的类型将不会在之后的按键设置中出现
- 很多内容我们基本用不到
- 相当于是在这里筛选输入设备
##### 说人话

- 选择什么控制类型，就只能选择 返回值为该类型 的设备按键
##### 参数相关

- Any
	- 任何指
- Analog
	- 模拟值，浮点数
- Axis
	- 一维轴浮点数
	- 例如
		- 摇杆输入返回值
- Bone
	- 骨骼
- Digital
	- 数字
- Double
	- 浮点数
- Dpad
	- 4向按钮
	- 例如
		- 摇杆上的D-pad
- Eyes
	- 头戴式VR相关数值
- Integer
	- 整数
- Quaternion
	- 四元数
- Stick
	- 摇杆相关
- Touch
	- 触屏相关
### Interactions

- 相互作用设置
- 用于特殊输入，比如长按、多次点击等等
- 当满足条件时才会触发这个行为
	- 设置长按时间、点击次数等等
#### 三个事件

- 以后在代码中使用
- 开始
	- Started
	- 一按下就会执行 
		- 比如鼠标一按下就会执行
- 触发
	- Performed
	- 满足条件才会触发
		- 比如鼠标需要按下一秒钟后才会执行
- 结束
	- Canceled
	- 按键抬起
- 这三个事件会在对应的时机会执行的事件
	- 可以利用代码往里面添加监听的函数
#### Hold

- 适用于需要输入设备保持一段时间的操作
- 当按钮按下会触发 Started
- 若在松开按钮前，按住时间大于等于 Hold Time
- 则会触发 Performed（时间一到就触发），否则触发 Canceled
#### Tap

- 和Hold相反，需要在一段时间内按下松开来触发
- 当按钮按下会触发 Started
- 若在 Max Tap Duriation 时间内（小于）松开按钮会触发 Performed，否则触发 Canceled
#### SlowTap

- 类似Hold，但是它在按住时间大于等于 Max Tap Duriation 的时候
- 并不会立刻触发 Performed，而是会在松开的时候才触发 Performed
#### MultiTap

- 用作于多次点击，例如双击或者三连击
- Tap Count为点击次数，Max Tap Spacing为每次点击之间的间隔
	- 默认值为 `2*Max TapDuration`
- Max Tap Duration 为每次点击的持续时间，即按下和松开按钮的这段时间
- 当每次点击时间小于 Max Tap Duration，且点击间隔时间小于 Max Tap Spacing
	- 点击 Tap Count 次，触发 Performed
#### Press
- 可以实现类似按钮的操作
##### Press Only

- 按下的时候触发 Started和 Performed
- 不触发 Canceled
##### Release Only

- 按下的时候触发 Started，松开的时候触发 Performed
##### Press And Release

- 按下的时候触发 Started 和 Performed
- 松开的时候会再次触发 Started和 Performed，但不触发 Canceled
##### Press Point

- 在 Input System 中，每个按钮都有对应的浮点值
- 例如普通的按钮，将会在0（未按下）和 （(按下）之间
- 因此我们可以利用这个值（Press Point）来进行区分，当大于等于这个值则认为按钮按下了
### Processors

- 值处理加工设置
- 对得到的值进行处理加工
- 用的比较少
#### Clamp

- 将输入的值钳制到`[min..max]`范围
#### Invert

- 反转控件中的值
	- 即，将值乘以 -1
#### Invert Vector 2

- 反转控件中的值
	- 即，将值乘以 -1
- 如果 InvertX 为真，则反转矢量的 X轴
- 如果 Inverty 为真，则反转矢量的 Y轴
#### Invert Vector 3

- 反转控件中的值
- 即，将值乘以 -1
- 如果反转 X 为真，则反转矢量的 X轴
- 如果反转 Y 为真，则反转矢量的 Y轴
- 如果反转 Z 为真，则反转矢量的 Z轴

#### **Normalize**

- 如果最小值>= 0 ，则将`[min..max]`范围内的输入值规格化为无符号规格化形式`[0,1]`
- 如果最小值< 0 ，则将输入值规格化为有符号规格化形式`[-1,1]`
#### **Normalize Vector 2**

- 将输入向量规格化为单位长度（1）
#### **Normalize Vector 3**

- 将输入向量规格化为单位长度（1）
#### Scale

- 将所有输入值乘以系数
#### Scale Vector 2

- 将所有输入值沿 X轴乘以 X ，沿 Y轴乘以 Y
- 把向量延长的感觉，扩大多少多少倍
#### Scale Vector 3

- 将所有输入值沿 X轴乘以 X ，沿 Y轴乘以 Y ，沿 Z轴 乘以 Z
- 把向量延长的感觉，扩大多少多少倍
#### **Stick Deadzone**

- 摇杆死区处理器缩放Vector2控件（如摇杆）的值
	- 双轴
- 以便任何幅值小于最小值的输入向量都将得到（0,0）
- 而任何幅值大于最大值的输入向量**都将规格化为长度1**
- 许多控件没有精确的静止点
	- 也就是说，当控件位于中心时，它们并不总是精确地报告（0,0）
	- 在死区处理器上**使用最小值可避免此类控件的无意输入**
- 此外，当轴一直移动时，某些控件不一致地报告其最大值
	- 在死区处理器上**使用最大值可确保在这种情况下始终获得最大值**
- **简单来说**
	- 就是有些设备过于灵敏
	- 导致需要设定一个范围，在这个范围内的值都是 0 / 最大值 
		- 0 ~ 0.125 ——> 都视为 0
		- 0.925 ~ 1 ——> 都是为 1
#### **Axis Deadzone**

- axis死区处理器缩放控件的值
	- 单轴
- **使绝对值小于最小值的任何值为 0 ，绝对值大于最大值的任何值为 1 或 -1**
- 许多控件没有精确的静止点、
	- 也就是说，当控件位于中心时，它们并不总是精确报告0
	- 在死区处理器上**使用最小值可避免此类控件的无意输入**
- 此外，当轴一直移动时，某些控件不一致地报告其最大值
	- 在死区处理器上**使用最大值可确保在这种情况下始终获得最大值**
- **简单来说**
	- 就是有些设备过于灵敏
	- 导致需要设定一个范围，在这个范围内的值都是 0 / 最大值 
		- 0 ~ 0.125 ——> 都视为 0
		- 0.925 ~ 1 ——> 都是为 1
## 点击 + 号
### Add Binding

- 添加新的输入绑定
- 单按键输入
### Add Positive\\Negative Binding Or Add 1D Axis Composite

- 添加1D轴组合
- 类似 Input 中的水平竖直热键，返回 -1 ~ 1 之间的一个值
#### 正负按键
##### Negative

- 负面按键，例如 0 ~ -1
##### Positive

- 正向按键，例如 0 ~ 1
#### Composite Type

- 复合类型
##### MinValue

- 最小值
##### MaxValue

- 最大值
#### Which side wins

- 哪一方获胜
- 当同时按下时如何处理
##### Neither

- 双方没有优先权，返回 MinValue 和 MaxValue 的中间值
##### Positive

- 正面优先，返回 MaxValue
##### Negative

- 负面优先，返回 MinValue
### Add Up\\Down\\Left\\Right Composite Or Add 2D Vector Composite

- 添加2D向量组合
- 类似将 Input 中的水平竖直热键组合在一起，得到的 Vector 中的 x，y 分别表示两个轴
- 比如 键盘控制移动方向 WASD
	- 并且选择 Digital Normalized
		- 单位化向量
#### 四方向

- Up
	- 上
	- (0，1)
- Down
	- 下
	- (0，-1)
- Left
	- 左
	- (-1，0)
- Right
	- 右
	- (1，0)
#### Composite Type

- 复合类型
- 同 Add Positive\\Negative Binding Or Add 1D Axis Composite
#### Mode

- 处理模式
##### Analog

- 模拟值，浮点值
##### Digital Normalized

- 单位化向量
##### Digital

- 未单位化的向量
### Add Up\\Down\\Left\\Right\\Forward\\Backward Composite

- 添加3D向量组合
- 类似 Input 中 得到 x，y，z 三个轴
- 使用与 2D的 十分相似，只是多一个 Z轴
### Add Button With One Modifier Composite

- 添加带有一个复合修改器的按钮
- 可以理解为双组合键，比如Ctrl+C、Ctrl+V
#### 参数相关

- Modifier
	- 复合输入内容
- Buttsn
	- 输入内容
- 举例
	- 复制按键 Ctrl+C
		- Ctrl 为 Modifier 
		- C 为 Button
### Add Button With Two Modifier Composite

- 添加带有两个复合修改器的按钮
- 可以理解为三组合键，比如 Ctrl+K+U
- 与 Add Button With One Modifier Composite 的使用十分相似
	- 只是多了一个键
## 创建一种输入后
### Path

- 从哪个控件接受输入
### Usages

- 常用用法
### GamePad

- 游戏手柄
### JoyStick

- 操纵杆
### Keyboard

- 键盘
### Mouse

- 鼠标
### Pen

- 笔
### Pointer

- 指针
### Sensor

- 传感器
### TouchScreen

- 触屏
### Tracked Device

- 履带装置
### XR Controller

- XR 控制器
### XR HMD

- XR 头戴显示器
### Other

- 其它
## 总结

- 先点击 + 号，选择 所需要的功能
	- 会创建一个竖直蓝条 `<No Binding>` 无绑定
	- 双击之后会进行 选择 Path 进行绑定按键
- Path 的作用 ——> 具体关联 输入动作 到底是哪一个设备的哪一个键
- 齿轮 决定 具体能选什么设备以及上面的按键
# InputAction的使用

- 在 InputAction是什么 中 已经声明了一个 `move`
- 这里用其进行示例说明
## 启用输入检测

- 执行过后，这个输入（InputAction）就会被启用
- 当它进行了输入后，只有被启用了，才能去调用事件函数
	- 操作监听相关
```C#
move.Enable();
```
## 操作监听相关

- 可以使用 Lambad 表达式，或者在外部声明函数
- 如果是 外部声明函数 的话
	- 它的变量类型为 `CallbackContext`
	- 但需要使用点运算符点出来
		- `private void TestFun(InputAction.CallbackContext context){  }`
- 这三个事件就是 InputAction参数相关 ——> 点击齿轮 ——> Interactions 中的 三个事件
	- Start 、Performed、Canceled
### 开始操作

```C#
move.start += ***(函数名);
```
### 真正触发

```C#
move.performed += ***(函数名);
```
### 结束操作

```C#
move.canceled += (context)=>{  };//context为 InputAction.CallbackContext 类型的参数
```
## 关键参数 Callbackcontext

- `context` 为 InputAction.CallbackContext 类型的参数
- 如果觉得不够用，可以直接 F12 进去看
### 当前状态

- 为一个枚举
	- `Disabled = 0`
		- 未启用
	- `Waiting = 1`
		- 等待
	- `Started = 2`
		- 开始
	- `Performed = 3`
		- 触发
	- `Canceled = 4`
		- 结束

```C#
context.phase
```
### 动作行为信息

- 代码中的 `action` 其实就是就是上面声明的 InputAction 对象
	- `move` 对象
```C#
context.action.name;
```
### 控件信息

- 得到使用的设备
```C#
context.control.name;
```
### 获取值

- 获取什么类型的值，就在泛型里面传什么类型
```C#
context.ReadValue<Vector>();
```
### 持续时间

```C#
context.duration
```
### 开始时间

```C#
context.startTime
```
# 特殊输入设置

- InputAction Packgae 的设置
## Default Deadzone Min

- 默认死区最小值
## Default Deadzone Max

- 默认死区最大值
## Default Button Press Point

- 默认按钮按下点
## Button Release Threshold

- 按钮释放阈值
## Default Tap Tlme

- 默认点击时间
## Default Slow Tap Time

- 默认慢速点击时间
## Default Hold Time

- 默认保持时间
## Tap Radius

- 点击半径
## MultTap Delay Tlme

- 多次点击延迟时间

---
