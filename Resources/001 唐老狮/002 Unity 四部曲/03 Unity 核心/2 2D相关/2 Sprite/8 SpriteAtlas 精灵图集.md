---
tags:
  - 科学/Unity/唐老狮/Unity核心/2D相关/Sprite/SpriteAtlas精灵图集
created: 2024-07-20
更新:
---

---
# 关联知识点

[[2 Resources资源同步加载]]

---
# 为什么要打图集

- 打图集的目的就是减少 Drawcall 提高性能
- 在2D游戏开发，以及 UI 开发中是会频繁使用的功能
# 在Unity中打开自带的打图集功能

- 打图集
	- 将一张张小图，合并成一个大图
## 打开功能

- 在工程设置面板中
	- Edit->Project setting->Editor
- Sprite Packer
	- 精灵包装器，可以通过unity自带图集工具生成图集
## Disabled

- 默认设置，不会打包图集
- 创建的模式不是2D时，Unity 默认就是这个
## Enabled For Builds (Legacy Sprite Packer)

- Unity仅在构建时（也就是打包时）打包图集，在编辑模式下不会打包图集
## Always Enabled (Legacy Sprite Packer)

- Unity在构建时（也就是打包时）打包图集，在编辑模式下运行前会打包图集
## Legacy Sprite Packer

- 传统打包模式 相对下面两种模式来说 多了一个设置图片之间的间隔距离
	- Enabled For Build
	- Always Enabled
## Padding Power

- 选择打包算法在计算打包的精灵之间以及精灵与生成的图集边缘之间的间隔距离
	- 这里的数字 代表2的n次方
## Enabled For Build

- Unity进在构建时（也就是打包时）打包图集，在编辑器模式下不会打包
## **Always Enabled**

- Unity在构建时（也就是打包时）打包图集，在编辑模式下运行前会打包图集
- 创建的模式为 2D 时，Unity默认就是这个
- **一般无特殊要求就选择这个**
# 打图集面板参数相关
## 创建

- Creat ——> Sprite Atlas
## Type 图集类型
### 主图集Master
#### Include in Build

- 选中可以在当前构建中包含图集
- 也就是说可以包含另一个图集
#### Allow Rotation

- 选中此选项将在打包图集时对图片元素进行旋转，可以最大限度提高组合后的图集密度
- 注意
	- **如果是UI图集，请禁用此选项，因为打包时会将场景中UI元素旅转**
#### Tight Packing

- 选中此选项在打包图集时使用图片轮廓来打包而不是根据矩形
	- 根据像素来分
- 可以最大限度提高组合后的图集密度
#### Padding

- 图集中各图片的间隔像素
### 变体类型的图集 Variant

- 变体图集的主要作用是以主图集为基础，对它进行缩放产生一个新的图集副本
- 如果想使用变体图集中的内容，只需要勾选变体图集的Include in build选项
- 而主图集禁用此选项
	- 即0
- 用得相当少
### Master Atlas

- 关联的主图集
	- 图集类型必须是Master
#### Include in Build

- 选中可以在当前构建中包含图集
#### Scale

- 设置变体图集的缩放因子（0~1），变体图集的大小是主图集乘以Scale的结果
- 可以理解为
	- 在原本图集上进行缩放，得到一个新的图集
##### 注意

- 如果希望使用变体图集的内容
	- 只需要勾选变体图集的 Include in Build
- 而主图集
	- 需要禁用 Include in Build
## Objects for Packing

- 关联需要打图集的图片们
### 注意

- 一定是Sprite类型的图片
- 可以直接拖文件夹
- 最后记得点击 Pack Preview
- 平时直接用，最后打包一下
# 代码控制

- 通过代码，直接加载 SpriteAtlas 图集当中的某一个资源
- 然后让它显示在场景之中
## 代码

```C#
GameObject obj = new GameObject();//创建物体
SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();//显示图片，添加脚本

//需要引用命名空间
//动态的去加载图集当中的某一张图片
//利用 Reasource 中 load 去加载
//类型是 SpriteAtlas  MyAtlas 是图集的名字

SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("MyAtlas");//加载图集资源
//Sprite1 是图集中一张图片的名字
sr.sprite = spriteAtlas.GetSprite("Sprite1");//加载图集中的图片资源
```

# 观察 DrawCall 数量

- 图集有没有生效，可以通过观察 DrawCall 数量来确定
## 方法

- Game 窗口 ——> Stats（右上角）——> Batches 的数量就是 DrawCall 的数量
	- 注意
		- 如果开始运行时 Batches 数量发生改变，是因为同一个图集的图片只会包含一次DrawCall

---