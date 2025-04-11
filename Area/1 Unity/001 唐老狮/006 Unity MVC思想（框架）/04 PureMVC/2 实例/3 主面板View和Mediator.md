---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Unity程序基础框架/主面板View和Mediator
created: 2025-03-16
---

---
# 关联知识点



---

View 还是不变

找控件
更新数据（如果是MVC，如果是MVP可以移到 Controller 中）

Mediator
固定写法：继承 Mediator脚本，写构造函数，重写监听通知的方法，重写处理通知的方法，可选（重写注册时的方法）
前两个和 Proxy 很像
构造函数中，可以去创建预制体等操作，但不建议，这部分逻辑应该放到 触发监听 中，不然每个 Mediator 都要重写一次

重写监听
返回值为一个字符串数值，它的作用就是：通知所有的 符合字符串 的 事件（可以理解为 呼叫事件）
可以与 通知名类 一同使用，减少出错，便于管理

重写处理
事件呼叫了，现在该去写处理的逻辑了
函数参数 `INotification` 包含 通知名 我们根据这个名字 来做对应的处理、通知包含的信息
使用 `switch`，根据通知名的不同，进行不同的处理
`notification.Body` 是一个 数据体 `Object` 类型


View Component
Mediator 父类中，存在一个 （Object）ViewComponent 属性，相当于 Proxy 中的 Data
ViewComponent 要和 面板脚本（也就是 View 进行关联）
由于 VC（以下皆简写）是个 `Objecy 类型` 所以在使用是需要用 `as` 进行转换


备注：注册方法，当 Facade 调用时，都是需要进行注册操作的


---
# 源代码

- ![[Unity MVC 框架（思想）PureMVC 主面板View和Mediator NewMainView.cs]]
- ![[Unity MVC 框架（思想）PureMVC 主面板View和Mediator NewMainViewMediator.cs]]

---