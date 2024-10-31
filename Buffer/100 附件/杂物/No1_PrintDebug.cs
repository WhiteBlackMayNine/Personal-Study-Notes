using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：打印函数
//***************************************** 
public class No1_PrintDebug : MonoBehaviour
{
    void Start()
    {
        print("这节开始我们正式进入API课程的学习");
        Debug.Log("UnityAPI常用方法类与组件");
        //1.测试
        Calculate();
        //2.检错
        Debug.Log("2+3的计算开始了");
        int a = 1;
        Debug.Log("a赋值完成，具体值是："+a);
        int b = 3;
        Debug.Log("b赋值完成，具体值是："+b);
        int num = a + b;
        Debug.Log("2+3="+num);

        Debug.LogWarning("这是一个警告！");
        Debug.LogError("这里有报错！");
    }

    void Update()
    {

    }

    private void Calculate()
    {
        //Debug.Log("计算");
        Add();
        Subtract();
    }

    private int Add()
    {
        //Debug.Log("加法");
        return 1 + 1;
    }

    private int Subtract()
    {
        //Debug.Log("减法");
        return 1 - 1;
    }
}

public class TestDebugAndPrintClass:MonoBehaviour
{ 
    public void TestPrint()
    {
        print("使用print必须让当前类继承自monobehavior");
    }

    public void TestDebug()
    {
        Debug.Log("使用debug再普通类里进行输出");
    }
}
