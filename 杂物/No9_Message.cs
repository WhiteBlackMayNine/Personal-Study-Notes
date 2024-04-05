using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：消息的发送
//***************************************** 
public class No9_Message : MonoBehaviour
{
    void Start()
    {
        //仅发送消息给自己（以及身上的其他MonoBehaviour对象）
        gameObject.SendMessage("GetMsg");
        SendMessage("GetSrcMsg","Trigger");
        SendMessage("GetTestMsg",SendMessageOptions.DontRequireReceiver);
        //广播消息（向下发，所有子对象包括自己）
        BroadcastMessage("GetMsg");
        //向上发送消息(父对象包含自己)
        SendMessageUpwards("GetMsg");
    }

    void Update()
    {

    }

    public void GetMsg()
    {
        print("测试对象本身接收到消息了");
    }

    public void GetSrcMsg(string str)
    {
        print("测试对象本身接收到的消息为："+str);
    }
}
