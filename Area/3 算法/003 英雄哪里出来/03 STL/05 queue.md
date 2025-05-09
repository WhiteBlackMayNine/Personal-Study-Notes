---
tags:
  - "#Cpp"
  - "#STL"
created: 2025-04-27
---

---
# 关联知识点

[[04 队列]] [[06 list]]

---
# 基础概念

队列
先进先出，头出尾加
## 逻辑结构

没有迭代器
不接受遍历操作

```C++
pop()//出队
front()//首元素
back()//尾元素
push()//入队
```
# 对象创建

`queue<int>` 模板中的第二个参数默认为一个 `deque`，与 `stack` 相同，可以进行容器更换


```C++
//默认构造函数  
queue<int> queue1;  
  
//拷贝构造函数  
queue<int> queue2(queue1);
```
# 赋值操作

运算符重载
将 `q2` 的值拷贝一份，分配给 `q1`

```C++
queue<int> q1;
queue<int> q2;
q1 = q2;
```
# 入队操作

实质上是调用 `deque` 的 `push_back`

```C++
queue<int> q1;
q1.push(10);
```
# 获取队首

实质上是获取双关队列的队首

```C++
queue<int> q1;
q1.front();
```
# 获取队尾

```C++
queue<int> q2;
q2.back();
```
# 出队操作

```C++
queue<int> q1;
q1.pop();
```
# 大小操作

```C++
queue<int> q1;
q1.empty();//判断是否为空
q1.size();//队列里有多少个元素
```










---
