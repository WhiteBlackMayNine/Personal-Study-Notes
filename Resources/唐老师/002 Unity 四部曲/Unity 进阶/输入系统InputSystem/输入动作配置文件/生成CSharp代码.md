---
tags:
  - 科学/Unity/唐老狮/Unity进阶/输入系统InputSystem/输入动作配置文件/生成CSharp代码
created: 2024-07-31
更新:
---

---
# 关联知识点



---
# 根据配置文件生成CSharp代码

- 选择 InputAction 文件
- 在 Inspector 窗口设置生成路径、类名、命名空间
	- 勾选 Generate C# Class
		- C# Class File
			- 文件放在哪 以及 文件名（脚本名）
		- C# Class Name
			- 类名
		- C# Class Namespace
			- 命名空间
- 应用后，生成代码
	- 代码是根据 JSON 文件进行解析的
# 使用CSharp代码进行监听
## 创建生成的代码对象

```C#
LessonInput ip = new LessonInput();//LessonInput 为生成代码时的类名
```
## 激活输入

```C#
ip.Enable();
```
## 事件监听

```C#
ip.Action1.Fire.started += (context) => {   };
//对象名 - Action Maps 的名字 - Actions 的名字 - 对应事件
```
## 代码示例

```C#
LessonInput ip;

void Start(){
	ip = new LessonInput();
	ip.Enable();
	ip.Action1.Fire.started += (context) => {   };
}

void Update(){
	print(ip.Action1.Move.ReadValue<Vector2>)());
	ip.Action1.Move.started += (context) => {   };
}
```

---
