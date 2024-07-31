---
tags:
  - 科学/Unity/唐老师/Unity核心/2D动画/2D骨骼动画/2DAnimation/PSB图片骨骼编辑
created: 2024-07-25
更新:
---

---
# 关联知识点



---
# 认识PSB文件

- 认识PSB之前先认识PS
- PS（photoshop）是一款强大的图像处理软件在各领域都被广泛使用
- 在游戏行业中也是美术同学使用最多的图像处理软件之一
- PSD 和 PSB 两种格式，都是PS这款软件用于保存图像处理数据的文件格式
- PSD 和 PSB 两种格式并没有太大的区别
- 最大的区别是 PSD 格式兼容除 PS 以外的其它一些软件
- 而 PSB 只能用 PS 打开
- 在Unity中官方建议使用 PSB 格式
# 在Unity中使用PSB文件

- 需要在 Packages Manager 窗囗中引入 2D PSD Importer 工具包
# 设置PSB文件关键参数
## Moszic

- 启用后，将图层生成 Sprite，并将他们合并成单个纹理
## Character Rig

- 是否使用人物已经绑定的骨骼
## Use Layer Grouping

- 使用 PSB文件中的层分组
## Pivot

- 轴心点位置
# 为PSB文件编辑骨骼信息
## 骨骼编辑

- Spite Editor 中切换为 Skinning Editor
- 不需要像图集图片骨骼编辑那样创建空物体进行连接
- PSB 文件可以在 Spite Editor 中直接进行骨骼编辑
## 蒙皮编辑

- 单独选择每个 Spite，点击 Auto Geometry 自动蒙皮
- 由于自动蒙皮可能造成 骨骼与蒙皮关联错误
	- 比如身体，应该只有 脊椎 这条骨骼来影响蒙皮
	- 但现在 手臂 的骨骼也会影响蒙皮
- 因此需要使用 Weights 中的 Bone Influence
	- 在右下角窗口中，把不需要的骨骼删除
		- 右下角的 `-` 号
- `+` 号作用
	- 在 Spite 上关联一个骨骼
		- 点击一个 Spite 再点击一个骨骼
		- 然后点击 `+`号
	- 生成蒙皮和权重
	- Spite 就会受骨骼影响
## 细微调整

- 选择 Spite 的蒙皮后
- 利用 Weight Slider 与 Weight Brush 进行细微编辑
# 为PSB文件制作骨骼动画

- 要使用 PSB 的骨骼动画，需要先创建一个空物体（根对象），然后把 PSB 文件拖到根对象上
- 调整位置，让根对象在对象的脚下
	- 这样在利用代码调整位置时，只需要调整根对象
	- 就能模仿出移动的效果
- 需要先对预设体进行 Unpack Prefab，才能对骨骼进行移动、删除

---
