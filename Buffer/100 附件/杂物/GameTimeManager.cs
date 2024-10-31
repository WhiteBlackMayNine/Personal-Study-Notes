using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool.Singleton;
using System;

public class GameTimeManager : Singleton<GameTimeManager>
{

    #region 变量

    [SerializeField] private int _initMaxTimerCount;//默认最大初始计时器数量
    private Queue<GameTimer> _notWorkerTimer = new Queue<GameTimer>();//用来保存所有的空闲计时器
    private List<GameTimer> _workeringTimer = new List<GameTimer>();//用来保存当前正在工作中的计时器

    //_workeringTimer需要动态地添加、删除和访问游戏计时器对象 因此使用 List
    #endregion

    #region 生命周期函数

    private void Start()
    {
        InitTimerManager();
    }

    private void Update()
    {
        UpdateWorkingTimer();
    }

    #endregion

    #region 函数相关

    #region 初始化计时器

    /// <summary>
    /// 初始化计时器
    /// </summary>
    private void InitTimerManager()
    {
        for (int i = 0; i < _initMaxTimerCount; i++)
        {
            CreatTimer();
        }
    }

    #endregion

    #region 创建计时器对象

    /// <summary>
    /// 创建计时器对象
    /// </summary>
    private void CreatTimer()
    {
        //new 一个对象
        var timer = new GameTimer();

        //将new 出来的对象放到队列中
        _notWorkerTimer.Enqueue(timer);
    }

    #endregion

    #region 外部获取计时器

    /// <summary>
    /// 获取计时器
    /// </summary>
    /// <param name="time"></param>
    /// <param name="task"></param>
    public void TryGetTimer(float time, Action task)
    {
        if (_notWorkerTimer.Count == 0)
        {
            //如果空闲计时器的数量为0 即没有一个空闲计时器可供使用

            //创建一个计时器
            CreatTimer();
            //将创建的空闲计时器拿出来
            var timer = _notWorkerTimer.Dequeue();
            //将外部设置的计时时长与任务传入这个计时器中
            timer.StartTimer(time, task);
            //最后 将这个计时器加入到队列中
            _workeringTimer.Add(timer);
        }
        else
        {
            //如果有空闲计时器可供使用

            //将空闲计时器拿出来
            var timer = _notWorkerTimer.Dequeue();
            //将外部设置的计时时长与任务传入这个计时器中
            timer.StartTimer(time, task);
            //最后 将这个计时器加入到队列中
            _workeringTimer.Add(timer);
        }
    }

    #endregion

    #region 更新计时器信息

    /// <summary>
    /// 更新计时器
    /// </summary>
    private void UpdateWorkingTimer()
    {
        if (_workeringTimer.Count == 0)
        {
            //如果工作中的计时器数量为0 那么就没有更新信息的必要  
            //毕竟都是空闲的

            return;
        }

        for (int i = 0; i < _workeringTimer.Count; i++)
        {
            if (_workeringTimer[i].GetTimerState() == TimerState.WORKING)
            {
                //对当前队列的第i个计时器的状态进行比较  如果为 工作中

                //更新这个计时器的信息
                _workeringTimer[i].UpdateTimer();
            }
            else
            {
                //执行到这里 就说明当前队列的第i个计时器的任务已经完成了

                //将这个计时器放到空闲计时器中
                _notWorkerTimer.Enqueue(_workeringTimer[i]);
                //重置计时器
                _workeringTimer[i].ResetTimer();
                //将这个计时器从工作中计时器列表中移除
                _workeringTimer.Remove(_workeringTimer[i]);
            }
        }
    }

    #endregion

    #endregion

}
