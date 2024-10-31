using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson26 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 规则瓦片 RuleTile
        //定义不同方向是否存在连接图片的规则
        //让我们更加快捷的进行地图编辑
        #endregion

        #region 知识点二 动画瓦片 AnimatedTile
        //可以指定序列帧
        //产生可以播放序列帧动画的瓦片
        #endregion

        #region 知识点三 管道瓦片 PipelineTile
        //根据自己相邻瓦片的数量更换显示的图片
        #endregion

        #region 知识点四 随机瓦片 RandomTile
        //根据你设置的图片，随机从中选一个进行绘制
        #endregion

        #region 知识点五 地形瓦片 TerrainTile
        //有点类似规则瓦片，只不过地形瓦片是帮助你定好的规则
        #endregion

        #region 知识点六 权重随机瓦片 WeightedRandomTile
        //可以不平均随机选择图片的瓦片
        #endregion

        #region 知识点七 (高级)规则覆盖瓦片 (Advanced)Rule Override Tile
        //在规则瓦片的基础上 改变图片或者指定启用的规则
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
