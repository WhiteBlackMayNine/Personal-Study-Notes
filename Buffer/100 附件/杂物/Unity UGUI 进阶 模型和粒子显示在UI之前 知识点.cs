using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson22 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 模型显示在UI之前

        #region 方法一：直接用摄像机渲染3D物体
        //Canvas的渲染模式要不是覆盖模式
        //摄像机模式 和 世界(3D)模式都可以让模型显示在UI之前（Z轴在UI元素之前即可）

        //注意：
        //1.摄像机模式时建议用专门的摄像机渲染UI相关
        //2.面板上的3D物体建议也用UI摄像机进行渲染
        #endregion

        #region 方法二：将3D物体渲染在图片上，通过图片显示
        //专门使用一个摄像机渲染3D模型，将其渲染内容输出到Render Texture上
        //类似小地图的制作方式
        //再将渲染的图显示在UI上

        //该方式 不管Canvas的渲染模式是哪种都可以使用
        #endregion

        #endregion

        #region 知识点二 粒子特效显示在UI之前

        //粒子特效的显示和3D物体类似

        //注意点：
        //在摄像机模式下时
        //可以在粒子组件的Renderer相关参数中改变排序层 让粒子特效始终显示在其之前不受Z轴影响
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
