using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson29 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 打开Animation窗口
        //Window——>Animation——>Animation
        #endregion

        #region 知识点二 Animation窗口是用来干啥的
        //Animation窗口 直译就是动画窗口
        //它主要用于在Unity内部创建和修改动画
        //所有在场景中的对象都可以通过Animation窗口为其制作动画

        //原理：
        //制作动画时：记录在固定时间点对象挂载的脚本的变量变化
        //播放动画时：将制作动画时记录的数据在固定时间点进行改变，产生动画效果
        #endregion

        #region 知识点三 关键词说明
        //动画时间轴：
        //每一个动画文件都有自己的一个生命周期，从动画开始到结束
        //我们可以在动画时间轴上编辑每一个动画生命周期中变化

        //动画中的帧：
        //假设某个动画的帧率为60帧每秒，意味着该动画1秒钟最多会有60次改变机会
        //每一帧的间隔时间是 1s/60 ≈ 16.67毫秒
        //也就是说 我们最快可以每16.67毫秒改变一次对象状态

        //关键帧：
        //动画在时间轴上的某一个时间节点上处于的状态
        #endregion

        #region 知识点四 认识Animation窗口功能

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
