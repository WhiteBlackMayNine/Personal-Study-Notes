using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lesson18 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        print(eventData.delta);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("鼠标（触碰）按下");
        print(eventData.pointerId);

        print(eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标进入 在移动设备上 是不存在 因为不存在 进入的概念
        print("鼠标进入");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标离开 在移动设备上 是不存在 因为不存在 进入的概念
        print("鼠标离开");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("鼠标（触碰）抬起");
    }

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 事件接口是用来解决什么问题的
        //目前所有的控件都只提供了常用的事件监听列表
        //如果想做一些类似长按，双击，拖拽等功能是无法制作的
        //或者想让Image和Text，RawImage三大基础控件能够响应玩家输入也是无法制作的

        //而事件接口就是用来处理类似问题
        //让所有控件都能够添加更多的事件监听来处理对应的逻辑
        #endregion

        #region 知识点二 有哪些事件接口

        #region 常用事件接口
        //IPointerEnterHandler - OnPointerEnter - 当指针进入对象时调用 （鼠标进入）
        //IPointerExitHandler - OnPointerExit - 当指针退出对象时调用 （鼠标离开）
        //IPointerDownHandler - OnPointerDown - 在对象上按下指针时调用 （按下）
        //IPointerUpHandler - OnPointerUp - 松开指针时调用（在指针正在点击的游戏对象上调用）（抬起）
        //IPointerClickHandler - OnPointerClick - 在同一对象上按下再松开指针时调用 （点击）

        //IBeginDragHandler - OnBeginDrag - 即将开始拖动时在拖动对象上调用 （开始拖拽）
        //IDragHandler - OnDrag - 发生拖动时在拖动对象上调用 （拖拽中）
        //IEndDragHandler - OnEndDrag - 拖动完成时在拖动对象上调用 （结束拖拽）
        #endregion

        #region 不常用事件接口 了解即可
        //IInitializePotentialDragHandler - OnInitializePotentialDrag - 在找到拖动目标时调用，可用于初始化值
        //IDropHandler - OnDrop - 在拖动目标对象上调用
        //IScrollHandler - OnScroll - 当鼠标滚轮滚动时调用
        //IUpdateSelectedHandler - OnUpdateSelected - 每次勾选时在选定对象上调用

        //ISelectHandler - OnSelect - 当对象成为选定对象时调用
        //IDeselectHandler - OnDeselect - 取消选择选定对象时调用

        //导航相关
        //IMoveHandler - OnMove - 发生移动事件（上、下、左、右等）时调用
        //ISubmitHandler - OnSubmit - 按下 Submit 按钮时调用
        //ICancelHandler - OnCancel - 按下 Cancel 按钮时调用
        #endregion

        #endregion

        #region 知识点三 使用事件接口
        //1.继承MonoBehavior的脚本继承对应的事件接口，引用命名空间
        //2.实现接口中的内容
        //3.将该脚本挂载到想要监听自定义事件的UI控件上
        #endregion

        #region 知识点四 PointerEventData参数的关键内容
        //父类：BaseEventData

        //pointerId： 鼠标左右中键点击鼠标的ID 通过它可以判断右键点击
        //position：当前指针位置（屏幕坐标系）
        //pressPosition：按下的时候指针的位置
        //delta：指针移动增量
        //clickCount：连击次数
        //clickTime：点击时间

        //pressEventCamera：最后一个OnPointerPress按下事件关联的摄像机
        //enterEvetnCamera：最后一个OnPointerEnter进入事件关联的摄像机
        #endregion

        #region 总结
        //好处：
        //需要监听自定义事件的控件挂载继承实现了接口的脚本就可以监听到一些特殊事件
        //可以通过它实现一些长按，双击拖拽等功能

        //坏处：
        //不方便管理，需要自己写脚本继承接口挂载到对应控件上，比较麻烦
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
