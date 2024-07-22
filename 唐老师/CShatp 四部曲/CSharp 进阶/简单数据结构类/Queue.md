---
tags:
  - 科学/编程语言/Csharp/唐老师/进阶/简单数据结构类/Queue
created: 2024-03-22
课时: "6"
来源: https://www.taikr.com/course/1144/task/35933/show
---

---
# 关联知识点

[[Stack]] [[Hashtable]] [[Arraylist]]

---
# 知识点

## 本质

- 是一个 C# 为我们封装好的类
- 本质也是 object 数组，只是封装了特殊的存储规则
- 是队列存储容器
- 队列是一种先进先出的数据结构
- 先存入的数据先获取，后存入的数据后获取
- **先进先出
	- ![[队列的先进先出.png]]
## 声明

- 需要引入命名空间
- `Queue queue = new Queue();`
## 增取查改

### 增

- `queue.Enqueue(Text)`
	- 增加一个
### 取

- 队列中不存在删的概念，只有取的概念
	- 取出先加入的对象
- `object obj = queue.Dequeue();`
### 查

- `obj = queue.Peek();`
	- 查看队列头部元素，但不移除
- `queue.Contains(Text)`
	- 查看元素是否存在于队列中
### 改

- 队列无法改变其中元素，只能进出
	- 实在要改，只能清空
- `queue.Clear();`
## 遍历

- 长度
	- `queue.Count;`
- 用 foreach 遍历
	- `foreach(object item in queue){}`
- 将队列转为 object 数组
	- `objcet[] obj = queue.ToArray();`
	- 之后使用 循环语句
## 循环出列

- `while(queue.Count > 0){object o = queue.Dequeue(); Console.WriteLine(o); }`

## 存在装箱拆箱

---
# 源代码

![[Queue知识点.cs]]

---
# 练习题

![[Queue练习题.cs]]

---


