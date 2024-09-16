---
tags: 
created: 2024-08-15
---
---
# 关联知识点


---
# 备注

- **这个是不继承 MonoBehaviour 的 单例类**
- 具体逻辑与 [[Singleton]] 类似
# 代码

```C#
public abstract class SingletonNonMono<T> where T : class,new()  
{  
    private static T _instance;  
    private static object _lock = new object();  
  
    public static T MainInstance  
    {  
        get  
        {  
            if (_instance == null)  
            {
	            lock (_lock)  
                {
					_instance ??= new T();  
                }
            }
            
            return _instance;  
        }
    }
}
```
# 讲解
## 关于泛型的约束

`public abstract class SingletonNonMono<T> where T : class,new()`

- 泛型 必须是一个 引用类型（`class`）
- 必须具有一个公共的无参构造函数（`new()`）
- 确保了 `T` 可以被实例化
- **并且可以通过调用其无参构造函数来创建新的实例**
## 关于属性 `MainInstance`

- 先判断 `_instance` 是否为空
- 进入 同步块，使用 `lock`  确保多线程时只有一个能执行
- `_instance ??= new T();` 利用 `??=` （ [[3 条件运算符#^4c5c61|空合并赋值运算符]]）为 `_instance` 赋值
- 最后，将 `_instance` 返回出去
# 总结

- 因为是没有继承 `MonoBehaviour` 的单例类
- 所有这个单例模式创建出来的单例实例，**不会随着 Unity 生命周期函数 进行生成销毁**
- **只有在 退出游戏 （或者手动销毁）的 时候才会去销毁，其余时间都是存在的**

- 一般用于 [[GameEventManager|事件管理器]] ，这种 全局存在 的单例实例中

---
# 源代码

![[SingletonNonMono.cs]]

---