using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson7 : MonoBehaviour
{
    public AuidoPlayBase audioPlay;

    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ʲô�����ݴ����Ķ�̬��Ϊ
        //ĳЩ��Ϊ�ı仯����Ϊ���ݵĲ�ͬ������
        //���ǿ������������������Ժ�ԭ���Լ����ģʽ���֪ʶ��
        //���ScriptableObject�������ӷ���Ĺ���

        //���������Ч����Ʒʰȡ��AI�ȵȵ�

        //�����Ч�������滻ԭ���������תԭ��
        //��������ʱ�����ܻ�������Ŷ����Ч���е�һ��

        //��Ʒʰȡ�������滻ԭ���������תԭ��
        //����ʰȡһ����Ʒ����Ʒ����Ҵ�����ͬ��Ч��

        //AI
        //��ͬ���ݴ����Ĳ�ͬ��Ϊģʽ

        //Ϊ�˷�������ʹ�ã����ǿ�������ScriptableObject�Ŀ���������������Щ����
        #endregion

        #region ֪ʶ��� ����˵��

        #endregion

        audioPlay.Play(this.GetComponent<AudioSource>());
        #region �ܽ�
        //��ʵ��Щ�����еĹ��ܾ��㲻��ScriptableObjectҲ���ܹ���
        //��������˼�� ��������ļ�����ɵ�
        //����ScriptableObject�߱��Լ��ļ����ŵ�
        //1.�����������
        //2.�������ݽ�Լ�ڴ�
        //��ʵ��ĳЩ���ܵ�ʱ��ʹ��ScriptableObject����ӷ������ǵ�ʹ��
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
