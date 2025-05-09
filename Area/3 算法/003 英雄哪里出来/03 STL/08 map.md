---
tags:
  - "#Cpp"
  - "#STL"
created: 2025-04-27
---

---
# 关联知识点

[[09 priority_queue]]

---
# Pair

模板类
```C++
//可以是任意类型 甚至可以进行嵌套
template <class_Ty1, class_Ty2>
struct pair{
	_Ty1f first;//存储的第一个值
	_Ty2s second;//存储的第二个值
}

pair<int,int> p(13,14); 
pair<int,int> p1 = make_pair(13,14);
```
# 基础概念
## Map

容器中所有元素都是 pair，first *不重复*
pair 中第一个数据是 key（键)，第二个数据 value (值）
所有元素都会根据元素的 key 值排序
## Multimap

容器中所有元素都是 pair，first *不可以重复*
Pair 中第一个数据是 key（键)，第二个数据 value (值）
所有元素都会根据元素的 key 值排序
## 容器特定

树形结构
## 物理结构

红黑树，根据 first 的值进行排序
## 逻辑结构

线性，元素按照 first 的大小进行排序

```C++
begin()//首迭代器
end()//尾迭代器
```
# 对象创建

```C++
void printMap(const map<int, int> &m) {  
    //这种 for 循环获得的it 是一个 pair 对象  
    //也就是遍历的列表中存储的元素 不是一个迭代器  
    for (auto it: m) {  
        cout << "Key = " << it.first << "Value = " << it.second << " ";  
    }  
    cout << endl;  
}



//默认构造函数  
map<int, int> map1;  
  
//初始化列表  
map<int, int> map2 = {  
        pair<int, int>(10, 20),  
        pair<int, int>(11, 21),  
        pair<int, int>(12, 22),  
        pair<int, int>(13, 23),  
};  
  
map<int, int> map2_2({  
                             pair<int, int>(10, 20),  
                             pair<int, int>(11, 21),  
                             pair<int, int>(12, 22),  
                             pair<int, int>(13, 23),  
                     });  
  
//迭代器  
map<int, int> map3(map2.begin(), map2.end());  
  
//拷贝构造函数  
map<int, int> map4(map2);
```
# 赋值操作

```C++
// = 对象
map<int,int> m1;
m1 = m;

//= 初始化列表
map<int,int> m2;
m2 = {  
			 pair<int, int>(10, 20),  
			 pair<int, int>(11, 21),  
			 pair<int, int>(12, 22),  
			 pair<int, int>(13, 23),  
	 };  
```
# 大小操作

```C++
enpty()// 判空
size()//获取当前元素数量
```
# 数据插入

`insert` 如果插入的元素的 Key 已经存在，那么插入将会失败
`[]` 下标访问则会修改值

```C++
//记住这一个就行  
map1.insert(pair<int, int>(2, 10));  
  
map1.insert(make_pair(3, 10));  
map1.insert(map<int, int>::value_type(4, 79));  
  
//Key 为 4  值 为 6map1[4] = 6;
map1[4] = 6;
map1[0];//如果不赋值 将会使用默认值 int - 0
```
# 数据查找

跟 [[07 set#^3e8ac8|set 数据查找]] 很像

```C++
//查找是否存在 x(键)
//如果存在则返回其迭代器
//如果不存在则返回 end() 的迭代器
auto it = map.find(x);
```
# 数据删除

注意 `end()` 指向的是 最后一个元素的后一个位置

```C++
m.erase(1);//删掉键为1的元素 包括值也会被删掉
m.erase(m.begin());//删掉 传入的迭代器 的元素
m.erase(m.begin(),m.end());//删掉一个区间 左闭右开 传入两个迭代器 

m.clear();//清空
```
# 数据修改

```C++
m[5] = 880;//赋值
m[5]++;//让 值 自增
m[5] -= 124;//让 值 减去 124
m[5] += 20;//让 值 加上 20
```
# 数据统计

> Map 内键不可重复，所以 count 的值仅为 0 或 1
> 但 Multimap 可以重复

```C++
m.count(i);//某个键 i 出现的次数
```

---
