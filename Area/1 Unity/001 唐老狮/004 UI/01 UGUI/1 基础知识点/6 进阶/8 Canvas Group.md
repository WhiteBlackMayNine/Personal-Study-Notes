---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-20
---

---
# 关联知识点



---
# Canvas Group

- 为面板父对象（Panel）添加 CanvasGroup 组件 即可整体控制  
	- 整体控制 显示、淡入淡出
	  
- 参数相关：  
	- Alpha
		- 整体透明度控制  
	- Interactable
		- 整体启用禁用设置  
	- Blocks Raycasts
		- 整体射线检测设置  
	- Ignore Parent Groups
		- 是否忽略父级CanvasGroup的作用
			- Panel 下的 小Panel
				- 勾选该参数后，修改 Panel 的 Canvas Group 不会影响 小Panel

---
# 源代码

![[Unity UGUI 进阶 Canvas Group 知识点.cs]]

---