---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/二进制/JsonUtlity反序列化
created: 2024-11-05
---

---
# 关联知识点

- 搭配 序列化 一起看
- 分为了两个部分

---
# 使用JsonUtlity进行反序列化

- 反序列化：把硬盘上的数据 读取到内存中
	- 方法 ——> `JsonUtility.FromJson(字符串)`

- 读取文件中的 Json字符串
	- `jsonStr = File.ReadAllText(Application.persistentDataPath + "/MrTang.json");`

- 读取 Json 文件，得到数据字符串
- 将数据字符串传入 `JsonUtility.FromJson` 进行反序列化
- 并使用相应的类对象存储
- ——> 读取 存储到 硬盘上的 数据

```C#
//读取文件中的 Json字符串  
jsonStr = File.ReadAllText(Application.persistentDataPath + "/MrTang.json");
//JsonUtility.FromJson 使用Json字符串内容 转换成类对象 返回一个 Object 类型的对象
//有两个重载 1.传入Json字符串 与 一个类的 Type
//2.传入Json字符串 与 一个类的泛型
MrTang t2 = JsonUtility.FromJson(jsonStr, typeof(MrTang)) as MrTang;
MrTang t3 = JsonUtility.FromJson<MrTang>(jsonStr);
```

- 注意
	- 如果Json中数据少了，读取到内存中类对象中是不会报错
	- 例如 字典，读取的时候会赋予默认值（`null`）
# 注意事项

- JsonUtlity无法直接读取数据集合
- 文本编码格式需要是UTF-8 不然无法加载
# 总结

- 必备知识点 —— File存读字符串的方法 ReadAllText和WriteAllText
- JsonUtlity提供的序列化反序列化方法 ToJson 和 FromJson
- 自定义类需要加上序列化特性 `[System.Serializable]`
- 私有保护成员 需要加上 `[SerializeField]`
- JsonUtlity不支持字典
- JsonUtlity不能直接将数据反序列化为数据集合
- Json文档编码格式必须是UTF-8



---
