---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/二进制/LitJson序列化
created: 2024-11-05
---

---
# 关联知识点

- 搭配 反序列化 一起看
- 分开为两部分了

---
# LitJson是什么

- 它是一个第三方库，用于处理 Json 的 序列化和反序列化  
- LitJson 是 C# 编写的，体积小、速度快、易于使用  
- 它可以很容易的嵌入到我们的代码中  
- 只需要将 LitJson 代码拷贝到工程中即可
# 获取LitJson

- 前往LitJson官网  
- 通过官网前往GitHub获取最新版本代码  
- 讲代码拷贝到Unity工程中 即可开始使用LitJson
# 使用LitJson进行序列化

- 方法
	- `JsonMapper.ToJson(对象)`

```C#
//传入对象，返回 Json 字符串
string jsonStr = JsonMapper.ToJson(t);  
print(Application.persistentDataPath);  //打印路径信息
//将得到的 Json 字符串，写到 Json 文件里面
File.WriteAllText(Application.persistentDataPath + "/MrTang2.json", jsonStr);
```

- 注意
	- 相对与JsonUtlity来说不需要加特性（`[System.Serializable]`）
	- 不能序列化私有变量  
	- **支持字典类型**，字典的键 建议都是字符串 
		- 因为 Json的特点 ——> Json中的，键名 会加上双引号  
	- 需要引用LitJson命名空间  
	- LitJson可以准确的保存 null 类型
		- 而不是根据数据类型来使用默认值


---
