---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Playerprefs/PalyerPrefs基本方法
created: ""
---

---
# 关联知识点



---
# 存储相关

- `SetXXX`
	- 存到内存中
- `.Save()`
	- 存储到硬盘当中

```C#
//只是存入到内存中，并没有存到硬盘
//参数一：键 参数二：值
PlayerPrefs.SetInt("MyAge", 18);
PlayerPrefs.SetFloat("MyHeight",1.75f);
PlayerPrefs.SetString("MyName","John Doe");
//直接调用set的相关方法，只会把数据存到内存当中
//当游戏结束的时候，会自动存到硬盘中
//但如果游戏崩溃（或不是正常结束），数据是不会存到硬盘中的

PlayerPrefs.Save();
//调用这个方法，就会马上存储到硬盘当中

//PlayerPrefs 只能存三种类型 如果想要存别的类型
//要么降低精度，要么上升精度

bool set = true;
PlayerPrefs.SetInt("MySet", set ? 0 : 1);

//如果不同类型用同一个键名存储，会覆盖前面存储的
```
# 读取相关

```C#
//注意 运行时 只有使用了 set 存储了对应的键值对
//即使没有马上存储 save 到本地硬盘
//也是可以进行读取的

//先从 内存 中查找，找不到再去 硬盘中查找
//如果都找不到，会返回一个默认值（int float 为 0 string 为 空字符串）

//int
int age = PlayerPrefs.GetInt("MyAge");
int age_2 = PlayerPrefs.GetInt("MyAge",200);//可以自己设置默认值
Debug.Log(age);

//float
float height = PlayerPrefs.GetFloat("MyHeight");
float height_2 = PlayerPrefs.GetFloat("MyHeight", 200);
Debug.Log(height);

//string
string name = PlayerPrefs.GetString("MyName");
string name_2 = PlayerPrefs.GetString("MySet","dsawd");
Debug.Log(name);

//判断数据是否存在

if(PlayerPrefs.HasKey("MyName")){}

//删除
PlayerPrefs.DeleteKey("MyName");

//删除所有数据
PlayerPrefs.DeleteAll();
```



---
