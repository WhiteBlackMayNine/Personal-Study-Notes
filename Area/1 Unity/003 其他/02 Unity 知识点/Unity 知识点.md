---
tags:
  - 科学/Unity/Unity小知识点
备注: 日积月累 厚积薄发
核心: 游戏是数据的一种表现
---

---

##Unity中打印信息的两种方式

- 没有继承MonoBehavior类的时候
	- `Debug.Log();`
- 继承了MonoBehavior有一个线程的方法可以使用
	- `print(1234567);`
---
## 通过 `.position` 得到的位置是相对于世界坐标系的原点位置

- 可能和面板上显示的不一样
	- 因为如果对象有父子关系并且父对象位置不在原点，那么面版上的信息肯定不一样
---
## 面朝向

- Z轴朝向 / Z轴与X轴朝向
- 注意坐标系的不同，面朝向的方向可能也不同
---
## 旋转轴

- X轴 ——> 抬头低头
- Y轴 ——> 转身
- Z轴 ——> 偏头
---
## 角度增量

- 目标和物体位置向量差除以每帧转动速度就是该帧要转动的角度增量
---
## Find 查找

- Find 查找是可以找到失活对象的，但 GameObject 却不可以
---
## 摄像机

- 可以通过 **清除标志** 、**剔除遮罩** 、**深度** ，一起使用来使多个摄像机叠加显示
---
## 物理帧与物理系统

- 物理帧更新时间直接影响物理系统
---
## 碰撞产生的必要条件

^5ece43

- 条件
	- 两个物体都有碰撞器
	- 至少一个物体有刚体
- 碰撞器表示物体的体积（形状）
- 刚体会利用体积进行碰撞计算，模拟真实的碰撞效果，产生力的作用
	- 碰撞器的体积 与 模型体积 概念不同
	- 碰撞为 碰撞器体积 进行计算，与模型体积无关
---
## 刚体加力

- 通过一个脚本里面去加一个方法，这个方法只会影响调用脚本的这个对象，不会影响全局
---
## 动量

- 力 * 时间 = 质量 * 速度

- ![[当前物体.jpg]]

- ![[运动的方向.jpg]]
---
## 当得到一个物体时，只要得到任意一个信息就可以得到所有信息

- `obj.collider.gameobject.name`
	- 只得到了碰撞器信息，但依旧可以使用`gameboject`来`.name`获取名字
---
## Transform 与 Vector3

- `Transform`组件代表一个游戏对象在世界坐标系中的位置和方向
- `Vector3`是一个三维向量，用于表示空间中的一个点或者一个方向
- 游戏对象（ Gameobject）位移、旋转、缩放、父子关系、坐标转换等相关操作都由 Transform 处理，是一个极其重要的类
- - `this.transform.position` 类型为 `Vector3` 即 一个点
---
## 补充知识 调试画线 ^df038c

### 画线段 ^e8db75

- `Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward,Color.red);
	- 起点 终点
### 画射线 ^0124f8

- `Debug.DrawRay(this.transform.position,this.transform.forward,Color.white);
	- 起点 方向
---
## 欧拉角不是运动是变换

