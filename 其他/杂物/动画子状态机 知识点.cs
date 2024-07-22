using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson54 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是子状态机
        //子状态机顾名思义就是在状态机里还有一个状态机
        //它的主要作用就是某一个状态时由多个动作状态组合而成的复杂状态
        //比如某一个技能它是由3段动作组合而成的，蹲下，开火，站起
        //当我们释放这个技能时会连续播放这3个动作
        //那么我们完全可以把他们放到一个子状态机中
        #endregion

        #region 知识点二 创建子状态机
        //在Animator Controller窗口中右键->Create Sub-State Machine
        #endregion

        #region 知识点三 编辑子状态机
        //注意：子状态机和外部状态的相互连接方式
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
