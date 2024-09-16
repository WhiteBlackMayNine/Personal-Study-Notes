using System.Collections.Generic;
using System;
using GGG.Tool;
using GGG.Tool.Singleton;
using UnityEngine;

public class GameEventManager : SingletonNonMono<GameEventManager>
{

    private Dictionary<string, IEventHelp> _eventCenter = new Dictionary<string, IEventHelp>();

    #region �ӿ�IEventHelp
    private interface IEventHelp
    {
        //��дһ���ӿ� ����ͬ���͵��¼�
        //��Ϊ��ͬ���¼�����Ĳ������ܲ�ͬ
        //����ӿ� ��Ҫ�����������ֵ����ͬ�������࣬���治��Ҫдʲô�߼�
        //ֻ��Ҫ����̳�����ӿ�
    }

    #endregion

    #region EventHelp

    //�����޲ε�
    private class EventHelp : IEventHelp
    {
        private event Action _action;//ֻ�����ڲ����ã��ⲿ�޷�����
        public EventHelp(Action action)//���캯��
        {
            //���ǵ�һ��ʵ����ʱ��Ҳ����˵ֻ��ִ����һ��
            _action = action;
        }

        /// <summary>
        /// ����ע����¼�
        /// </summary>
        /// <param name="action"></param>
        public void AddCall(Action action)//����Ҫע����������ʱ��ʹ���������������������Newһ��
        {
            _action += action;
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        public void Call()
        {
            _action?.Invoke();
        }

        /// <summary>
        /// �Ƴ��¼�
        /// </summary>
        /// <param name="action"></param>
        public void Remove(Action action)
        {
            _action -= action;
        }
    }
    //��һ��������
    private class EventHelp<T> : IEventHelp
    {
        private event Action<T> _action;//ֻ�����ڲ����ã��ⲿ�޷�����

        public EventHelp(Action<T> action)
        {
            //���ǵ�һ��ʵ����ʱ��Ҳ����˵ֻ��ִ����һ��
            _action = action;
        }

        //�����¼���ע�ắ��
        public void AddCall(Action<T> action)//����Ҫע����������ʱ��ʹ���������������������Newһ��
        {
            _action += action;
        }

        //�����¼�
        public void Call(T value)
        {
            _action?.Invoke(value);
        }

        //�Ƴ��¼�
        public void Remove(Action<T> action)
        {
            _action -= action;
        }
    }
    //������������
    private class EventHelp<T1, T2> : IEventHelp
    {
        private event Action<T1, T2> _action;//ֻ�����ڲ����ã��ⲿ�޷�����

        public EventHelp(Action<T1, T2> action)
        {
            //���ǵ�һ��ʵ����ʱ��Ҳ����˵ֻ��ִ����һ��
            _action = action;
        }

        //�����¼���ע�ắ��
        public void AddCall(Action<T1, T2> action)//����Ҫע����������ʱ��ʹ���������������������Newһ��
        {
            _action += action;
        }

        //�����¼�
        public void Call(T1 value, T2 value1)
        {
            _action?.Invoke(value, value1);
        }

        //�Ƴ��¼�
        public void Remove(Action<T1, T2> action)
        {
            _action -= action;
        }
    }
    //����ж����������ô��������
    private class EventHelp<T1, T2, T3> : IEventHelp
    {
        private event Action<T1, T2, T3> _action;//ֻ�����ڲ����ã��ⲿ�޷�����

        public EventHelp(Action<T1, T2, T3> action)
        {
            //���ǵ�һ��ʵ����ʱ��Ҳ����˵ֻ��ִ����һ��
            _action = action;
        }

        //�����¼���ע�ắ��
        public void AddCall(Action<T1, T2, T3> action)//����Ҫע����������ʱ��ʹ���������������������Newһ��
        {
            _action += action;
        }

        //�����¼�
        public void Call(T1 value, T2 value1, T3 value2)
        {
            _action?.Invoke(value, value1, value2);
        }

        //�Ƴ��¼�
        public void Remove(Action<T1, T2, T3> action)
        {
            _action -= action;
        }
    }
    private class EventHelp<T1, T2, T3, T4> : IEventHelp
    {
        private event Action<T1, T2, T3, T4> _action;//ֻ�����ڲ����ã��ⲿ�޷�����

        public EventHelp(Action<T1, T2, T3, T4> action)
        {
            //���ǵ�һ��ʵ����ʱ��Ҳ����˵ֻ��ִ����һ��
            _action = action;
        }

        //�����¼���ע�ắ��
        public void AddCall(Action<T1, T2, T3, T4> action)//����Ҫע����������ʱ��ʹ���������������������Newһ��
        {
            _action += action;
        }

