---
tags:
  - "#科学/Unity/ARPG/Manager/GameEventManager"
created: 2024-06-13
---
 
---
# 关联知识点



---
# 单例模式

- 用的是工具包中的单例
# 接口 IEventHelp

- 管理不同类型的事件
- 因为不同的事件里面的参数可能不同
- 这个接口 主要是用来利用 **字典** 管理不同参数的类，里面不需要写什么逻辑
- 只需要让类继承这个接口
# 字典

- `private Dictionary<string, IEventHelp> _eventCenter = new Dictionary<string, IEventHelp>();`
- 利用 接口 IEventHelp 来存储事件
- 因为下面的事件 EventHelp 都是继承 接口 的，**因此可以被写入字典中**
## 事件类 EventHelp

- 这个类就是用来实现下面三个（添加监听、呼叫事件，移除监听）的具体功能的
	- 里面包含了 增加 调用 移除 这三个功能
	- 参数个数的不同，就对泛型进行不同的修改（增加或删除）

```C#
	 private class EventHelp<T1, T2> : IEventHelp
    {
        private event Action<T1, T2> _action;//只能在内部调用，外部无法调用

        public EventHelp(Action<T1, T2> action)
        {
            //这是第一次实例的时候，也就是说只会执行这一次
            _action = action;
        }

        //增加事件的注册函数
        public void AddCall(Action<T1, T2> action)
        //当需要注册多个函数的时候，使用这个方法，而不是重新New一个
        {
            _action += action;
        }

        //调用事件
        public void Call(T1 value, T2 value1)
        {
            _action?.Invoke(value, value1);
        }

        //移除事件
        public void Remove(Action<T1, T2> action)
        {
            _action -= action;
        }
    }
```
## 添加监听 `AddEventListening`

- 利用 字典 和 类 EventHelp 完成功能
	- 利用字典的 `.TryGetValue` 先找有没有这个键的值，并返回这个 值
		- 如果有，那么转换类型并利用 EventHelp 的 AddCall 添加进去
		- 如果没有，那么就利用 字典 直接 new 这个委托

```C#
	    public void AddEventListening<T1,T2>(string eventName, Action<T1,T2> action)
    {
        //先从字典中找一下 键为eventName 对应的 值  并返回这个 值 (记作 e )
        if (_eventCenter.TryGetValue(eventName, out var e))
        {
            //如果找到了，那么把 e 转换为 EventHelp 类型 并调用其中的 AddCall 函数 添加进去
            (e as EventHelp<T1,T2>)?.AddCall(action);
        }
        else
        {
            //如果没有，那么就向 字典 中添加这个委托 (new 一个)
            _eventCenter.Add(eventName, new EventHelp(action));
        }
    }
```
## 调用事件 `CallEvent`

- 利用 字典 和 类 EventHelp 完成功能
	- 利用字典的 `.TryGetValue` 先找有没有这个键的值，并返回这个 值
		- 如果有，那么转换类型并利用 EventHelp 的 Call 进行调用事件
		- 如果没有，那么打印信息

```C#
      public void CallEvent<T1, T2>(string eventName, T1 value, T2 value1)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2>)?.Call(value, value1);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法执行该事件");
        }
    }
```
## 移除事件 `RemoveEvent`

- 利用 字典 和 类 EventHelp 完成功能
	- 利用字典的 `.TryGetValue` 先找有没有这个键的值，并返回这个 值
		- 如果有，那么转换类型并利用 EventHelp 的 Remove 进行移除事件
		- 如果没有，那么打印信息

```C#
      public void RemoveEvent<T1, T2>(string eventName, Action<T1, T2> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法移除该事件");
        }
    }
```
# 使用
## 事件监听

- 谁需要谁去监听
	- 比如玩家击杀怪物时获取经验，玩家需要去监听怪物是否被击杀
		- 则在玩家脚本中添加监听（`AddEventListening`）
		- 添加监听在 `Enable`  同时 必须移除监听 在 `OnDisable` （`RemoveEvent`）
## 事件触发

