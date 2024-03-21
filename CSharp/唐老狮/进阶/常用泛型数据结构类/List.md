
一般会选择频繁使用 List ，而不是 Arraylist

课时14

- List 本质
	- List 是一个 C# 封装好的类
	- 本质是一个可变类型的泛型数组
	- List 类帮助我们实现了很多方法
	- 比如泛型数组的增删查改
- 声明
	- 需要引入命名空间（建议小灯泡自动补全）
	- `List<T> list = new List<T>();`
		- T 为泛型占位符
- 增删查改（与 Arraylist 一模一样）
	- 增
		- `list.Add()`
			- 根据之前声明时的类型，括号内填入相应类型
		- `list.AddRange(list2)`
			- 批量增加
			- `list2` 为同类型的另外一个变量
		- `list.Insert(0,99)`
			- 参数一：开始位置
			- 参数二：插入元素
	- 删
		- `list.Remove(1)`
			- 移除指定元素
		- `list.RemoveAt(0)`
			- 移除指定位置的元素
			- 0 为索引
		- `list.Clear();`
			- 清空
	- 查
		- `list[0]`
			- 得到指定位置的元素
			- 0为索引
		- `list.Contains(1)`
			- 查看元素是否存在
			- 1 为一个元素
		- `list.IndexOf(5)`
			- 正向查找一个元素
			- 找到返回位置索引，找不到返回 -1
		- `list.LastIndexOf(5)`
			- 方向查找一个元素
			- 找到返回位置索引，找不到返回 -1
	- 改
		- `list[0] = `
		-  直接赋值
- 遍历
	- 长度
		- `list.Count`
	- 容量
		- `list.Capacity`
	- `foreach(类型 item in list){}`
		- 类型 为声明时填入的类型
	- `for(int i = 0 ; i < list.Count; i++){ }`
- list 与 Arraylist 区别
	- List内部封装的是一个泛型数组
	- ArrayList内部封装的是一个object数组

- 知识点
	- ![[List知识点.cs]]

- 练习题
	- ![[List练习题.cs]]