---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/二进制/JsonUtlity序列化
created: 2024-11-05
---

---
# 关联知识点

- 搭配 反序列化 一起看
- 分为了两个部分

---
# JsonUtlity是什么

- JsonUtlity 是Unity自带的用于解析Json的公共类
- 它可以
	- 将内存中对象序列化为Json格式的字符串
	- 将Json字符串反序列化为类对象
# 必备知识点——在文件中存读字符串

- 存储字符串到指定路径文件中
	- 第一个参数 填写的是 存储的路径（的文件）
	- 第二个参数 填写的是 存储的字符串内容
- 注意：第一个参数 必须是存在的文件路径 如果没有对应文件夹 会报错

```C#
//往 Application.persistentDataPath + "/Test.json" 这个 Json 文件
//存入字符串 唐老狮存储的json文件
File.WriteAllText(Application.persistentDataPath + "/Test.json", "唐老狮存储的json文件");
print(Application.persistentDataPath);//打印路径
```

- 在指定路径文件中读取字符串

```C#
//把这个路径（的文件）的内容，返回出去（返回类型为 String）
string str = File.ReadAllText(Application.persistentDataPath + "/Test.json");
print(str);
```
# 使用JsonUtlity进行序列化

- 序列化：把内存中的数据 存储到硬盘上
	- 方法 ——> `JsonUtility.ToJson(对象)`

- Jsonutility提供了线程的方法 可以把类对象 序列化为 json字符串

```C#
string jsonStr = JsonUtility.ToJson(t);
File.WriteAllText(Application.persistentDataPath + "/MrTang.json", jsonStr);
```

- 利用 JsonUtility.ToJson 将 数据 序列化为 字符串  
- 利用 File.WriteAllText 将 序列化的字符串 传入到 Json 文件中  
- 这样就实现了 *数据持久化* 的功能

- 注意：
	- `float` （`double`）序列化时看起来会有一些误差
		- 但读取的时候是没有问题的
	- 自定义类需要加上序列化特性 `[System.Serializable]`
		- 最外层的类不需要加
		- 但 类里面的 类对象 需要加
			- `class Test1 { Test2 test; }`
			- `Test2` 这个类就需要增加序列化特性
	- 想要序列化私有变量 需要加上特性 `[SerializeField]`
	- JsonUtility不支持字典
	- JsonUtlity存储 null 对象，（值）不会是 null ，而是根据数据类型来使用默认值
		- （例如 `int` ——> 0）


---
# 源代码

![[Unity 数据持久化 Json 6 JsonUtlity序列化 知识点.cs]]

---