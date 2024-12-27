---
tags:
  - 科学/Unity/ARPG/Health/CharacterHealthBaseData
created: 2024-08-14
---

---
# 关联知识点

[[CharacterHealthInfo]] 

---
# 说明

- 处理每一种敌人的初始生命值信息
	- 可能有多种敌人，导致初始生命值不一样
- 用于创建 生命值资产文件的基础文件
- 记录生命值基础信息
# 代码

```C#
//处理每一种敌人的初始生命值信息
//可能有多种敌人，导致初始生命值不一样
[SerializeField] private float _maxHP;//最大生命值
[SerializeField] private float _maxStrength;//最大体力值

/// <summary>
/// 初始最大生命值
/// </summary>
public float MaxHp => _maxHP;

/// <summary>
/// 初始最大体力值
/// </summary>
public float MaxStrength => _maxStrength;
```
# 总结

- 在这里记录 有关于对象生命值的基础信息
	- 基础数据
- [[CharacterHealthInfo]] 则是进行逻辑处理
	- 实例数据
- [[Unity 知识点#^afe6ee|分离数据与实例数据]]


---
# 源代码

![[CharacterHealthBaseData.cs]]

---