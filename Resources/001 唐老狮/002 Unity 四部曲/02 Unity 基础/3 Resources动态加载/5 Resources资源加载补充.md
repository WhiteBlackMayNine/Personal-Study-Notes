---
tags:
  - 科学/Unity/唐老狮/Unity基础/Resources资源动态加载/Resources资源加载补充
created: 2024-10-04
---

---
# 关联知识点



---
## Resource
### 文件夹可以有多个 

- 在一个工程当中` Resources`文件夹 可以有多个 
	- 通过API加载时 它会自己去这些同名的`Resources`文件夹中去找资源
	- 打包时 `Resources` 文件夹 里的内容 都会打包在一起
	- [[1 特殊文件夹]] [[2 Resources资源同步加载]]
### 同一资源

- [[2 Resources资源同步加载]] 同一资源
	- 加载同一资源，不会造成内存浪费，但会造成性能浪费
	- 当`Resources.Load` 发现是同一个资源后，就不会在加载了
	- 加载过一次后，就会存在内存中
	- Unity 中有一个缓存区，会先在缓存区里找，没找到才会进行加载并存在缓冲区中
### 内存卡顿

- [[2 Resources资源同步加载]] 内存卡顿
	- 把硬盘上的一个资源的数据加载到内存当中来存储
	- 就一定把这个资源所有内容加载到内存**过后才会继续执行
		- 使用时直接拿来使用
	- 这个过程可能会大于 16.66ms （60帧），便会造成卡顿
### 原理

- [[3 Resources资源异步加载]]原理 ^8ede27
	- 在执行这个异步加载的这一瞬间，会新开一个线程
	- 把要加载的资源告诉这个线程，这个线程有自己的一套逻辑，专门加载这个资源
	- 新开线程加载完毕，会把资源放在一个公共的内存区
	- 主线程只要检测到资源加载完毕，就会把资源拿出来使用
	- 缺点
		- 开始的一瞬间无法使用资源，必须等待加载完成
### 易错点

- [[3 Resources资源异步加载]] 易错点
	- 代码
		- `ResourceRequest rg = Resources.LoadAsync<Texture>("Tex/TestJPG");`
		- `rq.completed += Loadover;`
		- `rg.asset`
	- 这样写时错的
		-  刚刚执行了异步加载的 执行代码 资源还没有加载完毕 这样用 是不对的
		- 一定要等加载结束过后 才能使用



---
# 源代码



---