---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-13
---

---
# 关联知识点



---
# Toggle是什么

- Toggle是开关组件
	- 是UGUI中用于处理玩家单选框多选框相关交互的关键组件

- 开关组件 默认是多选框
- 可以通过配合ToggleGroup组件制作为单选框

- 默认创建的Toggle由4个对象组成
- 父对象
	- Toggle组件依附
- 子对象
	- 背景图（必备）、选中图（必备）、说明文字（可选）

# Toggle参数

- 部分参数与 Button 一样

- IsOn:当前是否处于打开状态

- Toggle Transition
	- 在开关值变化时的过渡方式
		- None
			- 无任何过渡，直接显示隐藏
		- Fade
			- 淡入淡出

- Graphic
	- 用于表示选中状态的图片

- Group（多个 Toggle 放到一个 挂载 Toggle Group 的父物体下）
	- 单选框分组
		- Allow Switch Off
			- 是否允许不选中任何一个单选框
				- ——> 是否允许 一个都不选
		- 注意
			- 单选框分组组件 可以挂载在任何对象上，只需要将其和一组单选框关联即可
				- Toggle，只要关联的 Group 为同一个就行
				- 无论是创建空物体，还是单独的给一个 Toggle 添加 Toggle Group 组件
	- 一般来讲，会单独创建一个空物体，然后添加组件 Toggle Group（开关分组组件）
		- 然后在各个 Toggle 中，将父对象拖入 Group 参数中
		- 如果 不同的 Toggle 关联的 Group 为同一物体
			- 就仍为这些 Toggle 为 一组

- OnValueChanged
	- 开关状态变化时执行的函数列表

# 代码控制

```C#
//获取组件
Toggle tog = this.GetComponent<Toggle>();  
tog.isOn = true;  
print(tog.isOn);  
  
ToggleGroup togGroup = this.GetComponent<ToggleGroup>();  
togGroup.allowSwitchOff = false;  
  
//可以遍历提供的迭代器 得到当前处于选中状态的 Toggle
//遍历所有关联 Toggle Group 的 Toggle，得到当前激活的 Toggle
//也就是 isOn = true 的 Toggle
foreach (Toggle item in togGroup.ActiveToggles())  
{  
    print(item.name + " " + item.isOn);  
}
```
# 监听事件的两种方式

- 方式一
	- 拖脚本
		- 在 Inspector 窗口中
		- 关联脚本，然后选择方法
			- 注意 ——> 选择上面的 动态（Dynamic bool）
				- 如果选择静态，当传入参数改变时，调用的方法中的参数可能没有改变
				- 所以选择 动态

- 方式二
	- 代码添加

```C#
//添加方法
tog.onValueChanged.AddListener(ChangeValue2);  
//Lambad 表达式 添加 匿名函数
tog.onValueChanged.AddListener((b) =>  
{  
    print("代码监听 状态改变" + b);  
});

//两个测试函数
public void ChangValue(bool isOn)  
{  
    print("状态改变" + isOn);  
}      
private void ChangeValue2(bool v)  
{  
    print("代码监听 状态改变" + v);  
}
```


---
# 源代码

![[Unity UGUI 组合控件 Toggle 开关控件 知识点.cs]]

---