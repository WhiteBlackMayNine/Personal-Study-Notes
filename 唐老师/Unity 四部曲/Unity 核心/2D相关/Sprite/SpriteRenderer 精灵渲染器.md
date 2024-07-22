---
tags:
  - 科学/Unity/唐老师/Unity核心/2D相关/Sprite/SpriteRenderer精灵渲染器
created: 2024-07-15
更新:
---

---
# 关联知识点



---
# Sprite Renderer是什么

- 顾名思义，Sprite Renderer 是精灵渲染器
- 所有2D游戏中游戏资源（除UI外）都是通过 Sprite Renderer 让我们看到的
- 它是2D游戏开发中的一个极为重要的组件
# 2D对象创建

- 直接拖入 Sprite 图片
- 右键创建
- 空物体添加脚本
# 参数讲解

## Sprite

- 渲染的精灵图片
## Draw Mode

- 绘制模式，当尺寸变化时的缩放方式
### Simple

- 简单模式，缩放时整个图像一起缩放
### Sliced

- 切片模式，9宫格切片模式，十字区域缩放，4个角不变化
- 一般用于变化不大的纯色图
- 注意:需要把精灵的网格类型设置为 Full Rect
### Tiled

- 平铺模式，将中间部分进行平铺而不是缩放
- 注意:需要把精灵的网格类型设置为 Full Rect
#### Continuous

- 当尺寸变化时，中间部分将均匀平铺
#### Adaptive

- 当尺寸变化时，类似Simple模式，当更改尺寸达到Stretch Value时，中间才开始平铺
## Mask Interaction

- 与精灵遮罩交互时的方式
### None

- 不与场景中任何精灵遮罩交互
### Visible inside Mask

- 精灵遮罩覆盖的地方可见，而遮罩外部不可见
### Visible Outside Mask

- 精灵遮罩外部的地方可见，而遮罩覆盖处不可见
## Additional Settings

- 高级设置
### Sorting Layer

- 排序层选择
### Order in Layer

- 层级序列号，数值越大约会显示在前面
# 代码设置

```C#
void Start(){

Gameobject obj= new Gameobject(); //创建一个空物体
SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();//给空物体添加组件并获取

sr.sprite = Resources.Load<Sprite>("dead1");//创建一个文件名为 dead1 的对象

Sprite[] sqrs = Resources.LoadAll<Sprite>("RobotBoyIdlesprite");
//RobotBoyIdlesprite 为一个图集名 
//数组会依次保存 图集 中的所以图片(图集里面的小图)
sr.sprite = sqrs[10]; //取出索引为10 的小图

其他的选项使用 点运算符 调用更改

}
```





---
