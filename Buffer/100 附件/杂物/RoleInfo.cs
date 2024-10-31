using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RoleInfo", menuName = "ScriptableObject/��ɫ��Ϣ")]
public class RoleInfo : SingleScriptableObject<RoleInfo>
{
    //�Զ��������û�а취�� Inspector ���� �༭��
    //��Ҫ���������� ���л�����
    //���ܱ༭
    [System.Serializable]
    public class RoleData
    {
        public int id;
        public string res;
        public int atk;
        public string tips;
        public int lockMoney;
        public int type;
        public string hitEff;

        public void Print()
        {
            Debug.Log(id);
            Debug.Log(res);
            Debug.Log(atk);
            Debug.Log(tips);
            Debug.Log(lockMoney);
            Debug.Log(type);
            Debug.Log(hitEff);
        }
    }

    public List<RoleData> roleList;
}


