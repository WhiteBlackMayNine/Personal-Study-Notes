---
tags:
  - "#科学/Unity/ARPG/PoolSound"
created: 2024-06-21
更新:
---

---
# 关联知识点

[[PoolItemBase]] [[AssestSoundSO]]

---
# 说明

- 这个函数继承 `PoolItemBase`
- 在这里实现声音的播放
- 这个脚本应当挂载到 声音预制体上
	- 在一个空物体上 添加 AudioSource
	- ![[PoolItemSound_声音预制体1.png]]
- 声音混合器
	- ![[PoolItemSound_声音切片.png]]
- GamePoolManage信息配置
	- ![[PoolItemSound_GamePoolManag.png]]
# 枚举 `SoundType`

- 里面包含了多个声音的类型
	- 根据类型不同，播放不同的声音
```C#
public enum SoundType//声音类型，为了更好的管理多种声音
{
    ATK,//攻击
    HIT,//受击
    BLOCK,//格挡
    FOOT//脚步声
}
```
# 变量

- `AudioSource _audioSource;`
- `SoundType _type
- `AssestSoundSO _soundAssest
# 函数

## 播放声音 `PlaySound`

- 根据不同的声音类型来播放不同的声音
```C#
private void PlaySound()
{
	_audioSource.clip = _soundAssest.GetAudioClip(_type);
	_audioSource.Play();
	StartRecycl();
}
```
## 生成函数 `Spawn`

```C#
public override void Spawn()
{
	//当自身被激活时，播放声音
	//播放声音后会开始倒计时，0.3s 后自身会被隐藏
	PlaySound();
}
```
## 回收函数 `StartRecycl`

```C#
private void StartRecycl() // 回收函数
{
	GameTimeManager.MainInstance.TryGetTimer(0.3f, DisableSelf);
}
```
## 自我销毁 `DisableSelf()`

```C#
private void DisableSelf()
{
	_audioSource.Stop();
	this.gameObject.SetActive(false);//隐藏掉
}
```
# 生命周期函数

## `Awake()`

```C#
private void Awake()
{
	_audioSource = GetComponent<AudioSource>();
}
```

---
# 源代码

![[PoolSound.cs]]

---