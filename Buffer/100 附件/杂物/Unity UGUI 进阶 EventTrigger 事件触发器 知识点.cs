using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lesson19 : MonoBehaviour
{
    public EventTrigger et;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 事件触发器是什么
        //事件触发器是EventTrigger组件
        //它是一个集成了上节课中学习的所有事件接口的脚本
        //它可以让我们更方便的为控件添加事件监听
        #endregion

        #region 知识点二 如何使用事件触发器
        //1.拖曳脚本进行关联

        //2.代码添加

        //申明一个希望监听的事件对象
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //申明 事件的类型
        entry.eventID = EventTriggerType.Drag;
        //监听函数关联
        entry.callback.AddListener((data) =>
        {
            print("抬起");
        });


        //把申明好的 事件对象 加入到 EventTrigger当中
        et.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        //申明 事件的类型
        entry.eventID = EventTriggerType.BeginDrag;
        //监听函数关联
        entry.callback.AddListener((data) =>
        {
            print("抬起");
        });

        et.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        //申明 事件的类型
        entry.eventID = EventTriggerType.BeginDrag;
        //监听函数关联
        entry.callback.AddListener((data) =>
        {
            print("抬起");
        });

        et.triggers.Add(entry);
        #endregion

        #region 总结
        //EventTrigger可以让我们写更少的代码
        //可以在面板类中处理面板控件的事件逻辑，更加的面向对象，便于管理
        #endregion
    }

    public void TestPointerEnter( BaseEventData data )
    {
        PointerEventData eventData = data as PointerEventData;

        print("鼠标进入 " + eventData.position);
    }
}
