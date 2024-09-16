using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool.Singleton;
using System;

public class GameTimeManager : Singleton<GameTimeManager>
{

    #region ����

    [SerializeField] private int _initMaxTimerCount;//Ĭ������ʼ��ʱ������
    private Queue<GameTimer> _notWorkerTimer = new Queue<GameTimer>();//�����������еĿ��м�ʱ��
    private List<GameTimer> _workeringTimer = new List<GameTimer>();//�������浱ǰ���ڹ����еļ�ʱ��

    //_workeringTimer��Ҫ��̬����ӡ�ɾ���ͷ�����Ϸ��ʱ������ ���ʹ�� List
    #endregion

    #region �������ں���

    private void Start()
    {
        InitTimerManager();
    }

    private void Update()
    {
        UpdateWorkingTimer();
    }

    #endregion

    #region �������

    #region ��ʼ����ʱ��

    /// <summary>
    /// ��ʼ����ʱ��
    /// </summary>
    private void InitTimerManager()
    {
        for (int i = 0; i < _initMaxTimerCount; i++)
        {
            CreatTimer();
        }
    }

    #endregion

    #region ������ʱ������

    /// <summary>
    /// ������ʱ������
    /// </summary>
    private void CreatTimer()
    {
        //new һ������
        var timer = new GameTimer();

        //��new �����Ķ���ŵ�������
        _notWorkerTimer.Enqueue(timer);
    }

    #endregion

    #region �ⲿ��ȡ��ʱ��

    /// <summary>
    /// ��ȡ��ʱ��
    /// </summary>
    /// <param name="time"></param>
    /// <param name="task"></param>
    public void TryGetTimer(float time, Action task)
    {
        if (_notWorkerTimer.Count == 0)
        {
            //������м�ʱ��������Ϊ0 ��û��һ�����м�ʱ���ɹ�ʹ��

            //����һ����ʱ��
            CreatTimer();
            //�������Ŀ��м�ʱ���ó���
            var timer = _notWorkerTimer.Dequeue();
            //���ⲿ���õļ�ʱʱ���������������ʱ����
            timer.StartTimer(time, task);
            //��� �������ʱ�����뵽������
            _workeringTimer.Add(timer);
        }
        else
        {
            //����п��м�ʱ���ɹ�ʹ��

            //�����м�ʱ���ó���
            var timer = _notWorkerTimer.Dequeue();
            //���ⲿ���õļ�ʱʱ���������������ʱ����
            timer.StartTimer(time, task);
            //��� �������ʱ�����뵽������
            _workeringTimer.Add(timer);
        }
    }

    #endregion

    #region ���¼�ʱ����Ϣ

    /// <summary>
    /// ���¼�ʱ��
    /// </summary>
    private void UpdateWorkingTimer()
    {
        if (_workeringTimer.Count == 0)
        {
            //��������еļ�ʱ������Ϊ0 ��ô��û�и�����Ϣ�ı�Ҫ  
            //�Ͼ����ǿ��е�

            return;
        }

        for (int i = 0; i < _workeringTimer.Count; i++)
        {
            if (_workeringTimer[i].GetTimerState() == TimerState.WORKING)
            {
                //�Ե�ǰ���еĵ�i����ʱ����״̬���бȽ�  ���Ϊ ������

                //���������ʱ������Ϣ
                _workeringTimer[i].UpdateTimer();
            }
            else
            {
                //ִ�е����� ��˵����ǰ���еĵ�i����ʱ���������Ѿ������

                //�������ʱ���ŵ����м�ʱ����
                _notWorkerTimer.Enqueue(_workeringTimer[i]);
                //���ü�ʱ��
                _workeringTimer[i].ResetTimer();
                //�������ʱ���ӹ����м�ʱ���б����Ƴ�
                _workeringTimer.Remove(_workeringTimer[i]);
            }
        }
    }

    #endregion

    #endregion

}
