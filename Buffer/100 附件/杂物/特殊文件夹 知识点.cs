using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson16 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 工程路径获取
        //注意 该方式 获取到的路径 一般情况下 只在 编辑模式下使用
        //我们不会在实际发布游戏后 还使用该路径
        //游戏发布过后 该路径就不存在了 
        print(Application.dataPath);
        #endregion

        #region 知识点二 Resources 资源文件夹
        //路径获取：
        //一般不获取
        //只能使用Resources相关API进行加载
        //如果硬要获取 可以用工程路径拼接
        print(Application.dataPath + "/Resources");

        //注意：
        //需要我们自己将创建
        //作用：
        //资源文件夹
        //1-1.需要通过Resources相关API动态加载的资源需要放在其中
        //1-2.该文件夹下所有文件都会被打包出去
        //1-3.打包时Unity会对其压缩加密
        //1-4.该文件夹打包后只读 只能通过Resources相关API加载
        #endregion

        #region 知识点三 StreamingAssets 流动资源文件夹
        //路径获取：
        print(Application.streamingAssetsPath);
        //注意：
        //需要我们自己将创建
        //作用：
        //流文件夹
        //2-1.打包出去不会被压缩加密，可以任由我们摆布
        //2-2.移动平台只读，PC平台可读可写
        //2-3.可以放入一些需要自定义动态加载的初始资源
        #endregion

        #region 知识点四 persistentDataPath 持久数据文件夹
        //路径获取：
        print(Application.persistentDataPath);

        //注意：
        //不需要我们自己将创建
        //作用：
        //固定数据文件夹
        //3-1.所有平台都可读可写
        //3-2.一般用于放置动态下载或者动态创建的文件，游戏中创建或者获取的文件都放在其中
        #endregion

        #region 知识点五 Plugins 插件文件夹
        //路径获取：
        //一般不获取

        //注意：
        //需要我们自己将创建
        //作用：
        //插件文件夹
        //不同平台的插件相关文件放在其中
        //比如IOS和Android平台
        #endregion

        #region 知识点六 Editor 编辑器文件夹
        //路径获取：
        //一般不获取
        //如果硬要获取 可以用工程路径拼接
        print(Application.dataPath + "/Editor");

        //注意：
        //需要我们自己将创建
        //作用：
        //编辑器文件夹
        //5-1.开发Unity编辑器时，编辑器相关脚本放在该文件夹中
        //5-2.该文件夹中内容不会被打包出去
        #endregion

        #region 知识点七 默认资源文件夹 Standard Assets
        //路劲过去：
        //一般不获取

        //注意：
        //需要我们自己将创建
        //作用：
        //默认资源文件夹
        //一般Unity自带资源都放在这个文件夹下
        //代码和资源优先被编译
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
