using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：生成随机数的类
//***************************************** 
public class No13_Random : MonoBehaviour
{
    void Start()
    {
        ////静态变量
        //print(Random.rotation+",随机出的旋转数是(以四元数形式表示)");
        //print(Random.rotation.eulerAngles+",四元数转换成欧拉角");
        //print(Quaternion.Euler(Random.rotation.eulerAngles)+",欧拉角转四元数");
        //print(Random.value+",随机出[0,1]之间的浮点数");
        //print(Random.insideUnitCircle+",在（-1，-1）~（1,1）范围内随机生成的一个vector2");
        print(Random.state+",当前随机数生成器的状态");
        //静态函数
        print(Random.Range(0,4)+",在区间[0,4）（整形重载包含左Min,不包含右Max）产生的随机数");
        print(Random.Range(0, 4f) + ",在区间[0,4）（浮点形重载包含左Min,包含右Max）产生的随机数");
        Random.InitState(1);
        print(Random.Range(0,4f)+",设置完随机数状态之后在[0,4]区间内生成的随机数");
    }

    void Update()
    {

    } 
}
