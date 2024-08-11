using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region 计时器状态

public enum TimerState
{
    NOTWORKING,//空闲中
    WORKING,//工作中
    DONE//已完成
}

#endregion

public class GameTimer 
{
    #region 变量

    private float _startTime;//计时的时长，由外部传入
    private Action _task;//计时结束后执行的委托，由外部传入
    private bool _isStopTimer;//是否停用当前计时器
    private TimerState _timerState;//计时器的状态

    #endregion

    #region 函数

    /// <summary>
    /// 开始计时
    /// </summary>
    /// <param name="time">计时时长</param>
    /// <param name="task">调用任务</param>
    public void StartTimer(float time,Action task)
    {
        //这个函数用来接受外部传入的数据 并将状态改为 工作中
        _startTime = time;
        _task = task;
        _isStopTimer = false;
        _timerState = TimerState.WORKING;
    }

    /// <summary>
    /// 更新计时器
    /// </summary>
    public void UpdateTimer()
    {
        //这个函数用来进行对计时器的更新
        if (_isStopTimer)//如果这个计时器被停用，直接返回
        {
            return;
        }

        _startTime -= Time.deltaTime;//计时时长减少

        if(_startTime < 0f)
        {
            //如果_startTime < 0f 那么说明计时已经完成可以开始执行任务了
            _task?.Invoke();//检测是否为空，如果不为空就执行委托里面的函数
            _timerState = TimerState.DONE;//将状态改为 已完成
            _isStopTimer = true;//停用计时器
        }
    }

    /// <summary>
    /// 确定计时器状态
    /// </summary>
    /// <returns></returns>
    public TimerState GetTimerState() => _timerState;

    /// <summary>
    /// 重置计时器信息
    /// </summary>
    public void ResetTimer()
    {
        _startTime = 0f;
        _task = null;
        _isStopTimer = true;
        _timerState = TimerState.NOTWORKING;
    }
    
    public GameTimer()
    {
        //当new时，直接调用重置函数  相当于进行一次赋值
        ResetTimer();
    }

    #endregion

}
