---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-13
---

---
# 关联知识点



---
# Scrollbar是什么

- Scrollbar是滚动条组件  
- 是UGUI中用于处理滚动条相关交互的关键组件  
  
- 默认创建的Scrollbar由2组对象组成  
	- 父对象
		- Scrollbar组件依附的对象  
	- 子对象
		- 滚动块对象  
		  
- 一般情况下我们不会单独使用滚动条
- 都是配合ScrollView滚动视图来使用
# Scrollbar参数

- Handle Rect
	- 关联滚动块图形对象

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

- Value
	- 滚动条初始位置值（0~1）
- Size
	- 滚动块在条中的比例大小（0~1）
	- 也就是图片的比例大小

- Number Of Steps
	- 允许可以滚动多少次（不同滚动位置的数量）
	- 例如：5 ——> 把滚动条分为五段，滑动一次走一段

- OnValuechanged
	- 滚动条值改变时执行的函数列表

# 代码控制

```C#
Scrollbar sb = this.GetComponent<Scrollbar>();
```
# 监听事件的两种方式

- 拖脚本
- 代码添加

```C#
sb.onValueChanged.AddListener((v) => {  
    print("代码监听的函数" + v);  
});

public void ChangeValue(float v)  
{  
    print(v);  
}
```

---
# 源代码

![[Unity UGUI 组合控件 ScrollBar 滚动条 知识点.cs]]

---