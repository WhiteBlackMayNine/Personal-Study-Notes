---
tags:
  - "#科学/Unity/SiKi/Unity常用API/SendMessage"
created: 2024-04-03
---

---
# 关联知识点



---
# 知识点

## 仅发送消息给自己

- 以及身上的其他MonoBehaviour对象
	- `gameObject.SendMessage("GetMsg");
	- `SendMessage("GetSrcMsg","Trigger");
		- 传递了一个字符串参数`Trigger
	- `SendMessage("GetTestMsg",SendMessageOptions.DontRequireReceiver);
		- `SendMessageOptions.DontRequireReceiver`作用
			- 即使没有找到接收该消息的游戏对象，也不会引发错误
## 广播消息

- 向下发，所有子对象包括自己
	- `BroadcastMessage("GetMsg");`
## 向上发送消息

- 父对象包含自己
	- `SendMessageUpwards("GetMsg");
## 注意

- `GetMsg` 为一个方法
- 发送消息就是让对象指行这个发送的法

---
# 源代码

![[No9_Message.cs]]

---