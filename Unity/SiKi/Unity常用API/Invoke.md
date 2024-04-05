---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Invoke"
created: 2024-04-03
备注: 延时调用方法
---

---
# 关联知识点



---
# 知识点

## 调用

- `Invoke(string methodName, float time);
	- 执行的方法名 延迟时间
	- 只执行一次
- `InvokeRepeating(string methodName, float time, float repeatRate);
	- 执行的方法名 延迟时间 重复间隔时间
	- 一直执行直到被停止
## 停止

- `CancelInvoke(string methodName);
	- 要停止的协程的名字
## 检测

- `IsInvoking()
	- 只要有协程运行就返回 true
- `IsInvoking(string methodName)

---
# 源代码

![[No16_Invoke.cs]]

---