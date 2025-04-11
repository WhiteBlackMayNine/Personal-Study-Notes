---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Unity程序基础框架/MVX总结
created: 2025-03-16
---

---
# 关联知识点



---
# 主要学什么

- PureMVC是什么
- PureMVC如何获取
- PureMVC的基本结构
# PureMVC 是什么

- 基于 MVC 思想和一些基础设计模式建立的一个轻量级的应用框架
	- 是一个免费开源框架

- 它最初是执行的 ActionScript 3 语言使用的
	- 现在已经移植到几乎所有主流平台
# PureMVC如何获取

- [官方网址](http://puremvc.org)
# PureMVC的基本结构

- ![[Unity MVC 思想框架 PureMVC 基本结构.png]]

- MVC +
	- 代理模式
	- 中介者模式
	- 外观模式
	- 命令模式
	- 观察者模式
	- 单例模式

- Model（数据模型）
	- 关联 Proxy（代理）对象，负责处理数据
- View（界面）
	- 关联 Mediator（中介）对象，负责处理界面
- Controller（业务控制）
	- 管理 Command（命令）对象，负责处理业务逻辑

- Facade（外观）是 MVC 三者的经纪人，统管全局
	- 可以获取代理、中介、命令
- Notification
	- 通知，负责传递信息
# 总结

- PureMVC是什么
	- 基于 MVC 思想的第三方开源框架方网站
- PureMVC如何获取
	- 前往Github获取
- PureMVC的基本结构
	- MVC + Proxy + Mediator + Command+Facade






---
# 源代码

- 

---