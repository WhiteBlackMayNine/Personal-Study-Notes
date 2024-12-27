using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//这几个类的作用 ——> Json 可以序列化 哪些类型
[System.Serializable]
public class Student
{
    public int age;
    public string name;

    public Student(int age, string name)
    {
        this.age = age;
        this.name = name;
    }
}

public class MrTang
{
    public string name;
    public int age;
    public bool sex;
    public float testF;
    public double testD;

    public int[] ids;
    public List<int> ids2;
    public Dictionary<int, string> dic;
    public Dictionary<string, string> dic2;

    public Student s1;
    public List<Student> s2s;

    [SerializeField]
    private int privateI = 1;
    [SerializeField]
    protected int protectedI = 2;
}

public class RoleData
{
    public List<RoleInfo> list;
}

//注意事项 ——> 1. 
//测试类 （假设已经导入了一个 名为 RoleInfo 的 Json 文件 并包含了一下成员变量名）
//可以看之前的几节课，用的是那里面的 Json 文件
[System.Serializable]
public class RoleInfo
{
    public int hp;
    public int speed;
    public int volume;
    public string resName;
    public int scale;
}

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 JsonUtlity是什么？
        //JsonUtlity 是Unity自带的用于解析Json的公共类
        //它可以
        //1.将内存中对象序列化为Json格式的字符串
        //2.将Json字符串反序列化为类对象
        #endregion

        #region 知识点二 必备知识点——在文件中存读字符串
        //1.存储字符串到指定路径文件中
        //第一个参数 填写的是 存储的路径
        //第二个参数 填写的是 存储的字符串内容
        //注意：第一个参数 必须是存在的文件路径 如果没有对应文件夹 会报错
        File.WriteAllText(Application.persistentDataPath + "/Test.json", "唐老狮存储的json文件");
        print(Application.persistentDataPath);

        //2.在指定路径文件中读取字符串
        string str = File.ReadAllText(Application.persistentDataPath + "/Test.json");
        print(str);
        #endregion

        #region 知识点三 使用JsonUtlity进行序列化
        //序列化：把内存中的数据 存储到硬盘上
        //方法：
        //JsonUtility.ToJson(对象)
        
        //赋值
        MrTang t = new MrTang();
        t.name = "唐老狮";
        t.age = 18;
        t.sex = false;
        t.testF = 1.4f;
        t.testD = 1.4;

        t.ids = new int[] { 1, 2, 3, 4 };
        t.ids2 = new List<int>() { 1, 2, 3 };
        t.dic = new Dictionary<int, string>() { { 1, "123" }, { 2, "234" } };
        t.dic2 = new Dictionary<string, string>() { { "1", "123" }, { "2", "234" } };
        //存放null对象 后面的注释 ——> 存放自定义类
        t.s1 = null;//new Student(1, "小红");
        t.s2s = new List<Student>() { new Student(2, "小明"), new Student(3, "小强") };

        //Jsonutility提供了线程的方法 可以把类对象 序列化为 json字符串
        string jsonStr = JsonUtility.ToJson(t);
        File.WriteAllText(Application.persistentDataPath + "/MrTang.json", jsonStr);
        
        //利用 JsonUtility.ToJson 将 数据 序列化为 字符串
        //利用 File.WriteAllText 将 序列化的字符串 传入到 Json 文件中
        //这样就实现了 数据持久化 的功能
        

        //注意：
        //1.float序列化时看起来会有一些误差
        //2.自定义类需要加上序列化特性[System.Serializable]
        //3.想要序列化私有变量 需要加上特性[SerializeField]
        //4.JsonUtility不支持字典
        //5.JsonUtlity存储null对象不会是null 而是默认值的数据
        #endregion

        #region 知识点四 使用JsonUtlity进行反序列化
        //反序列化：把硬盘上的数据 读取到内存中
        //方法：
        //JsonUtility.FromJson(字符串)
        //读取文件中的 Json字符串
        jsonStr = File.ReadAllText(Application.persistentDataPath + "/MrTang.json");
        //使用Json字符串内容 转换成类对象
        MrTang t2 = JsonUtility.FromJson(jsonStr, typeof(MrTang)) as MrTang;
        MrTang t3 = JsonUtility.FromJson<MrTang>(jsonStr);

        //注意：
        //如果Json中数据少了，读取到内存中类对象中时不会报错
        #endregion

        #region 知识点五 注意事项
        //1.JsonUtlity无法直接读取数据集合
        jsonStr = File.ReadAllText(Application.streamingAssetsPath + "/RoleInfo2.json");
        print(jsonStr);
        //List<RoleInfo> roleInfoList = JsonUtility.FromJson<List<RoleInfo>>(jsonStr); //将会报错
        RoleData data = JsonUtility.FromJson<RoleData>(jsonStr);//需要更改 Json 文件

        //2.文本编码格式需要时UTF-8 不然无法加载
        #endregion

        #region 总结
        //1.必备知识点 —— File存读字符串的方法 ReadAllText和WriteAllText
        //2.JsonUtlity提供的序列化反序列化方法 ToJson 和 FromJson
        //3.自定义类需要加上序列化特性[System.Serializable]
        //4.私有保护成员 需要加上[SerializeField]
        //5.JsonUtlity不支持字典
        //6.JsonUtlity不能直接将数据反序列化为数据集合
        //7.Json文档编码格式必须是UTF-8
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
