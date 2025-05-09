---
tags:
  - "#Cpp"
  - "#STL"
created: 2025-04-27
---

---
# 关联知识点

[[02 单向链表]] [[07 set]]

---
# 基本概念

==双向循环链表==

头结点的 `prev` 指向 尾结点的 `prev`
尾结点的 `next` 指向 头结点的 `next`

> `prev next` 在 物理结构 中有解释

存在一个虚拟头结点
## 逻辑结构

```C++
push_front()//头插入
pop_front()//头弹出
push_back()//尾插入
pop_back()//尾弹出

front()//首元素
back()//尾元素
begin()//首迭代器(指向首元素)
end()//尾迭代器(指向尾元素的下一个位置)
```
# 物理结构

与单向链表相似，只是多了一个 指向上一个结点 的指针
Node 有个值，分别为 `val next prev` 存储的值，指向下一个结点的指针，指向上一个结点的指针
# 对象创建

```C++
//默认构造函数  
list<int> list1;  
printList(list1);  
  
//初始化列表  
list<int> list2 = {1, 2, 3, 4, 5};  
printList(list2);  
list<int> lit2_2({1, 2, 3, 4, 5});  
printList(lit2_2);  
  
//迭代器  
list<int> list3(list2.begin(), list2.end());  
printList(list3);  
  
//全0初始化  
list<int> list4(8);  
printList(list4);  
  
//a 个 blist<int> list5(5, 8);  
printList(list5);  
  
//拷贝构造函数  
list<int> list6(list5);  
printList(list6);
```
# 赋值操作

```C++
//初始化列表  
list<int> l = {1, 2, 3, 4, 5};  
printList(l);  
  
// = 赋值  
list<int> list1;  
list1 = l;  
printList(list1);  
  
//assign 传入一个迭代器空间  
list<int> list2;  
list2.assign(list1.begin(), list2.end());  
printList(list2);  
  
//assign 初始化列表  
list<int> list3;  
list3.assign({1, 2, 3});  
printList(list3);  
  
//assign 初始化 a 个 blist<int> list4;  
list4.assign(8, 6);
```
# 大小操作

可以使用 `resize` 进行缩容

```C++
empty()//判空
size()//获取大小
resize()//设置大小

resize(10);//将大小改为10
resize(5,10);//如果出现新元素则设置为10
```
# 数据插入

在 list 中，迭代器只能使用 自增/自减，无法使用加号

```C++
push_front()//头插
push_back()//尾插
insert()//中间插

//初始化列表  
list<int> l = {1, 2, 3, 4, 5};  
printList(l);  
  
//迭代器 值  在迭代器这个位置前插入一个值  
l.insert(++l.begin(), 10);  
  
//迭代器 数量 值 在迭代器这个我位置前插入一定数量的值  
l.insert(++l.begin(), 10, 1);  
  
//迭代器 迭代器开始位置 迭代器结束位置 左开右内 将开始位置到结束位置插入到 迭代器前  
l.insert(++l.begin(), l.begin(), l.end());
```
# 数据删除

```C++
pop_front()//头删
pop_back()//尾删
erase()//中间删
clear()//全部删 size = 0

//传入迭代器位置  
//返回 被删除的下一个迭代器  
auto item = l.erase(l.begin());  
  
//传入一个 迭代器区间 左闭右开  
auto item1 = l.erase(item, l.end());
```
# 数据访问

List 不存在随机访问，无法使用下标与 `.at()`

> 与迭代器无法使用加号一样，都是因为效率低下

```C++
int getListIntItem(list<int> &l, int index) {  
    auto it = l.begin();  
    //传入一个下标，然后使用自增往后移动
    while (index) {  
        it++;  
        index--;  
    }  
  
    return *it;  
}
```
# 链表反转

简单来说，就是将一个结点的 `next prev` 进行交换便可

```C++
//C++ 的 STL 提供了一个方法

l.reverse();//将链表反转
```
# 链表排序

与二分查找类似
把链表分为两个单独的，对这两个单独的链表进行排序，使其都成为一个有序的链表
然后，将这两个链表，从第一个结点开始比较，小的拿出来，放回原链表中
直到全部结点都放回到原链表中

```C++
//C++中的STL提供了方法
l.sort();

int cmp(int a,int b){
	return a > b；
}
//逆序排列
l.sort(cmp);

```






---
