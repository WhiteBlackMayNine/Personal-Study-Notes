---
tags:
  - 科学/Unity/唐老狮/Unity核心/3D动画相关/动画IK控制
created: 2024-03-30
课时: "83"
---

---
# 关联知识点



---
# 知识点

## 什么是IK

### 正向动力学

- 在骨骼动画中，构建骨骼的方法被称为正向动力学
- 它的表现形式是，子骨骼（关节）的位置根据父骨骼（关节）的旋转而改变
- 用我们人体举例子
	- 当我们抬起手臂时，是肩部关节带动的整个手臂的运动
	- 用父子骨骼理解的话就是父带动了子
### 反向动力学

- 而IK全称是Inverse Kinematics，翻译过来的意思就是反向动力学的意思
	- 它和正向动力学恰恰相反
- 它的表现形式是，子骨骼（关节）末端的位置改变会带动自己以及自己的父骨骼（关节）旋转
- 用我们人体举例子
	- 当我们拿起一个杯子的时候是用手掌去拿，以杯子为参照物，我们移动杯子的位置，手臂会随着杯子一起移动
	- 用父子骨骼理解的话就是子带动了父
## 如何进行IK控制

 - 在状态机的层级设置中 开启 IK 通道
 - IK Pass 必须勾选
### 继承MonoBehavior的类中
 
 - Unity定义了一个IK回调函数
	 - OnAnimatorIK
 - 我们可以在该函数中调用Unity提供的IK相关API来控制IK
### Animator中的IK相关API

#### 头部

- SetLookAtWeight     设置头部IK权重
	- `animator.SetLookAtWeight(1, 1f, 1f);
- SetLookAtPosition   设置头部IK看向位置
	- 参数为 ： 想要看向的位置
	- `animator.SetLookAtPosition(pos.position);`
#### 四肢

- SetIKPositionWeight 设置IK位置权重
	- `animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1)`
- SetIKRotationWeight 设置IK旋转权重
	- `animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);`
- SetIKPosition       设置IK对应的位置
	- `animator.SetIKPosition(AvatarIKGoal.RightFoot, pos2.position);`
- SetIKRotation       设置IK对应的角度
	- `animator.SetIKPosition(AvatarIKGoal.RightFoot, pos2.position);`
- AvatarIKGoal枚举    四肢末端IK枚举
	- 点运算符调用
### 头部IK参数相关

- weight
	- LookAt全局权重0~1
- bodyWeight
	- LookAt时身体的权重0~1
- headWeight
	- LookAt时头部的权重0~1
- eyesWeight
	- LookAt时眼镜的权重0~1
- clampWeight
	- 0 表示角色运动时不受限制，1 表示角色完全固定无法执行LookAt
	- 0.5 表示只能够移动范围的一半
### 四肢IK参数相关

- 参数一 枚举类型
	- 直接 TAB 补全
	- 然后使用点运算符
- 参数二 权重
	- 根据 API 不同，参数二不同
	- 0 ~ 1
- 参数二 设置的位置 / 四元数
	- 根据 API 不同，参数二不同
## IK反向动力学控制对于我们的意义

- IK在游戏开发中的应用
	- 拾取某一件物品持枪
	- 或持弓箭瞄准某一个对象
	- 等等
## 关于 OnAnimatorIK 和 OnAnimatorMove 两个函数的理解

 - 我们可以简单理解这两个函数是两个和动画相关的特殊生命周期函数
	 - 他们在 Update 之后 LateUpdate 之前调用
	 - 他们会在每帧的状态机和动画处理完后调用
 - OnAnimatorIK 在 OnAnimatorMove 之前调用
	 - OnAnimatorIK 中主要处理 IK 运动相关逻辑
	 - OnAnimatorMove 主要处理 动画移动以修改根运动的回调逻辑
 - 他们存在的目的只是多了一个调用时机，当每帧的动画和状态机逻辑处理完后再调用
### OnAnimatorMove

- 如果动画本身有移动
- 还想再写移动的话
- 建议写在这个函数中
### OnAnimatorIK 

- 动画IK 相关的写在这个函数里面 

---
# 源代码

![[动画IK控制 知识点.cs]]

---
# 练习题

![[动画IK控制 练习题.cs]]

---

