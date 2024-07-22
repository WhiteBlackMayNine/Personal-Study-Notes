---
tags:
  - "#科学/Unity/ARPG/PoolItemBase"
created: 2024-06-21
更新:
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

---
# 源代码

![[PoolItemBase.cs]]

---