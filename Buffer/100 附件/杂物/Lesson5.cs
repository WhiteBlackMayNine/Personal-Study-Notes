using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson5 : MonoBehaviour
{
    public RoleInfo info;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 ScriptableObject数据文件为什么非常适合用来做配置文件?
        //1.配置文件的数据在游戏发布之前定规则
        //2.配置文件的数据在游戏运行时只会读出来使用，不会改变内容
        //3.在Unity的Inspector窗口进行配置更加的方便
        #endregion

        #region 知识点二 举例制作
        //以前我们的常规配置方式
        //都是利用之前学习过的 数据持久化四部曲当中的内容进行配置的
        //比如 xml配置 json配置 excel配置

        for (int i = 0; i < info.roleList.Count; i++)
        {
            info.roleList[i].Print();
        }
        #endregion

        #region 总结
        //只用不改
        //并且经常会进行配置的数据
        //非常适合使用ScriptableObject

        //我们可以利用ScriptableObject数据文件 来制作编辑器相关功能
        //比如：Unity内置的技能编辑器、关卡编辑器等等
        //      我们不需要把编辑器生成的数据生成别的数据文件，而是直接通过ScriptableObject进行存储
        //      因为内置编辑器只会在编辑模式下运行，编辑模式下ScriptableObject具备数据持久化的特性
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
