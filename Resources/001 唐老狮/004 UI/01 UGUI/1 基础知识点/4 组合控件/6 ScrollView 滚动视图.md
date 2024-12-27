---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-14
---

---
# 关联知识点



---
# ScrollRect是什么

- ScrollRect是滚动视图组件  
- 是UGUI中用于处理滚动视图相关交互的关键组件  
  
- 默认创建的ScrollRect由4组对象组成  
	- 父对象
		- ScrollRect组件依附的对象，还有一个Image组件 为背景图  
	- 子对象  
		- Viewport
			- 控制滚动视图可视范围和内容显示  
		- Scrollbar Horizontal 
			- 水平滚动条  
		- Scrollbar Vertical 
			- 垂直滚动条

- 在 Hierarchy 窗口中创建时，可以看见 ScrollView
- 但对象上依附的组件名为 ScrollRect
# ScrollRect参数

- ***Content***
	- 控制滚动视图显示内容的父对象，它的尺寸有多大滚动视图就能拖多远
		- 如果想要在滚动视图中显示内容，那么就要把UI元素放到 Content 关联的父对象下面
		- 也就是做为子对象

- Horizontal
	- 启用水平滚动

- Vertical
	- 启用垂直滚动

- Movement Type
	- 滚动视图元素的运动类型。主要控制拖动时的反馈效果
		- Unrestricted（一般不使用）
			- 不受限制，随便拖动
		- Elastic（常用）
			- 回弹效果，当滚出边缘后，会弹回边界
				- Elasticity
					- 回弹系数，控制回弹效果。值越大回弹越慢
		- Clamped
			- 夹紧效果，始终限制在范围内，没有回弹效果

- Inertia
	- 移动惯性
		- 如果开启，松开鼠标后会有一定的移动惯性

- Deceleration Rate
	- 减速率（0~1）
		- 0 ——> 没有惯性，1 ——> 不会停止

- Scroll sensitivity
	- 滚轮（鼠标中间）和触摸板（笔记本）的滚动事件敏感性

- Viewport
	- 关联滚动视图内容视口对象

- Horizontal Scrolbar
	- 关联水平滚动条

- Visibility
	- 是否在不需要时自动隐藏等模式
		- Permanent
			- 一直显示滚动条
		- Auto Hide
			- 自动隐藏滚动条
		- Auto Hide And Expand Viewport
			- 自动隐藏滚动条并且自动拓展内容视口

- Spacing
	- 滚动条和视口之间的间隔空间

- OnValueChanged
	- 滚动视图位置改变时执行的函数列表
# 代码控制

```C#
ScrollRect sr = this.GetComponent<ScrollRect>();  
//改变内容的大小 具体可以拖动多少 都是根据它的尺寸来的  
//sr.content.sizeDelta = new Vector2(200, 200);  
  
sr.normalizedPosition = new Vector2(0, 0.5f);
```
# 监听事件的两种方式

- 拖脚本
- 代码添加

- 备注
	- 传入的 Vector2 参数，为移动位置
	- 做了解即可

```C#
sr.onValueChanged.AddListener((vec) =>  
{  
    print(vec);  
});
```
# 补充

- 如果不想要滚动条，可以直接把滚动条物体删除
- 但要注意，记得去 ScrollRect 上，把 Scrollbar 给置空（即 选择 None）
	- 如果为 Miss，会导致出现 Bug
- 同时，还要改一下 Viewport 的尺寸

---
# 源代码

![[Unity UGUI 组合控件 ScrollView 滚动视图 知识点.cs]]

---