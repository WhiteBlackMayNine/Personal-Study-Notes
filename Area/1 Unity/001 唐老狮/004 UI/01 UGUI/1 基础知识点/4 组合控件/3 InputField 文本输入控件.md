---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-13
---

---
# 关联知识点



---
# InputField是什么

- InputField 是输入字段组件  
- 是UGUI中用于处理玩家文本输入相关交互的关键组件 
  
- 默认创建的InputField由3个对象组成  
- 父对象
	- InputField组件依附对象 以及 同时在其上挂载了一个Image作为背景图  
- 子对象
	- 文本显示组件（必备）、默认显示文本组件（必备）
# InputField参数

- TextComponent
	- 用于关联显示输入内容的文本组件

- Text
	- 输入框的起始默认值

- Character Limit
	- 可以输入字符长度的最大值
		- 如果为0 ——> 可以输入无限个字符

- Content Type
	- 输入的字符类型限制
		- Standard
			- 标准模式;可以输入任何字符
		- Autocorrected
			- 自动更正模式
				- 跟踪未知单词，向用户建议合适的替换候选词
		- Integer Number
			- 整数模式
				- 用户只能输入整数
		- Decimal Number
			- 十进制数模式
				- 用于只能输入数字，包括小数
		- Alphanumeric
			- 字母数字模式
				- 只能输入字母和数字
		- Name
			- 名字模式
				- 自动将每个单子首字母大写
		- Email Address
			- 邮箱地址模式
				- 允许最多输入一个@符号组成的字符和数字字符串
		- Password
			- 密码模式
				- 用星号隐藏输入的字符，允许使用符号
		- Pin
			- 别针模式
				- 用星号隐藏输入的字符，只允许输入整数
		- Custom
			- 自定义模式
				- 允许自定义行类型，输入类型，键盘类型和字符验证

- Line Type
	- 行类型，定义文本格式
		- Single Line
			- 只允许单行显示
		- Multi Line Submit
			- 允许使用多行
				- 仅在需要时使用新的一行
		- Multi Line NewLine
			- 允许便用多行
				- 用户可以按回车键空行

- Character Limit
	- 可以输入字符长度的最大值

- 以下参数不重要，了解即可
	- Placeholder
		- 关联用于显示初始内容文本控件
	- Caret Blink Rate
		- 光标闪烁速率
	- Caret Width
		- 光标宽
	- Custom Caret Color
		- 自定义光标颜色
	- Selection Color
		- 批量选中的背最颜色
	- Hide Mobile Input
		- 隐藏移动设备屏幕上键盘，仅适用于IOS
	- Read Only
		- 只读，不能改

# 代码控制

```C#
InputField input = this.GetComponent<InputField>();  
print(input.text);  
input.text = "123123123123";
```
# 监听事件的两种方式

- 注意
	- InputField 提供了两种事件
	- `On Value Changed(String)`
		- 当输入的内容改变时，调用事件
			- ——> 每次改变都去调用
	- `On End Editor(String)`
		- 结束编辑时，调用事件
			- ——> 写完了再去调用
- 备注
	- 只要 InputField 里面光标消失
	- 就会 认为 ——> 结束编辑

- 方式一
	- 拖脚本
		- 略

- 方式二
	- 代码添加

```C#
input.onValueChanged.AddListener((str) =>  
{  
    print("代码监听 改变" + str);  
});  
  
input.onEndEdit.AddListener((str) =>  
{  
    print("代码监听 结束输入" + str);  
});


public void ChangeInput(string str)  
{  
    print("改变的输入内容" + str);  
}  
  
public void EndInput(string str)  
{  
    print("结束输入时内容" + str);  
}
```



---
# 源代码

![[Unity UGUI 组合控件 InputField 文本输入控件 知识点.cs]]

---