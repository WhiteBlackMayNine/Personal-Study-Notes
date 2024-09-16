---
tags:
  - "#科学/Unity/ARPG/Unilts/ExpandClass"
created: 2024-08-16
---
---
# 关联知识点



---
# 说明

- 一个静态类，包含两个两个扩展方法
# 拓展方法
## `Look` 看着目标方向以Y轴为中心

- 传入参数 一个要旋转的对象的 `Transform` ，旋转目标位置 `target`，过渡时间 `timer`

- `var direction = (target - transform.position).normalized;`
	- 用 目标位置 减去 当前位置 ，得到的数值进行归一化
	- 得到一个 **单位向量**
- 将 得到的 **单位向量** 的 y 值 设置为 0，（只存在于 xz 平面）
- `Quaternion.LookRotation` 根据传入的向量创建一个旋转（四元数类型）
- 将创建的 四元数 赋值给 `transform.rotation` 
	- `Quaternion.Slerp`平滑地在两个旋转之间进行插值，生成一个新的旋转
	- 从 当前的旋转 平滑过渡到 `lookRotation` （目标的旋转量）
- `DevelopmentToos.UnTetheredLerp` 为工具包中的 平滑过渡时间
	- [[DevelopmentToos]]

```C#
public static void Look(this Transform transform, Vector3 target,float timer)  
{  
    var direction = (target - transform.position).normalized;  
    direction.y = 0f;
    Quaternion lookRotation = Quaternion.LookRotation(direction);  
	transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,
	DevelopmentToos.UnTetheredLerp(timer));  
}
```
## `AnimationAtTag` 检查当前动画片段是否是指定Tag

- 传入参数
	- `this Animator animator` ——> 调用这个函数的 `animator`
- 返回值
	- 调用Animator类的 `GetCurrentAnimatorStateInfo`方法获取指定层的当前动画状态信息
	- 然后使用 `IsTag` 方法检查该状态是否包含指定的标签
	- 如果包含，返回true；否则返回false
### 传入参数详解

- 扩展方法
	- 向现有的类型添加新的方法，而无需修改原始类型的源代码
	- 通过使用`this`关键字，可以将扩展方法与特定类型关联起来，使其看起来像是该类型的实例方法

- 简单理解
	- 使用了 `this` 关键字 创建 扩展方法后，就可以使用 点运算符 进行调用了
	- 比如说这个 方法，在使用的时候，可以在 `animator` 中使用 点运算符 调用了
	- ——> `animator.AnimationAtTag("Motion")`

```C#
public static bool AnimationAtTag(this Animator animator, string tagName,int indexLayer = 0)  
{  
    return animator.GetCurrentAnimatorStateInfo(indexLayer).IsTag(tagName);  
}
```



---
# 源代码

![[ExpandClass.cs]]

---