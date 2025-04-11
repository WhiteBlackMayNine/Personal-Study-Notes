---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Unity程序基础框架/pureMVC导入和创建通知名类
created: 2025-03-16
---

---
# 关联知识点



---

下载完毕后
将文件夹内的  Core、Interfaces、Patterns，拖入到 Unity 中

或者
——> VS打开 SIN 生成 dll 包
——> PureMVC bin Debug 将后缀为 dll 的导入

通知名类：
因为 pureMVC 使用的是观察者模式，所以在调用事件时要传入一个字符串
为了方便使用，建议创建一个 通知名类（PureNotification）
主要功能就是用来 声明各个通知的名字，方便使用和管理


---
# 示例代码

```C#
public class PureNotification{

	//常量可以使用 类名.XX 来调用 且必须初始化
	public const string SHOW_PANEL = "showPanel"；

}

```

- ![[Unity MVC 框架（思想）通知名类 PureNotification.cs]]