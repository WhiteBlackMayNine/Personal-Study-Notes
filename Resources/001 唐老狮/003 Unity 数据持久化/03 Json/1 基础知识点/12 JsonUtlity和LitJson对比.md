---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/二进制/JsonUtlity和LitJson对比
created: 2024-11-06
---

---
# 关联知识点



---
# JsonUtlity和LitJson相同点

- 他们都是用于Json的序列化反序列化
- Json文档编码格式必须是UTF-8
- 都是通过静态类进行方法调用
# JsonUtlity和LitJson不同点

- JsonUtlity是Unity自带，LitJson是第三方需要引用命名空间  
- JsonUtlity使用时自定义类需要加特性,LitJson不需要  
- JsonUtlity支持私有变量(加特性),LitJson不支持  
- JsonUtlity不支持字典,LitJson支持(但是键只能是字符串)  
 - JsonUtlity不能直接将数据反序列化为数据集合(数组字典),LitJson可以  
- JsonUtlity对自定义类不要求有无参构造，LitJson需要  
- JsonUtlity存储空对象时会存储默认值而不是null，LitJson会存null
# 如何选择两者

- 根据实际需求  
- 建议使用LitJson  
- 原因
	- LitJson不用加特性，支持字典，支持直接反序列化为数据集合，存储null更准确

---
# 源代码

![[Unity 数据持久化 Json 12 JsonUtlity和LitJson对比.cs]]

---