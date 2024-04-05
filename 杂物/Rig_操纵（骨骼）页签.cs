using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson48 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Rig操纵（骨骼）页签是用来干啥的
        //该页签主要是用于设置
        //如何将骨骼映射到导入模型中的网格，以便能够将其动画化
        //对于人形角色模型，需要分配或创建Avatar（替身信息）
        //对于非人形角色模型，需要在骨骼中确定根骨骼

        //简单来说Rig页签主要是设置骨骼和替身系统相关信息的
        //设置了他们，动画才能正常的播放
        #endregion--

        #region 知识点二 面板基础参数讲解

        #endregion

        #region 知识点三 Avatar化身系统是什么
        //理解化身系统首先要知道骨骼动画是什么
        //通过我们之前基础知识的讲解和2D骨骼动画的讲解
        //相信大家已经了解骨骼动画是什么
        //3D动画的本质 也是骨骼动画
        //为制作好的模型绑定骨骼制作动画是模型动画的制作流程

        //形象的理解
        //对于人来说
        //人的整体结构都是一致的
        //另一个人能做的动作理论上来说我们是完全可以模仿出来的
        //而化身系统的本质，就是动作的模仿（复用）
        //我们可以把一个标准人形动作通过化身系统复用到其它人形模型上
        //只要保证他们的关节点对应关系是一致的

        //而这节课要学习的就是如何设置人形模型在化身系统中关节的对应关系
        #endregion

        #region 知识点四 化身系统设置讲解

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
