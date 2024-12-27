---
tags:
  - "#科学/Unity/ARPG/Manager/GamePoolManager"
created: 2024-06-19
---

---
# 关联知识点



---

# 类PoolItem信息

- 用来配置信息
	- 在里面定义对象池中的属性
		- 比如 名字 对象物体 初始化次数
- `[System.Serializable]`
	- 使得一个类可以被序列化

```C#
    [System.Serializable]
    private class PoolItem
    {
        public string ItemName;
        public GameObject Item;
        public int InitMaxCount;
    }
```

# 变量

- `List<PoolItem> _configPoolItem = new List<PoolItem>();`
	- 配置信息
		- 在 Inspector 窗口中 `[System.Serializable]`
	- 类 PoolItem 需要添加特性
- `Dictionary<string, Queue<GameObject>> _poolItem = new Dictionary<string, Queue<GameObject>>();`
	- 保存信息
		- 会在初始化中进行保存信息
- `GameObject _poolItemParent`
	- 对象池的父对象
	- 可以有多个，分级管理
# 函数

## 初始化 `InitPool`

- 判断列表是否为空
	- 也就是在 Inspector 窗口中是否配置信息
- 对列表中的每一个元素对象进行遍历（第一次循环）
	- 对物体进行初始化（第二次循环）
		- 创建一个物体
		- 设置为失活
		- 设置为 `_poolItemParent` 的子对象（便于管理）
		- 判断字典 `_poolItem` 中是否有这个物体
			- 如果有就加入
			- 如果没有创建一个再加入

```C#
	private void InitPool()
{
	if (_configPoolItem.Count == 0)
	{
		return;
	}

	for (int i = 0; i < _configPoolItem.Count; i++)//对列表中的每一个元素进行遍历
	{
		for (int j = 0; j < _configPoolItem[i].InitMaxCount; j++)//对每一个元素进行初始化
		{
			//创建
			var item = Instantiate(_configPoolItem[i].Item);
			//设置为失活
			item.SetActive(false);
			//设置为子对象
			item.transform.SetParent(_poolItemParent.transform);

			//判断字典中是否有这个物体对象
			if (_poolItem.ContainsKey(_configPoolItem[i].ItemName))
			{
				_poolItem[_configPoolItem[i].ItemName].Enqueue(item);//如果有就添加进去
			}
			else
			{
				//如果没有就先创建再添加
				_poolItem.Add(_configPoolItem[i].ItemName, new Queue<GameObject>());
				_poolItem[_configPoolItem[i].ItemName].Enqueue(item);
			}

		}
	}
}
```

## 调用对象池 `TryGetOnePoolItem` （类型为 void）

- 函数参数
	- 名字 位置 角度（这个可以不写）
- 判断是否有这个名字（即传入的名字）的对象 ，利用 字典 中的 `.ContainsKey()`
	- 如果有，从字典中拿出来
		- 设置位置、角度
		- 最后放回去
	- 如果没有
		- 这里选择报错
		- 也可以像 初始化 那样创建一个

```C#
public void TryGetOnePoolItem(string name,Vector3 position,Quaternion roataion)
{
if (_poolItem.ContainsKey(name))//判断是否有这个名字的对象
{
	//如果有就拿出来
	var item = _poolItem[name].Dequeue();
	item.transform.position = position;
	item.transform.rotation = roataion;
	item.SetActive(true);
	_poolItem[name].Enqueue(item);
}
else
{
	DevelopmentToos.WTF("当前申请的池子可能不存在，申请的池子名为" + name);
}
}
```

## 调用对象池 `TryGetOnePoolItem` （类型为 GameObject）

```C#
public GameObject TryGetOnePoolItem(string name)
{
if (_poolItem.ContainsKey(name))
{
	var item = _poolItem[name].Dequeue();
	item.SetActive(true);
	_poolItem[name].Enqueue(item);

	return item;
}
else
{
	DevelopmentToos.WTF("当前申请的池子可能不存在，申请的池子名为" + name);
}

return null;
}
```
# 生命周期函数

## Start()
- 这里面设置的是 父对象 相关
- 先 new 一个新的对象，然后设置位置
- 如果需要更加细分的话，就是不断 new 新的父对象，然后将子对象设置成相应的父对象下面就行
- 调用初始化函数

```C#
	private void Start()
{
	_poolItemParent = new GameObject("对象池的父对象");
	_poolItemParent.transform.SetParent(this.transform);
	//如果希望更加细分，将不同的子对象分别放在其相应的父对象下
	//按照这个逻辑写下去就行 —— 位置设置父对象SetParent，然后设置为隐藏SetActive(false)
	InitPool();
}
```
# 更新
## 类PoolItem信息 与 `List<PoolItem> _configPoolItem`

- ` List<PoolItem> _configPoolItem` 列表用来存储 类型为 `PoolItem` 的对象
	- 也就是 存储 实例化后的 `PoolItem` 对象
