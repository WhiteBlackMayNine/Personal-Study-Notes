---
tags:
  - 科学/Unity/唐老狮/Unity入门/重要组件和API/最小单位Gameobject
created: 2024-03-23
课时: 25 26 27
来源 - 25: https://www.bilibili.com/video/BV1HX4y1V71E?p=16
来源 - 26: https://www.bilibili.com/video/BV1HX4y1V71E?p=17
来源 - 27: https://www.bilibili.com/video/BV1HX4y1V71E?p=18
---

---
# 关联知识点

[[7 静态成员]]

---
# 知识点

## [[2 成员变量和访问修饰符|成员变量]]

### 名字`.name`

- 可以二次修改
### 是否激活`.activeSelf

- 类型是一个布尔值
### 是否静态`.isStatic

- 类型是一个布尔值
### 层级`.layer

- 类型为`int
### 标记`.tag

- 类型为`string
### Transform

- `this.gameObject.transform.position `作用等价于`this.transform`（推荐后者）

## 静态方法

### 创建自带几何体

- `GameObject.CreatePrimitive();`
### 可以使用`GameObject`得到依附的信息
### 查找对象

#### 查找单个对象

##### 通过对象名

- （效率较为低下）
- 没有找到就会返回空（null）
- `GameObject i = GameObject.Find("Text");`
##### 通过标签名

- `GameObject m = GameObject.FindWithTag("Text");`
##### 注意

- 无法找到失活的对象
- 如果场景中有多个符合条件的对象，无法确定找到的是谁
### 查找多个对象

- 只能使用标签名
	- `GameObject[] objects = GameObject.FindGameObjectsWithTag ("Text");`
	- `objects.Lenth`个数
- 注
	- 无法找到失活对象
- **其余方法效率低
### 实例化对象

#### 作用

- 根据一个 Gameobject 对象创建出一个一模一样的对象
- 关联预设体动态创建对象
#### 方法

- 准备用来可以的对象
	- 可以是场景上的对象，也可以是预设体对象
	- `public GameObject obj`
- 进行实例化 ^c8bf0d
	- `GameObject.Instantiate(obj);
### 删除

#### 删除对象

- `GameObject.Destroy(obj, 5);`第二个参数为延迟时间，单位为秒
#### 删除脚本

- `GameObject.Destroy(obj.gameobject);`
#### 切场景不移除

- 默认情况下，切换场景后，会移除场景上所有对象
- `GameObject.DontDestroyOnLoad(obj);`
#### 注意

- 不会马上移除对象，只是加了一个移除标识
- 一般情况下，会在下一帧移除对象并从内存移除
- 切换场景后，会移除
- 异步减低卡顿概率

## [[3 成员方法]]

### 创建空物体

- new 一个 Gameobject 就是在创建一个空物体
- `GameObject obj = new GameObject("TEXT");`
	- TEXT为文本内容，可以为空（引号需要删除）
### 为对象添加脚本

- `obj.AddComponent<Text>();`
	- Text 为脚本名字
	- 通过返回值可以得到加入的脚本信息，来进行一些处理
- **优先使用泛型
### 得到脚本

- 与继承 Mono 的类得到脚本的方法一样
### 标签比较

- `this.gameObject.CompareTag("Text");`
	- 会返回一个布尔值，可以使用 if 语句进行判断
### 设置激活失活

- 失活 false ，激活 ture
	- `obj.SetActive(false);`
### 当前游戏物体的状态

- `gameobject.activeInHierarchy
- `gameobject.activeSelf`


## 备注

- 如果设置的对象为挂载脚本的对象，那么可以不获取对象
- 直接使用 `gameObject.`的方法
	- 如`gameObject.SetActive(true)`
	- 也可以加上 `this`

---
