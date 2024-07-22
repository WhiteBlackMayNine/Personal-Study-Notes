---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Rigidbody2D"
created: 2024-04-03
---

---
# 关联知识点



---
# 知识点

- 需要提前获取组件
	- `private Rigidbody2D rb;
	- `rb = GetComponent<Rigidbody2D>();`
## 添加一个力

- `rb.AddForce(Vector3.up * 10)
## 重力的影响程度

- `rb.gravityScale = 值
	- 默认值为1，表示受到正常重力影响
	- 如果设置为0，则不受重力影响
	- 如果设置为负数，则受到反向重力的影响
## 刚体的速度

- `rb.velocity = new Vector3(1, 0, 0)
	- 改变刚体的运动状态
	- 速度设置为向右移动
## 旋转到指定角度

- `rb.MoveRotation(Quaternion.Euler(90, 0, 0))

---
# 源代码

![[No17_Rigidbody.cs]]

---