using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson6 : MonoBehaviour
{
    public BulletInfo info;
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ʹ��Ԥ���������ܴ��ڵ��ڴ��˷�����
        //����ֻ�ò��������
        //����������˼��ȥ�����������ǿ��ܴ����ڴ��˷ѵ������

        //�������ӵ�����Ϊ��
        #endregion

        #region ֪ʶ��� ����˵�� ����ScriptableObject���ݶ��� ���ӽ�Լ�ڴ�

        #endregion

        #region �ܽ�
        //���ڲ�ͬ����ʹ����ͬ����ʱ
        //���ǿ���ʹ��ScriptableObject����Լ�ڴ�
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            info.speed += 1;
    }
}
