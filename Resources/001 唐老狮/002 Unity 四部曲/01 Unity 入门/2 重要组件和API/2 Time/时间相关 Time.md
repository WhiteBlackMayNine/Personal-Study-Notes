---
tags:
  - 科学/Unity/唐老狮/Unity入门/重要组件和API/时间相关Time
created: 2024-03-23
课时: "29"
来源: https://www.bilibili.com/video/BV1HX4y1V71E?p=19
---

---
# 关联知识点

[[2 生命周期函数]]

---
# 知识点

## 时间缩放比例

- `Time.timeScale = 0;`时间静止
- `Time.timeScale = NUM;`NUM为数字（倍数）

## 帧间隔时间

### 概念
- 最近的一帧所用的时间（单位为秒）
### 作用
- 主要用于计算位移
- 路程 = 时间 * 速度
### 受 Scale 影响
- 希望游戏暂停时就不动的，受时间缩放比例影响的
- `Time.deletaTime`
### 不受 Scale 影响
- `Time.unscaleDeletaTime`

## 游戏开始到现在的时间

### 作用

- 一般用于计时
### 受 Scale 影响

- `Time.time`
### 不受 Scale 影响

- `Time.unscaleTime`

## 物理帧间隔时间

### 注

- 一般写在 FixedUpdate 内
### 受 Scale 影响

- `Time.fixedDeletaTime`
### 不受 Scale 影响

- `Time.fixUnscaleDeletaTime`

## 帧数

- `Time.frameCount`

---


---


