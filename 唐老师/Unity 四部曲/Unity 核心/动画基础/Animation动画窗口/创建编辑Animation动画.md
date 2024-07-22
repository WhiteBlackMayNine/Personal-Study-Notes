---
tags:
  - 科学/Unity/唐老师/Unity核心/动画基础/Animation动画窗口/创建编辑Animation动画
created: 2024-03-24
课时: "44"
来源: https://www.taikr.com/course/1219/task/40473/show
备注: 了解即可，会做就行
备注 2: 练习题在 代码控制动画 中
---

---
# 关联知识点



---
# 知识点

## 创建动画

- 在场景中选中想要创建动画的对象
- 在Animation窗口中点击创建
- 选择动画文件将要保存到的位置

### 保存动画文件

- Unity会帮助我们完成以下操作
- 创建一个 Animator controller(动画控制器或称之为动画状态机)  资源(新动画系统)
- 将新创建的动画文件添加到Animator controller中
- 为动画对象添加Animator组件
- 为Animator组件关联创建的Animator controller文件
## 窗口上的变化

- ![[创建编辑Animation动画 左侧面板.png]]
- ![[创建编辑Animation动画 右侧面板.png]]
- ![[创建编辑Animation动画 动画文件参数.png]]

## 关键帧模式下编辑动画

- 录制模式下，选择时间点 然后修改变量
## 曲线模式下编辑动画

### 曲线模式

- 控制从某一个值变到另外一个值的时候，中间变化过程是什么样的
- 比如说
	- 是直接瞬间变化
	- 还是先快后慢
## 动画文件界面参数



---
# 源代码

![[创建编辑Animation动画 知识点.cs]]

![[创建编辑动画_参数相关.xmind]]

---
