using System.Collections.Generic;
using System;
using GGG.Tool;
using GGG.Tool.Singleton;
using UnityEngine;

public class GameEventManager : SingletonNonMono<GameEventManager>
{

    private Dictionary<string, IEventHelp> _eventCenter = new Dictionary<string, IEventHelp>();

    #region 接口IEventHelp
    private interface IEventHelp
    {
        //先写一个接口 管理不同类型的事件
        //因为不同的事件里面的参数可能不同
        //这个接口 主要是用来利用字典管理不同参数的类，里面不需要写什么逻辑
        //只需要让类继承这个接口
    }

    #endregion

    #region EventHelp

    //这是无参的
    private class EventHelp : IEventHelp
    {
        private event Action _action;//只能在内部调用，外部无法调用
        public EventHelp(Action action)//构造函数
        {
            //这是第一次实例的时候，也就是说只会执行这一次
            _action = action;
        }

        /// <summary>
        /// 增加注册的事件
        /// </summary>
        /// <param name="action"></param>
        public void AddCall(Action action)//当需要注册多个函数的时候，使用这个方法，而不是重新New一个
        {
            _action += action;
        }

        /// <summary>
        /// 调用事件
        /// </summary>
        public void Call()
        {
            _action?.Invoke();
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="action"></param>
        public void Remove(Action action)
        {
            _action -= action;
        }
    }
    //有一个参数的
    private class EventHelp<T> : IEventHelp
    {
        private event Action<T> _action;//只能在内部调用，外部无法调用

        public EventHelp(Action<T> action)
        {
            //这是第一次实例的时候，也就是说只会执行这一次
            _action = action;
        }

        //增加事件的注册函数
        public void AddCall(Action<T> action)//当需要注册多个函数的时候，使用这个方法，而不是重新New一个
        {
            _action += action;
        }

        //调用事件
        public void Call(T value)
        {
            _action?.Invoke(value);
        }

        //移除事件
        public void Remove(Action<T> action)
        {
            _action -= action;
        }
    }
    //有两个参数的
    private class EventHelp<T1, T2> : IEventHelp
    {
        private event Action<T1, T2> _action;//只能在内部调用，外部无法调用

        public EventHelp(Action<T1, T2> action)
        {
            //这是第一次实例的时候，也就是说只会执行这一次
            _action = action;
        }

        //增加事件的注册函数
        public void AddCall(Action<T1, T2> action)//当需要注册多个函数的时候，使用这个方法，而不是重新New一个
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
    //如果有多个参数，那么依次类推
    private class EventHelp<T1, T2, T3> : IEventHelp
    {
        private event Action<T1, T2, T3> _action;//只能在内部调用，外部无法调用

        public EventHelp(Action<T1, T2, T3> action)
        {
            //这是第一次实例的时候，也就是说只会执行这一次
            _action = action;
        }

        //增加事件的注册函数
        public void AddCall(Action<T1, T2, T3> action)//当需要注册多个函数的时候，使用这个方法，而不是重新New一个
        {
            _action += action;
        }

        //调用事件
        public void Call(T1 value, T2 value1, T3 value2)
        {
            _action?.Invoke(value, value1, value2);
        }

        //移除事件
        public void Remove(Action<T1, T2, T3> action)
        {
            _action -= action;
        }
    }
    private class EventHelp<T1, T2, T3, T4> : IEventHelp
    {
        private event Action<T1, T2, T3, T4> _action;//只能在内部调用，外部无法调用

        public EventHelp(Action<T1, T2, T3, T4> action)
        {
            //这是第一次实例的时候，也就是说只会执行这一次
            _action = action;
        }

        //增加事件的注册函数
        public void AddCall(Action<T1, T2, T3, T4> action)//当需要注册多个函数的时候，使用这个方法，而不是重新New一个
        {
            _action += action;
        }

