---
tags:
  - "#科学/Unity/SiKi/Unity常用API/Time"
created: 2024-04-03
---

---
# 关联知识点



---
# 知识点
## 时间间隔

- - 完成上一帧所用的时间间隔
	- `Time.deltaTime
## 特殊时间间隔

- 执行物理或者其他固定帧率更新的时间间隔
	- `Time.fixedDeltaTime
- 经过平滑处理的`Time.deltaTime`的时间
	- `Time.smoothDeltaTime
## 总时间

- 自游戏启动以来的总时间
	- `Time.fixedTime
		- 以物理或者其他固定帧率更新的时间间隔累计计算的
	- `Time.time
		- 游戏开始以来的总时间
## 实际时间

- 游戏开始以来的实际时间
	- `Time.realtimeSinceStartup
## 时间流逝

- 时间流逝的标度，可以用来慢放动作
	- `Time.timeScale
## 自加载上一个关卡以来的时间

- `Time.timeSinceLevelLoad

---
# 源代码

![[No11_Time.cs]]

---