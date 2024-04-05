---
tags:
  - 科学/Unity/Unity入门/脚本基础/Inspector窗口可编辑变量
created: 2024-03-23
课时: "20"
来源: https://www.bilibili.com/video/BV1HX4y1V71E?p=13
备注: Inspector 窗口显示的可编辑的变量就是脚本的成员变量
---



---
# 关联知识点



---
# 知识点


## 私有的和保护的无法显示编辑

## 让私有的和保护的也可以被显示

- 方法
	- 加上强制序列化字段特性
	- `[SerializeField]
	- 所谓序列化就是把一个对象保存到一个文件或数据库字段去
- 注意
	- 声明一个变量就需要添加一个序列化特性
- `Header("标题名")
	- 添加一个标题
- `[SerializeField, Header("地面检测")]`
## 公共的可以显示编辑

## 公共的也不让其显示编辑

- 变量前加上特性`[HideInspector]

## 大部分类型都能显示编辑

- 字典不能被显示

## 让自定义类型可以被访问

- 加上序列化特性
- `[System.SerializeField]`
- 但字典依旧不行

## 辅助特性

### 分组说明特性Header

- 为成员分组
- Header特性
- `[Header("分组说明")]`
```C#
[Header("基础属性")] public int age; public bool sex;
```
### 修饰数值的滑条范围Range

- `Range(最小值，最大值)]`
```C#
[Rhnge(0,10)]public float luck;
```
## 注意

- Inspector窗口中的变量关联的就是对象的成员变量，运行时改变他们就是在改变成员变量
- 拖曳到Gameobject对象后 再改变脚本中变量默认值 界面上不会改变
- 运行中修改的信息不会保存

---

