---
tags:
  - 科学/编程语言/Csharp/唐老师/进阶/常用泛型数据结构类/Dictionary
created: 2024-03-22
课时: "16"
来源: https://www.taikr.com/course/1144/task/35943/show
备注: 字典，使用上和哈希表一模一样
---

---
# 关联知识点

[[Hashtable]]

---
# 知识点

## 本质

- 可以理解为用于泛型的哈希表（Hashtable）
- 它也是基于键的哈希代码组织起来的 键/值对
- 键值对类型从 Hashtable 的 object 变为了可以自己指定的泛型
## 声明

- 需要引用命名空间
- `Dicitionary<T,K> dt = new Dicitionary<T,K>();`
	- T 为 键 的类型
	- K 为 值 的类型
## 增删查改

### 增

- `dt.Add( 键,值)`
	- 键 不可以重复
### 删

- `dt.Remove(键)`
	- 删除不存在的键，没有反应
### 查

- `dt.[键]`
	- 通过 键 来查找
	- 找不到返回空
- `dt.ContainsKey(键)`
	- 查看是否存在
	- 通过 键 来查找
- `dt.ContainsValue(值)`
	- 查看是否存在
	- 通过 值 来查找
### 改

- `dt[键] = 新值`
	- 赋值
## 遍历

- 容量
	- `dt.Count`
- 遍历所有键
	- `foreach(键的类型 item in de.Keys){ }`
- 遍历所有值
	- `foreach(值得类型 item in de.Value){ }`
- 键值一起遍历
	- `foreach(KeyValuePair<键得类型,值的类型> item in dt){ }`
- 迭代器遍历

---
# 源代码

![[Dictionary 知识点.cs]]

---
# 练习题

![[Dictionary练习题.cs]] 

---


