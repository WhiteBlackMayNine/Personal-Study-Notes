using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson50 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 使用导入的3D动画
        //1.将模型拖入场景中
        //2.为模型对象添加Animator脚本
        //3.为其撞见Animator Controller动画控制器（状态机）
        //4.将想要使用的相关动作 拖入Animator Controller动画控制器（状态机）窗口
        //5.在Animator Controller动画控制器（状态机）窗口编辑动画关系（使用之前学习的状态机相关知识）
        //6.代码控制状态切换
        #endregion

        #region 知识点二 状态设置相关参数
        //我们可以选中状态机窗口中的某一个状态为其设置相关参数
        //我们可以称之为动画状态设置
        //主要设置的是 当前状态的播放速度等等细节
        #endregion

        #region 知识点三 连线设置相关参数
        //我们可以选中状态机窗口中的某一条箭头为其设置相关参数
        //我们可以称之为动画过渡设置
        //主要设置的是 从一个状态切换到另一个状态时 的表现效果和切换条件
        #endregion

        #region 总结
        //注意点
        //1.Has Exit Time是否启用 如果希望瞬间切换动画不需过多等待，取消该选项
        //2.Can Transition To self是否启用 如果希望自己不要打断自己，取消该选项
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
