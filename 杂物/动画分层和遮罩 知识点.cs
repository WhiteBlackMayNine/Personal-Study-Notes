using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson51 : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 动画分层的主要目的
        //动画分层的作用
        //游戏中会有这样的需求
        //人物健康状态时播放正常动画
        //人物非健康状态时播放特殊动画
        //比如血量低于一定界限，人物的大部分动作将表现为虚弱状态
        //我们可以利用动画分层来快速实现这样的功能

        //动画分层和动画遮罩结合使用
        //3D游戏中我们常常会面对这样的需求
        //人物站立时会有开枪动作
        //人物跑动时会有开枪动作
        //人物蹲下时会有开枪动作
        //从表现上来看光是开枪动作可能就有3种
        //如果要让美术同学做3种开枪动作费时又费资源

        //我们是否可以这样做
        //比如开枪动画只影响上半身
        //下半身根据实际情况播放站立，跑动，蹲下动作
        //通过上下半身播放不同的动画就可以达到动画的组合播放

        //动画分层的主要就是达到这两个目的
        //1.两套不同层动作的切换
        //2.结合动画遮罩让两个动画叠加在一起播放
        //提升动画多样性，节约资源
        #endregion

        #region 知识点二 如何使用动画分层
        //1.新建一个动画层
        //2.设置动画层参数
        //3.在该层中设置状态机（注意：结合遮罩使用时默认状态一般为Null状态）
        //4.根据需求创建动画遮罩
        animator = this.GetComponent<Animator>();
        animator.SetLayerWeight(animator.GetLayerIndex("MyLayer2"), 1);

        #endregion

        #region 总结
        //利用动画分层我们可以做到
        //1.上下半身播放两个动画进行组合，比如上半身扔炸弹，下半身待机移动蹲下
        //2.快速制作正常状态和受伤状态的动作切换
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