- [无伤理解欧拉角中的“万向死锁”现象\_哔哩哔哩\_bilibili](https://www.bilibili.com/video/BV1Nr4y1j7kn/)
- [[1 为何使用四元数#^20329c|欧拉角]]
- [[3 角度和旋转]]
- [[2 四元数是什么#^02493d|弥补欧拉角的缺点]]
---
## 协同程序

- [[2 协同程序]]内可以写死循环
- [[2 协同程序]]一般不会写在 `Update`
- [[2 协同程序]]直接调用是没用的，但可以被一个对象接受
	- `IEnumeratorie = Test();
	- 也可以使用`.MoveNext()  .Current`手动遍历
		- 前者返回布尔值，后者为当前值
		- `.MoveNext()` 代表是否到了结尾（迭代器函数是否执行完毕） 
	- [[迭代器]]
- [[2 协同程序]] ^78096b
	- 分为两部分
		- 第一部分
			- 用协程函数本身（就是迭代器函数）
		- 第二部分
			- Unity 内部只要继承了 MonoBehavior 它其实就有自己的一个协程协调器
		- 第一部分 迭代器函数 决定分步进行
			- 通过 `yield return` 实现分步进行
		- 第二部分 协程协调器 决定分时进行
			- 通过 `yield return` 的返回值达到一个分时的作用
	- 总结
		- 迭代器函数 当遇到`yield return`时
		- 就会 停止执行之后的代码
		- 然后 协程协调器 通过得到 返回的值 去判断 下一次执行后面的步骤 将会是何时
---
## 场景转换

- `DontDestroyonLoad(this.game0bject);
	- 该脚本依附的对象 过场景时 不会被 移除DontDestroyonLoad(this.game0bject);
- [[场景异步加载]]
---
## 物理系统碰撞检测

### 必要条件

- 至少一个物体有刚体
- 两个物体都必须有碰撞器
### 碰撞和触发

- 碰撞会产生实际的物理效果
- 触发看起来不会产生碰撞但是可以通过函数监听触发
- 碰撞检测主要用于实体物体之间产生物理效果时使用
---
## 重要知识点：关于层级 ^bc8e6a

- 通过名字得到层级编号 `LayerMask.NameToLayer`
	- `1 << LayerMask.NameToLayer("层级名")
- 我们需要通过编号左移构建二进制数
- 这样每一个编号的层级 都是 对应位为 1 的 二进制数
- 我们通过 位运算 可以选择想要检测层级
- 好处 一个`int`就可以表示所有想要检测的层级信息
- 层级编号是 0~31 刚好32位
	- 是一个`int`数
	- 每一个编号 代表的 都是二进制的一位
		- 0 - 1 << 0 - 0000 0000 0000 0000 0000 0000 0000 0001 = 1
		- 1 - 1 << 1 - 0000 0000 0000 0000 0000 0000 0000 0010 = 2
		- 2 - 1 << 2 - 0000 0000 0000 0000 0000 0000 0000 0100 = 4
		- 3 - 1 << 3 - 0000 0000 0000 0000 0000 0000 0000 1000 = 8
		- 4 - 1 << 4 - 0000 0000 0000 0000 0000 0000 0001 0000 = 16
		- 每一个层级，都在不同位置上有且仅有一个 1
	- 检测多层时，只需要进行一个位运算 [[5 位运算符]]
		- 比如进行一个 或运算 
	- 就可以通过一个数字表示 n层 的一个组合
		- `1 << LayerMask.NameToLayer("层级名1") | 1 << LayerMask.NameToLayer("层级名2")
	- 在 Unity 内部，会把 或运算 的结果，把每一层的二进制数进行 与运算
		- 得到的结果只要不为 0，就会认为 **要检测** 这个层
	- 也可以进行 异或运算 结果为 0 的为 **不要检测的层级**
	  ```C#
		// 创建一个LayerMask，包含所有你想要检测的层级
		int layerMask = 1 << LayerMask.NameToLayer("层级名1") | 1 << LayerMask.NameToLayer("层级名2");
		// 使用Physics.OverlapBox进行检测，传入你的LayerMask
		Collider[] hitColliders = Physics.OverlapBox(center, halfSize, transform.rotation, layerMask);
		// 使用位运算排除某一层级
		layerMask = ~(1 << LayerMask.NameToLayer("不要检测的层级"));
		//也可以使用位运算中的异或运算排除某一层级
		layerMask ^= 1 << LayerMask.NameToLayer("不要检测的层级");
		// 再次进行检测，这次排除了"不要检测的层级"
		hitColliders = Physics.OverlapBox(center, halfSize, transform.rotation, layerMask);
		```
---
## Unity 基础总结

- 向量和四元数
	- 游戏中移动旋转都和它有关
- 协程
	- 可以分时分步处理逻辑，避免卡顿范围检测
	- 动作游戏必备
- 射线检测
	- 交互功能必备
- 资源场景的同步异步加载
	- 所有功能必备
---
## 对象间的父子关系

- 在 Hierarchy 中将一个对象拖动到另一个对象上而形成的树形结构
- **子对象会随着父对象的变化而变化**
- **子对象的 Inspector 窗口中 Transform 信息是相对父对象的**
---
## 值类型赋值相关

- 用属性和方法返回的结构体是不能修改其字段的
- 直接访问公有的结构体是可以修改其字段的
- 本质上就是值类型的浅拷贝和深拷贝，导致内存空间不同
## `[RequireComponent]

- 确保在将脚本附加到游戏对象时，该对象已经包含了脚本所需的特定组件
	- `[RequireComponent( typeof ( CharacterController ) ) ]`
---
## Mathf.Atan2(x,y)

^114e25
- **角色控制**
	- 在处理角色移动和转向时，你可以使用`Mathf.Atan2`来计算角色应该面向的方向
	- 例如，当玩家按下移动键时，你可以用它来确定新的转向角度，使角色朝向正确的方向移动
- **相机控制**
	- 对于摄像机的控制同样可以应用`Mathf.Atan2`
	- 在追逐目标或者跟随路径时
	- 计算所需的旋转角度来调整相机的方向
- **物体定位**
	- 如果你需要将一个物体放置在特定的角度
	- 比如对准某个目标或者与某个平面垂直
	- `Mathf.Atan2`可以帮助你计算出正确的角度值
- **导航系统**
	- 在开发游戏的AI导航系统时
	- `Mathf.Atan2`可以帮助AI确定其朝向目标的准确角度
---
## Mathf.SmoothDampAngle

^42a029
### 使用场景

- 在游戏中实现平滑的相机跟随效果
	- 使玩家体验更加自然流畅
- 对于需要在用户输入和实际游戏响应之间进行平滑处理的情况
	- 如缓慢移动角色或物体以响应玩家的指令
---
## Animator / Animation

- 设置参数 （`SetXX`）时
	- `Animator.SetFloat(string name, float value, float dampTime, float deltaTime)
		- 参数的名称
		- 要设置的新值
		- 达到目标值的时间
			- （阻尼时间）
		- 帧的增量时间
			- （通常用于计算参数值的变化速率，以便在动画过渡中实现更平滑的效果）
---
## Input Action Asset 

- 在Unity中用于**管理和存储用户输入的绑定关系**
---
## Quaternion.identity

- **一个没有应用任何旋转的四元数**
### 旋转**

- 通常用于将物体的旋转归零，即恢复到没有应用任何旋转的状态
- 这在编程中常常用于初始化场景或者重置物体的旋转状态
### 性能优化

- 可以避免不必要的计算
- 特别是在不需要改变对象旋转的情况下
- 直接应用`Quaternion.identity`可以保持当前的旋转状态不变
### 父子层级关系

- 在处理带有父子层级关系的物体时，如果父物体不可见，子物体虽然可能activeSelf为true（自身可见），但activeHierarchy只能为false（层级不可见）
- **在这种情况下，`Quaternion.identity`可以确保子物体的旋转状态不受父物体影响**
### 确切的值类型

- 与transform.rotation属性不同，`Quaternion.identity`是一个确切的值，表示没有旋转
- 而transform.rotation是物体当前的角度属性，其值随着物体旋转而变化

## `Quaternion.LookRotation` 

- 用于**生成一个四元数，该四元数表示将给定的向量（通常是Z轴）对齐到另一个目标向量的过程所产生的旋转**
---
## CreateAssetMenu

- 用于在 右键菜单中 添加
- `[CreateAssetMenu(fileName = "BaseHealthData", menuName = "Create/Character/HealthData/BaseData", order = 0)]`
- ![[Unity右键菜单添加.png]]
---
## Physics.Raycast 和Physics.CheckSphere 的区别

- **Physics.Raycast
	- 是一种射线检测方法用于检测从起点到指定方向的一条射线上是否有任何碰撞体相交
	- 这个方法通常用来处理视线范围内的物体检测
	- 如射击游戏中的子弹击中检测或者鼠标点击物体等场景
- **Physics.CheckSphere**
	- 是用来检测世界坐标系中由位置（position）和半径（radius）界定的球体内是否有任何碰撞体的重叠
	- 当需要检测一个区域内是否存在物体时
	- 比如角色周围的敌人检测或触发某些事件，就可以使用这个方法
- Physics.Raycast 用于**直线检测**，而 Physics.CheckSphere 用于 **球形区域** 的检测
---
## Sprite

- 2D游戏里面的图片，都和 Sprite 有关
---
## Tilemap
### 等距瓦片地图

- 想要轴心点单个排序，渲染模式为 Individual
- 否则就是整体排序
---
## .meta 配置文件

- 里面保存着 文件的信息
- 如果删除，那么这个文件的信息就会被删除
	- 删除骨骼、动画
---
## 2D骨骼

- 在添加骨骼时，应当熟练使用隐藏图层功能
- 蒙皮后细微调整时，建议使用 Bone Influence
---
## 配置文件特点

- 只会去用，不会去修改
---
## SRGB

- 是微软联合惠普、三菱、爱普生等厂商联合开发的通用色彩标准
- 它的主要作用就是避免在不同设备出现色差般
---
## Unity 旋转

- Unity采用左手坐标系 ^b95203
- 大拇指指向旋转轴的正方向时，其余四指的弯曲方向即为正旋转方向
- Y轴旋转为例
	- 拇指指Y轴正方向，四指 指的方向为正方向
---
## ScriptableObject

^afe6ee

- 创建两个不同的文件的好处
	- 例如 ARPG 中
	-  [[CharacterHealthInfo]] 与 [[CharacterHealthBaseData]]
	- [[CharacterComboSO]] 与 [[CharacterCombatBase]]

- 通过分离基础数据和实例数据，使得数据结构更加清晰
	- `CharacterHealthBaseData` 与 `CharacterCombatBase` 包含数据的基本信息（基础数据）
		- 生命值、连招伤害
	- `CharacterHealthInfo` 与 `CharacterComboSO` 则是利用数据的基本信息完成相应的逻辑处理（实例数据）
		- `CharacterHealthInfo` 关联变量、初始化变量（函数）、造成伤害（函数）等
		- `CharacterComboSO` 获取动作、受伤动画、格挡动画、伤害等一系列的函数
	- 一个记载基本信息，一个完成逻辑处理

- 便于后期维护、优化
	- 需要加新的信息就去 `CharacterCombatBase` 和 `CharacterHealthBaseData` 里面加
	- 需要加新的功能就去 `CharacterComboSO` 和 `CharacterHealthInfo` 里面加
---
## 属性的书写

^14520f

```C#
private float _currentHP;//当前生命值

public float CurrentHP => _currentHP;

private bool _isDie => (_currentHP <= 0f);
public bool IsDie => _isDie;
```

- 属性提供了一种封装类的成员变量的方法
- 使得外部代码可以访问这些成员变量
- 同时可以在访问或修改它们时执行一些额外的操作
- 通过使用属性，可以将类的内部实现细节隐藏起来，只暴露必要的接口给外部使用
- 这样可以降低耦合度，提高代码的可维护性

- 简而言之
	- 通过属性，外部调用的时候，调用的是属性（`IsDie`，`CurrentHP`）而不是变量（`_isDie`，`_currentHP`）
	- 在类的内部可以对变量进行一些处理，但外部的类调用属性时，不能直接修改变量
---
## `MatchTarget` 与 `isMatchingTarget`

- `Animator.MatchTarget`方法，它可以将指定身体部位的动画与位置和旋转进行匹配
- `Animator.isMatchingTarget`属性则用来表明是否有这样的匹配正在进行

- `MatchTarget` 参数
	- 目标位置
	- 目标旋转
	- 应用动画的目标部位
	- 权重掩码，用于控制不同部位的匹配程度
		- `Vector3.one` 表示所有部位都有相同的权重
		- 而0f表示没有权重
		- 意味着动画将在所有部位上均匀地应用
	- 开始匹配的时间偏移量
		- 以秒为单位
		- 设置为0，表示动画立即开始匹配
	- 匹配持续时间
		- 以秒为单位
		- 表示动画将持续匹配多少秒
---
## 点乘与叉乘

- 在Unity中，直接使用符号 `*` 的就是使用 [[3 向量点乘|点乘]]
	- `Vector.Dot` 也是使用点乘
- 如果需要使用 [[4 向量叉乘|叉乘]]
	- 就需要 `Vector3.Cross`
---
## Vector2 转 Vector3

- 二维转三维
- 将 二维的 X轴，Y轴，转换为 三维的 X轴 和 Z轴
- ——> 二维是 从上到下俯视看，中心点为原点的二维坐标系
- ——> 在三维中，二维坐标系对应的就是 三维世界坐标系的 X轴 和 Z轴
- ![[Unity知识点 二维转三维 坐标轴对应.png]]
---
## 逐元素乘积

- Vector2 乘以 Vector3 
- 使用的不是点乘，而是 逐元素乘积
- X轴 乘 X轴，Y轴 乘 Y轴，Z轴 乘 Z轴
- `result = (x1 ​* x2​, y1 * y2​, z1 * z2​)`
---
## Rigidbody 2D小知识点

- Info 中的变量 Position、Rotation 都会重写 Transform 组件上的内容
	- 可以通过 更改 Rigidbody 2D 的变量 来改变物体的位置

- 一旦一个物体挂载了 Rigidbody 刚体，这个物体的坐标就由 Rigidbody 进行驱动了
---
## 通配符 `_`

- **用于匹配任何未明确列出的值**
- 主要用于switch表达式中，它允许switch表达式匹配任何可能的值
- 如果没有任何特定的case标签与switch表达式的值相匹配，程序将执行与 `_` 关联的代码块
	- `_` 起到了一个“捕获所有其他情况”的作用
---
## Unity2D 摄像机
### 方法一

- 直接将 主摄像机 拖到 Player 下面
	- 成为 Player 的子对象
### 方法二

- **使用Cinemachine插件**
### 方法三

- 代码控制
	- 只做个示例
- ![[CameraPosition.cs]]
---
## 碰撞器 Collider 代码获取

- 任何类型的 Collider  碰撞器 都可以使用 Collider 类型进行实例化对象
- 但如果是具体想要调用某一种碰撞体形状的参数的话，建议使用具体的

```C#
private Collider2D _collider
private CapsuleCollider2D _ccollider
```
---
## 碰撞器 Collider 中 Layer Overrides属性

- CSDN 链接
	- [关于unity中碰撞盒Collider2D中的Layer Overrides属性-CSDN博客](https://blog.csdn.net/ykkykkkk/article/details/136753592)
### **Include Layer**

- 仅给当前Collider添加可碰撞的额外层
- 所以当前Collider发生碰撞or触发的层为：**层碰撞矩阵 + include layers**
### Exclude Layers

- 仅给当前Collider移除可碰撞的额外层
- 当前碰撞层级：**层碰撞矩阵 - exclude Layers**
### Callback Layers

- 默认为 Everything。与任何层发生碰撞都会触发碰撞和触发回调函数。与该设置中的未选项发生碰撞之后将不会触发回调函数
### Layer Override Priority

^2aae29

- 当A和B同时发生碰撞时，Unity会比较它们的Layer Override Priority
- 然后根据优先级高低来确定应该使用哪个设置来处理碰撞
### Contact Capture Layer

^d73d68

- 在Unity的Collider组件中，允许开发者指定**哪些层应该被捕获并生成接触信息**
- 从而使得物理查询和物理回调能够针对特定层进行优化和筛选
## coll.Bound

^b207aa

- **一个表示物体包围盒的属性，它包含了物体的大小、中心点、扩展大小、最大和最小坐标**

- - `_coll.bounds.size`
	- 表示包围盒的大小，即在每个轴向上的长度
- `_coll.bounds.center`
	- 表示包围盒的中心点位置
- `_coll.bounds.extents`
	- 表示包围盒在每个轴向上的一半长度，也就是从中心点到边界的距离
- `_coll.bounds.max`
	- 表示包围盒的最大坐标点
- `_coll.bounds.min`
	- 表示包围盒的最小坐标点
---
## C# 内建接口：IEnumerable 与 IEnumerator

- [C#内建接口：IEnumerable - 知乎 (zhihu.com)](https://zhuanlan.zhihu.com/p/444468135)
- [2021年了，`IEnumerator`、`IEnumerable`接口还傻傻分不清楚？-腾讯云开发者社区-腾讯云 (tencent.com)](https://cloud.tencent.com/developer/article/1777094)
---
## 2024TapTap聚光灯知识点

- `Input.mousePosition`
	- 得到鼠标在屏幕的位置

- `Camera.main.ScreenToWorldPoint(Input.mousePosition)`
	- 将 鼠标在屏幕的位置 修改为 世界坐标系

- `Debug.DrawRay(transform.position, direction * 10 , Color.red)`
	- 从当前位置发射一条射线，方向为 `direction * 10`
	- 颜色为 红色

- `Quaternion.Euler(0, 0, 15)`
	- 饶 Z轴 旋转 15度

- 四元数 与 向量 相乘 = 被旋转后的向量

	```C#
	//一个向量 绕Z轴旋转 15度（也就是修改 rotation 上的 X 值）
	
	var direction = mousePosition - (Vector2)transform.position;
	Vector2 rotatedVector = Quaternion.Euler(0, 0, 15) * direction;
	```

- 2D 游戏中
	- `transform.forward = direction;`
- 可以让物体一直朝向某个方向
	- ![[2024TapTap聚光灯 20241011  关于2D与3D 让物体始终朝向某一个方向.png]]
- 关于 让一个物体始终朝向鼠标
	- 方法一：修改物体的面朝向
	- 方法二：修改物体的 Rotation

```C#
//方法一
//获取鼠标与物体位置之间的方向（返回一个 Vector 向量）
var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

transform.forward = direction;

//方法二 ——> 选择 rotation
//获取物体位置
transform.position = player.position;  
// 获取鼠标在世界空间中的位置  
mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  

// 计算鼠标相对于手电筒的方向  
direction = mousePosition - flashlightTransform.position;  

// 计算手电筒旋转的角度  
float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  

// 旋转手电筒  
flashlightTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

```

- 关于 物理检测 返回的对象（例如`Physics2D.Raycast()`）
	- 每次进行一次物理检测，都会返回一个新的实例对象
	- 即使命中的为同一个对象，返回的 `RaycastHit2D` 对象 的引用也是不同的
	- 因此，可以选择，比较两者的 `collider` 属性
		- 如果命中了同一个物体，那么两者的 `collider` 属性 都应当指向同一个 `collider2D` 实例

- `GetComponent<>()` 方法的本质是检索附加到与当前脚本关联的游戏对象上的组件
	- （或者任何其他指定的游戏对象）
	- **查找组件**：它在游戏对象的组件列表中查找指定类型的组件
	- **返回组件**：如果找到了该类型的组件，它就返回该组件的实例；如果没有找到，它就返回 `null`
	```C#
	//如果一个物体上挂载的脚本，继承了一个接口
	//那么也可以使用 GetComponent<>() 来获取接口对象
	//但得到的对象，只能使用 接口里面所写的方法
	```

- 人物互动标识
	- 可以通过控制 SpriteRender 的激活/失活，来控制图片是否显示出来
	- 可以给一个图片添加 Animator，然后就可以播放动画了

- `OnTriggerStay2D` （以及其他触发检测函数）
	- 当碰撞触发时，就会调用相应的触发检测函数，并得到 被检测的对象
	- 也就是说
		- ——> 调用函数的对象 会得到 被检测到的对象
	- 简单来说
		- ——> A、B 两个物体都有 `OnTriggerStay2D`，那么当两者触发重叠时
		- ——> 在 A 的脚本中，`other` 为 物体B，而在 B 的脚本中，`other` 则为 A
		- ——> 如果 B 没有`OnTriggerStay2D`，那么只有 A 会调用这个函数
	- ——> `other` 指对面


---
## 关于 比较

- 如果做比较的为 *值类型*，那么就比较两者的 **值的大小**
- 如果做比较的为 *引用类型*，那么就比较两者的 **引用是否相同**
	- ——> 对象的引用，即 两个比较对象 是否都指向同一个内存
	- ——> 比较 引用指向 是否相同，而不是比较两者的 内容 是否相同
## 时缓效果

- `Time.timeScale` 时间缩放比例
	- 当其值为 0 时，为暂停效果
	- 当其值为 1 时，为正常播放速度
	- 当处于 0 ~ 1 之间时，就会按照一定比例的时缓进行播放
## `Mathf.Sign()`

- 在Unity中，`Mathf.Sign(direction.x)` 函数的作用是返回`direction.x`的符号。这个函数会检查`direction.x`的值，并返回三个可能的结果之一
	- 如果`direction.x`大于0，`Mathf.Sign`返回1
	- 如果`direction.x`等于0，`Mathf.Sign`返回0
	- 如果`direction.x`小于0，`Mathf.Sign`返回-1

- 这个函数常用于确定一个数值的正负，而不关心其具体的大小
	- 可以用于判断物体的移动方向、确定力的作用方向等场景
---
## switch 表达式

```C#
//要赋值的变量 = 变量 switch { 条件 => 结果值, _ =>  }
//_ 代表 如果以上所有条件都不符合时，走 这个条件
string result = number switch
{
    < 0 => "Negative",
    > 100 => "Hundred plus",
    _ => "Between 0 and 100"
};
```

---
## 关于`readonly`对`List Queue`的影响

- 两者的 *引用* 是不变的，但里面的内容仍是可变的
	- 也就是说，可以 添加、删除或修改其中的对象
---
## 六种设计模式

- 单例模式
	- 只能实例化一个对象 ——> ARPG 里面有详细的
		- ARPG ——> Unilts 里面

- 观察者模式
	- 一对多 ——> 事件管理
	- 也可使用 Action 、 UnityAction
		- += / -= 来快速监听（例如 血量变化）
	- 简单来说就是
		- 能够注册并接收到事件发生的通知，然后在这些事件发生时得到通知

- 命令模式
	- 命令模式就是把函数做成对象存起来
		- List、Queue等
	- 在 RTS 中及其常见

- 组件模式
	- Unity就是围绕组件模式进行构建的
		- 用于组合游戏对象和预制体
	- （一般会选择 使用两个小组件 组合成 一个完整对象）

- 享元模式
	- 重用数据并共享相同的数据

- 状态模式
	- FSM、状态机
---

## ScrollView

- Content
	- 一般都会选择给 Content 添加组件
		- Grid Layout Group
			- 修改子对象的大小、对齐方式
		- Content Size Fitter
			- 添加这个后，就不需要手动修改 Content 的大小了
			- 在运行时，会自动更加其内容进行修改 Content 的大小
				- ——> Vertical Fit 选择为 Preferred Size
---
## 关于 Unity 触发器 与 碰撞器

- [【全网最详解】Unity触发器（IsTrigger）和碰撞器（Collision)踩坑，坑点合集](https://blog.csdn.net/Jun_Xi_Yin/article/details/144888419)

---
## rectTransform.position

^ffa6d0

- 表示 **世界坐标系（World Space）** 下的绝对位置
- 而 `transform.position` 是当前脚本挂载对象的世界坐标位置

- **即使选项的本地坐标（Local Position）在 Inspector 中显示为几百甚至几千**
- `rectTransform.position` 会自动将父物体的层级位置累加进来
- 最终计算出选项的 **世界坐标**
## Time.timeScale

- `Time.timeScale = 0` 是一种“软暂停”机制
- 它通过修改时间参数影响依赖时间的逻辑，但不会阻止所有代码执行

- 通过修改时间缩放系数来影响与时间相关的计算
-  是一个全局缩放因子，默认值为 `1`（正常时间流速）
	- 当设置为 `0` 时：
		-  **`Time.deltaTime`** 和 **`Time.fixedDeltaTime`** 会被乘以 `0`，导致大多数基于它们的计算停止
		- **依赖时间的行为暂停**：例如物体的位移（`transform.Translate(...)`）、物理模拟、动画播放等
		- **FixedUpdate 可能停止调用**
			- Unity 的物理系统（FixedUpdate）默认依赖缩放后的时间
			- 当 `Time.timeScale = 0` 时，物理时间累积会停滞，导致 FixedUpdate 不再被调用

- `Update` 函数的调用本身**不受 `Time.timeScale` 影响**，它每帧都会执行
	- 如果代码中使用了 **`Time.unscaledDeltaTime`** 或 **`Time.realtimeSinceStartup`**
		- 这些值不受 `Time.timeScale` 影响，即使时间暂停，逻辑仍会执行
	- 如果代码不依赖 `Time.deltaTime` 或其他时间参数
		- 例如直接响应输入事件、更新 UI 文本、执行纯数学计算等，仍会正常执行

- 特定组件或系统的独立设置
	- 动画系统
		- 如果动画的 `Animator.updateMode` 被设置为 **`UnscaledTime`**，动画会无视 `Time.timeScale` 继续播放 
	- 理系统
		- 通过修改 `Time.fixedDeltaTime` 或使用自定义物理更新（`Physics.Simulate`），可以绕过时间缩放
	- 协程（Coroutine）
		- 使用 `WaitForSecondsRealtime` 代替 `WaitForSeconds` 的协程会继续运行

- 只有时间相关代码受影响
	- `Time.timeScale` 仅影响依赖以下时间参数的代码：
		- `Time.deltaTime`    
		- `Time.fixedDeltaTime`
		- `Time.time`（等价于 `Time.unscaledTime * Time.timeScale`）
		- 基于这些参数的封装功能（如 `Vector3.Lerp` 的时间插值）

- 注意
	- **音频播放**：`AudioSource.Play()` 不受 `Time.timeScale` 影响，需手动暂停    
	- **UI 动画**：使用 `Unscaled Time` 的 UI 动画（如 DOTween）会继续播放
	- **网络同步**：网络消息处理通常不受时间缩放影响
	- **性能问题**：即使 `Time.timeScale = 0`，`Update` 仍会消耗性能，若需彻底停止逻辑，可禁用脚本或对象
---
## 关于 TextMeshPro 显示图标

- 当Canvas设置为 摄像机模式时
- 需要把 Gizmos 关掉
- 不然在 Game 窗口上会显示图标（一个大T）
---
## 关于 Physics 区域检测 和 投影检测

- 区域检测
	-  `OverlapCircle`
		- 检测圆形区域内是否有Collider   
	- `OverlapCircleAll`
		- 返回圆形区域内所有与之重叠的Collider
	- `OverlapBox`
		- 检测矩形区域内是否有Collider
	- `OverlapBoxAll`
		- 返回矩形区域内所有与之重叠的Collider
	- `OverlapCapsule`
		- 检测胶囊形区域内是否有Collider
	- `OverlapCapsuleAll`
		- 返回胶囊形区域内所有与之重叠的Collider

- 投影检测
	-  `BoxCast`
		- 将一个盒子投影到场景中，检测与Collider的碰撞
	- `BoxCastAll`
		- 将一个盒子投影到场景中，返回所有与之相交的Collider
	- `CapsuleCast`
		- 将一个胶囊投影到场景中，检测与Collider的碰撞
	- `CapsuleCastAll`
		- 将一个胶囊投影到场景中，返回所有与之相交的Collider

- 区域检测用于检测某个固定形状（如圆形、矩形、胶囊形）内是否有Collider
	- 它主要用于静态检测，即检测某个区域内的Collider是否存在
- 投影检测是将一个形状（如盒子、胶囊）沿着某个方向移动，检测移动过程中是否与Collider发生碰撞
	- 它主要用于动态检测，即检测某个形状在移动过程中是否会与物体发生碰撞
---
## Collider 与 ScriptableObject

- **ScriptableObject 不能直接存储 `Collider`（如 `BoxCollider2D` 或 `Collider`）的实例**

- **ScriptableObject 的设计限制**
	- 依赖 GameObject 的组件
		- `Collider` 是 `Component` 的子类，必须依附于 `GameObject` 存在
		- 而 ScriptableObject 是独立的数据容器，无法直接保存场景或预制体中的组件实例
	- 序列化限制
		- Unity 的序列化系统无法将 `Component`（如 Collider）的实例直接保存到 ScriptableObject 中
		- 即使强行存储，引用也会在场景重载或退出后丢失
## 鼠标光标自定义

`Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);`

* *`cursorTexture`（Texture2D 类型）**
    - **作用**：设置光标的贴图（即光标显示的图像）
    - **注意事项**：
        - 需要确保纹理的 **导入设置** 中勾选 `Read/Write Enabled`（在 Inspector 面板中设置）
        - 建议使用小尺寸纹理（如 32x32 或 64x64），避免性能问题
        - 若需透明光标，需使用支持透明通道的格式（如 PNG）
            
- **`Vector2.zero`（热点坐标）**
    - **作用**：定义光标的 **“热点”**（即光标的实际作用点，例如箭头的尖端位置）
    - **坐标系**：以纹理的 **左上角为原点 `(0,0)`**，向右为 X 轴正方向，向下为 Y 轴正方向
    - **示例**：
        - 若光标贴图大小为 64x64，将热点设为 `new Vector2(32, 32)` 表示中心点
        - 对于手型光标，可能需要设为指尖位置（如 `(16, 48)`）
            
- **`CursorMode.Auto`（光标模式）**
    - **作用**：控制光标的渲染方式。可选值：
        - **`CursorMode.Auto`**：由 Unity 自动选择硬件或软件光标（推荐默认值）
        - **`CursorMode.ForceSoftware`**：强制使用软件渲染光标（适用于某些平台如 WebGL）
        - **`CursorMode.Hardware`**：强制使用硬件光标（性能更好，但功能受限）
---
## 如何遍历 Scroll 的子物体

```C++
//将对应的 Scroll View 拖到脚本的 scrollRect 引用处
public ScrollRect scrollRect;

var content = shopCellPanel.transform;  

foreach (Transform cell in content)  
{  
	Destroy(cell.gameObject);
}

//脱离子物体
//如果只是想让 Content 下的子物体脱离其父物体，可使用 ScrollRect 的 DetachChildren 方法
//这种方法只是将子物体脱离 Content 容器，并不会销毁它们，若要彻底删除，还需结合 Destroy 方法
scrollRect.content.DetachChildren();

```
## 父子物体的碰撞器

如果父物体 A 没有碰撞器，但子物体 B 有碰撞器组件，有希望当子物体发生碰撞时调用父物体 A 的 `OnTriggerEnter` 方法
那么此时可以往父物体添加一个刚体 Rigidbody 组件
添加后，子物体的碰撞器会被视为父物体的复合碰撞器，当玩家与子物体B发生碰撞时，父物体A的脚本中的 `OnTriggerEnter2D()` 会被触发，因为物理事件由父物体的刚体处理
无论事后是否再往父物体添加碰撞器，子物体发生碰撞时都会调用父类的 `OnTriggerEnter2D()`
但，**子物体B不能有自己的 `Rigidbody2D`**







