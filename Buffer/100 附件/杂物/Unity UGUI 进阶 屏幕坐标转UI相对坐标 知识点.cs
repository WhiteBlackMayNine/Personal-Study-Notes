using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lesson20 : MonoBehaviour,IDragHandler
{
    public RectTransform parent;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 RectTransformUtility类
        //RectTransformUtility 公共类是一个RectTransform的辅助类
        //主要用于进行一些 坐标的转换等等操作
        //其中对于我们目前来说 最重要的函数是 将屏幕空间上的点，转换成UI本地坐标下的点
        #endregion
    }

    public void OnDrag(PointerEventData eventData)
    {
        #region 知识点二 将屏幕坐标转换为UI本地坐标系下的点
        //方法：
        //RectTransformUtility.ScreenPointToLocalPointInRectangle
        ////参数一：相对父对象
        ////参数二：屏幕点
        ////参数三：摄像机
        ////参数四：最终得到的点
        ////一般配合拖拽事件使用
        Vector2 nowPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent,
            eventData.position,
            eventData.enterEventCamera,
            out nowPos );

        this.transform.localPosition = nowPos;

        //this.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        #endregion
    }

    void Update()
    {
        
    }
}
