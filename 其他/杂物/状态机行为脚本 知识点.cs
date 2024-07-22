using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson57 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 状态机行为脚本是什么？
        //状态机行为脚本时一类特殊的脚本,继承指定的基类
        //它主要用于关联到状态机中的状态矩形上
        //我们可以按照一定规则编写脚本
        //当进入、退出、保持在某一个特定状态时我们可以进行一些逻辑处理
        //简单解释就是为Animator Controller状态机窗口中的某一个状态添加一个脚本
        //利用这个脚本我们可以做一些特殊功能
        //比如
        //1.进入或退出某一状态时播放声音
        //2.仅在某些状态下检测一些逻辑，比如是否接触地面等等
        //3.激活和控制某些状态相关的特效
        #endregion

        #region 知识点二 如何使用状态机脚本
        //1.新建一个脚本继承StateMachineBehaviour基类
        //2.实现其中的特定方法进行状态行为监听
        //  OnStateEnter    进入状态时，第一个Update中调用
        //  OnStateExit     退出状态时，最后一个Update中调用
        //  OnStateIK       OnAnimatorIK后调用
        //  OnStateMove     OnAnimatorMove后调用
        //  OnStateUpdate   除第一帧和最后一帧，每个Update上调用
        //  OnStateMachineEnter     子状态机进入时调用，第一个Update中调用
        //  OnStateMachineExit      子状态机退出时调用，最后一个Update中调用
        //3.处理对应逻辑
        #endregion

        #region 知识点三 状态机行为脚本和动画事件如何选择
        //状态机行为脚本相对动画事件来说更准确
        //但是使用起来稍微麻烦一些

        //根据实际需求选择使用
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
