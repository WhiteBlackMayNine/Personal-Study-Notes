using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldReturnTime
{
    //记录 下次还要执行的 迭代器接口
    public IEnumerator ie;
    //记录 下次执行的时间点
    public float time;
}

public class CoroutineMgr : MonoBehaviour
{
    private static CoroutineMgr instance;
    public static CoroutineMgr Instance => instance;

    //申明存储 迭代器函数对象的 容器 用于 一会继续执行
    private List<YieldReturnTime> list = new List<YieldReturnTime>();

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void MyStartCoroutine(IEnumerator ie)
    {
        //来进行 分步走 分时间执行的逻辑

        //传入一个 迭代器函数返回的结构 那么应该一来就执行它
        //一来就先执行第一步 执行完了 如果返现 返回的true 证明 后面还有步骤
        if(ie.MoveNext())
        {
            //判断 如果yield return返回的是 数字 是一个int类型 那就证明 是需要等待n秒继续执行
            if(ie.Current is int)
            {
                //按思路 应该把 这个迭代器函数 和它下一次执行的时间点 记录下来
                //然后不停检测 时间 是否到达了 下一次执行的 时间点 然后就继续执行它
                YieldReturnTime y = new YieldReturnTime();
                //记录迭代器接口
                y.ie = ie;
                //记录时间
                y.time = Time.time + (int)ie.Current;
                //把记录的信息 记录到数据容器当中 因为可能有多个协程函数 开启 所以 用一个 list来存储
                list.Add(y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //为了避免在循环的时候 从列表里面移除内容 我们可以倒着遍历
        for (int i = list.Count - 1; i >= 0; i--)
        {
            //判断 当前该迭代器函数 是否到了下一次要执行的时间
            //如果到了 就需要执行下一步了
            if( list[i].time <= Time.time  )
            {
                if(list[i].ie.MoveNext())
                {
                    //如果是true 那还需要对该迭代器函数 进行处理
                    //如果是 int类型 证明是按秒等待
                    if(list[i].ie.Current is int)
                    {
                        list[i].time = Time.time + (int)list[i].ie.Current;
                    }
                    else
                    {
                        //该list 只是存储 处理时间相关 等待逻辑的 迭代器函数的 
                        //如果是别的类型 就不应该 存在这个list中 应该根据类型把它放入别的容器中
                        list.RemoveAt(i);
                    }
                }
                else
                {
                    //后面已经没有可以等待和执行的了 证明已经执行完毕了逻辑
                    list.RemoveAt(i);
                }
            }
        }
    }
}
