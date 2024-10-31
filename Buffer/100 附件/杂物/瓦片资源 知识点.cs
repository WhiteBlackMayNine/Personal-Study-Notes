using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson22 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是Tilemap？
        //Tilemap一般称之为 瓦片地图或者平铺地图
        //是Unity2017中新增的功能
        //主要用于快速编辑2D游戏中的场景
        //通过复用资源的形式提升地图多样性

        //工作原理就是用一张张的小图排列组合为一张大地图

        //它和SpriteShape的异同
        //共同点
        //他们都是用于制作2D游戏的场景或地图的
        //不同点
        //1.SpriteShape可以让地形有弧度,TileMap不行
        //2.TileMap可以快捷制作有伪“Z”轴的地图，SpriteShape不行
        #endregion

        #region 知识点二 从PackageManager中引入Tilemap包

        #endregion

        #region 知识点三 Tilemap的最小单位——"瓦片"
        //首先导入学习用资源

        //方法一：
        //Assets——>Create——>Tile

        //方法二：
        //在Tile Palette瓦片调色板窗口创建
        //1.首先新建一个瓦片地图编辑文件
        //2.将资源拖入到窗口中选择要保存的路径
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
