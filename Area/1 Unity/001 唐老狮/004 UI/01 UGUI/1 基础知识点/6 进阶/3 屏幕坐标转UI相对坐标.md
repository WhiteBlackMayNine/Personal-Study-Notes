---
tags:
  - 科学/Unity/唐老狮/UI/UGUI
created: 2024-11-20
---

---
# 关联知识点



---
# RectTransformUtility类

- RectTransformUtility 公共类是一个RectTransform的辅助类  
- 主要用于进行一些 坐标的转换等等操作  
- 其中对于我们目前来说 最重要的函数是 将屏幕空间上的点，转换成UI本地坐标下的点
# 将屏幕坐标转换为UI本地坐标系下的点

- 方法
	- `RectTransformUtility.ScreenPointToLocalPointInRectangle`
		- 参数一：相对父对象
		- 参数二：屏幕点
		- 参数三：摄像机
		- 参数四：最终得到的点
		- 一般配合拖拽事件使用
			- 比如说 摇杆
- 一般来讲
	- 脚本会挂载到 被拖拽的对象身上（比如说 摇杆 中心的杆 身上）
	- 鼠标在屏幕中点击时，鼠标的坐标为（以左下角为原点的）屏幕坐标系
	- 通过该方法，可将 屏幕坐标系 下的一个点，转换为 相对于 父对象 的坐标
		- ——> 即 将 屏幕点  转换成 UI 的相对 父对象坐标系下的 某个点

```C#
public RectTransform parent;//得到 RectTransform 对象

Vector2 nowPos;  
RectTransformUtility.ScreenPointToLocalPointInRectangle(  
    parent,  
    eventData.position,  
    eventData.enterEventCamera,  
    out nowPos );  
  
this.transform.localPosition = nowPos;  
  
//this.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
```

---
# 源代码

![[Unity UGUI 进阶 屏幕坐标转UI相对坐标 知识点.cs]]

---