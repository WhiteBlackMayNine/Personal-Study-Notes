---
tags:
  - 科学/Unity/唐老狮/Unity入门/重要组件和API/必不可少Transform/必备Vector3基础
created: 2024-03-23
课时: "32"
来源: https://www.bilibili.com/video/BV1HX4y1V71E?p=21
---

---
# 关联知识点

[[5 父子关系]] [[3 角度和旋转]] [[4 缩放和看向]]  [[6 坐标转换]] 

---
# 知识点

## 必备 Vector3 基础
### 作用

- 主要用来表示三维坐标系中的**一个点**或者**一个向量**
### 声明

#### 第一种方法

- （一般不用）
- `Vector3 v = new Vector3();`
	- 声明了一个点
- x、y、z 赋值
	- `v.x = 10; v.y = 10 ; v.z = 10;`
#### 第二种

- **（一步到位，推荐使用）
- 重载
	- `Vector3 v2 = new Vector3(10,10)`
		- 如果只传入 x，y，则 z 默认为0
### Vector3 的基本计算

- `+ - * /`
	- `Vector3 v3 = v1 + v2;`
### 常用

- `Vector3.zero  Vector3.left  Vector3.right`
	- 分别为 (0 0 0) (1 0 0) (-1 0 0) 的三个点
- `Vector3.forward`
	- 一个 (0 0 -1) 的点，一般用于一个物体的**面朝向
- ` Vector3.back`
	- 一个(0 0 -1)的点，一般用于一个物体的**背朝向
- `Vector3.up  Vector3.down`
	- 分别为 (0 1 0) (0 -1 0)的两个点
### 常用的一个方法

- 计算两个点之间距离的方法
	- `Vector3.Distance(v1,v2)`
		- 计算 v1 与 v2 的距离

---
