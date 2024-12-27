using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Lesson17 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 为什么要打图集
        //UGUI和NGUI使用上最大的不同是 NGUI使用前就要打图集
        //UGUI可以再之后再打图集

        //打图集的目的就是减少DrawCall 提高性能
        //具体DrawCall是什么在NGUI课程中已经详细讲解
        //该节课是免费课 即使没有购买 也可以前往观看

        //简单回顾DrawCall
        //DC就是CPU通知GPU进行一次渲染的命令
        //如果DC次数较多会导致游戏卡顿
        //我们可以通过打图集，将小图合并成大图，将本应n次的DC变成1次DC来提高性能
        #endregion

        #region 知识点二 在Unity中打开自带的打图集功能
        //在工程设置面板中打开功能
        //Edit——>Project Setting——>Editor
        //Sprite Packer(精灵包装器，可以通过Unity自带图集工具生成图集)
        //Disabled：默认设置，不会打包图集

        //Enabled For Builds（Legacy Sprite Packer）：Unity仅在构建时打包图集，在编辑模式下不会打包图集
        //Always Enabled（Legacy Sprite Packer）：Unity在构建时打包图集，在编辑模式下运行前会打包图集

        //Legacy Sprite Packer传统打包模式 相对下面两种模式来说 多了一个设置图片之间的间隔距离
        //Padding Power:选择打包算法在计算打包的精灵之间以及精灵与生成的图集边缘之间的间隔距离
        //              这里的数字 代表2的n次方

        //Enabled For Build：Unity进在构建时打包图集，在编辑器模式下不会打包
        //Always Enabled：Unity在构建时打包图集，在编辑模式下运行前会打包图集
        #endregion

        #region 知识点三 打图集参数注意

        #endregion

        #region 知识点四 代码加载
        //加载图集 注意：需要引用命名空间
        SpriteAtlas sa = Resources.Load<SpriteAtlas>("MyAlas");
        //从图集中加载指定名字的小图
        sa.GetSprite("bk");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
