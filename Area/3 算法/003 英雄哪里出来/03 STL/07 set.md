---
tags:
  - "#Cpp"
  - "#STL"
created: 2025-04-27
---

---
# 关联知识点

[[08 map]]

---
# 基本概念

有序集合

与数学的集合类似，容器内元素不重复；插入一个元素，容器中的会进行有序排列

> Multiset 容器元素可以重复；插入一个元素，容器中的会进行有序排列
## 容器特点

线性容器：vector string list
树形容器：set multiset

物理结构上，使用红黑树的结构实现
但逻辑结构是一个序列
# 对象创建

> 创建后，容器内的元素会进行一次有序排列

```C++
//默认构造  
set<int> set1;  
  
//初始化列表  
set<int> set2 = {9, 8, 7, 6, 5, 4};  
set<int> set2_2({8, 6, 5, 4, 1});  
  
//迭代器  
set<int> set4(set2.begin(), set2.end());  
  
//拷贝构造函数  
set<int> set5(set2);
```
# 赋值操作

```C++
set<int> s = {9, 8, 7, 6, 5, 4};  
  
// =  
set<int> set1;  
set1 = s;  
  
//初始化列表  
set<int> set2;  
set2 = {9, 8, 6, 5, 3};
```
# 大小操作

```C++
empty();//判空
size();//获取大小
```
# 数据插入

每次插入元素都会进行一次排序，确保容器内是有序的
如果容器内已经存在将要插入的元素，那就不会进行插入

> 时间复杂度：$O(n)$

```C++
set<int> s1;
//直接插入一个值
s1.insert(10);
s1.insert(45);
s1.insert(2);

//传入迭代器 可以传入其他容器的迭代器
vector<int> v1 = {8,5,2,3,4,1};
s1.insert(v1.begin(),v1.end());
```
# 数据查找

^3e8ac8


```C++
set<int> s = {1, 2, 3, 4, 5, 6};  
//返回值为一个迭代器  
auto it = s.find(3);  
//如果查找失败 那么 it 为 s.end() 的迭代器  
//如果成功 那么it则是一个 不为 s.end() 的迭代器  
if (it != s.end()) {  
	cout << *it << endl;
}
```
# 数据删除

```C++
set<int> s = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};  
  
//删除一个指定值  
s.erase(3);  
  
//传入一个迭代器  
auto it = s.find(4);  
if (it != s.end()) {  
    s.erase(it);  
}  
  
//传入一个迭代器区间 左闭右开  
auto it1 = s.find(6);  
auto it2 = s.find(10);  
if (it != s.end()) {  
    s.erase(it1, it2);  
}  
  
//注意 删除之后 指向的迭代器将会失效  
//比如这里的 it it1 it2 都会失效 如果再次使用它们将会报错
```
# 数据统计

> Set 元素不能重复，导致 count 的值为 0 或 1
> 但 multiset 可以重复

```C++
//统计 x 在 集合中的出现次数
s.count(x);
```
# 排序规则

如果插入的元素是一个类对象或者结构体
那么就需要自己来手动写一个排序规则

重载运算符：`<`

```C++
class CGaGa {  
public:  
    CGaGa() {  
        _name = " ";  
        _priority = -1;  
    }  
  
    CGaGa(string name, int pri) : _name(name), _priority(pri) {}  
  
    bool operator<(const CGaGa &other) const {  
        //不会修改传入的 对象 中的 成员 所以加了个 const        //同时也不会修改自己的 _priority 的值 所以为常函数  
        return _priority < other._priority;  
    }  
      
    void print() const{  
        cout << "(" << _priority << ")" << _name << endl;  
    }  
  
private:  
    string _name;  
    int _priority;  
};  

set<CGaGa> s;  
s.insert( CGaGa("A",1));  
s.insert( CGaGa("G",7));  
s.insert( CGaGa("D",4));  
s.insert( CGaGa("C",3));  
s.insert( CGaGa("E",5));  
s.insert( CGaGa("B",2));  
s.insert( CGaGa("F",6));  
  
for (auto temp : s) {  
    temp.print();  
}
```



---
