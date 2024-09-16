---
tags: 
created: 2024-08-15
---
---
# 关联知识点



---
# 备注

- **这个是继承 MonoBehaviour 的 单例类**
- 用来实现单例
# 代码

```C#
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>  
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
					 _instance = FindObjectOfType<T>() as T; //先去场景中找有没有这个类  
					if (_instance == null)
					//如果没有，那么我们自己创建一个Gameobject然后给他加一个T这个类型的脚本，并赋值给instance;  
					{  
						GameObject go = new GameObject(typeof(T).Name);  
						_instance = go.AddComponent<T>();  
					} 
				} 
			}  
			return _instance;  
		}        
	}          
	
	protected  virtual void Awake()  
	{            
		if (_instance == null)  
		{                
			_instance = (T)this;  
			DontDestroyOnLoad(gameObject);  
		}
		else  
		{  
			Destroy(gameObject);  
		}        
	}  

	private void OnApplicationQuit()//程序退出时，将instance清空  
	{  
		_instance = null;  
	}    
}    
```
# 讲解
## 关于抽象类的泛型
- `public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>`
	- 传入的泛型 T 必须是继承 Singleton 的
- 例如
	- `public class GameInputManager : Singleton<GameInputManager>`
- GameInputManager 继承于 Singleton
- 在 `where` 中，GameInputManager 确实是一个 继承 Singleton 的类
- 满足了 `where T : Singleton<T>` 这个约束
## 关于声明的变量

```C#
private static T _instance;
	
private static object _lock = new object();  
```

- `_instance` ——> 传入的泛型类型的变量
- `_lock` ——> new 一个 对象（引用类型的变量），用于 [[多线程#^f85533|多线程的锁]]
### 关于 `_lock` 

- 在多线程中的 `lock(){}` 中，需要传入一个 *引用类型的变量*
- 这个 *引用类型的变量* 就是作为一个 ==锁==

- **首次锁定**
	- 当第一个线程执行到 `lock(_lock)` 语句时，由于 `_lock` 当前未被锁定，该线程会进入并执行 `lock` 语句块中的代码
	- 在此期间，`_lock` 对象被锁定，其他任何尝试进入 `lock` 语句块的线程将被阻塞
- **后续锁定尝试**
	- 如果有 *其他线程* 在第一个线程还在 `lock` 语句块内部执行时 尝试执行 `lock(_lock)`
	- 那么这些线程将无法进入 `lock` 语句块
	- 它们将在 `lock` 语句处等待，直到第一个线程离开 `lock` 语句块并释放 `_lock` 上的锁
		- 锁 就是 `_lock` 这个引用类型变量
- **锁的释放**
	- 当拥有锁的线程完成 `lock` 语句块中的代码执行并退出该语句块时，锁将被释放
	- 时，正在等待的线程中的一个将获得锁并开始执行 `lock` 语句块
## 关于属性 `MainInstance`

```C#
public static T MainInstance  
{  
    get  
    {  
        if (_instance == null)  
        {            
	        lock (_lock)  
            {               
	             _instance = FindObjectOfType<T>() as T; //先去场景中找有没有这个类  
	            if (_instance == null)//如果没有，那么我们自己创建一个Gameobject然后给他加一个T这个类型的脚本，并赋值给instance;  
                {  
                    GameObject go = new GameObject(typeof(T).Name);  
                    _instance = go.AddComponent<T>();  
                }            
	        }    
	    }  
	    
        return _instance;  
	}
}
```

- 当外部调用 `MainInstance` 时，会触发 `get{}` 方法

- 判断 `_instance` 是否为空，也就是 **判断是否创建了一个 实例化对象**
	- 例如，传入的是 GameInputAction，这里就判断是否创建了一个 GameInputAction 的 实例化对象
- 调用 `lock()` 锁一下线程，防止多线程时出现错误
	- `FindObjectOfType<T>` 在 Unity中寻找一个类型为 T 的对象（或者说是 类型为T的一个实例化对象）
		- 然后转换为 T 类型
	- 例如，GameInputAction ，在 Unity 寻找一个类型为 GameInputAction 的对象（实例化对象）
		- 然后转换为 GameInputAction 类型
- 再次判断 `_instance` 是否为空，也就是 **是否在场景上找到了一个 实例化对象**

- `GameObject go = new GameObject(typeof(T).Name); `
	- 创建一个 名字为 类型T 的变量
	- 例如，创建一个 名为 GameInputAction 的变量
- `_instance = go.AddComponent<T>();`
	- 在 变量 `go` 身上添加组件 类型T，然后赋值给 `_instance`
	- 例如，在变量`go`身上添加组件 GameInputAction，然后赋值给 `_instance`

- 最后 `return _instance;` 将实例化完成的对象 `_instance` 返回出去
## 关于 `Awake`

```C#
protected  virtual void Awake()  
{  
    if (_instance == null)  
    {
	    _instance = (T)this;  
        DontDestroyOnLoad(gameObject);  
    }    
    else  
    {  
        Destroy(gameObject);  
    }
}
```

- 判断 `_instance` 是否为空
	- 如果是，就说明没有创建 `_instance`
		- `_instance = (T)this;` 将当前对象转换为 类型T，并赋值给 `_instance = (T)this;`
	- 如果不会空，删除当前对象，避免出现多个对象

- `DontDestroyOnLoad(gameObject)` 确保当前对象在切换场景中不会被删除

- 举例说明，GameInputAction 继承了这个 Singleton 类，并重写了 `Awake`
- 那么，在 `Awake` 中，先判断 `_instance` 是否为空，如果是，就说明还没有创建 GameInputAction 实例
- 此时，就会将当前对象（即 新创建的 `GameInputAction` 对象）转换为 `GameInputAction` 类型并赋值给 `_instance`
	- `_instance = (T)this`
- 这样，无论何时访问 `_instance`，都将得到同一个 `GameInputAction` 对象的引用
## 关于程序退出

```C#
private void OnApplicationQuit()//程序退出时，将instance清空  
{  
    _instance = null;  
}
```
# 总结

- 一个抽象的单例类
- 用于在Unity中实现单例模式
- 确保一个类只有一个实例存在，并提供一个全局访问点来获取该实例

- 核心功能是 ——> `MainInstance`
- 在 `Awake` 中，确保单例对象的唯一性
	- 检测 `_instance` 是否被创建，如果没有就去创建一个，如果有就删除多余的
	- 这样，无论场景中有多少个这样的游戏对象，都只会有一个实例被创建并存储在 `_instance` 变量中
	- 此外，加载新的场景时保持单例对象不被销毁

- 因为是继承了 `MonoBehaviour` 的单例类
- 所以 **它的生命周期会受到Unity的控制**，单例实例附加到的游戏对象被销毁，那么该单例实例也会随之销毁
	- 除非采取措施如DontDestroyOnLoad来防止其被销毁
- 一般用于 *需要使用到 Unity控制的生命周期函数* 的 单例类
	- 比如，[[GameInputManager]]

---
# 源代码

![[Singleton.cs]]

---