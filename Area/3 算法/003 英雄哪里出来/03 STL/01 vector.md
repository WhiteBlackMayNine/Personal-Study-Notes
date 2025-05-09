---
tags:
  - "#Cpp"
  - "#STL"
created: 2025-04-24
---

---
# 关联知识点

[[01 顺序表]] [[02 string]]

---
# 基础概念

是一个柔性数组，可以动态进行扩容
可以理解为 封装后的 顺序表

```C++
front()//头元素 
back()//尾元素
begin()//迭代器 可以理解为 头指针 指向 第一个元素
end()//迭代器 可以理解为 尾指针 指向 最后一个元素的下一个元素

//注意 end() 指向的位置是无效位置 它的前一个位置才是有效的
```

一般在表示区间时，都是左闭右开的

```C++
//需要引入头文件
# include <vecotr>

vector<int> v;
cout << v.capacity()

v.push_back(7)//在尾部插入一个元素

cout << "begin:" << *v.begin() << endl;
cout << "end:" << *v.end() << endl;
``` 
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

在创建的时候，$size = capacity$
 
```C++
void PrintVector(vector<int> &v) {  
    //为什么要解引用  
    //少一次拷贝过程  
    for (vector<int>::iterator iter = v.begin(); iter != v.end(); iter++) {  
        //固定写法 从 vector 的 begin 开始 一直遍历到 end        //也可以直接使用 auto (类似于 C# 的 var)        cout << *iter << endl;  
  
        //注意 iter 是一个指针 因为 v.begin() 就是一个指针  
    }  
    
    for (int & iter : v) {  
    //或者直接迭代器 从v中获取元素  
    cout << iter << endl;  
	}
}

//默认构造函数 不会任何数据  
vector<int> v1;  
  
//初始化数组 隐式构造  
vector<int> v2 = {9, 8, 7, 6, 5};  
PrintVector(v2);  
  
//有参构造 显示构造 与上面是同一个构造函数  
vector<int> v2_2({9, 8, 7, 6, 5});  
  
//迭代器  
//将 v2 从begin到end的前一个 拷贝到 v3 中  
//注意 左闭右开  
vector<int> v3(v2.begin(), v2.end());  
  
//全0初始化  
//申请8个内存  
vector<int> v4(8);  
  
//申请a个空间的元素，每个元素的值初始化为 bvector<int> v5(8, 6);  
  
//6.拷贝构造函数  
vector<int> v6(v2_2);  
PrintVector(v6);
```
# 赋值操作

```C++
// = 赋值  
vector<int> v1 = {9, 8, 5, 2, 1, 1};  
cout << "v1:";  
PrintVector(v1);  
  
//assign(迭代器） 拷贝  
vector<int> v2;  
v2.assign(v1.begin(), v1.end());  
cout << "v2:";  
PrintVector(v2);  
  
//初始化列表  
vector<int> v3;  
v2.assign({9, 8, 5, 2, 1, 1});  
cout << "v3:";  
PrintVector(v3);  
  
//初始化h个b  
vector<int> v4;  
v4.assign(8, 6);  
cout << "v4:";  
PrintVector(v4);
```
# 数据插入

```C++
vector<int> v;  
for (int i = 0; i < 10; ++i) {  
    //往后插入 效率最高  
    v.push_back(i);  
}  
PrintVector(v);  
  
//插入到某个元素的前面  
v.insert(v.begin(), 888);  
PrintVector(v);
```
# 数据删除

```C++
//将末尾元素删掉  
v.pop_back();  
  
//删除某一个迭代器指向的值  
//返回的是 删除的迭代器 指向的 下一个 迭代器的值  
//如果 删除 首元素 那么指向的就是 第二个元素  
auto iter = v.erase(v.begin());  
  
//清空内存  
v.clear();
```
# 扩容机制

容量 `capacity ()` 这个容器能装多少
大小 `size ()` 这个容易已经装了多少

> 容量 永远大于等于 大小

在 C++中，如果 size = capacity 时，再执行插入操作，那么就会让 capacity 的大小变为原来的 1.5 倍
注意：根据编译器、版本的不同，扩容系数也是不同的

如果扩容后是一个小数，则会向下取整
$y = x + x / 2$

