using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson45 : MonoBehaviour
{
    private SkeletonAnimation sa;

    [SpineAnimation]
    public string jumpName;

    [SpineBone]
    public string boneName;

    [SpineSlot]
    public string slotName;

    [SpineAttachment]
    public string attachmentName;

    // Start is called before the first frame update
    void Start()
    {
        sa = this.GetComponent<SkeletonAnimation>();
        #region 知识点一 动画播放
        //方法一：直接改变SkeletonAnimation中参数
        //sa.loop = true;
        //sa.AnimationName = "jump";

        //方法二：使用SkeletonAnimation中动画状态改变的函数
        //马上播放
        sa.AnimationState.SetAnimation(0, jumpName, false);
        //排队播放
        sa.AnimationState.AddAnimation(0, "walk", true, 0);
        #endregion

        #region 知识点二 转向
        sa.skeleton.ScaleX = -1;
        #endregion

        #region 知识点三 动画事件
        //动画开始播放
        sa.AnimationState.Start += (t) =>
        {
            print( sa.AnimationName +  "动画开始播放");
        };
        //动画被中断或者清除
        sa.AnimationState.End += (t) =>
        {
            print(sa.AnimationName + "动画中断或者清除");
        };
        //播放完成
        sa.AnimationState.Complete += (t) =>
        {
            print(sa.AnimationName + "动画播放完成");
        };
        //做动画时添加的自定义事件
        sa.AnimationState.Event += (t, e) =>
        {
            print(sa.AnimationName + "自定义事件");
        };
        #endregion

        #region 知识点四 便捷特性
        // 动画特性
        //[SpineAnimation]

        // 骨骼特性
        //[SpineBone]

        // 插槽特性
        //[SpineSlot]

        // 附件特性
        //[SpineAttachment]
        #endregion

        #region 知识点五 获取骨骼、设置插槽附件
        //获取骨骼
        Bone b = sa.skeleton.FindBone(boneName);
        
        //sa.skeleton.SetAttachment(slotName, attachmentName);
        #endregion

        #region 知识点六 在UI中使用
        //SkeletonGraphic（UnityUI）
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
