---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Coroutine"
created: 2024-04-03
---

---
# 关联知识点

[[协同程序]] [[协同程序原理]]

---
# 知识点

## 协程的启动
### 直接启动

- `StartCoroutine("ChangeState");
- `StartCoroutine(ChangeState());
### 间接启动

- `IEnumerator ie = ChangeState();
- `StartCoroutine(ie);
## 协程的停止

- `StopCoroutine("ChangeState");
- `StopCoroutine(ChangeState());
- `StopCoroutine(ie);
	- 停止协程ie
- `StopAllCoroutines();
	- 停止所有协程
## `yield return`

- 看唐老狮的

---
# 源代码

![[No15_Coroutine.cs]]

---