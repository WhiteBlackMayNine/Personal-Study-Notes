---
tags:
  - "#科学/Unity/ARPG/GameTimerManage"
created: 2024-06-18
更新:
---

---
# 关联知识点

[[GameTimer]]

---

# GameTimerManager

- 外部调用这个类，调用计时器进行计时
- 单例是利用工具包的工具
## 变量

- `int _initMaxTimerCount`
	- 默认最大初始计时器数量
- `Queue<GameTimer> _notWorkerTimer = new Queue<GameTimer>();`
	- 用来保存所有的空闲计时器
- `List<GameTimer> _workeringTimer = new List<GameTimer>();`
	- 用来保存当前正在工作中的计时器
## 函数

### 初始化计时器 `InitTimerManager`

```C#
  private void InitTimerManager()
{
	for(int i = 0;i < _initMaxTimerCount; i++)
	{
		CreatTimer();
	}
}
```

### 创建计时器对象 `CreatTimer`

```C#
  private void CreatTimer()
{
	//new 一个对象
	var timer = new GameTimer();

	//将new 出来的对象放到队列中
	_notWorkerTimer.Enqueue(timer);
}
```

### 外部获取计时器 `TryGetTimer`

- 外部需要计时器时，调用这个函数

```C#
  public void TryGetTimer(float time,Action task)
{
	if(_notWorkerTimer.Count == 0)
	{
		//如果空闲计时器的数量为0 即没有一个空闲计时器可供使用

		//创建一个计时器
		CreatTimer();
		//将创建的空闲计时器拿出来
		var timer = _notWorkerTimer.Dequeue();
		//将外部设置的计时时长与任务传入这个计时器中
		timer.StartTimer(time, task);
		//最后 将这个计时器加入到队列中
		_workeringTimer.Add(timer);
	}
	else
	{
		//如果有空闲计时器可供使用

		//将空闲计时器拿出来
		var timer = _notWorkerTimer.Dequeue();
		//将外部设置的计时时长与任务传入这个计时器中
		timer.StartTimer(time, task);
		//最后 将这个计时器加入到队列中
		_workeringTimer.Add(timer);
	}
}
```

### 更新计时器信息 `UpdateWorkingTimer`

```C#
  private void UpdateWorkingTimer()
{
	if(_workeringTimer.Count == 0)
	{
		//如果工作中的计时器数量为0 那么就没有更新信息的必要  
		//毕竟都是空闲的

		return;
	}

	for(int i = 0; i< _workeringTimer.Count; i++)
	{
		if(_workeringTimer[i].GetTimerState() == TimerState.WORKING)
		{
			//对当前队列的第i个计时器的状态进行比较  如果为 工作中

			//更新这个计时器的信息
			_workeringTimer[i].UpdateTimer();
		}
		else
		{
			//执行到这里 就说明当前队列的第i个计时器的任务已经完成了

			//将这个计时器放到空闲计时器中
			_notWorkerTimer.Enqueue(_workeringTimer[i]);
			//重置计时器
			_workeringTimer[i].ResetTimer();
			//将这个计时器从工作中计时器列表中移除
			_workeringTimer.Remove(_workeringTimer[i]);
		}
	}
}
```
## 生命周期函数

#### `Start()`
- `InitTimerManager();`
	- 初始化创建计时器
#### ` Update()`
- `UpdateWorkingTimer();`
	- 更新计时器，进行计时作用

# 外部调用时

- 当外部需要一个计时器，调用 GameTimerManager 中的 `TryGetTimer` 函数
	- 传入计时的时长与计时完成后要执行的委托
- `TryGetTimer` 函数 判断空闲计时器的数量，决定是否需要创建一个计时器（`timer`）
- 调用`timer`中的`StartTimer` 函数，将时长与委托传进去
- 在 `Update` 函数中 调用 `UpdateWorkingTimer` 函数
	- `UpdateWorkingTimer` 函数 内部会使用 for 循环来判断计时器的工作状态
		- 如果为 工作中 ，就调用计时器对象的 `UpdateTimer` 函数 进行更新
		- 如果工作完成，就放到 空闲计时器队列 中，然后调用`ResetTimer` 函数重置一下
			- 并将其 在 工作中计时器 队列中 删除
# 最后

- 记得创建一个空物体，并将 GameTimerManager 脚本 挂上去

---
# 源代码

![[GameTimeManager.cs]]

---