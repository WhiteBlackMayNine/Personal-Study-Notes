using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Lesson4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �ع�ͨ��ScriptableObject�����ǳ־û�����
        MyData data = ScriptableObject.CreateInstance<MyData>();
        #endregion

        #region ֪ʶ��� �ع����ݳ־û�
        //Ӳ��<=>�ڴ�
        //ʹ������ʱ��Ӳ���ж�ȡ
        //���ݸı�󱣴浽Ӳ����
        //��Ϸ�˳�����رպ�������Ϣ�ᱻ�洢��Ӳ���ϣ��ﵽ�־û���Ŀ��

        //���ǽ��ڹ������ݳ־û����֪ʶ
        //PlayerPrefs
        //XML
        //Json
        //2����

        //ScriptableObject�����ʺ����������ݳ־û�����
        //�������ǿ�����������ѧ�������ݳ־û����� ����־û�
        #endregion

        #region ֪ʶ���� ����Json���ScriptableObject�洢����
        data.PrintInfo();

        //data.i = 9999;
        //data.f = 6.6f;
        //data.b = true;
        //�����ݶ��� ���л�Ϊ json�ַ���
        //string str = JsonUtility.ToJson(data);
        //print(str);
        ////���������л���Ľ�� ����ָ��·������
        //File.WriteAllText(Application.persistentDataPath + "/testJson.json", str);
        //print(Application.persistentDataPath);
        #endregion

        #region ֪ʶ���� ����Json���ScriptableObject��ȡ����
        //�ӱ��ض�ȡ Json�ַ���
        string str = File.ReadAllText(Application.persistentDataPath + "/testJson.json");
        //����json�ַ��������л������� �����ݸ��ǵ����ݶ�����
        JsonUtility.FromJsonOverwrite(str, data);
        data.PrintInfo();
        #endregion

        #region �ܽ�
        //����ScriptableObject������
        //����������Ϸ�������й������޷����־û�
        //���ǿ������� PlayerPrefs��XML��Json��2���Ƶȵȷ�ʽ
        //������Դﵽ�������־û���Ŀ��

        //�����Ҹ��˲�������������ScriptableObject�������ݳ־û�
        //�е㻭���������˼��
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

