---
tags:
  - "#Cpp"
created: 2025-04-17
---

---
# 关联知识点

[[06 运算符重载]]

---

跟 Csharp 相似

```C++
class A{
public:
	int a;
	void Call();
}
```
# 与 Struct 区别

Struct 默认公有
Class 默认私有
# 属性

不同于 Csharp，这里是写个 `SetName() 与 GetName()` 方法，进行修改变量的值与获取变量的值
# 拷贝构造函数

用一个对象构造出另外一个对象

`类名(const 类型& 对象名)`

- `&` 引用，避免调用过程中，传参的时候
	- 多次拷贝
- `const`
	- 传进来的参数不会被改变
		- 因为是 通过 A 对象创建 B 对象
		- 改变 A 对象就不太合理了

调用拷贝构造函数的前提为：生成一个新的对象
如果一个函数传入类型为一个 Class，且参数类型为指针，那么不会去调用拷贝构造函数
因为没有新的对象产生

但如果参数类型不为指针，那么就会调用拷贝构造函数生成一个 Class 对象

函数返回值同理
如果 `return` 一个新的对象，那么就会调用拷贝构造函数
如果没有，那么就是普通的构造函数
但是，编辑器会有 rvo 优化，导致都会使用普通构造函数
# This 指针

解决命名冲突
`*this` 可以得到这个对象本身

```C++
class Hero{
public:
	int hp;
	Hero(int hp){
		this -> hp = hp;
	}
}
```

```C++
this = &h
*this = *(&h)
```
# 常函数

常函数内部无法修改成员变量的值

在函数后面加上 `const`

```C++
int getHp() const{
	//无法修改成员变量的值
}
```

如果类对象为一个常量，那么不可以调用非常函数与非常量

```C++
class Her(){
public:
	int hp;
	void PringtS(){
	
	}
}

int main(){
	const Hero h;
	h.hp;//报错
	h.PringtS();//报错
}
```

简单来说，常量调用一个非常函数，调用时会修改对象的值
这就和 常量 的性质有冲突

> 常量对象只能调用常函数
# `mutable`

与 `const` 相对
在常函数中，可以修改成员变量
在成员变量前面加上 `mutable` 便可

```C++
class Hero{
public:
	void PrintS() const {
		count++;
	}
private:
	mutable int count;
}
```
# 友元

让一个类（或者函数）去访问另外一个类的私有成员

关键字：`friend`

三种：全局函数、类、成员函数
## 全局函数做友元

```C++
class People{
	friend void friendPrintS(People* p);//全局函数做友元
private:
	int id;
}

void friendPrintS(People* p){
	cout << p -> id;
}

int main(){
	People p;
	friendPrintS(&p);
}


```
## 类做为友元

```C++
class People{
	friend class Friend;//类做友元
private:
	int id;
	string name;
}

class Friend{
public:
	void visitFriend(People* p){
		cout << p -> id << p -> name;
	}
}
```
## 成员函数做友元

```C++
class Friend{
public:
	void visitFriend(People* p){
		cout << p -> id << p -> name;
	}
}

class People{
	friend void Friend::visitFriend(People* p);//成员函数做友元
private:
	int id;
	string name;
}


```




---
