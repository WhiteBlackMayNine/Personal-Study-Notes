using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson43 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Spine导出的Unity资源
        //Spine导出的资源有3个文件
        //.json 存储了骨骼信息
        //.png  使用的图片图集
        //.atlas.txt    图片在图集中的位置信息

        //当我们把这三个资源导入到已经引入了Spine运行库的Unity工程后
        //会自动为我们生成
        //_Atlas    材质和.atlas.txt文件的引用配置文件
        //_Material 材质文件
        //_SkeletonData json和_Atlas资源的引用配置文件
        #endregion

        #region 知识点二 使用Spine导出的骨骼动画
        //1.直接将_SkeletonData文件 拖入到场景中
        //  选择创建 SkeletonAnimation对象

        //2.创建空对象 然后手动添加脚本进行关联
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
