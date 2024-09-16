---
tags:
  - "#科学/Unity/SiKi/Unity常用API/AudioSource"
created: 2024-04-03
---

---
# 关联知识点



---
# 知识点

- `private AudioSource audioSource ; audioSource = GetComponent<AudioSource>();
- `public AudioClip musicClip;
## 播放

- `audioSource.Play();`
## 设置音乐切片

- `audioSource.clip = musicClip;
## 音量

 - `audioSource.volume = 1;
	 - 1 为最大值
## 何时播放

- `audioSource.time = 3;`
	- 从第三秒开始播放
## 停止

- `audioSource.Stop();
## 静音

- `audioSource.mute = false`
## 暂停

- `audioSource.Pause();
## 恢复

- `audioSource.UnPause();
## 播放一次

- `audioSource.PlayOneShot(clip, 1f);
	- 要播放的音频剪辑  音频剪辑的音量大小
## 在指定位置播放剪辑

- `audioSource.PlayClipAtPoint(clip, Vector3.zero, 1f);
	- 要播放的音频剪辑  音频剪辑播放的位置  音频剪辑的音量大小

---
# 源代码

![[No18_AudioSource.cs]]

---