跟顺序表的扩容一致，申请新的内存，拷贝过来，然后释放掉旧内存

```C++
//改变vector的大小
vec.resize(5, 20); // 将向量大小调整为 5，新元素填充为 20
//如果新大小大于当前容量，内部会分配更大的内存空间；但如果过大，那么就直接将新大小赋值过去
//如果新大小小于当前容量，多余的元素会被销毁，内存空间也会相应释放
//当新大小大于当前向量的大小时，可以指定一个值用于填充新添加的元素 (可选)
```

> 如果 resize 设置的大小，大于扩容一次后的大小，那么将会直接使用 设置的大小
> 但 resize 不会自动缩容，capacity 不会减小
# 随机访问

可以理解为数组下标访问
消耗的时间比较少
跟数组下标访问方式一样的

所以，使用 `for` 循环时，可以跟循环数组一样进行循环 vector

```C++
cout << v[2] << endl;//如果超出 使用默认值 如 0
cout << v.at(2) << endl;
```

两种方式作用相同，只是第二个多了个异常处理，速度较慢

> `v.front() / back()` 不叫随机访问，分别访问第一个元素和最后一个元素
# 内存交换

```C++
//内存交换  
vector<int> v1 = {1, 2, 3, 4, 5};  
vector<int> v2 = {9, 8, 7, 6, 5};  
cout << "v1: ";  
PrintVector(v1);  
cout << "v2: ";  
PrintVector(v2);  
//交换 v1 v2 的内存  
//也代表 v1 和 v2 存储的值 做了交换  
v1.swap(v2);  
cout << "v1:";  
PrintVector(v1);  
cout << "v2:";  
PrintVector(v2);  
  
//缩容  
v1.resize(1000000);  
v1.resize(5);  
cout << "v1.capacity =" << v1.capacity() << endl;  
//vector<int>(v1) 是一个匿名对象 使用拷贝构造 创建出一个对象  
//创建完毕后调用 swap 与 v1 进行内存交换  
//执行完这句代码后 匿名对象 的内存便会倍回收  
vector<int>(v1).swap(v1);  
//也可以直接使用 v1.shrink_to_fit();cout << "v1.capacity = " << v1.capacity() << endl;  
  
//内存清理  
v2.resize(1000000);  
v2.clear();  
//clear 后 内存并不会被释放掉 capacity 仍然可以调用  
cout << "v2.capacity =" << v2.capacity() << endl;  
//构造一个空的vector进行内存交换 一个执行完代码语句后就被释放 一个交换完后为空vector  
vector<int>({}).swap(v2);  
cout << "v2.capacity =" << v2.capacity() << endl;
```
# 空间预留

每次扩容都要移动大量数据
非常耗时
所有可以事先预留一些空间（也就是预先指定一个 capacity）

```C++
vector<int> v_temp_1;  
v_temp_1.reserve(100);  
for (int i = 0; i < 100; ++i) {  
    cout << "size ="  
         << v_temp_1.size() << ", " << "capacity =" << v_temp_1.capacity() << endl;  
    v_temp_1.push_back(i);  
}  
//reserve 修改的是 capacity
//resize 修改的是 size
```
# 高效删除

Vector 是一个数组，如果删除一个元素
那么后面的元素都会往前移动

```C++
void PrintVector(vector<int> &v) {  
    //为什么要解引用  
    //少一次拷贝过程  
    for (int i = 0; i < v.size(); ++i) {  
        cout << v[i] << " ";  
    }  
}  
  
void remove(vector<int> &v, int index) {  
    //要进行大量的内存移动 效率低  
    //时间复杂度为 O(n)    v.erase(v.begin() + index);  
}

void remove2(vector<int> &v, int index) {  
    //时间复杂度为 O(1)  
    //将要删除的元素与最后一个元素交换位置  
    swap(v[index], v.back());  
    //删除最后一个元素  
    v.pop_back();  
}

vector<int> v;  
for (int i = 0; i < 10; ++i) {  
    v.push_back(i);  
}  
remove(v, 4);  
PrintVector(v);  
  
for (int i = 0; i < 10; ++i) {  
    v.push_back(i);  
}  
remove2(v, 4);  
PrintVector(v);
```
# 数据排序

```C++
sort(v.begin(),v.end(),cmp);//cmd为比较函数
```



---