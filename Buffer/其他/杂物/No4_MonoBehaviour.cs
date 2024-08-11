using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：MonoBehaviour(基类)，所有Unity脚本都派生自该基类
//***************************************** 
public class No4_MonoBehaviour : MonoBehaviour
{
    void Start()
    {
        //MonoBehaviour派生自组件脚本,因此组件脚本所有的公有，保护的属性，成员变量
        //方法等功能，MonoBehaviour也都有，继承mono之后这类可以挂载到游戏物体上   
        Debug.Log("No4_MonoBehaviour组件的激活状态是："+this.enabled);
        Debug.Log("No4_MonoBehaviour组件挂载的对象名称是：" + this.name);
        Debug.Log("No4_MonoBehaviour组件挂载的标签名称是：" + this.tag);
        Debug.Log("No4_MonoBehaviour组件是否已激活并启用Behaviour：" + this.isActiveAndEnabled);
        print("Trigger");
        //Destroy();
        //FindObjectsOfType();
        //Instantiate();
    }
}
