---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-14
---

---
# 关联知识点



---
# DropDown是什么

- DropDown是下拉列表（下拉选单）组件  
- 是UGUI中用于处理下拉列表相关交互的关键组件  
  
- 默认创建的DropDown由4组对象组成  
	- 父对象  
		- DropDown组件依附的对象 还有一个Image组件 作为背景图
	- 子对象
		- Label是当前选项描述  
		- Arrow右侧小箭头  
		- Template下拉列表选单
# DropDown参数

- Navigation
	- 导航模式，可以设置UI元素如何在播放模式中控制器导航

- Interactable
	- 是否接受输入

- Template
	- 关联下拉列表对象

- Caption Text
	- 关联显示当前选择内容的文本组件

- Caption lmage
	- 关联显示当前选择内容的图片组件

- Item Text
	- 关联下拉列表选项用的文本控件

- Item lmage
	- 关联下拉列表选项用的图片控件

- Value
	- 当前所选选项的索引值

- Alpha Fada Speed
	- 下拉列表窗口淡入淡出的速度

- ***Options***
	- 存在的选项列表

# 代码控制

```C#
Dropdown dd = this.GetComponent<Dropdown>();  
//当前所选选项的索引值
print(dd.value);  
//通过索引，得到信息
print(dd.options[dd.value].text);  
  
dd.options.Add(new Dropdown.OptionData("123123123"));
```
# 监听事件的两种方式

- 拖脚本
- 代码添加

```C#
dd.onValueChanged.AddListener((index) => {  
  
    print(index);  
});
```


---
# 源代码



---