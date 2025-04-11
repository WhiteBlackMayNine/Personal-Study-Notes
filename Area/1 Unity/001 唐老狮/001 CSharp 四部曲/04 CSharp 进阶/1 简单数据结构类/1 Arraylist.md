---
tags:
  - 科学/编程语言/Csharp/唐老狮/进阶/简单数据结构类/Arraylist
created: 2024-03-22
课时: "2"
来源: https://www.taikr.com/course/1144/task/35929/show
---

---
# 关联知识点

[[4 Hashtable]] [[3 Queue]] [[2 Stack]]
 
---
# 知识点

## 本质

- 是一个 C# 封装好的类
- 本质是一个 object 类型的数组
	- 从头到尾，一个类型为一个数组元素
- Arraylist 类帮助我们实现了很多方法
- 比如数组的增删查改
## 声明

- 需要提前引用命名空间（建议点击小灯泡自动补全）
- `Arraylist array = new Arraylist();`
## 增删查改

- （Text 可以是任何类型）
### 增

- `array.Add(Text)`
	- 增加一个
- `array.AddRange(array2)`
	- 范围增加（批量增加，把另一个 list 容器里面的内容加到后面）
- `array.Insert(1,Text)`
	- 参数一：开始位置
### 删

- `array.Remove(Text)`
	- 从头找，移除指定元素
- `array.RemoveAt(2)`
	- 移除指定位置的元素（2为索引）
- `array.Clear();`
	- 清空
### 查

- `array[索引]`
	- 得到指定位置元素
- `array.Contains(Text)`
	- 返回值为布尔值
- `array.IndexOf(Text)`
	- 正向查找元素位置
	- 找的的返回值为 位置；找不到的返回值为 -1
- `array.LastIndexOf(Text)`
	- 反向查找元素位置
	- 但索引依旧为从头开始
### 改

- `array[索引] = Text;`
### 遍历

- 长度
	- `array.Count`
- 容量
	- `array.Capacity`
- 循环语句
	- `for(int i = 0 ; i < array.Count ; i++){}`
- 迭代器循环
	- `foreach(object item in array){ }`
	- 每次循环，都会把`array`的元素赋给`item
## 装箱拆箱

- 本质是一个 object 类型的数组，自然存在装箱拆箱
- 当往其中存储值类型时，为装箱；当将存储的值类型取出时，为拆箱
- 所以 Arraylist 建议少用，之后会有更好的数据容器

---
# 补充

- `Arraylist.Sort();`
	- 自带的升序排序方法
- 建议与[[List排序]]一起学习

---
# 源代码

![[ArrayList知识点.cs]]

---
# 练习题

![[ArrayList练习题.cs]]

---



