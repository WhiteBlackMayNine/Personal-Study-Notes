using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson23 : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是异形按钮
        //图片形状不是传统矩形的按钮
        #endregion

        #region 知识点二 如何让异形按钮能够准确点击

        #region 方法一 通过添加子对象的形式
        //按钮之所以能够响应点击，主要是根据图片矩形范围进行判断的
        //它的范围判断是自下而上的，意思是如果有子对象图片，子对象图片的范围也会算为可点击范围
        //那么我们就可以用多个透明图拼凑不规则图形作为按钮子对象用于进行射线检测
        #endregion

        #region 方法二 通过代码改变图片的透明度响应阈值
        //1.第一步：修改图片参数 开启Read/Write Enabled开关
        //2.第二步：通过代码修改图片的响应阈值

        //该参数含义：指定一个像素必须具有的最小alpha值，以变能够认为射线命中了图片
        //说人话：当像素点alpha值小于了 该值 就不会被射线检测了
        img.alphaHitTestMinimumThreshold = 0.1f;
        #endregion

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
