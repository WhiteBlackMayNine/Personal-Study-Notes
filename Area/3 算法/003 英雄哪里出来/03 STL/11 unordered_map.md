---
tags:
  - "#Cpp"
  - "#STL"
created: 2025-04-28
---

---
# 关联知识点

[[08 map]]

---
# 基础概念

无序映射 
功能与 map（有序映射） 相似
底层数据结构是哈希表，键值对，与 [[10 unordered_set]] 的底层实现同理

Set 和Map的区别是：set 的元素是一个数据，map 的元素（一个Pair）则是两个数据
# 对象创建

```C++
//默认构建
unordered_map<int,int> m1;
//初始化列表
unordered_map<int,int> m2 = {pair<int,int>(1,2),pair<int,int>(3,4),};
unordered_map<int,int> m2({pair<int,int>(1,2),pair<int,int>(3,4),});
//迭代器
unordered_map<int,int> m3(m2.begin(),m2.end());
//拷贝构造
unordered_map<int,int> m4(m2);
```
# 赋值操作

```C++
// = 对象
unordered_map<int,int> m;
m = m1;
// = 初始化列表
m = {pair<int,int>(1,2),pair<int,int>(3,4),};
```
# 大小操作

```C++\
empty();//判空
size();//获取大小
```
# 数据插入

```C++
m.insert(pair<int,int>(3,4));
m.insert(make_pair(5,20));
m.insert(unordered_map<int,int>::value_type(4,7));
m[8] = 6;//8 为键 6 为值

//[] 如果键已经存在时，将会修改值
//如果没有进行 = 赋值操作，那么就会使用默认值 如0
//insert 有返回值 pair<unordered_map<int,int>::iterator,bool> ret = ......
//第一个参数为 迭代器 第二个参数为 是否成功插入 如果键已经存在，则不插入
```
# 数据查找

```C++
//找到返回迭代器 不存在返回 m.end();
auto it = m.find(i)

//数据查找不用使用 []
cout << m[5];
//无论是否存在 键为5 的值 都将为0 (如果存在则修改为0 不存在则插入一个0)
```
# 数据删除

```C++
m.erase(3);//删除一个值
m.erase(it);//删除迭代器
m.erase(it1,it2);//传入一个迭代器区间 但无序映射 不建议用 不知道删掉到底是什么
m.clear();//清空
```
# 数据修改

```C++
m[3] = 888;
m[3]++;
m[3] -= 10;
```
# 数据统计

```C++
m.count(i);//某个元素
```

> 注意 unordered_multimap 中的元素可以重复

---
