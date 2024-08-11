using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：延时调用方法
//***************************************** 
public class No16_Invoke : MonoBehaviour
{
    public GameObject grisGo;

    void Start()
    {
        //调用
        //Invoke("CreateGris",3);
        InvokeRepeating("CreateGris",1,1);
        //停止
        CancelInvoke("CreateGris");
        //CancelInvoke();
        InvokeRepeating("Test",1,1);
    }

    void Update()
    {
        print(IsInvoking("CreateGris"));
        print(IsInvoking());
    }

    private void CreateGris()
    {
        Instantiate(grisGo);
    }

    private void Test()
    {
        
    }
}
