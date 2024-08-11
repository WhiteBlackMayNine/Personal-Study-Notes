using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson56 : MonoBehaviour
{
    private Animator animator;

    public Transform targetPos;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是动画目标匹配
        //动画目标匹配主要指的是
        //当游戏中角色要以某种动作移动，该动作播放完毕后，人物的手或者脚必须落在某一个地方
        //比如：角色需要跳过踏脚石或者跳跃并抓住房梁
        //那么这时我们就需要动作目标匹配来达到想要的效果
        #endregion

        #region 知识点二 如何实现动画目标匹配
        //Unity中的Animator提供了对应的函数来完成该功能
        //使用步骤是
        //1.找到动作关键点位置信息（比如起跳点，落地点，简单理解就是真正可能产生位移的动画表现部分）
        //2.将关键信息传入MatchTargetAPI中

        animator = this.GetComponent<Animator>();
        #endregion

        #region 知识点三 注意
        //调用匹配动画的时机有一些限制
        //1.必须保证动画已经切换到了目标动画上
        //2.必须保证调用时动画并不是处于过度阶段而真正在播放目标动画
        //如果发现匹配不正确，往往都是这两个原因造成的
        //3.需要开启Apply Root Motion
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space) )
        {
            animator.SetTrigger("Jump");
        }
    }

    private void MatchTarget()
    {
        //参数一：目标位置
        //参数二：目标角度
        //参数三：匹配的骨骼位置
        //参数四：位置角度权重
        //参数五：开始位移动作的百分比
        //参数六：结束位移动作的百分比
        animator.MatchTarget(targetPos.position, targetPos.rotation, AvatarTarget.RightFoot, new MatchTargetWeightMask(Vector3.one, 1), 0.4f, 0.64f);
    }
}
