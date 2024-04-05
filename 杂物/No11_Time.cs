using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：从Unity获取时间信息的接口
//***************************************** 
public class No11_Time : MonoBehaviour
{
    void Start()
    {
        //Time.timeScale = 10;
    }

    void Update()
    {
        print(Time.deltaTime + ",完成上一帧所用的时间（以秒为单位）");
        print(Time.fixedDeltaTime + ",执行物理或者其他固定帧率更新的时间间隔");
        print(Time.fixedTime + ",自游戏启动以来的总时间（以物理或者其他固定帧率更新的时间间隔累计计算的）");
        print(Time.time + ",游戏开始以来的总时间");
        print(Time.realtimeSinceStartup + ",游戏开始以来的实际时间");
        print(Time.smoothDeltaTime + ",经过平滑处理的Time.deltaTime的时间");
        print(Time.timeScale + ",时间流逝的标度,可以用来慢放动作");
        print(Time.timeSinceLevelLoad + ",自加载上一个关卡以来的时间");
    } 
}
