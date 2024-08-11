---
tags:
  - "#科学/Unity/ARPG/声音播放/PoolItemBase"
created: 2024-06-21
---

---
# 关联知识点

[[PoolSound]] [[AssestSoundSO]]

---
# 说明

- 基类
- 写有生成（`Spawn()`）与回收（`Recycl()`）两个函数
- 便于管理
# 代码

```C#
public interface IPoolItem
{
    void Spawn();
    void Recycl();
}
public abstract class PoolItemBase : MonoBehaviour, IPoolItem
{
    private void OnEnable()
    {
        Spawn();
    }

    private void OnDisable()
    {
        Recycl();
    }

    public virtual void Recycl()
    {

    }

    public virtual void Spawn()
    {

    }
}
```
# 更新

- 写有 生成 与 回收 两个虚函数的基类
- 让子类去继承并重写这两个函数
- 也可以不去写这个脚本
- 直接在子类脚本把这两个脚本给写上去
- 写这个脚本的目的，就是希望 如果以后要写其他 播放声音的脚本
- 就去继承这个基类，便于管理
	- ~~而且可以少写一些代码~~

---
# 源代码

![[PoolItemBase.cs]]

---