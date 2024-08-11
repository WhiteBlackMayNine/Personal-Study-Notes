using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesMgr
{
    private static ResourcesMgr instance  = new ResourcesMgr();
    public static ResourcesMgr Instance => instance;

    private ResourcesMgr()
    {
    }

    public void LoadRes<T>(string name, UnityAction<T> callBack) where T:Object
    {
        ResourceRequest rq = Resources.LoadAsync<T>(name);
        rq.completed += (a) =>
        {
            //直接得到 传入的 对象 通过它得到资源内容 然后转换成对应类型 传出去 外面不需要转换 直接使用
            callBack((a as ResourceRequest).asset as T);
        };
    }
}
