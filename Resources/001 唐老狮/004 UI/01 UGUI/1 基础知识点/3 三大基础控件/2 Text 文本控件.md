---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-12
---

---
# 关联知识点



---
# Text是什么

- Text是文本组件
- 是UGUI中用于显示文本的关键组件
# Text参数相关

- Text
	- 文本显示内容

- Font
	- 字体
- Fontstyle
	- 字体样式
		- Normal
			- 普通
		- Bold
			- 加粗
		- ltalic
			- 斜体
		- Bold And ltalic
			- 加粗 + 斜体

- Font size
	- 字体大小
	- 如果字体过大，但 RectTransforms 的矩形太小
		- 那么文字不会显示

- Line Spacing
	- 行之间的垂直间距

- Rich Text
	- 是否开启富文本

- Alignment
	- 对其方式

- Horizontal Overflow
	- 处理 ==文本太宽== 无法放入矩形范围内时的处理方式
		- Wrap
			- 包裹模式（推荐使用）
				- 字体始终在矩形范围内，会自动换行
		- Overflow
			- 溢出模式（可能会挡住其他UI元素）
				- 字体可以溢出矩形框

- Vertical Overflow
	- 处理 ==文本太高== 无法放入矩形范围内时的处理方式
		- Truncate
			- 有截断模式（推荐使用）
				- 字体始终在矩形范围内，超出部分裁剪
		- Overflow
			- 溢出模式（可能会挡住其他UI元素）
				- 字体可以溢出矩形框

- Best Fit
	- 忽略 ==字体大小==，始终把内容完全显示在矩形框中，*会自动调整字体大小*
		- Minsize
			- 最小多小
		- MaxSize
			- 最大多大

- Align By Geometry
	- 使用字形集合形状范围进行水平对其，而不是字形指标
	- （不是很重要，一般不设置）
# 富文本

- UGUI 中特殊的文本格式
	- 希望 Text 中，部分的内容有特殊效果时，可以使用这个
	- 需要勾选 Rich Text

- 加粗
	- `<b>文本内容</b>`
- 斜体
	- `<i>文本内容</i>`
- 大小
	- `<size=50>文本内容</size>`
- 颜色
	- `<color=#ff0000ff>文本内容</size>`
	- `<color=red>文本内容</size>`
# 边缘线和阴影
## 边缘线

- 额外添加组件 `outline`
## 阴影

- 额外添加组件 `Shadow`

# 代码控制

```C#
Text txt = this.GetComponent<Text>();  
txt.text = "唐老狮 哈哈哈哈哈";
```
# 补充

- Windows 自带字体
	- C盘 ——> Windows ——> Fonts

- Text
	- 也会遮挡射线检测
	- 如果发现按钮点击不了
	- 记得检查一下 Raycast Target 是否勾选

---
# 源代码

![[Unity UGUI 三大基础控件 Text 文本控件 知识点.cs]]

---