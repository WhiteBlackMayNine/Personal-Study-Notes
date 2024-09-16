---
tags:
  - "#科学/Unity/ARPG/PlaySound/PoolSound"
created: 2024-06-21
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
# 更新

- 脚本挂载于声音预制体上
	- 创建声音预制体
		- 场景上创建一个空物体，添加脚本 Audio Source
		- Assets 上创建一个 Audio Mix
			- 把 Output 上的东西挂载上去
		- 然后就是拖入 Assets 中 创建一个预制体
- 然后把 利用 `AssetsSoundSO` 脚本创建的 文件挂上去

- 枚举 `SoundType`
	- 用来判断声音的类型
	- 在 Inspector 窗口上设置
	- 设置的是什么类型，预制体就用来播放什么类型的声音

- 变量
	- `_audioSource` 用来获取挂载的 AudioSource 脚本
	- `SoundType _type` 外部设置类型
	- `AssestSoundSO _soundAssest` 挂载的 文件（`AssetsSoundSO`）

- 函数
	- `Spawn()` 生成函数
		- 用来播放声音
		- 调用 `PlaySound()` 函数
	- `PlaySound()` 播放函数
		- 利用 `AssetsSoundSO` 脚本中的函数，取出一个声音切片
		- `_audioSource.clip = _soundAssest.GetAudioClip(_type);`
		- 然后使用 `.Play()` 进行播放
		- 最后调用 `StartRecycl()` 回收函数
	- `StartRecycl()` 回收函数
		- 调用计时器，将 销毁函数 `DisableSelf()` 传进去
	- `DisableSelf()` 销毁函数
		- `.Stop()` 停止播放
		- 然后设置为失活

---
# 源代码

![[PoolSound.cs]]

---