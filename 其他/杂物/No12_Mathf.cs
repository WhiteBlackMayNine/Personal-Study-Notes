using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：数学库函数
//***************************************** 
public class No12_Mathf : MonoBehaviour
{
    private float endTime = 10;

    void Start()
    {
        ////静态变量
        //print(Mathf.Deg2Rad+",度到弧度换算常量");
        //print(Mathf.Rad2Deg+ ",弧度到度换算常量");
        //print(Mathf.Infinity+"正无穷大的表示形式");
        //print(Mathf.NegativeInfinity + "负无穷大的表示形式");
        //print(Mathf.PI);
        ////静态函数
        //print(Mathf.Abs(-1.2f)+ ",-1.2的绝对值");
        //print(Mathf.Acos(1)+",1（以弧度为单位）的反余弦");
        //print(Mathf.Floor(2.74f)+",小于或等于2.74的最大整数");
        //print(Mathf.FloorToInt(2.74f)+",小于或等于2.74的最大整数");
        ////a+(b-a)*t
        //print(Mathf.Lerp(1,2,0.5f)+",a和b按参数t进行线性插值");        
        //print(Mathf.LerpUnclamped(1, 2, -0.5f) + ",a和b按参数t进行线性插值");
    }

    void Update()
    {
        print("游戏倒计时：" + endTime);
        endTime = Mathf.MoveTowards(endTime,0,0.1f);      
    } 
}
