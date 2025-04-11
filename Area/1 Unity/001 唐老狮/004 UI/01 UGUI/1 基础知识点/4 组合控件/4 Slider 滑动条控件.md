---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-13
---

---
# 关联知识点



---
# Slider是什么

- Slider是滑动条组件  
- 是UGUI中用于处理滑动条相关交互的关键组件  
  
- 默认创建的Slider由4组对象组成  
	- 父对象
		- Slider组件依附的对象  
	- 子对象
		- 背景图、进度图、滑动块三组对象
# Slider参数

- FillRect
	- 用于填充的进度条图形

- Handle Rect
	- 用于滑动的滑动块图形
		- Direction
			- 滑动条值增加的方向
				- Left To Right
					- 从左到右
				- Right To Left
					- 从右到左
				- Bottom To Top
					- 从下到上
				- Top To Bottom
					- 从上到下

- Min Value 和 Max Value
	- 最小值和最大值，滑动滚动条时，值将从 最小 到 最大 之间变化（左右、上下极值）

- Whole Numbers
	- 是否约束为整数值变化

- Value
	- 当前滑动条代表的数值

- OnValuechanged
	- 滑动条值改变时执行的函数列表

# 代码控制

```C#
Slider s = this.GetComponent<Slider>();
```
# 监听事件的两种方式

- 拖脚本
- 代码添加

- 与前面几个相同
	- 略

```C#
s.onValueChanged.AddListener((v) =>  
{  
    print("代码添加的监听" + v);  
});
```

---
# 源代码

![[Unity UGUI 组合控件 Slider 滑动条控件 知识点.cs]]

---