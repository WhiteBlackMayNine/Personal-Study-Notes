---
tags:
  - "#科学/Unity/ARPG/GamePoolManager"
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
---
# 源代码

![[GamePoolManager.cs]]

---