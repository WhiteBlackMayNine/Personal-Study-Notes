using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectTool
{
    //Unity 上方的菜单栏里面加
    //在菜单栏点击后，就会执行 CreateMyData() 函数
    [MenuItem("ScriptableObject/CreateMyData")]
    public static void CreateMyData()
    {
        //书写创建数据资源文件的代码
        MyData asset = ScriptableObject.CreateInstance<MyData>();

        //通过编辑器API 根据数据创建一个数据资源文件
        AssetDatabase.CreateAsset(asset, "Assets/Resources/MyDataTest.asset");
        //保存创建的资源
        AssetDatabase.SaveAssets();
        //刷新界面
        AssetDatabase.Refresh();
    }
}
