using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson5 : MonoBehaviour
{
    public RoleInfo info;
    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ ScriptableObject�����ļ�Ϊʲô�ǳ��ʺ������������ļ�?
        //1.�����ļ�����������Ϸ����֮ǰ������
        //2.�����ļ�����������Ϸ����ʱֻ�������ʹ�ã�����ı�����
        //3.��Unity��Inspector���ڽ������ø��ӵķ���
        #endregion

        #region ֪ʶ��� ��������
        //��ǰ���ǵĳ������÷�ʽ
        //��������֮ǰѧϰ���� ���ݳ־û��Ĳ������е����ݽ������õ�
        //���� xml���� json���� excel����

        for (int i = 0; i < info.roleList.Count; i++)
        {
            info.roleList[i].Print();
        }
        #endregion

        #region �ܽ�
        //ֻ�ò���
        //���Ҿ�����������õ�����
        //�ǳ��ʺ�ʹ��ScriptableObject

        //���ǿ�������ScriptableObject�����ļ� �������༭����ع���
        //���磺Unity���õļ��ܱ༭�����ؿ��༭���ȵ�
        //      ���ǲ���Ҫ�ѱ༭�����ɵ��������ɱ�������ļ�������ֱ��ͨ��ScriptableObject���д洢
        //      ��Ϊ���ñ༭��ֻ���ڱ༭ģʽ�����У��༭ģʽ��ScriptableObject�߱����ݳ־û�������
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
