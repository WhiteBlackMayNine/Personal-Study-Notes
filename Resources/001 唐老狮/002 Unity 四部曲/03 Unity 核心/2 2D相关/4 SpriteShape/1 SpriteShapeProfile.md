---
tags:
  - 科学/Unity/唐老狮/Unity核心/2D相关/SpiteShape/Profile
created: 2024-07-22
更新: 
备注: 精灵形状概述文件
---

---
# 关联知识点


---
# SpriteShape是用来做什么的

- 顾名思义，SpriteShape是精灵形状的意思
- 从名字上看不出到底是用来干什么的
- 其实它主要是方便我们以节约美术资源为前提
- 制作2D游戏场景地形或者背景的
# 导入SpriteShape工具

- 在Package Manager中导入相关工具
- 可以选择性导入事例和拓展资源
- Package Manager ——> 搜索 2D SpriteShape
# 准备 精灵形状概括资源

- 想要节约美术资源的创建地形或其它类似资源
- 首先我们要准备精灵形状概括资源
- 之后我们就会使用它来创建地形等资源
- 一般分为两种
	- 开放不封闭的图形
		- 右键 Creat ——> SpriteShapeProfile ——> Open Shape
	- 封闭的图形
		- 右键 Creat ——> SpriteShapeProfile ——> Closed Shape
- 主要作用
	- 对地形进行描述
	- 有了它，才能真正的去创建地形
## 参数讲解
### Use Sprite Borders

- 是否使用精灵边框，用于九宫格拉的
- **默认勾选**
### Texture

- 用于填充实心部分的纹理
- 使用的纹理的亚铺模式必须是 Repeat 重复模式
### Offset

- 纹理偏移量
### Angle Ranges

- 这里的设置主要用于
- 封闭图形在不同角度范围内使用的图片不同可以达到一个封闭效果
- 也就是在编辑时，角度不同，使用的图片资源也不同
#### Angle Ranges(360)

- 角度范围
#### Start

- 起始角度
#### End

- 结束角度
#### Order

- Sprite相交时的优先级，优先级高的显示在前面
### Sprites

- 指定角度范围内的精灵列表在该角度范围内，可以选择使用的图片资源
### Corners

- 指定角显示的精灵图片主要用于封闭图形
- 上面四个 ——> 外部四个角用的图片
- 下面四个 ——> 内部四个角用的图片

# 使用精灵形状概括资源创建地形

- 把创建好的 精灵形状概括资源 直接拖入场景上，就可以使用



---
# 源代码

![[Sprite_Shape_Profile 知识点.cs]]

![[Sprite_Shape_Profile参数相关.png]]

---