using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson21 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 遮罩是什么
        //在不改变图片的情况下
        //让图片在游戏中只显示其中的一部分
        #endregion

        #region 知识点二 遮罩如何使用
        //实现遮罩效果的关键组件时Mask组件
        //通过在父对象上添加Mask组件即可遮罩其子对象

        //注意：
        //1.想要被遮罩的Image需要勾选Maskable
        //2.只要父对象添加了Mask组件，那么所有的UI子对象都会被遮罩
        //3.遮罩父对象图片的制作，不透明的地方显示，透明的地方被遮罩
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
