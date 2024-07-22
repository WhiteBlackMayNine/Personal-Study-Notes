---
tags:
  - "#科学/Unity/ARPG/GameEventManager"
created: 2024-06-13
更新: 2024-06-13
---
 
---
# 关联知识点



---
# 单例模式

- 用的是工具包中的单例
# 接口 IEventHelp

- 管理不同类型的事件
- 因为不同的事件里面的参数可能不同
- 这个接口 主要是用来利用**字典**管理不同参数的类，里面不需要写什么逻辑
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

---
# 源代码

![[GameEventManager.cs]]

---