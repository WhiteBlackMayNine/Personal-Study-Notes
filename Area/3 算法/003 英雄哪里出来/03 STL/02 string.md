---
tags:
  - "#Cpp"
  - "#STL"
created: 2025-04-26
---

---
# 关联知识点

[[05 串]] [[03 deque]]

---
# 基本概念

是一个类
在 C++ 中，对 Char 类型的操作，都可以到 String 中通过对成员函数的调用进行操作
同时，在头文件 `iostream` 中，已经对 `string` 进行了封装
# 对象创建

```C++
//1．无参构造  
string s1;  
cout << s1 << endl;  
//2.初始化列表  
string s2({'h', 'e', 'l', 'l', 'o'});  
cout << s2 << endl;  
//3.字符串初始化  
string s3("data");  
cout << s3 << endl;  
//4.字符串的前n个字符  
string s4("英雄哪里出来", 4);  
cout << s4 << endl;  
//5.拷贝构造函数  
string s5(s4);  
cout << s5 << endl;  
//6.a个字符b  
string s6(8, 'o');  
cout << s6 << endl;
```
# 赋值操作

```C++
//1.字符串常量的赋值  
string s1;  
s1 = "英雄哪里出来";//双引号  
cout << s1 << endl;  
//2.字符串变量的赋值  
string s2;  
s2 = s1;  
cout << s2 << endl;  
//3.字符常量赋值  
string s3;  
s3 = 'o';//单引号  
cout << s3 << endl;  
//4. assign vl  
string s4;  
s4.assign("英雄算法联盟");  
cout << s4 << endl;  
//5. assign v2 取长  
string s5;  
s5.assign("英雄算法联盟", 6);  
cout << s5 << endl;  
//6.assign v3 拷贝构造函数  
string s6;  
s6.assign(s4);  
cout << s6 << endl;  
//7.a 个 bstring s7;  
s7.assign(8, 'a');  
cout << s7 << endl;
```
# 拼接操作

```C++
// + 运算符重载  
string s1 = "英雄";  
string t1 = "出来";  
s1 = s1 + "哪里"; //"哪里" 为一个 const char* 类型  
cout << s1 << endl;  
s1 = s1 + t1;//t1 为一个 const string& 类型  
cout << s1 << endl;  
s1 = s1 + ';';//';' 为一个 char 类型、  
  
//+=  
string s2 = "夜深";  
string t2 = "写算法";  
s2 += t2;  
cout << s2 << endl;  
  
//append 方法  
string s3 = "英雄";  
string t3 = "夜深";  
s3.append(t3);  
s3.append("联盟");  
s3.append("23565885", 6);//取长 取六个元素  
cout << s3 << endl;  
s3.append("23565885", 1, 5);//从索引开始 取五个元素 35658cout << s3 << endl;  
  
//push_back  
string s4 = "520";  
s4.push_back('a');  
cout << s4 << endl;
```
# 比较操作

```C++
// 1. compare  
//从第一个元素开始比较 相等则比较下一个 直到有一个小于或大于  
//如果两个长度不同，那么长的更大  
string s1 = "aab";  
string t11 = "abb";  
int r11 = s1.compare(t11);//三值 -1 0 1 小于 等于 大于  
cout << t11 << " -> " << r11 << endl;  
  
string t12 = "aaa";  
int r12 = s1.compare(t12);//三值 -1 0 1 小于 等于 大于  
cout << t12 << " -> " << r12 << endl;  
  
string t13 = "aaa";  
int r13 = s1.compare(t13);//三值 -1 0 1 小于 等于 大于  
cout << t13 << " -> " << r13 << endl;  
  
string t14 = "aaa";  
int r14 = s1.compare(t14);//三值 -1 0 1 小于 等于 大于  
cout << t14 << " -> " << r14 << endl;  
  
// 2. < > <= >= == !=  
cout << t11 << "->" << (s1 == t11) << endl;  
cout << t11 << "->" << (s1 != t11) << endl;  
cout << t11 << "->" << (s1 <= t11) << endl;
```
# 随机访问

```C++
string  s1 = "1314 520";  
for (int i = 0; i < s1.size(); ++i) {  
    //size 不包括 '\0'    
    cout << s1[i] << endl;  
    cout << s1.at(i) << endl;  
}

//修改  
s1[2] = 'a';  
s1.at(3) = 'b';  
cout << s1 << endl;
```
# 数据插入

```C++
string s1 = "1314520";  
s1.insert(2, 2, 'l');//在索引2 前面 插入 2个 l 只能插入字符 '' 单引号  
cout << s1 << endl;  
s1.insert(4, "0");//在索引4 前面 插入 0 只能插入字符串 “” 双引号  
cout << s1 << endl;  
  
//插入尾部  
s1.insert(s1.size(), "data");  
cout << s1 << endl;  
  
//插入头部  
s1.insert(s1.begin(), 'l');//如果传入一个迭代器 那么只能插入字符  
cout << s1 << endl;
```
# 数据删除

```C++
string s1;  
  
//全部删除  
s1 = "dadadadada";  
s1.erase();  
cout << s1 << endl;  
  
//从第几个元素开始删除  
s1 = "dadawdfsag";  
s1.erase(7);  
cout << s1 << endl;  
  
//从第四个元素开始 删3个  
s1 = "dadawdfsag";  
s1.erase(4, 3);  
cout << s1 << endl;  
  
//传入迭代器 只删除一个  
s1 = "dadawdfsag";  
s1.erase(s1.begin());  
cout << s1 << endl;  
  
//左闭右开 把这个区间的元素全部删除  
s1 = "dadawdfsag";  
s1.erase(s1.begin() + 1, s1.end());  
cout << s1 << endl;
```
# 数据查找

如果没有找到字符，返回 `std::string::npos`
是一个静态常量，具体大小取决于系统和编译器

```C++
string  s1;  
  
//查找某个字符/字符串 在 string 中的 第一次出现的索引位置  
s1 = "dawdsdawgsa";  
cout << s1.find("dsda") << endl;//从头开始找  
  
//从某个索引出开始 找  
//如果返回一个奇怪的数字 可以加个 intcout << s1.find("dsda",8) << endl;  
  
//在字符串中找这个字符第一次出现的索引位置  
cout << s1.find('s') << endl;  
  
//从右边开始找 a - s - g (查找的字符也是从右开始读)  
cout << s1.rfind('s');  
cout << s1.rfind("dsda",5);
```
# 数据替换

```C++
string s1;  
  
//从 起始位置 位置 选取 5 个元素 替换为 ors1 = "dadsdadasda";  
s1.replace(7, 5, "or");  
  
//从 起始位置 到 终点位置 替换为 ors1 = "dadsdadasda";  
s1.replace(s1.begin(), s1.end(), "or");  
  
//替换的字符串 将会取长  
s1 = "dadsdadasda";  
s1.replace(s1.begin(), s1.end(), "o85549daa", 5);
```
# 子串获取

```C++
//从哪里开始 选取多少个元素  
s1 = "dadsdadasda";  
string substr = s1.substr(4, 4);  
string substr2 = s1.substr(4);//从索引4开始，到字符串结尾
cout << substr << endl;
```




---
