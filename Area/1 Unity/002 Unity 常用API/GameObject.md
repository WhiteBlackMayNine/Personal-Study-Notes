---
tags:
  - "#科学/Unity/SiKi/Unity常用API/GameObject"
created: 2024-04-02
---

---
# 关联知识点

[[最小单位 Gameobject]]

---
# 知识点

## 创建方式

### 创建一个空的游戏对象

- `GameObject obj = new GameObject("名字(可以不写)");`
	- 利用构造函数来创建
	- 声明 + 实例化
### 克隆

- `Gameobject.Instantiate(游戏物体);
	- 根据现有的预制体（游戏物体）资源
	- 或者游戏场景已有的游戏物体来实例化
### 原始几何体

- `Gameobject.CreatePrimitive(PrimitiveType.XX)
	- 使用特别的API创建一些基本的游戏物体类型
	- 使用点运算符查看
## 查找和获取


### 访问当前游戏物体

- `this.gameobject`
	- 可以使用 点运算符 查看成员变量
### 查找
#### 通过游戏名查找

- `gameObject.Find("游戏对象名")`
	- 游戏对象名为字符串对比形式去查找
	- 有符号有空格都需要打上
#### 通过标签查找

- `GameObject.FindGameObjectWithTag("标签名");
#### 通过类型查找

- `Gameobject.FindobjectofType<类型>();`
	- 也就是脚本名 / 组件名
#### 查找多个对象

- `GameObject[] 变量名 = GameObject.FindGameObjectsWithTag("对象名");
	- 对象名为 父物体 名字
- `类型[] 变量名 = GameObject.FindobjectsofType<类型>();
## this

- 当前组件（即挂载的脚本）

---
# 源代码

![[No3_GameObject.cs]]

---