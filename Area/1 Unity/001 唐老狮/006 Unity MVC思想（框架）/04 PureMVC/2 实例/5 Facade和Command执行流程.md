---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Unity程序基础框架/Facade和Command执行流程
created: 2025-03-16
---

---
# 关联知识点



---

Facade：
继承父类Facade、写一个单例属性、重写注册Controller内容、必须要有一个启动函数


M V C 都是由 Facade 进行管理的

继承 PureMVC 中的 Facade 脚本
为了方便使用，建议写一个单例模式的属性

Facade 中包含三个接口，用来管理 M V C
```C#
/// <summary>References to Controller</summary>  
protected IController controller;  
/// <summary>Reference to Model</summary>  
protected IModel model;  
/// <summary>References to View</summary>  
protected IView view;
```

重写注册Controller
必须调用父类，然后在这里面去写一些关于命令和通知的绑定逻辑

Controller 需要像 Model 和 View 要额外写 DataObject 或者 ViewComponent
Controller 要写的内容就是 Command

命令的写法也是有套路的
继承、重写执行函数


可以这么理解
`GameFacade.Instance.StartUp();` 调用写有启动代码的方法 `SendNotification(PureNotification.START_UP);`
这就相当于事件管理器，发送通知 就是 分发了一个事件

```C#
RegisterCommand(PureNotification.START_UP, () =>  
{  
    return new StartUpCommand();//一个继承了 PureMVC 的命令  
});
```

`RegisterCommand` 见监听了这个事件，Lambad 表达式 则是 监听事件 要执行的方法（也就是 Controller 写的 命令）
然后去执行 命令 里面所重写的代码逻辑

---
# 源代码

- ![[Unity MVC 框架（思想）PureMVC Facade和Command执行流程 GameFacade.cs]]
-  ![[Unity MVC 框架（思想）PureMVC Facade和Command执行流程 StartUpCommand.cs]]
- ![[Unity MVC 框架（思想）PureMVC Facade和Command执行流程 Main.cs]]


---