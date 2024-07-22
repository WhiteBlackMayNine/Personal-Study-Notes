using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson31 : MonoBehaviour
{
    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是老动画系统
        //Unity中有两套动画系统
        //新：Mecanim动画系统——主要用Animator组件控制动画
        //老：Animation动画系统——主要用Animation组件控制动画（Unity4之前的版本可能会用到）

        //目前我们为对象在Animation窗口创建的动画都会被新动画系统支配
        //有特殊需求或者针对一些简易动画，才会使用老动画系统
        #endregion

        #region 知识点二 老动画系统控制动画播放
        //注意：
        //在创建动画之前为对象添加Animation组件之后再制作动画
        //这时制作出的动画和之前的动画格式是有区别的

        //Animation参数
        #endregion

        #region 知识点三 代码控制播放
        animation = this.GetComponent<Animation>();
        //1.播放动画
        
        //2.淡入播放,自动产生过渡效果

        //3.前一个播完再播放下一个

        //4.停止播放所有动画

        //5.是否在播放某个动画

        //6.播放模式设置

        //7.其它（了解即可，新动画系统中会详细讲解）
        //层级和权重以及混合（老动画系统需要通过代码来达到动画的遮罩、融合等效果）
        //设置层级
        //设置权重
        //混合模式 叠加还是混合
        //设置混组相关骨骼信息
        #endregion

        #region 知识点四 动画事件
        //动画事件主要用于处理 当动画播放到某一时刻想要触发某些逻辑
        //比如进行伤害检测、发射子弹、特效播放等等
        #endregion

        #region 总结
        //老动画系统主要用于处理老版本项目和简单的一些自制动画
        //新项目都不建议大家使用了

        //关键组件：Animation
        #endregion
    }

    public void AnimationEvent()
    {
        print("动画事件触发");
    }

    // Update is called once per frame
    void Update()
    {
        //1.播放动画
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            animation.Play("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animation.Play("2");
        }
        //2.淡入播放,自动产生过渡效果
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            //当你要播放的动画的开始状态 和当前的状态 不一样时 
            //就会产生过渡效果
            animation.CrossFade("3");
            //animation.Play("3");
        }

        //3.前一个播完再播放下一个
        if( Input.GetKeyDown(KeyCode.Alpha4) )
        {
            //animation.PlayQueued("2");
            animation.CrossFadeQueued("2");
        }

        //4.停止播放所有动画
        //animation.Stop();

        //5.是否在播放某个动画
        if( animation.IsPlaying("1") )
        {

        }

        //6.播放模式设置
        //animation.wrapMode = WrapMode.Loop;

        //7.其它（了解即可，新动画系统中会详细讲解）
        //层级和权重以及混合（老动画系统需要通过代码来达到动画的遮罩、融合等效果）
        //设置层级
        //animation["1"].layer = 1;
        //设置权重
        //animation["1"].weight = 1;
        //混合模式 叠加还是混合
        //animation["1"].blendMode = AnimationBlendMode.Additive;
        //设置混组相关骨骼信息
        //animation[""].AddMixingTransform();
    }
}
