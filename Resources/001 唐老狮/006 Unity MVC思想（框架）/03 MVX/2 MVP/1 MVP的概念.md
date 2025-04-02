---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Unity程序基础框架/MVP的概念
created: 2025-03-15
---

---
# 关联知识点



---
# MVP的概念

- 全称为模型（Model）—— 视图（View）—— 主持人（Presenter）
- Model 提供数据，View 负责界面，Presenter 负责逻辑的处理
- 它是MVC的一种变式，是针对MVC中 M 和 V 存在耦合的优化
# MVP和MVC的对比

- MVP与MVC有着一个重大的区别
	- 在 MVC 中 View 会直接从 Model 中读取数据，而不是通过 Controller

- 而在MVP中 View 并不直接使用Model，它们之间的通信是通过 Presenter 来进行的
	- 所有的交互都发生在 Presenter 内部

- ![[Unity MVC 框架思想 如何理解MVC中V会从M中读取数据.png]]
# MVP 的作用
- ![[Unity MVC 框架思想 MVP的作用.png]]
- MVP中的 Presenter（主持人）将完全断绝 View 和 Mode l的来往
- 主要程序逻辑都在 Presenter 中实现





---