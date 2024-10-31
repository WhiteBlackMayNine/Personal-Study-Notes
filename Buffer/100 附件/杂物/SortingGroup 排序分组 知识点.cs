using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson13 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 SortingGroup是什么？
        //顾名思义，SortingGroup是排序分组的意思
        //它的主要作用就是对多个精灵图片进行分组排序
        //Unity会将同一个排序组中的精灵图片一起排序，就好像他们是单个游戏对象一样
        //主要作用是对于需要分层的2D游戏用于整体排序
        #endregion

        #region 知识点二 SortingGroup的使用

        #endregion

        #region 知识点三 注意事项
        //1.子排序组，先排子对象 再按父对象和别人一起排 （同层和同层比）
        //2.多个 挂载排序分组组件的预设体 之间 通过修改 排序索引号来决定前后顺序
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
