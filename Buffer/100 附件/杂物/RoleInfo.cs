using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RoleInfo", menuName = "ScriptableObject/角色信息")]
public class RoleInfo : SingleScriptableObject<RoleInfo>
{
    //自定义的类是没有办法在 Inspector 窗口 编辑的
    //需要添加这个特性 序列化特性
    //才能编辑
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


