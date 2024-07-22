---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Component/Animator"
created: 2024-04-03
---

---
# 关联知识点

唐老狮 Unity 核心 动画基础

---
# 知识点

## 动画播放

- `animator.Play("Run");
## 播放速度

- `animator.speed = 5;
## 设置参数

- `animator.SetFloat("Speed",1);
- `animator.SetBool("Dead",false);
- `animator.SetInteger("HP",100);
- `animaation.SetTrigger("条件名")`
## 淡入淡出

- 以标准单位化时间进行淡入淡出效果来播放动画
	- `animator.CrossFade("Run", 0.5f);`
- 以秒为单位进行淡入淡出效果来播放动画
	- `animator.CrossFadeInFixedTime("Run", 0.5f);`

---
# 源代码

![[No10_Animator.cs]]

---