using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    public MyData data;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 ScriptableObject的非持久化数据指的是什么
        //指的是不管在编辑器模式还是在发布后都 不会持久化的数据
        //我们可以根据自己的需求随时创建对应数据对象进行使用
        //就好像直接new一个数据结构类对象
        #endregion

        #region 知识点二 如何利用ScriptableObject生成非持久化的数据
        //利用ScriptableObject中的静态方法 CreateInstance<>()
        //该方法可以在运行时创建出指定继承ScriptableObject的对象
        //该对象只存在于内存当中，可以被GC
        //调用一次就创建一次

        //通过这种方式创建出来的数据对象 它里面的默认值 不会受到脚本中设置的影响
        //data = ScriptableObject.CreateInstance("MyData") as MyData;
        data = ScriptableObject.CreateInstance<MyData>();

        data.PrintInfo();
        #endregion

        #region 知识点三 ScriptableObject的非持久化数据存在的意义
        //只是希望在运行时能有一组唯一的数据可以使用
        //但是这个数据又不太希望保存为数据资源文件浪费硬盘空间
        //那么ScriptableObject的非持久化数据就有了存在的意义
        //它的特点是
        //只在运行时使用，在编辑器模式下也不会保存在本地
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
