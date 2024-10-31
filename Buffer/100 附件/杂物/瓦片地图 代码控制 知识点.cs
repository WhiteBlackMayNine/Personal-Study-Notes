using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lesson28 : MonoBehaviour
{
    //瓦片地图信息 可以通过它得到瓦片格子
    public Tilemap map;
    //格子位置相关控制 可以通过它 进行坐标转换
    public Grid grid;
    //瓦片资源基类通过它可以得到瓦片资源
    public TileBase tileBase;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 获取Tilemap和TileBase和Grid
        //Tilemap组件：用于管理瓦片地图
        //TileBase组件：瓦片资源对象基类
        //Grid组件：用于坐标转换

        //使用他们需要引用命名空间
        #endregion

        #region 知识点二 重要API
        //1.清空瓦片地图
        //map.ClearAllTiles();

        //2.获取指定坐标格子
        TileBase tmp = map.GetTile(Vector3Int.zero);
        print(tmp);

        //3.设置删除瓦片
        map.SetTile(new Vector3Int(0, 2, 0), tileBase);

        map.SetTile(new Vector3Int(1, 0, 0), null);

        //map.SetTiles()

        //4.替换瓦片
        map.SwapTile(tmp, tileBase);

        //5.世界坐标转格子坐标

        //  屏幕坐标转世界坐标
        //  世界坐标转格子坐标
        //传入的参数是世界坐标
        //grid.WorldToCell()
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
