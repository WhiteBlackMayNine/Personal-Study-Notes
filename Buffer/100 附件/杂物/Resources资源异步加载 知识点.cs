using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson18 : MonoBehaviour
{
    private Texture tex;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Resources异步加载是什么？
        //上节课学习的同步加载中
        //如果我们加载过大的资源可能会造成程序卡顿
        //卡顿的原因就是 从硬盘上把数据读取到内存中 是需要进行计算的
        //越大的资源耗时越长，就会造成掉帧卡顿

        //Resources异步加载 就是内部新开一个线程进行资源加载 不会造成主线程卡顿
        #endregion

        #region 知识点二 Resources异步加载方法
        //注意：
        //异步加载 不能马上得到加载的资源 至少要等一帧

        //1.通过异步加载中的完成事件监听 使用加载的资源
        //这句代码 你可以理解 Unity 在内部 就会去开一个线程进行资源下载
        //ResourceRequest rq = Resources.LoadAsync<Texture>("Tex/TestJPG");
        //马上进行一个 资源下载结束 的一个事件函数监听
        //rq.completed += LoadOver;
        print(Time.frameCount);
        //这个 刚刚执行了异步加载的 执行代码 资源还没有加载完毕 这样用 是不对的 
        //一定要等加载结束过后 才能使用
        //rq.asset ××××××××××××

        //2.通过协程 使用加载的资源
        StartCoroutine(Load());
        #endregion

        #region 总结
        //1.完成事件监听异步加载
        //好处：写法简单
        //坏处：只能在资源加载结束后 进行处理
        //“线性加载”

        //2.协程异步加载
        //好处：可以在协程中处理复杂逻辑，比如同时加载多个资源，比如进度条更新
        //坏处：写法稍麻烦
        //“并行加载”

        //注意：
        //理解为什么异步加载不能马上加载结束，为什么至少要等1帧
        //理解协程异步加载的原理
        #endregion
    }

    IEnumerator Load()
    {
        //迭代器函数 当遇到yield return时  就会 停止执行之后的代码
        //然后 协程协调器 通过得到 返回的值 去判断 下一次执行后面的步骤 将会是何时
        ResourceRequest rq = Resources.LoadAsync<Texture>("Tex/TestJPG");
        print(Time.frameCount);
        //第一部分
        //Unity 自己知道 该返回值 意味着你在异步加载资源 
        //yield return rq;
        //Unity 会自己判断 该资源是否加载完毕了 加载完毕过后 才会继续执行后面的代码
        print(Time.frameCount);
        
        //判断资源是否加载结束
        while(!rq.isDone)
        {
            //打印当前的 加载进度 
            //该进度 不会特别准确 过渡也不是特别明显
            print(rq.progress);
            yield return null;
        }
        tex = rq.asset as Texture;

        //yield return null;
        ////第二部分
        //yield return new WaitForSeconds(2f);
        ////第三部分
    }

    private void LoadOver( AsyncOperation rq)
    {
        print("加载结束");
        //asset 是资源对象 加载完毕过后 就能够得到它
        tex = (rq as ResourceRequest).asset as Texture;
        print(Time.frameCount);
    }

    private void OnGUI()
    {
        if( tex != null)
            GUI.DrawTexture(new Rect(0, 0, 100, 100), tex);
    }
}
