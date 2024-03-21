using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 世界坐标系
        //目前学习的和世界坐标系相关的
        //this.transform.position;
        //this.transform.rotation;
        //this.transform.eulerAngles;
        //this.transform.lossyScale;
        //修改他们 会是相对世界坐标系的变化
        #endregion

        #region 知识点二 物体坐标系
        //相对父对象的物体坐标系的位置 本地坐标 相对坐标
        //this.transform.localPosition;
        //this.transform.localEulerAngles;
        //this.transform.localRotation;
        //this.transform.localScale;
        //修改他们 会是相对父对象物体坐标系的变化
        #endregion

        #region 知识点三 屏幕坐标系
        //Input.mousePosition
        //Screen.width;
        //Screen.height;
        #endregion

        #region 知识点四 视口坐标系
        //摄像机上的 视口范围
        #endregion

        #region 坐标转换相关
        //世界转本地
        //this.transform.InverseTransformDirection
        //this.transform.InverseTransformPoint
        //this.transform.InverseTransformVector

        //本地转世界
        //this.transform.TransformDirection
        //this.transform.TransformPoint  
        //this.transform.TransformVector

        //世界转屏幕
        //Camera.main.WorldToScreenPoint
        //屏幕转世界
        //Camera.main.ScreenToWorldPoint

        //世界转视口
        //Camera.main.WorldToViewportPoint
        //视口转世界
        //Camera.main.ViewportToWorldPoint

        //视口转屏幕
        //Camera.main.ViewportToScreenPoint
        
        //屏幕转视口
        //Camera.main.ScreenToViewportPoint;
        
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
