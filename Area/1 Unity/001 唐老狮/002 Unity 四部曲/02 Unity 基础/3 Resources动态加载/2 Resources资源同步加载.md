---
tags:
  - 科学/Unity/唐老狮/Unity基础/Resources资源动态加载/Resources资源同步加载
created: 2024-03-23
课时: "32"
来源: https://www.taikr.com/course/1190/task/38811/show
---

---
# 关联知识点


---
# 知识点

## `Resources`资源动态加载的作用

- 通过代码动态加载`Resources`文件夹下指定路径资源
- 避免繁琐的拖曳操作
## 常用资源类型

### 预设体对象

- `Gameobject`[[最小单位 Gameobject]]
### 音效文件

- `Audioclip
### 文本文件

- `TextAsset
### 图片文件

- `Texture
### 其它类型

- 需要什么用什么类型
### 注意

- 预设体对象加载需实例化
- 其它资源加载一般直接用
	- 存储数据时需要对应类型
## 资源同步加载 普通方法

### 预设体对象

- **想要创建在场景上 记住实例化
- 第一步
	- 要去加载预设体的资源文件
		- 本质上就是加载配置数据在内存中
	- `Object = obj = Resources.Load("预设体名字")`
		- 预设体需要在`Resources`文件夹下
- 第二步
	- 如果想要在场景上 创建预设体 一定是加载配置文件过后 然后**实例化**[[最小单位 Gameobject#^c8bf0d]]
		- `Instantiate(obj);
### 音效资源

- 第一步：加载音乐文件数据
	- `Resources.Load("Music(文件夹名)/BKMusic(音乐文件名)")
		- 有多个文件夹时，使用`/` 隔开
- 第二步
- 使用数据，不需要实例化音效切片，只需要把数据赋值到正确的脚本上即可
	- `Object obj3 = Resources.Load("Music/BKMusic"); audios.clip = obj3 as Audioclip;audios.Play();`
		- `audios` 为声明的音效组件的变量
		- `audios.Play();` 播放音乐
### 文本资源

- 文本资源支持的格式
	- `.txt .xml .bytes .json .html .csv 其他`**前四种常用
- 第一步
	- 加载文本文件数据
		- 使用 Unity 提供的类型存储
	- `TextAsset ta = Resources.Load("Txt/Test") as TextAsset;
- 第二步
	- 获取文本内容
		- `ta.text`
	- 获取字节数据数组
		- `ta.bytes`
### 图片

- 第一步
	- 加载图片数据
		- `Texture t = Resources.Load("Tex/TestJPG") as Texture;
- 第二步
	- 使用GUI
		- `private void OnGUI(){ GUI.DrawTexture(new Rect(0,0,100，100)，tex);}
			- 参数一
				- `new Rect(0,0,100,100)`  起始坐标 ( 0 , 0 )
				- 宽度 100 ，高度 100
			- 参数二
				- 图片类型变量
### 其他类型

- 需要什么类型，就用什么类型
- 比如
	- 材质 - `Material`
### 资源同名

- `Resources.Load`加载同名资源时 无法准确加载出你想要的内容
- 可以使用另外的API
	- 加载指定类型的资源
		- `tex= Resources.Load("Tex/TestJPG",typeof(Texture)) as Texture;
			- 参数一
				- 路径
			- 参数二
				- 类型
	- 加载指定名字的所有资源1
		- `object[]objs = Resources.LoadAll("Tex/TestJPG");foreach(object item in objs){ }`
		- 很少使用，性能消耗过大
## 资源同步加载 泛型方法

- `TextAsset ta2 = Resources.Load<TextAsset>("Tex/TestJPG");
	- 确定资源类型，且不需要使用`as`转换
	- `< >` 内写存储类型

---
# 总结

- `Resources`动态加载资源的方法
- 让拓展性更强
- 相对拖曳来说 它更加一劳永逸 更加方便
- 重要知识点
	- 记住API
	- 记住一些特定的格式
	- 预设体加载出来一定要实例化
	- 资源加载出来后，必须使用，否则就是无意义

---
# 源代码

![[Resources同步加载.cs]]

---
# 练习题

![[Resources同步加载 练习题.cs]]

---