        //调用事件
        public void Call(T1 value, T2 value1, T3 value2, T4 value3)
        {
            _action?.Invoke(value, value1, value2, value3);
        }

        //移除事件
        public void Remove(Action<T1, T2, T3, T4> action)
        {
            _action -= action;
        }
    }
    private class EventHelp<T1, T2, T3, T4, T5> : IEventHelp
    {
        private event Action<T1, T2, T3, T4, T5> _action;//只能在内部调用，外部无法调用

        public EventHelp(Action<T1, T2, T3, T4, T5> action)
        {
            //这是第一次实例的时候，也就是说只会执行这一次
            _action = action;
        }

        //增加事件的注册函数
        public void AddCall(Action<T1, T2, T3, T4, T5> action)//当需要注册多个函数的时候，使用这个方法，而不是重新New一个
        {
            _action += action;
        }

        //调用事件
        public void Call(T1 value, T2 value1, T3 value2, T4 value3, T5 value4)
        {
            _action?.Invoke(value, value1, value2, value3, value4);
        }

        //移除事件
        public void Remove(Action<T1, T2, T3, T4, T5> action)
        {
            _action -= action;
        }
    }

    #endregion

    #region 添加监听

    public void AddEventListening(string eventName, Action action)
    {
        //先从字典中找一下 键为eventName 对应的 值  并返回这个 值 (记作 e )
        if (_eventCenter.TryGetValue(eventName, out var e))
        {
            //如果找到了，那么把 e 转换为 EventHelp 类型 并调用其中的 AddCall 函数 添加进去
            (e as EventHelp)?.AddCall(action);
        }
        else
        {
            //如果没有，那么就向 字典 中添加这个委托 (new 一个)
            _eventCenter.Add(eventName, new EventHelp(action));
        }
    }
    public void AddEventListening<T>(string eventName, Action<T> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T>(action));
        }
    }
    public void AddEventListening<T1, T2>(string eventName, Action<T1, T2> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T1, T2>(action));
        }
    }
    public void AddEventListening<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T1, T2, T3>(action));
        }
    }
    public void AddEventListening<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3, T4>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T1, T2, T3, T4>(action));
        }
    }
    public void AddEventListening<T1, T2, T3, T4, T5>(string eventName, Action<T1, T2, T3, T4, T5> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3, T4, T5>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T1, T2, T3, T4, T5>(action));
        }
    }

    #endregion

    #region 调用事件
    public void CallEvent(string eventName)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp)?.Call();
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法执行该事件");
        }
    }
    public void CallEvent<T>(string eventName, T value)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T>)?.Call(value);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法执行该事件");
        }
    }
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
    public void CallEvent<T1, T2, T3>(string eventName, T1 value, T2 value1, T3 value2)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3>)?.Call(value, value1, value2);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法执行该事件");
        }
    }
    public void CallEvent<T1, T2, T3, T4>(string eventName, T1 value, T2 value1, T3 value2, T4 value3)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3, T4>)?.Call(value, value1, value2, value3);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法执行该事件");
        }
    }
    public void CallEvent<T1, T2, T3, T4, T5>(string eventName, T1 value, T2 value1, T3 value2, T4 value3, T5 value4)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3, T4, T5>)?.Call(value, value1, value2, value3, value4);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法执行该事件");
        }
    }

    #endregion

    #region 移除事件
    public void RemoveEvent(string eventName, Action action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法移除该事件");
        }
    }
    public void RemoveEvent<T>(string eventName, Action<T> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法移除该事件");
        }
    }
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
    public void RemoveEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法移除该事件");
        }
    }
    public void RemoveEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3, T4>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法移除该事件");
        }
    }
    public void RemoveEvent<T1, T2, T3, T4, T5>(string eventName, Action<T1, T2, T3, T4, T5> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//看函数是否存在
        {
            (e as EventHelp<T1, T2, T3, T4, T5>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"当前未找到{eventName}的事件，无法移除该事件");
        }
    }

    #endregion

}

