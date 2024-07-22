---
tags:
  - 科学/Unity/Unity基础/Resources资源动态加载/Resources资源异步加载
created: 2024-03-23
课时: "34"
来源: https://www.taikr.com/course/1190/task/38833/show
---

---
# 关联知识点

[[特殊文件夹]] [[Resources资源卸载]] [[Resources资源同步加载]]

---
# 知识点

## `Resources`异步加载是什么

- [[Resources资源同步加载]]中
- 如果我们加载过大的资源可能会造成程序卡顿
- 卡顿的原因就是 从硬盘上把数据读取到内存中 是需要进行计算的
- 越大的资源耗时越长，就会造成掉帧卡顿
- `Resources`异步加载 就是内部新开一个线程进行资源加载 不会造成主线程卡顿
## `Resources`异步加载方法

### 通过异步加载中的完成事件监听 使用加载的资源

- `ResourceRequest rq = Resources.LoadAsync<类型>(路径);`
- 利用基类（`Asyncoperation`）的一个成员事件来监听是否加载完毕
	- `rq.completed += LoadOver;`
	- `public void LoadOver( Asyncoperation rq) { print("加载完毕"); tex =(rg as ResourceRequest).asset as Texture}
		- `asset` 是资源对象 加载完毕过后 就能够得到它
- 使用
	- `private void OnGUI(){ if( tex != null ){ GUI.DrawTexture(new Rect(0,0,100,100),tex); } }`
- 总结
	- 这句代码 你可以理解 unity 在内部 就会去开一个线程进行资源下载
	- 马上进行一个 资源下载结束 的一个事件函数监听
### 通过协程 使用加载的资源

- 需要提前写一个协程
	- `IEnumerator Load(){ }
- 在协程内部写
	- `ResourceRequest rg = Resources.LoadAsync<Texture>("Tex/TestJPG");`
	- `yield return rg;`
		- Unity 自己知道 该返回值 意味着在异步加载资源
		- Unity 会自己判断 该资源是否加载完毕了 加载完毕过后 才会继续执行后面的代码 
	- `tex = rg.asset as Texture`
- 在 `Updeta` 中写
	- `Startcoroutine(Load());`
		- 开启线程
- 其他
	- `rg.isDone` 是否加载接受
	- `rg.priorite` 当前进度
		- 该进度不会特别精准，过渡也不会过于明显
		- 范围为 0 ~ 1
### 注意

- 异步加载 不能马上得到加载的资源 至少要等一帧

---
# 总结

- 完成事件监听异步加载
	- 好处
		- 写法简单
	- 坏处
		- 只能在资源加载结束后 进行处理
	- ”线性加载“
- 协程异步加载
	- 好处
		- 可以在协程中处理复杂逻辑，比如同时加载多个资源，比如进度条更新
	- 坏处
		- 写法稍麻烦
	- “并行加载”
- 注意
	- 理解为什么异步加载不能马上加载结束，为什么至少要等1帧
	- 理解协程异步加载的[[Unity 知识点#^8ede27|原理]]

---
# 源代码

![[Resources资源异步加载 知识点.cs]]

---
# 练习题

![[Resources异步加载 练习题.cs]]

---



