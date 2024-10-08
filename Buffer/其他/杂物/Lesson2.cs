using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2 : MonoBehaviour
{
    public MyData data;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 ScriptableObject数据文件的使用
        //1.通过Inspector中的public变量进行关联
        //1-1.创建一个数据文件
        //1-2.在继承MonoBehaviour类中申明数据容器类型的成员
        //    在Inspector窗口进行关联
        //data.PrintInfo();

        //2.通过资源加载的信息关联
        //加载数据文件资源
        //注意：Resources、AB包、Addressables都支持加载继承ScriptableObject的数据文件
        data = Resources.Load<MyData>("MyDataTest");
        data.PrintInfo();

        //注意：如果多个对象关联同一个数据容器文件，他们共享的是一个对象
        //     因为是引用对象，所以在其中任何地方修改后，其它地方也会发生改变
        #endregion

        #region 知识点二 ScriptableObject的生命周期函数
        //ScriptableObject和MonoBehavior很类似
        //它也存在生命周期函数
        //但是生命周期函数的数量更少
        //主要做了解，一般我们使用较少

        //Awake 数据文件创建时调用

        //OnDestroy ScriptableObject 对象将被销毁时调用
        //OnDisable ScriptableObject 对象销毁时、即将重新加载脚本程序集时 调用
        //OnEnable ScriptableObject 创建或者加载对象时调用

        //OnValidate 编辑器才会调用的函数，Unity在加载脚本或者Inspector窗口中更改值时调用
        #endregion

        #region 知识点三 ScriptableObject好处的体现
        //1.编辑器中的数据持久化
        //通过代码修改数据对象中内容，会影响数据文件
        //相当于达到了编辑器中数据持久化的目的
        //(该数据持久化 只是在编辑模式下的持久,发布运行时并不会保存数据)
        data.i = 9999;
        data.f = 5.5f;
        data.b = false;

        //2.复用数据
        //如果多个对象关联同一个数据文件
        //相当于他们复用了一组数据，内存上更加节约空间
        #endregion

        #region 总结
        //其实创建出来的数据资源文件，你可以把它理解成一种记录数据的资源
        //它的使用方式，和我们以前使用Unity当中的其它资源规则是一样的
        //比如：预设体、音频文件、视频文件、动画控制器文件、材质球等等
        //只不过通过继承ScriptableObject类生成的数据资源文件，它主要是和数据相关的
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
