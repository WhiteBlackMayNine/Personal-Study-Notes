---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-12
---

---
# 关联知识点



---
# RawImage是什么

- RawImage是原始图像组件
- 是UGUI中用于显示任何纹理图片的关键组件

- 它和 Image 的区别是
	- 一般 RawImage 用于显示大图（背景图，不需要打入图集的图片，网络下载的图等等）
# RawIamge参数

- Texture
	- 图像纹理
	- 可以关联任何形式的图片

- UV Rect
	- 图像在UI矩形内的偏移和大小
	- 位置偏移 X 和 Y（取值0~1）
	- 大小偏移 W 和 H（取值0~1）
	- 改变他们图像边缘将进行拉伸来填充UV矩形周围的空间
# 代码控制

```C#
RawImage raw = this.GetComponent<RawImage>();  
raw.texture = Resources.Load<Texture>("ui_TY_lvseshuzi_08");  
raw.uvRect = new Rect(0, 0, 1, 1);
```

---
# 源代码

![[Unity UGUI 三大基础控件 RawImage 原始图像控件.cs]]

---