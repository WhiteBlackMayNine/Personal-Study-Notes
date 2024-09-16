---
tags:
  - "#科学/Unity/ARPG/Timer/GameTimer"
created: 2024-06-20
---

---
# 关联知识点

[[GameTimerManager]]

---
# GameTimer

- 这个类定义的是计时器
- 里面的函数与变量实现的都是计时器的功能
	- 开始计时、更新等
- 不需要继承任何类
## 变量

- `float _startTime`
	- 计时的时长
- `Action _task`
	- 计时结束后执行的委托
- `bool _isStopTimer`
	- 是否停用当前计时器
- `TimerState _timerState`
	- 计时器的状态
## 函数

### 开始计时 `StartTimer`

```C#
  public void StartTimer(float time,Action task)
{
	//这个函数用来接受外部传入的数据 并将状态改为 工作中
	_startTime = time;
	_task = task;
	_isStopTimer = false;
	_timerState = TimerState.WORKING;
}
```
### 更新计时器 `UpdateTimer`

```C#
  public void UpdateTimer()
{
	//这个函数用来进行对计时器的更新
	if (_isStopTimer)//如果这个计时器被停用，直接返回
	{
		return;
	}

	_startTime -= Time.deltaTime;//计时时长减少

	if(_startTime < 0f)
	{
		//如果_startTime < 0f 那么说明计时已经完成可以开始执行任务了
		_task?.Invoke();//检测是否为空，如果不为空就执行委托里面的函数
		_timerState = TimerState.DONE;//将状态改为 已完成
		_isStopTimer = true;//停用计时器
	}
}
```
### 定计时器状态

```C#
public TimerState GetTimerState() => _timerState;
```
### 重置计时器信息 `ResetTimer`

```C#
  public void ResetTimer()
{
	_startTime = 0f;
	_task = null;
	_isStopTimer = true;
	_timerState = TimerState.NOTWORKING;
}
```
### 构造函数

```C#
  public GameTimer()
{
	//当new时，直接调用重置函数  相当于进行一次赋值
	ResetTimer();
}
```
## 枚举 `TimerState`

### 计时器的工作状态

```C#
public enum TimerState
{
    NOTWORKING,//空闲中
    WORKING,//工作中
    DONE//已完成
}
```
# 更新

- 这个脚本用来 实现 计时器 对象
- 包括，开始计时、更新计时器（更新计时时长）、确定计时器状态、重置计时器信息
- 而 GameTimerManager 则是用来管理计时器对象的（管理 实例化的 GameTimer）
- 在 GameTimerManager 中，调用 GameTimer 的方法，完成计时功能
- 具体实现 ——> [[GameTimerManager#^b15e63|GameTimerManager 更新]]

---
# 源代码

![[GameTimer.cs]]

---