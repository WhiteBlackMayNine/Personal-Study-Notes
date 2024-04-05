using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：鼠标回调事件(MonoBehaivour里的方法)
//***************************************** 
public class No14_OnMouseEventFunction : MonoBehaviour
{
    private void OnMouseDown()
    {
        print("在Gris身上按下了鼠标");
    }

    private void OnMouseUp()
    {
        print("在Gris身上按下的鼠标抬起了");
    }

    private void OnMouseDrag()
    {
        print("在Gris身上用鼠标进行了拖拽操作");
    }

    private void OnMouseEnter()
    {
        print("鼠标移入了Gris");
    }

    private void OnMouseExit()
    {
        print("鼠标移出了Gris");
    }

    private void OnMouseOver()
    {
        print("鼠标悬停在了Gris上方");
    }

    private void OnMouseUpAsButton()
    {
        print("鼠标在Gris身上松开了");
    }
}
