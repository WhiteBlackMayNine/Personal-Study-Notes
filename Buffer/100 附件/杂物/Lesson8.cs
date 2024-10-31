using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson8 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 为什么要单例模式化的获取数据
        //对于只用不变并且要复用的数据
        //比如配置文件中的数据
        //我们往往需要在很多地方获取他们
        //如果我们直接通过在脚本中 public关联 或者 动态加载
        //如果在多处使用，会存在很多重复代码，效率较低
        //如果我们将此类数据通过单例模式化的去获取
        //可以提升效率，减少代码量
        #endregion

        #region 知识点二 实现单例模式化获取数据
        //知识点
        //面向对象、单例模式、泛型等等

        //我们可以实现一个ScriptableObject数据单例模式基类
        //让我们只需要让子类继承该基类
        //就可以直接获取到数据
        //而不再需要去通过 public关联 和 资源动态加载

        print(RoleInfo.Instance.roleList[0].id);
        print(RoleInfo.Instance.roleList[1].tips);

        print(TestData.Instance.i);
        print(TestData.Instance.b);
        #endregion

        #region 总结
        //这种基类比较适合 配置数据 的管理和获取
        //当我们的数据是 只用不变，并且是唯一的时候，可以使用该方式提高我们的开发效率
        //在此基础上你也可以根据自己的需求进行变形
        //比如添加数据持久化的功能，将数据从json中读取，并提供保存数据的方法
        //但是不建议大家用ScriptableObject来制作数据持久化功能
        //除非你有这方面的特殊需求
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
