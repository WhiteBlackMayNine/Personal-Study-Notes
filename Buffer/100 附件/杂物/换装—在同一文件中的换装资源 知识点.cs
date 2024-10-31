using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Lesson40 : MonoBehaviour
{
    public SpriteResolver sr;

    private Dictionary<string, SpriteResolver> equipDic = new Dictionary<string, SpriteResolver>();

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 如何在同一个psb文件中制作换装资源
        //1.在ps中制作美术资源时，将一个游戏对象的所有换装资源都摆放好位置
        //2.当我们导入该资源时，要注意是否导入隐藏的图层
        #endregion

        #region 知识点二 编辑换装资源的骨骼信息以及分组类别
        //注意事项：
        //每个部位 关联的骨骼要明确设置
        //为同一个部位的不同装备分组
        #endregion

        #region 知识点三 如何换装
        //两个关键组件
        //SpriteLibrary——精灵资料库，确定类别分组信息
        //SpriteResolver——精灵解算器，用于确定部位类别和使用的图片
        //一个数据文件
        //SpriteLibraryAsset——精灵资料库资源，具体记录类别分组信息的文件
        #endregion

        #region 知识点四 代码换装
        //1.获取各部位的SpriteResolver（需要引用命名空间）
        //2.使用SpriteResolver的API进行装备切换
        //GetCategory() 获取当前部位默认的类别名
        //SetCategoryAndLabel 设置当前部位想要切换的图片信息
        //sr.SetCategoryAndLabel(sr.GetCategory(), "CASK 1");

        SpriteResolver[] srs = this.GetComponentsInChildren<SpriteResolver>();
        for (int i = 0; i < srs.Length; i++)
        {
            equipDic.Add(srs[i].GetCategory(), srs[i]);
        }

        ChangeEquip("Cask", "CASK 1");

        #endregion
    }


    public void ChangeEquip(string category, string equipName)
    {
        if( equipDic.ContainsKey(category) )
        {
            equipDic[category].SetCategoryAndLabel(category, equipName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