        //�����¼�
        public void Call(T1 value, T2 value1, T3 value2, T4 value3)
        {
            _action?.Invoke(value, value1, value2, value3);
        }

        //�Ƴ��¼�
        public void Remove(Action<T1, T2, T3, T4> action)
        {
            _action -= action;
        }
    }
    private class EventHelp<T1, T2, T3, T4, T5> : IEventHelp
    {
        private event Action<T1, T2, T3, T4, T5> _action;//ֻ�����ڲ����ã��ⲿ�޷�����

        public EventHelp(Action<T1, T2, T3, T4, T5> action)
        {
            //���ǵ�һ��ʵ����ʱ��Ҳ����˵ֻ��ִ����һ��
            _action = action;
        }

        //�����¼���ע�ắ��
        public void AddCall(Action<T1, T2, T3, T4, T5> action)//����Ҫע����������ʱ��ʹ���������������������Newһ��
        {
            _action += action;
        }

        //�����¼�
        public void Call(T1 value, T2 value1, T3 value2, T4 value3, T5 value4)
        {
            _action?.Invoke(value, value1, value2, value3, value4);
        }

        //�Ƴ��¼�
        public void Remove(Action<T1, T2, T3, T4, T5> action)
        {
            _action -= action;
        }
    }

    #endregion

    #region ��Ӽ���

    public void AddEventListening(string eventName, Action action)
    {
        //�ȴ��ֵ�����һ�� ��ΪeventName ��Ӧ�� ֵ  ��������� ֵ (���� e )
        if (_eventCenter.TryGetValue(eventName, out var e))
        {
            //����ҵ��ˣ���ô�� e ת��Ϊ EventHelp ���� ���������е� AddCall ���� ��ӽ�ȥ
            (e as EventHelp)?.AddCall(action);
        }
        else
        {
            //���û�У���ô���� �ֵ� ��������ί�� (new һ��)
            _eventCenter.Add(eventName, new EventHelp(action));
        }
    }
    public void AddEventListening<T>(string eventName, Action<T> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T>(action));
        }
    }
    public void AddEventListening<T1, T2>(string eventName, Action<T1, T2> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T1, T2>(action));
        }
    }
    public void AddEventListening<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T1, T2, T3>(action));
        }
    }
    public void AddEventListening<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3, T4>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T1, T2, T3, T4>(action));
        }
    }
    public void AddEventListening<T1, T2, T3, T4, T5>(string eventName, Action<T1, T2, T3, T4, T5> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3, T4, T5>)?.AddCall(action);
        }
        else
        {
            _eventCenter.Add(eventName, new EventHelp<T1, T2, T3, T4, T5>(action));
        }
    }

    #endregion

    #region �����¼�
    public void CallEvent(string eventName)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp)?.Call();
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ�и��¼�");
        }
    }
    public void CallEvent<T>(string eventName, T value)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T>)?.Call(value);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ�и��¼�");
        }
    }
    public void CallEvent<T1, T2>(string eventName, T1 value, T2 value1)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2>)?.Call(value, value1);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ�и��¼�");
        }
    }
    public void CallEvent<T1, T2, T3>(string eventName, T1 value, T2 value1, T3 value2)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3>)?.Call(value, value1, value2);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ�и��¼�");
        }
    }
    public void CallEvent<T1, T2, T3, T4>(string eventName, T1 value, T2 value1, T3 value2, T4 value3)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3, T4>)?.Call(value, value1, value2, value3);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ�и��¼�");
        }
    }
    public void CallEvent<T1, T2, T3, T4, T5>(string eventName, T1 value, T2 value1, T3 value2, T4 value3, T5 value4)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3, T4, T5>)?.Call(value, value1, value2, value3, value4);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷�ִ�и��¼�");
        }
    }

    #endregion

    #region �Ƴ��¼�
    public void RemoveEvent(string eventName, Action action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ����¼�");
        }
    }
    public void RemoveEvent<T>(string eventName, Action<T> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ����¼�");
        }
    }
    public void RemoveEvent<T1, T2>(string eventName, Action<T1, T2> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ����¼�");
        }
    }
    public void RemoveEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ����¼�");
        }
    }
    public void RemoveEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3, T4>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ����¼�");
        }
    }
    public void RemoveEvent<T1, T2, T3, T4, T5>(string eventName, Action<T1, T2, T3, T4, T5> action)
    {
        if (_eventCenter.TryGetValue(eventName, out var e))//�������Ƿ����
        {
            (e as EventHelp<T1, T2, T3, T4, T5>)?.Remove(action);
        }
        else
        {
            DevelopmentToos.WTF($"��ǰδ�ҵ�{eventName}���¼����޷��Ƴ����¼�");
        }
    }

    #endregion

}

