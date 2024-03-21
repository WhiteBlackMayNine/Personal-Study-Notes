using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lesson20 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 回顾场景同步切换

        //SceneManager.LoadScene("Lesson20Test");

        #region 场景同步切换的缺点
        //在切换场景时
        //Unity会删除当前场景上所有对象
        //并且去加载下一个场景的相关信息
        //如果当前场景 对象过多或者下一个场景对象过多
        //这个过程会非常的耗时 会让玩家感受到卡顿

        //所以异步切换就是来解决该问题的
        #endregion
        #endregion

        #region 知识点二 场景异步切换
        //场景异步加载和资源异步加载 几乎一致 有两种方式

        //1.通过事件回调函数 异步加载
        //AsyncOperation ao = SceneManager.LoadSceneAsync("Lesson20Test");
        //当场景异步加载结束后 就会自动调用该事件函数 我们如果希望在加载结束后 做一些事情 那么久可以在该函数中
        //写处理逻辑
        //ao.completed += (a) =>
        //{
        //    print("加载结束");
        //};

        //ao.completed += LoadOver;


        //2.通过协程异步加载
        //需要注意的是 加载场景会把当前场景上 没有特别处理的对象 都删除了
        //所以 协程中的部分逻辑 可能是执行不了的 
        //解决思路
        //让处理场景加载的脚本依附的对象 过场景时 不被移除

        //该脚本依附的对象 过场景时 不会被 移除
        DontDestroyOnLoad(this.gameObject);

        StartCoroutine(LoadScene("Lesson20Test"));
        #endregion

        #region 总结
        //场景异步加载 和 资源异步加载 一样
        //有两种方式
        //1.通过事件回调函数
        //2.协程异步加载

        //他们的优缺点表现和资源异步加载 也是一样的
        //1.事件回调函数
        //优点：写法简单，逻辑清晰
        //缺点：只能加载完场景做一些事情 不能再加载过程中处理逻辑
        //2.协程异步加载
        //优点：可以在加载过程中处理逻辑，比如进度条更新等
        //缺点：写法较为麻烦，要通过协程

        #endregion
    }

    private void LoadOver(AsyncOperation ao)
    {
        print("LoadOver");
    }


    IEnumerator LoadScene(string name)
    {
        //第一步
        //异步加载场景
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        //Unity内部的 协程协调器 发现是异步加载类型的返回对象 那么就会等待
        //等待异步加载结束后 才会继续执行 迭代器函数中后面的步骤
        print("异步加载过程中 打印的信息");
        //协程的好处 是异步加载场景时 我可以在加载的同时 做一些别的逻辑
        //yield return ao;
        //第二步
        print("异步加载结束后 打印的信息");

        //比如 我们可以在异步加载过程中 去更新进度条
        //第一种 就是利用 场景异步加载 的进度 去更新 但是 不是特别准确 一般也不会直接用
        //while(!ao.isDone)
        //{
        //    print(ao.progress);
        //    yield return null;
        //}

        //离开循环后 就会认为场景加载结束
        //可以把进度条顶满 然后 隐藏进度条

        //第二种 就是根据你游戏的规则 自己定义 进度条变化的条件
        yield return ao;
        //场景加载结束 更新20%进度
        //接着去加载场景中 的其它信息
        //比如
        //动态加载怪物
        //这时 进度条 再更新20%
        //动态加载 场景模型
        //这时 就认为 加载结束了 进度条顶满 
        //隐藏进度条
    }

}
