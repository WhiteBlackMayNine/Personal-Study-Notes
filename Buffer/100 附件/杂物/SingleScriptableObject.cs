using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//T�� �����Ǽ̳� ScriptableObject ����
public class SingleScriptableObject<T> :ScriptableObject where T:ScriptableObject
{
    private static T instance;

    public static T Instance
    {
        get
        {
            //���Ϊ�� ����Ӧ��ȥ��Դ·���¼��� ��Ӧ�� ������Դ�ļ�
            if (instance == null)
            {
                //���Ƕ���������
                //1.���е� ������Դ�ļ������� Resources�ļ����µ�ScriptableObject��
                //2.��Ҫ���õ� Ψһ��������Դ�ļ��� ���Ƕ�һ�����򣺺�������һ����
                // T �����������ͣ�ͨ����� typeof �õ�����
                instance = Resources.Load<T>("ScriptableObject/" + typeof(T).Name);
            }
            //���û������ļ� Ϊ�˰�ȫ��� ���ǿ�������ֱ�Ӵ���һ������
            if(instance==null)
            {
                instance = CreateInstance<T>();
            }
            //�������������� ��json���ж�ȡ���ݣ������Ҳ�������ScriptableObject�������ݳ־û�

            return instance;
        }
    }
}
