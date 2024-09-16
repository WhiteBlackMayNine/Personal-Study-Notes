---
tags:
  - "#科学/Unity/ARPG/PlaySound/AssestSoundSO"
created: 2024-06-21
---

---
# 关联知识点

[[PoolSound]] [[PoolItemBase]]

---
# 说明

- ScriptableObject
	- 创建一个声音文件，里面包括不同类型的声音切片
- 预制体上会通过 `PoolSound` 脚本 选择类型
	- 播放声音的时候会根据 声音类型 来播放 不同的切片
- ![[AssestSound.png]]
# 变量
## 类 Sounds

- 包括了 声音的类型 与 声音的音频文件
- 用于配置信息
```C#
[System.Serializable]
private class Sounds
{
	public SoundType SoundType;//声音的类型
	public AudioClip[] AudioClip;//音频文件
}
```
## List列表

- `[SerializeField] private List<Sounds> _configSound = new List<Sounds>();`
# 函数

## 得到声音切片 `GetAudioClip`

```C#
public AudioClip GetAudioClip(SoundType type)
{
	if (_configSound.Count == 0)
	{
		return null;
	}

	switch (type)
	{
		//_configSound[0] 代表的是列表中第一个    case后面的类型要和Unity中的顺序一致     
		// Random从声音切片中随机拿一个出来
		case SoundType.ATK:
			return _configSound[0].AudioClip[Random.Range(0, _configSound[0].AudioClip.Length)];
		case SoundType.HIT:
			return _configSound[1].AudioClip[Random.Range(0, _configSound[1].AudioClip.Length)];
		case SoundType.BLOCK:
			return _configSound[2].AudioClip[Random.Range(0, _configSound[2].AudioClip.Length)];
		case SoundType.FOOT:
			return _configSound[3].AudioClip[Random.Range(0, _configSound[3].AudioClip.Length)];
	}
	return null;
}
```
# 更新

- 首先继承 ScriptableObject
- 这个脚本用来创建一个声音资源文件
	- 创建的声音预制体，会挂载脚本 `PoolSound`
	- 这个脚本所创建的声音资源文件 会挂载到 脚本`PoolSound` 上

* 用 List 保存配置信息，List 的类型为 类`Sounds`
	- 类 想要去修改，需要添加特性 `[System.Serializable]` 序列化才行
- 类`Sounds` 和 List 列表 用来配置和保存声音信息
	- 创建一个 AssestSoundSO 文件后
	- 添加对象 选择声音类型 添加声音切片

- 函数 `GetAudioClip`
	- 根据所选择（外部传入）的类型，去返回一个类型中的随记一个声音切片
	- 需要注意的是 `_configSound[2]` 这个List对象的索引
	- 要和外部所配置的类型一致
	- 比如
		- 外部 索引为2 的类型为 ATK
		- 那么这里的 `case` 就也为 ATK

- 这个脚本主要就是生成 ScriptableObject 数据文件
	- 然后挂载到 `PoolSound` 脚本上
	- `PoolSound` 脚本中去获取相应类型的切片，进行播放

---
# 源代码

![[AssestSoundSO.cs]]

---