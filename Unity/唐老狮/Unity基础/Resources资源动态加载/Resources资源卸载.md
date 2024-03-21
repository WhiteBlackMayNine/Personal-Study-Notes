
[[特殊文件夹]]
[[Resources资源异步加载]]
[[Resources资源同步加载]]

课时36

- `Resources`重复加载资源会浪费内存吗
	- 其实`Resources`加载一次资源过后
	- 该资源就一直存放在内存中作为缓存
	- 第二次加载时发现缓存中存在该资源
	- 会直接取出来进行使用
	- 所以 多次重复加载不会浪费内存
	- 但是 会浪费性能（每次加载都会去查找取出，始终伴随一些性能消耗）
- 如何手动释放掉缓存中的资源
	- 卸载指定资源
		- `Resources.UnloadAsset` 方法
		- 注意
			- 该方法 不能释放 `Gameobject` 对象 [[最小单位 Gameobject]]
				- 因为它会用于实例化对象
			- 它只能用于一些 不需要实例化的内容 比如 图片 和 音效 文本等等
			- 一般情况下 我们很少单独使用它
	- `Gameobject obj= Resources.Load<Gameobject>("cube"); Resources.UnloadAsset(obj);
		- 即使是没有实例化的 Gameobject 对象也不能进行卸载
	- 卸载未使用的资源
		- `Resources.UnloadUnusedAssets(); GC.Collect();
		- 注意
			- 一般在过场景时和 Gc[[构造、析构、垃圾回收机制]] 一起使用
			- 或者内存达到瓶颈时

- 知识点
	- ![[Resources资源卸载 知识点.cs]]

- 练习题
	- 