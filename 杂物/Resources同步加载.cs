using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson17 : MonoBehaviour
{
    public AudioSource audioS;

    private Texture tex;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Resources资源动态加载的作用
        //1.通过代码动态加载Resources文件夹下指定路径资源
        //2.避免繁琐的拖曳操作
        #endregion

        #region 知识点二 常用资源类型
        //1.预设体对象——GameObject
        //2.音效文件——AudioClip
        //3.文本文件——TextAsset
        //4.图片文件——Texture
        //5.其它类型——需要什么用什么类型

        //注意：
        //预设体对象加载需要实例化
        //其它资源加载一般直接用
        #endregion

        #region 知识点三 资源同步加载 普通方法
        //在一个工程当中 Resources文件夹 可以有多个 通过API加载时 它会自己去这些同名的Resources文件夹中去找资源
        //打包时 Resources文件夹 里的内容 都会打包在一起

        //1.预设体对象 想要创建在场景上 记住实例化
        // 第一步：要去加载预设体的资源文件(本质上 就是加载 配置数据 在内存中)
        Object obj = Resources.Load("Cube");
        //第二步：如果想要在场景上 创建预设体 一定是加载配置文件过后 然后实例化
        Instantiate(obj);

        // 第一步：要去加载预设体的资源文件(本质上 就是加载 配置数据 在内存中)
        Object obj2 = Resources.Load("Sphere");
        //第二步：如果想要在场景上 创建预设体 一定是加载配置文件过后 然后实例化
        Instantiate(obj2);

        //2.音效资源
        //第一步：就是加载数据
        Object obj3 = Resources.Load("Music/BKMusic");
        //第二步：使用数据 我们不需要实例化 音效切片 我们只需要把数据 赋值到正确的脚本上即可
        audioS.clip = obj3 as AudioClip;
        audioS.Play();

        //3.文本资源
        //文本资源支持的格式
        //.txt
        //.xml
        //.bytes
        //.json
        //.html
        //.csv
        //.....
        TextAsset ta = Resources.Load("Txt/Test") as TextAsset;
        //文本内容
        print(ta.text);
        //字节数据组
        //print(ta.bytes);

        //4.图片
        //tex = Resources.Load("Tex/TestJPG") as Texture;

        //5.其它类型 需要什么类型 就用什么类型就行


        //6.问题：资源同名怎么办
        //Resources.Load加载同名资源时 无法准确加载出你想要的内容

        //可以使用另外的API
        //6-1加载指定类型的资源
        //tex = Resources.Load("Tex/TestJPG", typeof(Texture)) as Texture;

        ta = Resources.Load("Tex/TestJPG", typeof(TextAsset)) as TextAsset;
        //print(ta.text);

        //6-2加载指定名字的所有资源
        Object[] objs = Resources.LoadAll("Tex/TestJPG");
        foreach (Object item in objs)
        {
            if (item is Texture)
            { 

            }
            else if(item is TextAsset)
            {

            }
        }

        #endregion

        #region 知识点四 资源同步加载 泛型方法

        TextAsset ta2 = Resources.Load<TextAsset>("Tex/TestJPG");
        print(ta2.text);

        tex = Resources.Load<Texture>("Tex/TestJPG");

        #endregion

        #region 总结
        //Resources动态加载资源的方法
        //让拓展性更强
        //相对拖曳来说 它更加一劳永逸 更加方便

        //重要知识点：
        //记住API
        //记住一些特定的格式
        //预设体加载出来一定要实例化
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 100, 100), tex);
    }
}
