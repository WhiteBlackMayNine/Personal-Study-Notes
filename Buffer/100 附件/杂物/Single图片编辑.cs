using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 SpriteEditor是什么？
        //顾名思义，SpriteEditor就是 精灵图片编辑器
        //它主要用于编辑2D游戏开发中使用的Sprite精灵图片
        //它可以用于编辑 图集中提取元素，设置精灵边框，设置九宫格，设置轴心（中心）点等等功能
        #endregion

        #region 知识点二 安装 2D Sprite
        //新版本Unity 需要安装 2D Sprite包才能使用SpriteEditor
        #endregion

        #region 知识点三 Single图片编辑 功能讲解
        //Single图片编辑主要讲解的就是在设置图片时
        //将精灵图片模式（Sprite Mode）设置为Single的精灵图片在Sprite Editor窗口中如何编辑

        //1.Sprite Editor
        //  基础图片设置（右下角窗口）
        //  主要用于设置单张图片的基础属性

        //2.Custom Outline（决定渲染区域）
        //  自定义边缘线设置，可以自定义精灵网格的轮廓形状
        //  默认情况下不修改都是在矩形网格上渲染,边缘外部透明区域会被渲染，浪费性能
        //  使用自定义轮廓，可以调小透明区域，提高性能

        //3.Custom Physics Shape（决定碰撞判断区域）
        //  自定义精灵图片的物理形状，主要用于设置需要物理碰撞判断的2D图形
        //  它决定了之后产生碰撞检测的区域

        //4.Secondary Textures(为图片添加特殊效果)
        //  次要纹理设置，可以将其它纹理和该精灵图片关联
        //  着色器可以得到这些辅助纹理然后用于做一些效果处理
        //  让精灵应用其它效果
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
