using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson32 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是有限状态机
        //有限状态机（Finite - state machine, FSM）
        //又称有限状态自动机，简称状态机
        //是表示有限个状态以及在这些状态之间的转移和动作等行为的数学模型

        //有限：表示是有限度的不是无限的
        //状态：指所拥有的所有状态

        //举例说明：
        //假设我们人会做很多个动作，也就是有很多种状态
        //这些状态包括 站立、走路、跑步、攻击、防守、睡觉等等
        //我们每天都会在这些状态中切换，而且这些状态虽然多但是是有限的
        //当达到某种条件时，我们就会在这些状态中进行切换
        //而且这种切换时随时可能发生的
        #endregion

        #region 知识点二 有限状态机对于我们的意义
        //游戏开发中有很多功能系统都是有限状态机
        //最典型的状态机系统
        //动作系统 —— 当满足某个条件切换一个动作，且动作是有限的
        //AI（人工智能）系统 —— 当满足某个条件切换一个状态，且状态时有限的

        //所以状态机是游戏开发中一个必不可少的概念
        #endregion

        #region 知识点三 最简单的状态机实现
        //最简单的状态机实现代码就是基于switch的实现

        //假设我们只有一个值来控制当前玩家的状态
        string animName = "idle";
        switch (animName)
        {
            case "idle":
                //待机动作逻辑
                break;
            case "move":
                //移动动作逻辑
                break;
            case "run":
                //跑步动作逻辑
                break;
            case "atk":
                //攻击动作逻辑
                break;
        }
        #endregion

        #region 总结
        //游戏开发中某些系统中存在有限种状态的切换变化时
        //我们可以使用有限状态机的设计思路来进行逻辑编写
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
