---
tags:
  - "#Cpp"
  - "#STL"
created: 2025-04-26
---

---
# 关联知识点

[[04 队列]] [[04 stack]]

---
# 基本概念

==双端队列==
可以在头部和尾部进行高效的插入

核心是一个指针数组，每个元素都指向一个连续的内存
指针数组内的元素间隔为 8 (64 位) 或 4（32 位）
元素指向的连续内存中的间隔根据  `deque` 传入的参数决定

每次从头部或者尾部插入，不会产生数据移动，效率较高

> Deque 是逻辑连续的

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
# 对象创建

```C++
void printDeque(deque<int> &d) {  
    for (auto temp: d) {  
        cout << temp << endl;  
    }  
}

//默认构造函数  
deque<int> deque1;  
  
//初始化列表  
deque<int> deque2({1, 23, 45, 6, 7, 8, 9});  
  
//初始化列表 等号赋值  
deque<int> deque2_2 = {1, 23, 45, 6, 7, 8, 9};  
  
//迭代器  
deque<int> deque3(deque2.begin(), deque2.end());  
  
//全0初始化  
deque<int> deque4(8, 0);  
  
//a 个 bdeque<int> deque5(10, 10);  
  
//拷贝构造函数  
deque<int> deque6(deque4);
```
# 赋值操作

```C++
//运算符重载  
deque<int> deque1({9, 8, 5, 2, 1, 1});  
  
deque<int> deque2 = deque1;//该处使用的是拷贝构造函数 不是运算符重载  
  
deque<int> deque3;  
deque3 = deque1;//这个走的是运算符重载  
  
//assign 迭代器  
deque<int> deque4;  
deque4.assign(deque1.begin(), deque1.end());  
  
//初始化列表  
deque<int> deque5;  
deque5.assign({1, 2, 300});  
  
//初始化 a 个 bdeque<int> deque6;  
deque6.assign(8, 6);
```
# 大小操作

队列的大小

> 不同于 stack，deque 可以使用 resize 进行缩容

```C++
//empty 判空 size 大小 resize 设置大小  
deque<int> deque1;  
cout << deque1.empty() << deque1.size() << endl;  
//empty 如果为空 返回 1  
//将队列的size 设置为 18 新元素将设置为 默认值  
deque1.resize(18);  
deque1.resize(20,20);//将size设置为20 新元素初始化为20
```
# 数据插入

插入分为三种：头插、尾插、中间插

> `push_front push_back insert`

```C++
deque<int> deque1;  
  
deque1.push_front(1);  
deque1.push_front(2);  
deque1.push_back(3);  
deque1.push_back(4);  
printDeque(deque1);  
  
//insert  
//在 某个位置前 插入一个元素  
//位置 需要传入一个迭代器  
deque1.insert(deque1.begin() + 2, 10);  
//在某个位置前 插入 一定数量 的 某个元素  
deque1.insert(deque1.end() - 2, 5, 8);//5 个 8//在某个位置前 插入 从deque1.begin() + 3这个元素 到 deque1.end() - 1 的元素 左闭右开区间  
deque1.insert(deque1.begin() + 1, deque1.begin() + 3, deque1.end() - 1);
```
# 数据删除

删除分三种：头删、尾删、中间删

```C++
pop_front()//头删
pop_back()//尾删
clear()//全删

deque<int> deque1({9, 5, 6, 5, 4, 6, 6});  
  
//传入一个迭代器 删除某个元素  
//返回值为 被删除的元素 的下一个元素 的迭代器  
auto item = deque1.erase(deque1.begin() + 2);  
deque1.erase(item);  
  
//删除多个元素 从起始位置 删到 终点位置 左闭右开  
deque1.erase(deque1.begin() + 2, deque1.begin() + 5);
```
# 扩容机制

> 很难懂 建议多看看视频

物理结构其实是一个指针数组，里面的每一个元素都指向数组的首地址
往后插入的时候，如果没有位置，那么就会去 指针数组内 的下一个元素 指向的元素 继续插入
比如 从索引 0 指向的数组开始插入，插入 4 个元素按下没有位置，那么就会去索引为 1 指向的数组进行插入

往前插入的时候，如果没有位置，那么就从这个空间的最后一个位置开始插入，往前插，如果没有位置就往前找一个新的

如果插着插着快插满了，那么就会进行扩容
把 往后插入 的元素 全部移到到 往前插入 的元素 的后面
这样 前面有空的空间，后面也有空的空间，往前插和往后插都可以实现


# 随机访问

类似于数组下标

```C++
deque<int> deque1({9, 5, 6, 5, 4, 6, 6});
//都是获取 索引为2的元素 但 at 多了一个异常处理
deque1[2];//如果元素不存在，则返回0
deque1.at(2);

```



---
