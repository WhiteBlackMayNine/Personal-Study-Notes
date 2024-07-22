using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson52 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是动画混合
        //游戏动画中常见的功能就是在两个或者多个相似运动之间进行混合
        //比如
        //1.根据角色的速度来混合行走和奔跑动画
        //2.根据角色的转向来混合向左或向右倾斜的动作

        //你可以理解是高级版的动画过渡

        //之前我们学习的动画过渡是处理两个不同类型动作之间切换的过渡效果
        //而动画混合是允许合并多个动画来使动画平滑混合
        #endregion

        #region 知识点二 如何在状态机窗口创建动画混合状态
        //在Animator Controller窗口 右键->Create State->From New Blend Tree
        #endregion

        #region 知识点三 1D混合的使用
        //1D混合就是通过一个参数来混合子运动
        //注意：
        //往混合树里面加入动作时需要找到动画文件进行关联
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
