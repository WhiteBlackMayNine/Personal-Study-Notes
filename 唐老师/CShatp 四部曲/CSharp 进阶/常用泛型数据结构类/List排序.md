---
tags:
  - 科学/编程语言/Csharp/唐老师/进阶/常用泛型数据结构类/List排序
created: 2024-03-22
课时: "33"
来源: https://www.taikr.com/course/1144/task/36050/show
---

---
# 关联知识点

[[List]]

---
# 知识点

## List 自带排序方法

### 注意

- 需要引用命名空间
### 排序方法

- `list.Sort();`
	- 默认升序
## 自定义类的排序

- 利用泛型**必须继承接口`IComparable<T>`，并实现方法`CompareTo
	- 另外一个接口为不带泛型的`IComparable<T>`，但类型为`object`，使用时需要`as`转换
- `class Item : IComparable<Item> { public int CompareTo(Item other){ if(this.money > other.money){  return 1;}else{ return -1; }  } }`
	- 返回值的含义
		- 小于0
			- 放在传入对象的前面
		- 等于0
			- 保持当前的位置不变
		- 大于0
			- 放在传入对象的后面
		- 可以理解为 传入位置 为 数轴的零点
- `List<Item> itemList = new List<Item>();`
	- 声明对象
	- 使用`Add`方法添加此处省略
- 随后便可使用方法`.Sort();`来进行排序
	- 需要注意，判断语句中若是升序，则大于返回 1 ，若是降序，则大于返回 -1
## 通过委托函数进行排序

- `class ShopItem { }`
	- 定义一个类型
- `List<ShopItem> si = new List<ShopItem>();`
	- 声明对象
- 通过自定义函数来排序
	- `static int SortShopItem(ShopItem a,ShopItem b){ if(a.id > b.id){return 1;}else{return -1;}}`
		- 返回值、参数（泛型）要和委托一致
		- 传入的两个对象 为列表中 的两对象
			- 进行两两比较，用左边和右边的条件比较
			- 返回值与之前一样，0做标准，负数在左（前），整数在右（后）
- `si.Sort(SortShopItem)
	- 随后便可使用方法
		- 记得把自定义的函数名传入
	- 当 `.Sort()`被调用时，会把对象内`si`的所有值都进行排序
- 还可以使用匿名函数、Lambad表达式 进行书写排序
	- 匿名函数
		- `si.Sort(delegate(ShopItem a,ShopItem b){if(a.id > b.id){return 1;}else{return -1;}});`
			- 更加快捷
	- Lambad 表达式
		- `si.Sort((a,b)=>{if(a.id > b.id){return 1;}else{return -1;}});
			- 甚至连参数类型都不用写
- 判断语句中的逻辑处理可以使用三目运算符
	- ` return a.id > b.id ? 1 : -1;`
		- Lambad 表达式中，其他需要注意参数类型



---
# 总结

- 系统自带的变量（`int,float,double.....`）一般都可以直接 Sort
- 自定义类 Sort 有两种方式
	- 继承接口 `IComparable 
		- 建议继承泛型接口 `IComparable<T>
	- 在 Sort 中传入委托函数

---
# 源代码

![[List排序知识点.cs]]

---
# 练习题

![[List排序 练习题.cs]]

---

