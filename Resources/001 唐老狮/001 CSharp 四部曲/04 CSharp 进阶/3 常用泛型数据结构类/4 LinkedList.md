---
tags:
  - 科学/编程语言/Csharp/唐老狮/进阶/常用泛型数据结构类/LinkedList
created: 2024-03-22
课时: "20"
来源: https://www.taikr.com/course/1144/task/35947/show
备注: 建议学习一下链表的原理
---


---
# 关联知识点

[[3 顺序存储和链式存储]]

---
# 知识点

## LinkedList

- 是一个 C# 封装好的类
- 本质是一个可变类型的泛型双向链表
## 声明

- 需要引用命名空间
	- `LinkedList<T> ll = new LinkedList<T>();`
	- T 可以是任何类型
	- 链表对象，需要掌握两个类
		- 一个是链表本身，一个是链表节点类 LinkedListNode
## 增删查改

### 增

- `ll.AddLast(Text)`
	- 在链表尾部添加元素
- `ll.AddFrist(Text)`
	- 在链表头部添加元素
- `LinkedListNode<T> n = ll.Find(值); ll.AddAfter(n,15);`
	- 在某一个节点之后添加一个节点
		- 要指定节点 先得得到一个节点
	- 第一个参数为 一个节点
	- 第二个参数为 插入元素
- `LinkedListNode<T> n = ll.Find(值); ll.AddBefore(n,Text);`
- 在某一个节点之前添加一个节点
	- 要指定节点 先得得到一个节点
### 删

- `ll.RemoveFirst();`
	- 移除头节点
- `ll.RemoveLast();`
	- 移除尾节点
- `ll.Remove(Text)`
	- 移除指定节点
		- 无法通过位置直接移除
		- 传入链表中存在的值
- `ll.Clear()`
	- 清空
### 查

- `LinkedListNode<T> first = ll.First;`
	- 头节点
	- T 为之前声明时的类型
- `LinkedListNode<T> last = ll.Last;`
	- 尾节点
- `LinkedListNode<T> node = ll.Find();`
	- 找到指定值的节点
		- 无法直接通过下标获取中间元素
		- 只有遍历查找指定位置元素
		- 没找到返回空
	- 打印
		- `node.Value`
- `ll.Contains(元素)`
	- 判断是否存在
### 改

- `ll.First.Vaule = 10;`
	- 要先得到一个节点后，再改变其中的值
## 遍历

### `foreach`

- `foreach(传入类型 item in ll){ }`
	- 通过迭代器处理，直接得到节点的值
### 通过节点遍历

- 从头到尾
	- `LinkedListNode<T> nowHead = ll.First; while(nowHead != null){ nowHead = nowHead.Next; }`
		- 获取头节点，在循环中从后往前拉
- 从尾到头
	- `LinkedListNode<T> nowNode = ll.Last; while(nowNode != null){ nowNode = nowNode.Previos; }`
		- 获取尾节点，在循环中从前往后推

---
# 源代码

![[LinkedList知识点.cs]]

---
# 练习题

![[LinkedList 练习题.cs]]

---


