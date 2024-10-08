---
tags:
  - 科学/编程语言/Csharp/唐老狮/进阶/简单数据结构类/Hashtable
created: 2024-03-22
课时: "8"
来源: https://www.taikr.com/course/1144/task/35935/show
---

---
# 关联知识点

[[1 Arraylist]] [[3 Queue]] [[2 Stack]]

---
# 知识点

## 本质

- 又叫散列表
- 是基于键的哈希代码组织起来的 键/值对
- 存储类型为 object
- 它的主要作用是提高数据查询效率
- **使用键来访问集合中的元素
	- 通过**键**来映射**值
## 声明

- 需要引入声明空间
- `Hashtable ht = new Hashtable()'`
## 增删查改

### 增

- `ht.Add(Text1,Text2);`
- Text1 为键 ，Text2 为 值
	- Text 可以为任何类型
- 注意
	- 不能出现相同键
	- 值可以重复
### 删

- `ht.Remove(键)`
	- 只能通过**键**来删除
- 删除不存在的**键** ，没有反应
- `ht.Clear();`
	- 直接清空
### 查

- `ht[键]`
	- 通过**键**来查找
		- 找不到会返回空
- 查看是否存在
	- 根据**键**检测
		- `ht.Contains(键)`
		- `ht.ContainsKey(键)`
		- 两者作用相同
	- 根据**值**检测
		- `ht.ContainsValue(值)`
### 改

- `ht[键] = 新值`
- 只能修改**键**对应的值内容，不能修改键
## 遍历

- 得到键值对的对数
	- `ht.Count`
- 遍历所有键
	- `foreach(object item in ht.Keys){ Console.WriteLine(ht[item]); }`
- 遍历所有值
	- `foreach(object item in ht.Values){ Console.WriteLine(item); }`
- 键值一起遍历（少）
	- `foreach(DictionaryEntry item in ht){ Console.WriteLine("键" + item.Key + "值" + item.Value); }`
- 迭代器遍历（少）
	- `IDictionaryEnumerator myEnumerator = hashtable.GetEnumerator();
	- `bool flag = myEnumerator.MoveNext();
	- `while(flag){ Console.WriteLine("键" +myEnumerator.Key +"值"+myEnumerator.Value);flag = myEnumerator.MoveNext(); }

## 存在装箱拆箱

---
# 源代码

![[Hashtable知识点.cs]]

---
# 练习题

![[Hashtable练习题 .cs]]

---
