---
tags:
  - 科学/编程语言/Csharp/唐老狮/进阶/反射和特性/反射/概念和关键类Type
created: 2024-03-22
课时: "41"
来源: https://www.taikr.com/course/1144/task/36066/show
备注: 在 Unity 中及其重要，主要用于得到其他程序集的信息，最重要的为 Type
---

---
# 关联知识点

[[关键类Assembly和Activator]]

---
# 知识点

## 什么是程序集

- 程序集是经由编译器编译得到的，供进一步编译执行的那个中间产物
- 在WINDOWS系统中，它一般表现为后缀为`.d11`（库文件）或者是`.exe`（可执行文件）的格式
- 说人话
	- 程序集就是我们写的一个代码集合，我们现在写的所有代码
	- 最终都会被编译器翻译为一个程序集供别人使用
	- 比如一个代码库文件`.d11`或者一个可执行文件`.exe`
## 元数据

- 元数据就是用来描述数据的数据
- 这个概念不仅仅用于程序上，在别的领域也有元数据
- 说人话
	- 程序中的类，类中的函数、变量等等信息就是 程序的 元数据
	- 有关程序以及类型的数据被称为 元数据，它们保存在程序集中
## 反射的概念

- 说人话
	- 在程序运行时，通过反射可以得到其它程序集或者自己程序集代码的各种信息
	- 类，函数，变量，对象等等，实例化它们，执行它们，操作它们
## 反射的作用

- 因为反射可以在程序编译后获得信息，所以它提高了程序的拓展性和灵活性
- 程序运行时得到所有元数据，包括元数据的特性
- 程序运行时，实例化对象，操作对象
- 程序运行时创建新对象，用这些对象执行任务
## 语法相关

### Type

### 概念
- `Type`（类的信息类）
- 它是反射功能的基础
- 它是访问元数据的主要方式
- 使用 Type 的成员获取有关类型声明的信息
- 有关类型的成员（如构造函数、方法、字段、属性和类的事件）
### 获取Type

### `GetType()`

- 万物之父`object`中的 `GetType()`可以获取对象的`Type`
	- `int a = 42;Type type = a.GetType();`
	- 无论是值类型还是引用类型，都可以使用
### `typeof`

- 通过`typeof`关键字 传入类名 也可以得到对象的`Type`
	- `Type type2 = typeof(int);`
### 类的名字

- 通过类的名字 也可以获取类型
	- `Type type3 = Type.GetType("System.Int32");
	- 注意 类名必须包含命名空间 不然找不到
### 注意

- `type type2 type3`
	- 三者的引用地址是一致的，内存都是相同的一份（栈不同，堆相同）
### 得到类的程序集信息

- 可以通过`Type`可以得到类型所在程序集信息
- `Console.Write(type.Assembly);`
#### 获取类中的所有公共成员

- 首先得到`Type`
	- `Type t=typeof(Test);
		- Test 为一个自定义的一个类
- 然后得到所有公共成员
	- `MemberInfo[] infos = t.GetMembers();
- 需要引用命名空间 `using system.Reflection;
#### 获取类的公共构造函数并调用

### 获取所有构造函数

- `ConstructorInfo[] ctors = t.GetConstructors();
### 获取其中一个构造函数 并执行

- 需要构造函数 传入`Type`数组 数组中内容按顺序是参数类型
- 执行构造函数 传入`object`数组 表示按顺序传入的参数
### 得到无参构造

- `ConstructorInfo info =t.GetConstructor(new Type[0]);
	- `new Type[0]`意思为 创建一个`Type`数组，并作为参数传入
- 执行无参构造
	- `Test obj = info.Invoke(参数) as Test;`
		- 执行无参构造后类型为`object`，使用`as`转换为`Test`并存入`obj`中
		- 无参构造 没有参数 传`null
		- 其实这个过程就是实例化对象
### 得到有参构造

- `ConstructorInfo info2 = t.Getconstructor(new Type[]{ typeof(int) });`
	- `Type[]{ }` 里面需要写入有参构造的参数类型
	- 多个参数使用`,`隔开
- `Test obj = info2.Invoke(new object[]{ 2 }) as Test;` 
	- 记得传入具体参数
	- 多个参数使用`,`隔开
#### 获取类的公共成员变量

### 得到所有成员变量

- `FieldInfo[] fieldInfost = t.GetFields();
### 得到指定名称的公共成员变量

- `FieldInfo infoJ = t.GetField("成员变量名");`
	- 双引号不能省略
	- 因为传入的参数类型为`string`
### 通过反射获取和设置对象的值

- `Test test = new Test(); test.j = 99; test.str = "222"`
	- `j str` 都是公共成员变量
- 通过反射 获取对象的某个变量的值
	- `infoJ.GetValue(test);
		- `infoJ` 得到了某一公共成员变量，使用`GetValue`进行获取
- 通过反射 设置指定对象的某个变量的值
	- `infoJ.SetValue(test,100)`
#### 获取类的公共成员方法

- 通过`Type`类中的 `GetMethod` 方法 得到类中的方法
	-  `MethodInfo` 是方法的反射信息
	- `Type strType = typeof(string);`
- 如果存在方法重载 用`Type`数组表示参数类型
	- `MethodInfo[] methods = strType.GetMethods();
	- `MethodInfo substr = strType.GetMethod("substring"new Type[]{typeof(int) ,typeof(int)});
- 调用该方法 
	- `object result = substr.Invoke(str,new object[]{7，5});
	- 注意
		- 第一个参数 相当于 是哪个对象要执行这个成员方法
		- 如果是静态方法 `Invoke` 中的第一个参数传`null`即可
### 其它
- `Type;
	- 得枚举
		- `GetEnumName
		- `GetEnumNames
	- 得事件
		- `GetEvent
		- `GetEvents
	- 得接口
		- `GetInterface
		- `GetInterfaces
	- 得属性
		- `GetProperty
		- `GetPropertys

---
# 总结

总结在 [[关键类Assembly和Activator]]

---
# 源代码

- 代码在这里

![[反射练习题.cs]]

---
# 练习题

![[反射知识点 .cs]]

---





