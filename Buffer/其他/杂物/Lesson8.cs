using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson8 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ΪʲôҪ����ģʽ���Ļ�ȡ����
        //����ֻ�ò��䲢��Ҫ���õ�����
        //���������ļ��е�����
        //����������Ҫ�ںܶ�ط���ȡ����
        //�������ֱ��ͨ���ڽű��� public���� ���� ��̬����
        //����ڶദʹ�ã�����ںܶ��ظ����룬Ч�ʽϵ�
        //������ǽ���������ͨ������ģʽ����ȥ��ȡ
        //��������Ч�ʣ����ٴ�����
        #endregion

        #region ֪ʶ��� ʵ�ֵ���ģʽ����ȡ����
        //֪ʶ��
        //������󡢵���ģʽ�����͵ȵ�

        //���ǿ���ʵ��һ��ScriptableObject���ݵ���ģʽ����
        //������ֻ��Ҫ������̳иû���
        //�Ϳ���ֱ�ӻ�ȡ������
        //��������Ҫȥͨ�� public���� �� ��Դ��̬����

        print(RoleInfo.Instance.roleList[0].id);
        print(RoleInfo.Instance.roleList[1].tips);

        print(TestData.Instance.i);
        print(TestData.Instance.b);
        #endregion

        #region �ܽ�
        //���ֻ���Ƚ��ʺ� �������� �Ĺ���ͻ�ȡ
        //�����ǵ������� ֻ�ò��䣬������Ψһ��ʱ�򣬿���ʹ�ø÷�ʽ������ǵĿ���Ч��
        //�ڴ˻�������Ҳ���Ը����Լ���������б���
        //����������ݳ־û��Ĺ��ܣ������ݴ�json�ж�ȡ�����ṩ�������ݵķ���
        //���ǲ���������ScriptableObject���������ݳ־û�����
        //���������ⷽ�����������
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
