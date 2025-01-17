---
tags:
  - 科学/编程语言/Csharp/唐老狮/核心/面向对象相关知识点/String
created: 2024-03-22
课时: "45"
来源: https://www.taikr.com/course/1139/task/35640/show
---

---
# 关联知识点



---
# 知识点

## 字符串指定位置获取

- 字符串本质是`char`数组
	- `string str = "asdas"; Console.WriteLine(str[0]);`
- 转为`char`数组
	- `char[] chars = str.ToCharArray();`
## 字符串指定位置获取

- `str = string.Format({0},……);`
	- 利用占位符拼接
## 正向查找字符位置

- `str.IndexOf("Text")`
	- 返回值为字符（或一个词） Text 所在的索引
	- 没找到则会返回 -1
## 反向查找指定字符位置

- `str.LastIndexOf("Text")`
	- 从后面开始查找字符（或一个词） Text 所在的索引
		- 但索引依旧是从头开始
	- 没找到则会返回 -1
## 移除指定位置后的字符

- `str.Remove(4);
	- 从索引4开始，把后面的移除
- `str.Remove(1，5);
	- 参数一：开始位置
	- 参数二：字符个数
## 替换指定字符串

- `str.Replace("Text1","Text2");`
	- 被替换文本 ， 替换文本
## 大小写转换

- `str.ToUpper` 小写转大写
- `str.ToLower` 大写转小写
## 字符串截取

- `str.Substring(2)`
	- 截取从指定位置开始后的字符串
- `str.Substring(2,3)`
	- 参数一：开始位置
	- 参数二：截取长度
		- 但不能超过字符串长度
## 字符串切割

- `string str = "1,2,3,4,5,6"; string[] strs = str.Split(',');`
	- 将字符串以`，`来切割，切割成一段一段的存到字符串数组中
## 注意

- 如果方法修改了原字符串，一般情况下不会修改原字符串，而是返回一个新的字符串
- 需要重新赋值或定义一个新的字符串来接取

---
# 源代码

![[string知识点.cs]]

---
# 练习题

![[string练习题.cs]]

---



