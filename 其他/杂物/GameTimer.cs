using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region ��ʱ��״̬

public enum TimerState
{
    NOTWORKING,//������
    WORKING,//������
    DONE//�����
}

#endregion

public class GameTimer 
{
    #region ����

    private float _startTime;//��ʱ��ʱ�������ⲿ����
    private Action _task;//��ʱ������ִ�е�ί�У����ⲿ����
    private bool _isStopTimer;//�Ƿ�ͣ�õ�ǰ��ʱ��
    private TimerState _timerState;//��ʱ����״̬

    #endregion

    #region ����

    /// <summary>
    /// ��ʼ��ʱ
    /// </summary>
    /// <param name="time">��ʱʱ��</param>
    /// <param name="task">��������</param>
    public void StartTimer(float time,Action task)
    {
        //����������������ⲿ��������� ����״̬��Ϊ ������
        _startTime = time;
        _task = task;
        _isStopTimer = false;
        _timerState = TimerState.WORKING;
    }

    /// <summary>
    /// ���¼�ʱ��
    /// </summary>
    public void UpdateTimer()
    {
        //��������������жԼ�ʱ���ĸ���
        if (_isStopTimer)//��������ʱ����ͣ�ã�ֱ�ӷ���
        {
            return;
        }

        _startTime -= Time.deltaTime;//��ʱʱ������

        if(_startTime < 0f)
        {
            //���_startTime < 0f ��ô˵����ʱ�Ѿ���ɿ��Կ�ʼִ��������
            _task?.Invoke();//����Ƿ�Ϊ�գ������Ϊ�վ�ִ��ί������ĺ���
            _timerState = TimerState.DONE;//��״̬��Ϊ �����
            _isStopTimer = true;//ͣ�ü�ʱ��
        }
    }

    /// <summary>
    /// ȷ����ʱ��״̬
    /// </summary>
    /// <returns></returns>
    public TimerState GetTimerState() => _timerState;

    /// <summary>
    /// ���ü�ʱ����Ϣ
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
        //��newʱ��ֱ�ӵ������ú���  �൱�ڽ���һ�θ�ֵ
        ResetTimer();
    }

    #endregion

}
