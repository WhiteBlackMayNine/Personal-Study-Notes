---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-20
---

---
# 关联知识点



---
# 事件触发器是什么

- 事件触发器是 **EventTrigger 组件 **
	- 可以在 Inspector 窗口上添加
- 它是一个集成了上节课中学习的所有事件接口的脚本  
- 它可以让我们更方便的为控件添加事件监听
# 如何使用事件触发器

- 拖曳脚本进行关联  
	- 根据其他组件一样
	- 但要注意，需要先选择接口，然后才能拖脚本
	  
- 代码添加
	- 先创建一个 `EventTrigger` 对象
		- 用于 得到 EventTrigger 组件
	- 然后声明 一个希望监听的事件对象，并选择 该事件的类型（`eventID`）
		- 也就是 [[1 UI事件监听接口]] 的那些方向
	- 进行监听关联
		- `entry.callback.AddListener` 往回调方法中添加想要监听的方法
	- 然后把 事件对象 添加到 `EventTrigger` 对象 中

- 注意 代码添加后 Inspector 窗口不会显示出来
	- 但其实已经添加进去了

```C#
public EventTrigger et;

//申明一个希望监听的事件对象  
EventTrigger.Entry entry = new EventTrigger.Entry();  
//申明 事件的类型  
entry.eventID = EventTriggerType.Drag;  
//监听函数关联  
entry.callback.AddListener((data) =>  
{  
    print("抬起");  
});

//把申明好的 事件对象 加入到 EventTrigger当中  
et.triggers.Add(entry);  
  
entry = new EventTrigger.Entry();  
//申明 事件的类型  
entry.eventID = EventTriggerType.BeginDrag;  
//监听函数关联  
entry.callback.AddListener((data) =>  
{  
    print("抬起");  
});  
  
et.triggers.Add(entry);  
  
entry = new EventTrigger.Entry();  
//申明 事件的类型  
entry.eventID = EventTriggerType.BeginDrag;  
//监听函数关联  
entry.callback.AddListener((data) =>  
{  
    print("抬起");  
});  
  
et.triggers.Add(entry);
```
# 总结

- EventTrigger 可以让我们写更少的代码  
- 可以在面板类中处理面板控件的事件逻辑，更加的面向对象，便于管理


---
# 源代码

![[Unity UGUI 进阶 EventTrigger 事件触发器 知识点.cs]]

---