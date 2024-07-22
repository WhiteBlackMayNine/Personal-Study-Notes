---
tags:
  - "#科学/Unity/ARPG/AssestSoundSO"
created: 2024-06-21
更新:
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





---
# 源代码

![[AssestSoundSO.cs]]

---