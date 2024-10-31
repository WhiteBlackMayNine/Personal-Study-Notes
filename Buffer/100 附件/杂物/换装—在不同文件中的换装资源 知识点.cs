using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson41 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 如何在不同psb文件中制作换装资源
        //1.保证个部位在PS文件中的统一
        //2.基础部位可选择性隐藏
        #endregion

        #region 知识点二 编辑换装资源的骨骼信息
        //注意事项：
        //不同文件的骨骼信息必须统一,所以我们直接使用复制的方式
        #endregion

        #region 知识点三 手动添加关键组件和数据文件
        //1.首先创建SpriteLibraryAsset数据文件
        //2.为跟对象添加SpriteLibrary并关联数据文件
        //3.为换装部位关联SpriteResolver
        #endregion

        #region 总结 如何选择 同一文件和 不同文件 制作换装资源两种方案
        //换装较少的游戏 比如只有面部表情更换 可以使用同一psb文件方案
        //换装较多的游戏 比如各部位有n种装备 可以使用不同psb文件方案
        //不同psb文件 拓展性更强

        //一切根据需求而定
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