- 谁触发谁去调用
	- 比如玩家击杀怪物时获取经验，玩家需要去监听怪物是否被击杀
		- 则在怪物脚本中调用事件（`CallEvent`）
## 切换场景

- 清空事件中心，也就是清空字典
## 简单理解

- 调用事件，就是说明也就满足了条件，可以执行某个函数委托了
- 添加监听，就是查看某个函数委托是否可以执行
- 移除监听，避免造成资源浪费等其他bug
# 更新
## 为什么要继承 `IEventHelp`

- 首先，事件管理器是由 [[2 Dictionary|字典]] `_eventCenter` 来进行存储、删除的
	- 也就通过往字典的增删查改 完成 监听、呼叫、移除
- `EventHelp` 继承接口`IEventHelp`
	- 实例化后`EventHelp`的变量也继承 `IEventHelp`
- 这样 字典里面的 值对象 都是 实例化后`EventHelp`的变量
	-  都是继承了 `IEventHelp`

- `EventHelp` 中提供了完成逻辑的方法
	- *添加监听* ——> `AddCall()`
	- *移除事件* ——> `Call()`
	- *呼叫事件* ——> `Remove()`

- 简单来说
	- 根据代码来说，第一次往字典里面添加，走的是`new EventHelp(action)`
	- 实例化一个`EventHelp` 对象，做为字典的值对象添加进行
	- 也就是说，字典的 **键为 事件的名字** ，**值为 一个 `EventHelp` 实例化对象**
	- 往字典添加、移除事件
		- 其实就是往这个 **实例化** 对象上调用相应的方法进行
		- 添加事件（`AddCall`）和移除事件（`Remove`）
		- 往这个 **实例化** 对象 身上进行 *添加事件* 和 *移除事件*
		- 并不是 往 字典 里面 *添加事件* 和 *移除事件*

- 大致逻辑
	- 就是往 字典里面去添加一个 类型为 `EventHelp` （继承`IEventHelp`）的 值对象
	- 然后往 这个 值对象 里面去添加事件
	- 直接往字典里去添加事件，需要考虑添加的事件的参数
	- 这样做就不需要了，可以直接创建不同数量的泛型的重载
	- 实例化的时候就是 不同的重载 的 实例化对象
	- 往 字典 里面添加的 值对象 就是 不同参数数量 的 重载 了
## 添加监听
### 作用

- 往字典中的实例化事件中添加一个事件
### 大致逻辑

- 第一次给一个事件添加，一定是没有 所对应的 键值对的，`_eventCenter.TryGetValue` 只能返回 `false`
- 所有根本就不可能去走 `if` 里面的语句，直接去走 `else` 里面的
	- `_eventCenter.Add(eventName, new EventHelp(action));`
- 在字典里面添加（`new`）一个 `EventHelp` 的实例化对象
	- `EventHelp` 类中的构造函数，当 `new` 的时候，就已经往里面添加了 事件

- 第二次添加事件（键对象 一致，也就是 *一个事件添加两个函数* ）时
- 那么就会去走 `if` 语句，取出这个 键对象 所对应的 值对象（也就是 `EventHelp` 实例化对象）
- 然后调用 `EventHelp` 中的 `.AddCall` 方法 
- 往这个 实例化对象 添加事件
## 调用事件

- 判断字典中是否有这个键值对
	- 如果有
		- 调用 `EventHelp` 中的 `.Call` 方法 
		- 执行 `_action?.Invoke();` 把 值对象（实例化对象）中的所有事件对应的函数依次执行
	- 如果没有
		- `DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法执行该事件");`
		- 也可以像 *添加事件* ，往 字典里面新建一个 *键值对*
		- 然后执行该事件
## 移除事件

- 判断字典中是否有这个键值对
	- 如果有
		- 调用 `EventHelp` 中的 `.Remove` 方法 
		- 执行 `_action -= action;` 删除这个委托事件
	- 如果没有
		- `DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法移除该事件");`
## 外部函数调用

- 在 `OnEnable` 中添加事件
- 在 `OnDisable` 中移除事件
- 如果需要呼叫的时候，需要把对应参数传进去

---
# 源代码

![[GameEventManager.cs]]

---