using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson23_Exercises : MonoBehaviour
{
    public float offsetY;
    private Transform nowSelObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    RaycastHit info;
    // Update is called once per frame
    void Update()
    {
        #region 练习题一
        //Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(r.origin, r.direction);
        ////请用资料区给的资源，实现鼠标点击场景上一面墙，在点击的位置创建子弹特效和弹孔
        //if ( Input.GetMouseButtonDown(0) )
        //{
        //    //按下鼠标就进行 射线检测 从屏幕发射一条射线
        //    //如果碰撞到东西 就会进入 if语句
        //    if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 
        //                                                     out info, 
        //                                                     1000, 
        //                                                     1 << LayerMask.NameToLayer("Monster")))
        //    {
        //        //碰撞到的点 和 法线向量 
        //        //创建 打击特效
        //        GameObject obj = Instantiate(Resources.Load<GameObject>("Effect/HitEff"));
        //        //设置点位置
        //        obj.transform.position = info.point + info.normal*0.2f;
        //        //设置角度 朝向 我们
        //        obj.transform.rotation = Quaternion.LookRotation(info.normal);
        //        //延迟删除特效
        //        Destroy(obj, 0.8f);

        //        obj = Instantiate(Resources.Load<GameObject>("Effect/DDD"));
        //        //设置点位置
        //        obj.transform.position = info.point + info.normal * 0.2f;
        //        //设置角度 朝向 我们
        //        obj.transform.rotation = Quaternion.LookRotation(info.normal);
        //    }
        //}
        #endregion

        #region 练习题二
        //场景上有一个平面，有一个立方体，当鼠标点击选中立方体时，长按鼠标左键 可以拖动立方体 在平面上移动，点击鼠标右键取消选中
        
        //选中
        if(Input.GetMouseButtonDown(0))
        {
            if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info, 1000, 1<<LayerMask.NameToLayer("Player")))
            {
                //记录选中的 对象  位置信息
                nowSelObj = info.transform;
            }
        }

        //当前有选中的对象 并且 是长按
        if( Input.GetMouseButton(0) && nowSelObj != null )
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info, 1000, 1<< LayerMask.NameToLayer("Floor")))
            {
                nowSelObj.position = info.point + Vector3.up*offsetY;
            }
        }

        //取消选中
        if(Input.GetMouseButtonDown(1))
        {
            //取消记录
            nowSelObj = null;
        }
        #endregion
    }
}
