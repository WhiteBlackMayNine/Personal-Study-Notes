---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Vector2"
created: 2024-04-02
---

---
# 关联知识点



---
# 知识点

## 静态变量

- 与 Vector3 异曲同工
- `Vector2.down Vector2.up Vector2.left Vector2.right Vector2.one Vector2.zero
## 构造函数

- `Vector2 v2 = new Vector2(2,2)`
## 成员变量

### 模长

- `v2.magnitude`
### 模长平方

- `v2.sqrmagnitude`
### 单位向量

- `v2.normalized`
	- v2 的值不会改变
### x y

- `v2.x v2.y`
## 公共函数

### 判断相等

- `v2.Equals( 另一个点 )`
	- 返回值为布尔值
	- 模长方向都要相等才行
## 设置Vector2的值

- `v2.Normalize()
	- 直接单位化
	- 模长改变方向不变
- `v2.Set(5,9)
	- 设置 x y 的值
- `transform.position = v2;`
	- 类型转换为 Vector3 后 改变
	- 不能单独改变
## 静态函数
### 计算夹角

- `Vector2.Angle(va,vb)`
	- 从 va 方向指向 vb 方向
	- **计算的无符号夹角
- `Vector2.SignedAngle(va,vb)`
	- va 和 vb 之间的有符号角度
	- 以度为单位，逆时针为正
### 计算距离

- `Vector2.Distance(va,vb)`
### 计算点集

- `Vector2.Dot(va,vb)`
## Max / Min 方法

- `Vector2.Max(va,vb));
- **返回值为向量va和向量vb在各个方向上的最大分量组成的新向量
- `Min`方法同理
### 相乘

- `Vector2.Scale(va,vb)`
- 返回值为 va和vb在各个方向上的分量相乘得到的新向量
## 插值运算

- [[5 向量插值运算]]
## 运算符

- Unity 已经自动进行重载了
- [[2 向量加减乘除]]


---
# 总结

- 用于表示2D向量和点

---
# 源代码

![[No7_Vector2.cs]]

---