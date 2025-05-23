---
tags:
  - 科学/编程语言/Csharp/唐老狮/核心/面向对象相关知识点/StringBuilder
created: 2024-03-22
课时: "47"
来源: https://www.taikr.com/course/1139/task/35642/show
---

---
# 关联知识点



---
# 知识点

## 基本概念

### 概念

- C# 提供的一个用于处理字符串的公共类
### 主要解决问题

- 修改字符串但不创建新的对象
- 需要频繁修改和拼接的字符串可以使用，提升性能
- 使用前，需要引入命名空间
	- `using System.Text`
## 初始化

- （直接指明内容）
- `StringBuilder str = new StringBuilder("Text"，100);`
	- Text 为字符串文本，100为容量
## 容量

### 问题

- 本质依旧是一个 字符数组，存在容量问题
- 每次往里面增加时，会自动扩容（如果超过，会乘以2）
	- 虽然会产生一定垃圾，但比`string`要少很多
- **如果输入的字符超过参数二设定的容量，便会自动乘以2，然后存放字符
### 获得容量

- `str.Capacity`
### 获得字符长度（实际字符长度）

- `str.Length`
## 增删查改替换

### 增

- `str.Append("Text");`
	- Text 为字符串文本
- `str.AppendFormat("{0},……",……);`
	- 通过占位符进行拼接
### 插入

- `str.Insert(0,"Text")`
	- 参数一：从哪里插入
	- 参数二：文本
### 删

- `str.Remove(0,10);`
	- 参数一：开始位置
	- 参数二：删除多少个
### 清空

- `str.Clear();`
### 查

- `str[索引]`
	- 本质是个 字符数组
### 改

- `str[索引] = 'a';`
### 替换

- `str.Replace("Text1","Text2");`
	- Text1 被替换文本
	- Text2 替换文本
### 重新赋值

- `str.Clear(); str.Append("Text")`
- 先清除、再添加
	- 防止垃圾产生
### 判断是否相同

- 利用万物之父的方法`Equals
- `if(str.Equals("123456")){ }`

---
# 源代码

![[StringBuilder知识点.cs]]

---
# 练习题

- 描述`StringBuililder`与`String`的区别
- 如何优化内存
- ![[StringBuilder练习题.cs]]


---


