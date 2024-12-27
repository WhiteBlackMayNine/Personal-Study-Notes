---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: ""
---

---
# 关联知识点



---
# Button是什么

- Button是按钮组件
- 是UGUI中用于处理玩家按钮相关交互的关键组件

- 默认创建的Button由2个对象组成
- 父对象——Button组件依附对象 同时挂载了一个Image组件 作为按钮背景图
- 子对象——按钮文本（可选）
# Button参数

- Interactable
	- 是否接受输入
		- 按钮是否可以被点击

- Transition
	- 响应用户输入的过渡效果
		- None
			- 没有状态变化效果
		- ColorTint
			- 用颜色表示不同状态的变化
				- TargetGraphic
					- 控制的目标图形
				- Normal Color
					- 正常状态颜色
				- Highlighted Color
					- 鼠标进入时显示高亮颜色
				- Pressed Color
					- 按下颜色
				- Selected Color
					- 选中的颜色
				- Disabled Color
					- 禁用时的颜色
				- Color Multiplier
					- 颜色倍增器，过渡颜色乘以该值
				- FadeDuration
					- 衰减持续时间，从一个状态进入另一个状态时需要的时间
		- Sprite Swap
			- 用图片表示不同状态的变化
				- Highlighted Sprite
					- 选中时图片
				- Pressed Sprite
					- 按下时图片
				- Disabled Sprite
					- 禁用时显示的图片
		- Animation
			- 用动画表示不同状态的变化
				- Normal Trigger
					- 正常动画触发器
				- Highlighted Trigger
					- 鼠标进入状态时触发器
				- Pressed Trigger
					- 按下时触发器
				- Selected Trigger
					- 选中时触发器
				- Disabled Trigger
					- 禁用时触发器
			- ——> 其实就是 动画控制器（可以点击下方的 Auto 来自动创建）
				- 点击动画后，可以使用 Animation （Ctrl + 6）进行编辑动画

- Navigation
	- 导航模式，可以设置UI元素如何在播放模式中控制器导航
		- None
			- 无键盘导航
		- Horizontal
			- 水平导航
		- Verticval
			- 垂直导航
		- Automatic
			- 自动导航
		- Explicit
			- 指定周边控件进行导航
			- 手动设置，将想要导航的 按钮 拖入到组件中
		- Visualize
			- 点击后，可以在场景窗口看到导航连线
				- 导航 ——> 使用 WASD 进行切换
				- ——> 需要在 Event System 中，勾选 Send Navigation Events

- OnClick
	- 单击（按下再抬起）执行的函数列表
# 代码控制




# 监听点击事件的两种方式

- 点击事件 是 在按钮区域抬起按下一次 就算一次点击
	- OnClick

- 方式一
	- 拖脚本
		- 在 OnClick 中点击 + 号
		- 然后关联脚本，选择其中的方法

- 方式二
	- 代码添加

```C#
//获取组件
Button btn = this.GetComponent<Button>();
//添加方法
btn.onClick.AddListener(ClickBtn2);  
//利用 Lambad 添加 匿名函数
btn.onClick.AddListener(() => {  
    print("123123123");  
});  
//移除 指定的方法
btn.onClick.RemoveListener(ClickBtn2);  
//移除 所有方法
btn.onClick.RemoveAllListeners();

//两个测试方法
public void ClickBtn()  
{  
    print("按钮点击，通过拖代码的形式");  
}  
  
private void ClickBtn2()  
{  
    print("按钮点击，通过拖代码的形式2");  
}
```
# 补充

- Button 本质上仍然为一个组件
- 创建 Text 后，可以在 Text 上添加 Button 组件
- 这样，文字就可以被点击了

---
# 源代码

![[Unity UGUI 组合控件 Button 按钮控件.cs]]

---