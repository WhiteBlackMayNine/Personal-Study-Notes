---
tags:
  - 科学/Unity/唐老狮/Unity核心/2D相关/SpiteShape/Renderer和Controller
created: 2024-07-22
更新: 
备注: 精灵形状渲染器和控制器
---

---
# 关联知识点


---
# Sprite Shape Renderer参数相关
## 主要功能

- 该控件主要是控制 材质 颜色 以及和其它Sprite交互时的排序等等信息
## Color

- 颜色
## Mask Interaction

- 遮置相互作用规则设置
## Fil Material 和 Edge Material

- 填充材质和边缘材质
- 无特殊要求不更改
## Sorting Layer 和 Order in Layer

^83fc25

- 排序相关
- 值为 -1（或更小） 永远在最下层
# Sprite Shape Controller参数相关
## 主要作用

- 编辑地形
## Profile

- 使用的精灵形状概述文件
## Detail

- 精灵形状的质量高中低三种质量
## Is Open Ended

- 是否是开放的，不封闭的
- 勾选会使用开放的 Profile
## Adaptive UV

- 自适应UV
- 如果开启，会自动帮助我们判断是平铺还是拉伸
	- 开启后只有宽度够才会平铺
	- 如果宽度不够会拉伸
- 不开启始终平铺，但是可能会出现裁剪效果
- 一般根据你的实际效果进行选择
## Optimize Geometry

- 优化三角形数量，勾选后会最小化精灵图形中的三角形数量
## Corner Threshold

- 角阈值，当拐角处的角度达到这个阈值时将使用角图片
- X轴 与 这条边 所成的夹角
	- 大于这个角度时，不会使用角图片
	- 也就是 Profile 中的 Corners
## 启用编辑状态后
### Tangent Mode

- 切线模式
- 从左至右依次是
	- 顶点模式 ——> 点两侧不构成曲线
	- 切线模式 ——> 点两侧构成曲线，并且可以控制切线弧度
		- 拱桥、有弧度的地形
	- 左右切线模式 ——> 点两侧构成曲线，并且可以分别控制左右两侧切线弧度
### Position

- 选中点的局部坐标位置
### Height

- 控制点左右两侧精灵图片的高度
## Corner

- 是否使用角度图片
### Disabled

- 不使用角度图片
### Automatic

- 自动
## Sprite Variant

- 选择使用的精灵图片
# 生成碰撞器

## 使用边界碰撞器

- 在 Profile 上添加边界碰撞器后
- 边界碰撞器会根据目前的形状自动识别并设置边界
- **推荐使用这个**
## 使用多边形 碰撞器配合复合碰撞器

- 添加多边形后，再添加复合碰撞器
- 且勾选 Used By Composite
- 此时会自动添加刚体组件
	- 可能需要更改一下 刚体类型

---
