using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2 : MonoBehaviour
{
    public MyData data;
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ScriptableObject�����ļ���ʹ��
        //1.ͨ��Inspector�е�public�������й���
        //1-1.����һ�������ļ�
        //1-2.�ڼ̳�MonoBehaviour�������������������͵ĳ�Ա
        //    ��Inspector���ڽ��й���
        //data.PrintInfo();

        //2.ͨ����Դ���ص���Ϣ����
        //���������ļ���Դ
        //ע�⣺Resources��AB����Addressables��֧�ּ��ؼ̳�ScriptableObject�������ļ�
        data = Resources.Load<MyData>("MyDataTest");
        data.PrintInfo();

        //ע�⣺�������������ͬһ�����������ļ������ǹ������һ������
        //     ��Ϊ�����ö��������������κεط��޸ĺ������ط�Ҳ�ᷢ���ı�
        #endregion

        #region ֪ʶ��� ScriptableObject���������ں���
        //ScriptableObject��MonoBehavior������
        //��Ҳ�����������ں���
        //�����������ں�������������
        //��Ҫ���˽⣬һ������ʹ�ý���

        //Awake �����ļ�����ʱ����

        //OnDestroy ScriptableObject ���󽫱�����ʱ����
        //OnDisable ScriptableObject ��������ʱ���������¼��ؽű�����ʱ ����
        //OnEnable ScriptableObject �������߼��ض���ʱ����

        //OnValidate �༭���Ż���õĺ�����Unity�ڼ��ؽű�����Inspector�����и���ֵʱ����
        #endregion

        #region ֪ʶ���� ScriptableObject�ô�������
        //1.�༭���е����ݳ־û�
        //ͨ�������޸����ݶ��������ݣ���Ӱ�������ļ�
        //�൱�ڴﵽ�˱༭�������ݳ־û���Ŀ��
        //(�����ݳ־û� ֻ���ڱ༭ģʽ�µĳ־�,��������ʱ�����ᱣ������)
        data.i = 9999;
        data.f = 5.5f;
        data.b = false;

        //2.��������
        //�������������ͬһ�������ļ�
        //�൱�����Ǹ�����һ�����ݣ��ڴ��ϸ��ӽ�Լ�ռ�
        #endregion

        #region �ܽ�
        //��ʵ����������������Դ�ļ�������԰�������һ�ּ�¼���ݵ���Դ
        //����ʹ�÷�ʽ����������ǰʹ��Unity���е�������Դ������һ����
        //���磺Ԥ���塢��Ƶ�ļ�����Ƶ�ļ��������������ļ���������ȵ�
        //ֻ����ͨ���̳�ScriptableObject�����ɵ�������Դ�ļ�������Ҫ�Ǻ�������ص�
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
