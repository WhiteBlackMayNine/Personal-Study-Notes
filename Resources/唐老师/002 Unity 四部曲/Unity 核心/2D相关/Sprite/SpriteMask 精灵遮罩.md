---
tags:
  - 科学/Unity/唐老狮/Unity核心/2D相关/Sprite/SpriteMask精灵遮罩
created: 2024-07-20
更新:
---

---
# 关联知识点

[[SpriteRenderer 精灵渲染器]]

---
# SpriteMask 是什么

- 顾名思义，SpriteMask 是精灵遮量的意思
- 它的主要作用就是对精灵图片产生遮罩
- 制作一些特殊的功能，比如只显示图片的一部分让玩家看到
# 参数讲解
## Sprite

- 遮罩图片
## Alpha Cutoff

- 如果 Alpha 包含透明区域和不透明区域之间的混合（半透明）
- 则可以手动确定所显示区域的分界点（0~1）
## Custom Range

- 自定义遮罩范围开启后可以设置遮量的范围，按照排序层来划分
- 在场景上有不同的遮罩，不同的遮罩控制的图片也是不同的
### Default

- 图片（要渲染的）层级要大于等于 Back 的层级，小于 Front 的层级
	- 才能被遮罩所影响
### New Layer

- 新加的层
- 图片（要渲染的，且图层为 New Layer）层级要在 Back 的层级 与 New Layer 的层级之间
	- 图层为 Default 则只需要大于 Back 的图层便好
### 简而言之

- 大于等于 Back
- 小于等于 Fornt
- 新加的层永远在默认层之上

# 使用 SpriteMask

- 与 SpriteRenderer 精灵渲染器 一同使用
- 先创建一个 遮罩，在其他图片上的 SpriteRenderer 更改遮罩参数
	- 不使用遮罩
	- 在遮罩内部显示
	- 在遮罩外部显示
- 可用于做头像


---