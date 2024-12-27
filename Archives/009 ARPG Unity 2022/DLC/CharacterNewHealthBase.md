---
tags:
  - 科学/Unity/ARPG/DLC/CharacterNewHealthBase
created: 2024-08-16
---

---
# 关联知识点



---
# 说明

- 功能与 老生命值基类 基本相同
- 只是多了一个接口
# 新攻击系统接口

```C#
public interface IDamaged
{
	void CharacterNormalDamaged(string hitName, string parryName, float damage, Transform attack, DamageType damageType);
}
```
# 新攻击输入系统接口实现

^df515d

- 外部可以通过接口来调用这个函数
- 函数内部又会 调用 **角色受击函数** （[[PlayerNewHealth]] 里会去重写）

```C#
public void CharacterNormalDamaged(string hitName, string parryName, float damage, Transform attack, DamageType damageType)
{

	CharacterHitAction(damage, hitName, parryName);
}
```



---
# 源代码

![[CharacterNewHealthBase.cs]]

---