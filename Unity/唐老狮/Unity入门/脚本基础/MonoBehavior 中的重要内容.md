---
tags:
  - 科学/Unity/Unity入门/脚本基础/MonoBehavior中的重要内容
created: 2024-03-23
课时: "22"
来源: https://www.bilibili.com/video/BV1HX4y1V71E?p=13
---

---
# 关联知识点



---
# 知识点

## 重要成员

### 获取依附的Gameobject

- `this.gameobject.name`
### 获取依附的Gameobject的位置信息

- 比如得到位置信息
- `this.transform.position`
### 获取脚本是否激活

- `this.enabled = ture` 激活

## 重要方法

### 得到依附对象上挂载的其它脚本

#### 得到自己挂载的单个脚本
##### 根据脚本名获取

- `lesson l = this.GetComponent("lesson") as lesson;`
#####根据Type获取

- `lesson l = this.GetComponent(typrof(lesson)) as lesson;`
##### 根据泛型获取 

- `lesson l = this.GetComponent<lesson>();`
- 建议使用泛型获取 因为不用二次转换
##### 得到自己挂载的多个脚本

- `lesson[] arry =  this.GetComponent<lesson>;`
- `List<lesson> list = new List<lesson>();this.GetComponent<lesson>(list);`
##### 得到子对象挂载的脚本

- （默认也会找自己身上是否挂载脚本）
- `lesson l = this.GetComponentInChildren<Lesson3 Test>(true);print(l);
	- 注
		- 函数是有一个参数的 默认不传 是false 意思就是 如果子对象失活 是不会去找这个对象上是有某个脚本的
		- 如果传true 即使 失活 也会找
- 得子对象 挂载脚本 单个
	- `Lesson3 t = this.GetComponentInChildren<Lesson3 Test>(true);print(t);
- 得子对象 挂载脚本 多个
	- 方法一
		- `lesson3 Testl] lts = this.GetComponentsInChildren<Lesson3 Test>(true);
	- 方法二
		- `List<Lesson3 Test> list2 = new List<Lesson3 Test>();
		- `this.GetComponentsInChildren<Lesson3 Test>(true, list2);
##### 得到父对象挂载的脚本

- （默认也会找自己身上是否挂载脚本）
- `Lesson3 l = this.GetComponentInParent<Lesson3_Test>();
- 也有`List`方法，此处省略，和上面一个套路
##### 尝试获取脚本

- `Lesson3_Test 13t;
- 提供了一个更加安全的 获取单个脚本的方法 如果得到了 会返回true
- 然后在来进行逻辑处理即可
- `if(this.TryGetComponent<Lesson3_Test>(out 13t)){ 逻辑处理 }`·
- 注意
	- 如果获取失败，就会返回空
- 只要能得到场景中别的对象或者对象所依附的脚本，那就可以获取到所有信息

## `this`

- 我自己
	- 即这个物体本身

---

