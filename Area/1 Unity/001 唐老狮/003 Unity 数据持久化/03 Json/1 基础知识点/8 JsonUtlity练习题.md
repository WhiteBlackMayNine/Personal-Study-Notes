---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/二进制/JsonUtlity练习题
created: 2024-11-05
---

---
# 关联知识点



---

```C#
//序列化对象  
public void SaveData(PlayerInfo player, string path)  
{  
    string jsonStr = JsonUtility.ToJson(player);  
    print(Application.persistentDataPath);  
    File.WriteAllText(Application.persistentDataPath + "/" + path + ".json", jsonStr);  
}  
  
//反序列化对象  
public PlayerInfo LoadData(string path)  
{  
    string jsonStr = File.ReadAllText(Application.persistentDataPath + "/" + path + ".json");  
    return JsonUtility.FromJson<PlayerInfo>(jsonStr);  
}
```


---
# 源代码

![[Unity 数据持久化 Json 6 JsonUtlity序列化 练习题.cs]]

--