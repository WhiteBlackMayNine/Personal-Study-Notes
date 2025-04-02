---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Unity程序基础框架/MVVM的概念
created: 2025-03-16
---

---
# 关联知识点



---
# MVVM的概念

- 全称为模型（Model）—— 视图（View）—— 视图模型（ViewModel）
	- Model
		- 提供数据
	- View
		- 负责界面
	- ViewModel
		- 负责逻辑的处理

- MVVM 的由来是 MVP（Model — View — Presenter）模式与 WPF 结合
	- 发展演变过来的一种新型框架
# MVVM 和 MVP 两者对比

- ![[Unity MVC 思想框架 MVVM和 MVP 的对比.png]]
# 什么是数据绑定

- 将一个用户界面元素（控件）的属性，绑定到 一个类型（对象）实例上的某个属性的方法
- 如果开发者有一个 MainViewMode 类型的实例
	- 那么他就可以把 MainViewMode 的 “Lev” 属性绑定到一个 UI 中 Text 的 “Text” 属性上
- “绑定” 了这两个属性之后，对 Text 的 Text 属性的更改将 “传播” 到 MainViewMode 的 Lev 属性
	- 而对 MainViewMode 的 Lev 属性的更改同样会 “传播” 到 Text 的 Text 属性

- ![[Unity MVC 思想框架 MVVM 数据绑定.png]]
# MVVM 在 Unity 中水土不服

- View 对象始终由我们来书写，并没有 UI 配置文件（如WPF中的XAML）的存在
- 硬要在 Unity 中实现 MVVM，需要写三模块，并且还要对 V 和 VM 进行数据绑定，工作量大，好处也不够明显
# Unity 的第三方MVVM框架

- Loxodon Framework
	- [Loxodon Framework](https://github.com/vovgou/loxodon-framework)

- uMVVM
	- [uMVVM](https://github.com/MEyes/uMVVM)
# MVVM 粗暴变式 —— MP

- MVVM 中的关键是 V 和 VM 的数据双向绑定，即改变 V 或者 VM 对方的属性，对方也会随之变化
	- 一切对外的的处理都通过 VM 来处理了，V 只负责更新和显示

- 那么尝试将他们合二为一，并且达到将界面和逻辑某种意义上的解耦即可

---
# 源代码

- 

---