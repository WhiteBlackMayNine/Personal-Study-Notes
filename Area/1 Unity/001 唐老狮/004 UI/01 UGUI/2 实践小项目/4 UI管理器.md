---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-26
---

---
# 关联知识点



---
# 明确需求

- 单例模式
- 面板字典
- 显示面板
- 隐藏面板获取面板

- 利用UI管理器
	- 动态添加、删除面板
# 关于 Canvas 下的子对象

- 即 EventSystem 与 UICamera
- 这两个在过场景的时候我们是不希望删除它们的
- 只有保证 Canvas 不被删除
	- 那么 EventSystem 与 UICamera 也就不会被删除
# 作用

- 获取面板
- 显示面板
- 隐藏（删除）面板

- 代码逻辑写在源文件里
- （注释）

---
# 源代码

![[Unity UGUI 实践小项目 UI管理器 UIManager_1.cs]]

---