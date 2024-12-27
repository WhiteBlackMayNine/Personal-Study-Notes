---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-12
---

---
# 关联知识点



---
# Image是什么

- Image是图像组件  
- 是UGUI中用于显示精灵图片的关键组件  
- 除了背景图等大图，一般都使用Image来显示UI中的图片元素
# Image参数

- Source lmage
	- 图片来源（图片类型必须是”精灵“类型）
- Color
	- 图像的颜色
- Material
	- 图像的材质（一般不修改，会使用UI的默认材质）
- Raycast Target
	- 是否作为射线检测的目标（**如果不勾选将不会响应射线检测**）
- Maskable
	- 是否能被遮罩（之后结合遮罩相关知识点进行讲解）
- Image Type
	- 图片类型
		- Simple
			- 普通模式，均匀缩放整个图片
		- Sliced
			- 切片模式，九宫格拉伸，只拉伸中央十字区域
				- 选择后，需要到 图片 Sprite 中，去设置图片的九宫格
			- 一般 面板背景图 会选择该模式 
		- Tiled
			- 平铺模式，重复平铺中央部分
			- 面板的背景纹路
		- Filled
			- 填充模式
				- 参数
					- Fill Method
						- 填充方式
					- Fill Origin
						- 填充原点
					- Fill Amount
						- 填充量
					- Clockwise
						- 顺时针方向
					- Preserve Aspect
						- 保持宽高比
			- 可以制作 进度条、血量条等
- Use Sprite Mesh
	- 使用精灵网格，勾选的话Unity会帮我们生成图片网格
	- UI 元素一般不会勾选
- Preserve Aspect
	- 确保图像保持其现有尺寸
- Set Native size
	- 设置为图片资源的原始大小
	- 会根据 Canvas Scaler 的三种不同渲染模式，选择不同的算法进行计算
# 代码控制

- 使用 UI 的组件，需要引用命名空间
	- `using UnityEngine.UI;`

```C#
//获取组件
Image img = this.GetComponent<Image>();  
//.sprite 设置图片
img.sprite = Resources.Load<Sprite>("ui_TY_fanhui_01");  
//设置大小尺寸
(transform as RectTransform).sizeDelta = new Vector2(200, 200);  

img.raycastTarget = false;  
  
img.color = Color.red;
```
# 补充

- 越后创建的 Image，渲染时会渲染在前面
- 或者说
	- 在 Hierarchy 窗口中，越在下面的 Image，渲染时越在前面

---
# 源代码

![[Unity UGUI 三大基础控件 Image 图像控件 知识点.cs]]

---