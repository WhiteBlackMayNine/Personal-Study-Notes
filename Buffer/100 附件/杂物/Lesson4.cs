using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Lesson4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 回顾通过ScriptableObject创建非持久化数据
        MyData data = ScriptableObject.CreateInstance<MyData>();
        #endregion

        #region 知识点二 回顾数据持久化
        //硬盘<=>内存
        //使用数据时从硬盘中读取
        //数据改变后保存到硬盘上
        //游戏退出程序关闭后，数据信息会被存储到硬盘上，达到持久化的目的

        //我们讲授过的数据持久化相关知识
        //PlayerPrefs
        //XML
        //Json
        //2进制

        //ScriptableObject并不适合用来做数据持久化功能
        //但是我们可以利用我们学过的数据持久化方案 让其持久化
        #endregion

        #region 知识点三 利用Json结合ScriptableObject存储数据
        data.PrintInfo();

        //data.i = 9999;
        //data.f = 6.6f;
        //data.b = true;
        //将数据对象 序列化为 json字符串
        //string str = JsonUtility.ToJson(data);
        //print(str);
        ////把数据序列化后的结果 存入指定路径当中
        //File.WriteAllText(Application.persistentDataPath + "/testJson.json", str);
        //print(Application.persistentDataPath);
        #endregion

        #region 知识点四 利用Json结合ScriptableObject读取数据
        //从本地读取 Json字符串
        string str = File.ReadAllText(Application.persistentDataPath + "/testJson.json");
        //根据json字符串反序列化出数据 将内容覆盖到数据对象中
        JsonUtility.FromJsonOverwrite(str, data);
        data.PrintInfo();
        #endregion

        #region 总结
        //对于ScriptableObject的数据
        //由于它在游戏发布运行过程中无法被持久化
        //我们可以利用 PlayerPrefs、XML、Json、2进制等等方式
        //让其可以达到被真正持久化的目的

        //但是我个人并不建议大家利用ScriptableObject来做数据持久化
        //有点画蛇添足的意思了
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

