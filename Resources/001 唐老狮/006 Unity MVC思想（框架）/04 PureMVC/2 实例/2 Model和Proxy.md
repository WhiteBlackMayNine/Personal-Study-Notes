---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Unity程序基础框架/Model和Proxy
created: 2025-03-16
---

---
# 关联知识点



---

学习的是数据这一块内容

Model 和 Proxy

但是，虽然有 Model、Controller、View，但我们并不会直接创建它们
比如说 Model，我们创建的是 Data Object（数据对象相关），和 Proxy（代理，处理数据对象逻辑）
我们不会直接使用 Model

在 PureMVC 的 Model 脚本为一个单例，同时有一个 只读 的容器

```C#
/// <summary>Mapping of proxyNames to IProxy instances</summary>  
protected readonly ConcurrentDictionary<string, IProxy> proxyMap;  
  
/// <summary>Singleton instance</summary>  
protected static IModel instance;
```

`IProxy` 就是代理，存储在 只读容器 内部

使用时，Facade 得到 Model 中的 Proxy（也就是 直接获取 Proxy）

所以，先创建 DataObject 脚本
例如：PlayerDataObj 里面包含了玩家的信息

然后就是创建一个 Proxy（玩家数据代理对象），用来处理玩家信息
注意，PlayerProxy 固定写法：继承 Proxy 父类，写构造函数

因为 Facade 要通过名字来得到 Proxy，所以代理的名字十分重要
此时就可以将代理的名字写到 通知名类 中
使用的时候，调用 通知名类 获取便可

或者 `public new const string NAME = "PlayerProxy";`
将父类的 NAME 有意隐藏，在 Proxy 脚本中创建个常量

关联数据方式
方法一：外部传入一个数据 `public PlayerProxy(PlayerDataObj data):base(PlayerProxy.NAME,data)`
在 Proxy 的构造函数中，便会把 data 赋值到 Data（也就是 代理脚本 所真正 控制 的数据）

方法二：在构造函数中 初始化一个数据 进行关联 `PlayerDataObj data = new PlayerDataObj();`
初始化完成之后可以对其进行赋值，然后需要关联一下 Data `Data = data;`


完成了这两项后，就可以去写逻辑了（例如 升级、保持数据等），但要注意，使用 `Data.XX` 来访问、保存
不过，`Data` 使用的是 里氏替换原则，所以要转换一下 `PlayerDataObj data = Data as PlayerDataObj; data.lev += 1;`


---
# 源代码

- ![[Unity MVC 框架（思想）PureMVC Model 和 Proxy PlayerDataObj.cs]]
- 
![[Unity MVC 框架（思想）PureMVC Model 和 Proxy PlayerProxy.cs]]

---