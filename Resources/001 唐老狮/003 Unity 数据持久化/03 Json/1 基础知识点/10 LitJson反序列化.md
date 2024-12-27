---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/二进制/LitJson反序列化
created: 2024-11-05
---

---
# 关联知识点

- 搭配 序列化 一起看
- 分开为两部分了

---
# 使用LitJson反序列化

- 方法
	- `JsonMapper.ToObject(字符串)`

```C#
//获取之前写入的文件里面的数据，返回为一个 Json 字符串
jsonStr = File.ReadAllText(Application.persistentDataPath + "/MrTang2.json");  

//JsonData是LitJson提供的类对象 可以用键值对的形式去访问其中的内容  
JsonData data = JsonMapper.ToObject(jsonStr);//返回值为 JsonData 类型的对象
print(data["name"]); //可以直接使用 变量名 做为键名来访问相应的值
print(data["age"]); //但做了解便可

//通过泛型转换 更加的方便 建议使用这种方式  
MrTang2 t2 = JsonMapper.ToObject<MrTang2>(jsonStr);//返回的是 MrTang2 类型对象
```

- 注意 ——> 字典相关
	- 字典的键如果为 `int`类型
	- 那么在序列化的时候，会自动为其添加 `""` （Json 的特性）
	- 但是在反序列化的时候，LitJson 却会仍为这是一个 `string` 类型
	- 于是 ——> 报错

- 注意
	- 类结构需要 **无参构造函数** ，否则反序列化时报错
	- 字典虽然支持 但是键在使用为数值时会有问题 需要使用字符串类型
# 注意事项

- LitJson可以直接读取数据集合

```C#
jsonStr = File.ReadAllText(Application.streamingAssetsPath + "/RoleInfo.json");  
RoleInfo2[] arr = JsonMapper.ToObject<RoleInfo2[]>(jsonStr);  
  
List<RoleInfo2> list = JsonMapper.ToObject<List<RoleInfo2>>(jsonStr);  
  
jsonStr = File.ReadAllText(Application.streamingAssetsPath + "/Dic.json");  
Dictionary<string, int> dicTest = JsonMapper.ToObject<Dictionary<string, int>>(jsonStr);
```

- 文本编码格式需要是UTF-8 不然无法加载
# 总结

- LitJson提供的序列化 与 反序列化方法 `JsonMapper.ToJson / ToObject<>`
- LitJson无需加特性  
- LitJson不支持私有变量  
- LitJson支持字典序列化反序列化  
- LitJson可以直接将数据反序列化为数据集合  
- LitJson反序列化时 自定义类型需要无参构造  
- Json文档编码格式必须是UTF-8

---
