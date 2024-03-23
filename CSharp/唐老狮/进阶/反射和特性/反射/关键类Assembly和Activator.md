---
tags:
  - 科学/编程语言/Csharp/唐老师/进阶/反射和特性/反射/关键类Assembly和Activator
created: 2024-03-22
课时: "42"
来源: https://www.taikr.com/course/1144/task/36067/show
---

---
# 关联知识点

[[概念和关键类Type]]

---
# 知识点

## `Activator`

### 作用

- 用于快速实例化对象的类
- 用于将`Type`对象快捷实例化为对象
### 方法

- 先得到`Type
	- `Type testType = typeof(Test);`
- 然后 快速实例化一个对象
	- 无参构造
		- `Test testobj = Activator.CreateInstance(testType)as Test;
	- 有参构造
		- `Test testobj = Activator.CreateInstance(testType, 99)as Test;
			- 第一个参数为：得到`Type`的对象
			- 后面的参数为：构造函数的参数
## `Assembly`

### 程序集类

- 主要用来加载其它程序集，加载后
- 才能用`Type来使用其它程序集中的信息
- 如果想要使用不是自己程序集中的内容 需要先加载程序集
- 比如 `.dll`文件（库文件）
- 简单的把库文件看成一种代码仓库，它提供给使用者一些可以直接拿来用的变量、函数或类
### 三种加载程序集的函数

- 一般用来加载在同一文件下的其它程序集
	- `Assembly asembly2 = Assembly.Load("程序集名称");
- 一般用来加载不在同一文件下的其它程序集
	- `Assembly asembly = Assembly.LoadFrom("包含程序集清单的文件的名称或路径”);
	- `Assembly asembly3 = Assembly.LoadFile("要加载的文件的完全限定路径”);
### 先加载一个指定程序集

- `Assembly asembly = Assembly.LoadFrom(@"C\……)`
	- 路径名需要注意转义字符
		- 使用`\\` 或者`@`
- `Type[]types = asembly.GetTypes();
### 再加载程序集中的一个类对象 之后才能使用反射

- `Type icon = asembly.GetType("Lesson18 练习题.Icon");
- `MemberInfo[] members = icon.GetMembers();
## 通过反射 实例化一个对象

- 首先得到枚举`Type`来得到可以传入的参数
	- `Type moveDir = asembly.GetType("Lesson18 练习题.E MoveDir"),FieldInfo right = moveDir.GetField("Right");
- 直接实例化对象
	- `object iconobj = Activator.CreateInstance(icon, 10,5,right.GetValue(null));
- 通过反射，得到对象中的方法
	- `MethodInfomove = icon.GetMethod("Move");
	- `MethodInfo draw = icon.GetMethod("Draw");
	- `MethodInfo clear =icon.GetMethod("Clear");
## 类库工程创建

- 类库（`.dll`工程）工程创建
	- 创建
		- 右键创建选择类库文件
	- 作用
		- 没有控制台，纯写算法、逻辑，无法被执行

---
# 总结

- 反射
	- 在程序运行时，通过反射可以得到其他程序集或者自己的程序集代码的各种信息
	- 类、函数、变量、对象等等，实例化他们，执行他们，操作他们
- 关键类
	- `Type
	- `Assembly
	- `Activator
- 对于我们的意义
	- 在初中级阶段 基本不会使用反射
	- 所以目前对于大家来说，了解反射可以做什么就行
	- 很长时间内都不会用到反射相关知识点
- 为什么要学反射
- 为了之后学习unity引擎的基本工作原理做铺垫
- unity引擎的基本工作机制 就是建立在反射的基础上

---
# 源代码 / 练习题

知识点、练习题代码在[[概念和关键类Type]]

---