- 而 类 PoolItem 则是用来 定义 实例化对象 的一些信息
	- `string ItemName` 这个对象的名字
	- `GameObject Item` 指向一个物体（在这个项目中，指的是 声音预制体文件）
	- `int InitMaxCount` 初始化最大数量
		- 初始化的时候，会根据这个数量，来创建多少个物体（`Item`）
## `Dictionary _poolItem`

- `List _configPoolItem` 的作用是存储 `PoolItem` 实例化后的对象
- 而 `Dictionary _poolItem` 的作用，则是在初始化对象池时，将 `_configPoolItem` 存储的 实例化对象 创建出来
	- 简单来说，实例化对象 身上有 一个挂载上去的预制体（`Item`），以及一个 初始化最大数量（`InitMaxCount`）
	- 在 初始化对象池 函数中，`Item` 会创建 `InitMaxCount` 个对象 
	- `_poolItem` 就是用来保存这些创建出来的 `Item`
		- 在 `PoolItem` 实例化对象身上还有 `ItemName`
- 调用对象池，在 `_poolItem` 字典里面去调用
## 初始化对象池 `InitPool()`

- 判断 `_configPoolItem.Count` 是否为 0
	- 是否存在实例化对象

- `for (int i = 0; i < _configPoolItem.Count; i++)`
	- 对列表中的每一个元素进行遍历
		- `_configPoolItem.Count` 有几个实例化对象就遍历几次
	- `for (int j = 0; j < _configPoolItem[i].InitMaxCount; j++)`
		- 对每一个元素进行初始化（）
			- `j < _configPoolItem[i].InitMaxCount` 根据 第 `i` 个 实例化对象的 `InitMaxCount` 决定遍历次数
		- `var item = Instantiate(_configPoolItem[i].Item);`
			- 创建 *预制体对象*
		- `item.SetActive(false);`
			- 将创建出来的 *预制体对象* 设置为失活
		- `item.transform.SetParent(_poolItemParent.transform);`
			- 设置为 `_poolItemParent` 的子对象，方便管理
		- `if (_poolItem.ContainsKey(_configPoolItem[i].ItemName))`
			- 判断字典中是否有这个物体对象
			- `_poolItem[_configPoolItem[i].ItemName].Enqueue(item)`
				- 如果有就添加进去
			- 走 `if` 语句的逻辑，基本就是第二次以后了
				- 在 `else` 已经创建了一个 `ItemName` 所对应的 `Queue` 对象了
				- 直接调用 `Enqueue` 添加创建出来的 *预制体对象* 就行
		- `else` 创建一个 *预制体对象* 第一次都会先走 `else` 而不是 `if`
			- `_poolItem.Add(_configPoolItem[i].ItemName, new Queue<GameObject>());`
				- 先创建再添加
				- 键为 `_configPoolItem[i].ItemName` 
					- 实例化对象上的 `ItemName`
				- 值为 ` new Queue<GameObject>()`
					- `new` 一个 类型为 `GameObject` 的 队列`Queue` 对象
			- `_poolItem[_configPoolItem[i].ItemName].Enqueue(item);`
				- 通过索引 `i` 调用 `_configPoolItem[i]` 的 `ItemName` 从而得到对应的 **值**
					- ——> 说人话：`ItemName` 是 **字典的键对象**，通过键得到 **值对象**
					- ——> 值对象 是一个 `GameObject`类型的 队列 `Queue`对象
					- ——> 值对象 也就可以调用 `.Enqueue` 添加 `item` 创建出来的**预制体对象**
## 调用对象池

- 有两个重载
- 第一个返回值类型为 `void`，在函数内部就会设置 调用的对象 的位置
- 第二个返回值类型为 `GameObject`，函数内部不会设置 调用的对象 的位置，而是直接返回出去

- 第一个
	- `_poolItem.ContainsKey(name)` 判断是否有这个名字的对象（字典中有没有这个 *键值对*）
	- 如果有
		- 就拿出来 `var item = _poolItem[name].Dequeue()`
		- 并且设置其 `position` `rotation`
		- 设置为 激活状态 `item.SetActive(true)`
		- 最后 放回 队列中 `_poolItem[name].Enqueue(item)`
- 第二个
	- 跟第一个类似，判断是否有这个名字的对象（字典中有没有这个 *键值对*）
	- 如果有
		- 就拿出来 `var item = _poolItem[name].Dequeue()`
		- 设置为激活
		- 然后 放回 队列中 `_poolItem[name].Enqueue(item)`
		- 最后 返回出去 `return item`
## 声音的实现

- 对象池被调用时会被设置为 激活
- 创建出来的 预制体对象 上挂载的 [[PoolSound]] 脚本上
- 当设置为激活时，在 `OnEnable` 中调用 `Spawn` 函数
- `Spawn`函数中又去调用了 `PlaySound` 函数
- 去播放声音切片并调用 `StartRecycl` 回收函数
- 调用 [[GameTimerManager]] 一个计时器，在 0.3s 后执行`DisableSelf` 销毁函数
- 停止播放，并且将自身设置为 失活
- 这样，一次声音也就播放完毕了

---
# 源代码

![[GamePoolManager.cs]]

---