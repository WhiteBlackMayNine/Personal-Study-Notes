using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson25 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 问题：如何整体控制一个面板的淡入淡出等
        //如果我们想要整体控制一个面板的淡入淡出 或者 整体禁用
        //使用目前学习的知识点 是无法方便快捷的设置的
        #endregion

        #region 知识点二 解决方案：Canvas Group
        //为面板父对象添加 CanvasGroup组件 即可整体控制

        //参数相关：
        //Alpha：整体透明度控制
        //Interactable:整体启用禁用设置
        //Blocks Raycasts：整体射线检测设置
        //Ignore Parent Groups：是否忽略父级CanvasGroup的作用
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
