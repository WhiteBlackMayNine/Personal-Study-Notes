---
tags:
  - 科学/Unity/Unity基础/画线功能LineRenderer
created: 2024-03-23
课时: "39"
来源: https://www.taikr.com/course/1190/task/38846/show
---

---
# 关联知识点



---
# 知识点

## LineRenderer 是什么

- Unity 提供的一个用于画线的组件
- 使用它我们可以在场景中绘制线段
- 一般可以用于
	- 绘制攻击范围
	- 武器红外线
	- 辅助功能
	- 其它画线功能
## 参数相关

- 需要先添加组件
### 重要参数

- Loop
	- 是否终点起始自动相连
- Positions
	- 线段的点
	- 线段宽度曲线调整
- Color
	- 颜色变化
- Corner Vertices
	- 角顶点，圆角
	- 此属性指示在一条线中绘制角时使用了多少额外的顶点
	- 增加此值，使线角看起来更圆
- End Cap Vertices
	- 终端顶点，圆角
	- 终点圆角
- Use World space
	- 是否使用世界坐标系
	- 取消勾选，会跟着物体运动
- Materials
	- 线使用的材质球
### 不重要参数

- Alignment
	- 对其方式
- Texture Mode
	- 纹理模式
- Lighting
	- 光照影响
- Cast Shadows
	- 是否开启阴影
- Receive Shadows
	- 接收阴影
- Probes 光照探针
	- Light Probes 光探测器模式
		- 不使用光探针
		- 使用内插光探针
		- 使用三维网格内插光探针
		- 自定义从材质决定
	- Reflection Probes 反射探测器模式
		- 不使用反射探针
		- 启用混合反射探针
		- 启用混合反射探针并且和天空和混合
		- 启用普通探针，重叠式不混合
- Additional Settings 附加设置
	- Motion Vectors 运动矢量
		- 使用相机运动来跟踪运动
		- 对特定对象来跟踪运动
		- 不跟踪
	- Dynamic Occludee 
		- 动态遮挡剔除
	- Sorting Layer 
		- 排序图层
	- Order in Layer 
		- 此线段在排序图层中的顺序
## 代码相关

### 动态添加一个线段

- `Gameobject line = new Gameobject();line.name = "Line";
- `LineRenderer lineRenderer = line.Addcomponent<LineRenderer>;
	- 创建一个空物体，并改名为 “Line” ，随后添加 LineRenderer 组件，并存储
### 首尾相连

- `lineRenderer.loop =true;
### 开始/结束 宽

- `lineRenderer.startwidth = 0.02f;
- `lineRenderer.endwidth = 0.02f;
### 开始结束颜色

- `lineRenderer.startColor = Color.white;
- `lineRenderer.endcolor =Color.red;
### 设置材质

- `lineRenderer.material = 材质;
	- 可以使用 [[Resources资源同步加载]] / [[Resources资源异步加载]]
### 设置点

- 一定注意 设置点 要 先设置点的个数
	- `lineRenderer.positionCount = 4;
		- 相当于一个数组
- 接着就设置 对应每个点的位置
	- `lineRenderer.setPositions(new Vector3[] { 各个点的坐标 new Vector3(0,0,0,) , ……}
		- 如果点不够，那么没赋值的点就是默认 `(0,0,0,)
- 指定点的赋值
	- `lineRenderer.SetPosition( 索引 , new Vector3(5，0，0) );
### 是否使用世界坐标系

- `lineRenderer.useWorldspace =false;
	-  决定 是否随对象移动而移动
### 让线段受光影响

- `lineRenderer.generateLightingData=true;
	- 会接受光数据 进行着色器计算

---
# 源代码

- ![[LineRenderer 知识点.cs]]
- ![[画线功能 LineRenderer -参数1.png]]
- ![[画线功能 LineRenderer -参数2.png]]
- ![[画线功能 LineRenderer -参数3.png]]
- ![[画线功能 LineRenderer -参数4.png]]


---
# 练习题

![[LineRenderer 练习题.cs]]

---




