using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    public MyData data;
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ScriptableObject�ķǳ־û�����ָ����ʲô
        //ָ���ǲ����ڱ༭��ģʽ�����ڷ����� ����־û�������
        //���ǿ��Ը����Լ���������ʱ������Ӧ���ݶ������ʹ��
        //�ͺ���ֱ��newһ�����ݽṹ�����
        #endregion

        #region ֪ʶ��� �������ScriptableObject���ɷǳ־û�������
        //����ScriptableObject�еľ�̬���� CreateInstance<>()
        //�÷�������������ʱ������ָ���̳�ScriptableObject�Ķ���
        //�ö���ֻ�������ڴ浱�У����Ա�GC
        //����һ�ξʹ���һ��

        //ͨ�����ַ�ʽ�������������ݶ��� �������Ĭ��ֵ �����ܵ��ű������õ�Ӱ��
        //data = ScriptableObject.CreateInstance("MyData") as MyData;
        data = ScriptableObject.CreateInstance<MyData>();

        data.PrintInfo();
        #endregion

        #region ֪ʶ���� ScriptableObject�ķǳ־û����ݴ��ڵ�����
        //ֻ��ϣ��������ʱ����һ��Ψһ�����ݿ���ʹ��
        //������������ֲ�̫ϣ������Ϊ������Դ�ļ��˷�Ӳ�̿ռ�
        //��ôScriptableObject�ķǳ־û����ݾ����˴��ڵ�����
        //�����ص���
        //ֻ������ʱʹ�ã��ڱ༭��ģʽ��Ҳ���ᱣ���ڱ���
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
