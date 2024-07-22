---
tags:
  - 科学/Unity/Unity入门/重要组件和API/屏幕相关Screen
created: 2024-03-23
课时: "45"
来源: https://www.bilibili.com/video/BV1HX4y1V71E?p=28
---

---
# 关联知识点

这里写入与本知识点有关联的、相似的知识点，以供复习参考

---
# 知识点

## 静态属性

### 常用

#### 当前屏幕分辨率

- `Screen.currentResolution`
- 获取宽和高
	- `Resolution r = Screen.currentResolution`
	- `r.width`与`r.height`
	- 得到的是设备屏幕的宽高
#### 屏幕窗口当前宽高

- `Screen.width` 与 `Screen.height`
- 得到的时 game  窗口的宽高
- 写代码做计算用这个
#### 屏幕休眠模式

- `Screen.sleepTimeout = SleepTimeout.NeverSleep;`永不息屏
- `Screen.sleepTimeout = SleepTimeout.SystemSetting;` 随系统设定
### 不常用

#### 运行时是否全屏

- `Screen.fullScreen = ture;`
#### 窗口模式

##### 独占全屏
	- `Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;`
##### 全屏窗口
	- `Screen.fullScreenMode = FullScreenMode.FullScreenWindow;`
##### 最大化窗口
	- `Screen.fullScreenMode = FullScreenMode.MaximizedWindow`
##### 窗口模式
	- `Screen.fullScreenMode = FullScreenMode.Windowed;`
#### 移动设备屏幕转向相关

##### 允许自动旋转为左横向，Home 键在左
	- `Screen.autorotateToLandscapeLeft = true;`
##### 允许自动旋转为右横向，Home 键在右
	- `Screen.autorotateToLandscapeRight = true;`
##### 允许自动旋转为纵向，Home 键在下
	- `Screen.autorotateToPortrait = true;`
##### 允许自动旋转为纵向到放，Home 键在上
	- `Screen.autorotateToPortraitUpsideDown = true;`
##### 指定屏幕显示方向
	- `Screen.orientation = ScreenOrientation.Text`
	- AutoRotatio 四面八方都可以， Portrait 竖屏，  Landscape 横屏
	- LandscapeLeft / LandscapeRight 横屏 左 / 右
	- PortraitUpsideDown 竖屏倒放
#### 屏幕转型相关图片

- ![[屏幕方向.png]]

## 静态方法

### 设置分配率（一般移动设备不使用）

- 参数一**宽**，参数二**高**，参数三**是否全屏**
- `Screen.SetResolution(1920, 1080, true);`

---
