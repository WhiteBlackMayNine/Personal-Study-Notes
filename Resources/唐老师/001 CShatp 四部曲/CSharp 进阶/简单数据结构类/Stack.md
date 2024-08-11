---
tags:
  - 科学/编程语言/Csharp/唐老狮/进阶/简单数据结构类/Stack
created: 2024-03-22
课时: "4"
来源: https://www.taikr.com/course/1144/task/35931/show
备注: 中文名为 栈
---

---
# 关联知识点

[[Hashtable]] [[Arraylist]] [[Queue]]

---
# 知识点

## 本质

- stack（栈）是一个C#为我们封装好的类
- 它的本质也是object数组，只是封装了特殊的存储规则
- stack是栈存储容器，栈是一种**先进后出**的数据结构
	- 先存入的数据后获取，后存入的数据先获取
- 栈是先进后出
## 声明

- 需要先引用类名空间（建议使用小灯泡自动补全）
- `Stack stack = new Stack()'`
## 增取查改

- （Text 为任何类型）
### 增

- `stack.Push(Text)`
	- **压栈（存）
### 取

- 栈中不存在删除概念，只有取的概念
	- 遵循 先进后出
- `object obj = stack.Pop();`
	- **弹栈（取）
### 查

- 栈无法查看指定位置的元素，只能查看栈顶的内容
- `obj = stack.Peek();`
	- 注意
		- 只是看一看，并没有从中取出来
- `stask.Contains(Text)`
	- 查看元素是否存在于栈中
	- 返回值为布尔值
### 改

- 栈无法改变其中元素，只能压（存）和弹（取）
- 实在要改，只有清空
- `stack.Clear();`
## 遍历

- 长度
	- `stack.Count`
- foreach 遍历
	- 而且遍历出来的顺序也是从栈顶到栈底
	- `foreach(object item in stack){ }`
- 还有一种方法
	- 方法
		- 将栈转换为 object 数组
		- 遍历出来的顺序也是从栈顶到栈底
	- 代码
		- `object[] array = stack.ToArray();`
		- 随后使用 循环语句
- 循环弹栈
	- 概念
		- 使用循环结构来连续地从栈（Stack）中移除元素，直到满足某个特定的条件
		- 一般搭配 while 来使用
	- 代码
		- `while(stack.Count > 0){object o = stack.Pop(); Console.WriteLine(o);}`
		- 边取边用，利用先进后出
- 注意
	- 由于栈中没有提供索引器通过中括号来访问接口的，无法使用 `for` 循环来遍历
## 装箱拆箱

- 装箱拆箱
	- 当往其中进行值类型存储时，就是在装箱
	- 当将值类型对象取出来转换使用时，就存在拆箱

---
# 源代码

![[Stack 练习题.cs]]

---
# 练习题

![[Stack知识点.cs]]

---

