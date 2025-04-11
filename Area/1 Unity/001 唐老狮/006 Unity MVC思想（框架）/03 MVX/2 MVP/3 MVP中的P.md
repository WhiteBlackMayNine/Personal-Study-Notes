---
tags:
  - 科学/Unity/唐老狮/Unity数据持久化/Unity程序基础框架/MVP中的P
created: 2025-03-15
---

---
# 关联知识点



---

Persenter 的作用 就是分离 M 和 V

P 和 C 的功能基本都是相同的，但数据更新需要做额外处理

C 是根据 M 来 更新 V，但 P 是 直接访问 M，然后对 V 进行更新

P 与 V 仍然需要挂载到 Panel 上


---
# 源代码

- ![[Unity MVC 思想（框架）MainPresenter.cs]]
- ![[Unity MVC 思想（框架）RolePresenter.cs]]

---