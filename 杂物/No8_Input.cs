using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：访问输入系统的接口类
//***************************************** 
public class No8_Input : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        ////连续检测（移动）
        //print("当前玩家输入的水平方向的轴值是："+Input.GetAxis("Horizontal"));
        //print("当前玩家输入的垂直方向的轴值是：" + Input.GetAxis("Vertical"));
        //print("当前玩家输入的水平方向的边界轴值是：" + Input.GetAxisRaw("Horizontal"));
        //print("当前玩家输入的垂直方向的边界轴值是：" + Input.GetAxisRaw("Vertical"));
        //print("当前玩家鼠标水平移动增量是："+Input.GetAxis("Mouse X"));
        //print("当前玩家鼠标垂直移动增量是：" + Input.GetAxis("Mouse Y"));

        //连续检测（事件）
        if (Input.GetButton("Fire1"))
        {
            print("当前玩家正在使用武器1进行攻击！");
        }
        if (Input.GetButton("Fire2"))
        {
            print("当前玩家正在使用武器2进行攻击！");
        }
        if (Input.GetButton("RecoverSkill"))
        {
            print("当前玩家使用了恢复技能回血！");
        }
        //间隔检测（事件）
        if (Input.GetButtonDown("Jump"))
        {
            print("当前玩家按下跳跃键");
        }
        if (Input.GetButtonUp("Squat"))
        {
            print("当前玩家松开蹲下建");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("当前玩家按下Q键");
        }
        if (Input.anyKeyDown)
        {
            print("当前玩家按下了任意一个按键，游戏开始");
        }
        if (Input.GetMouseButton(0))
        {
            print("当前玩家按住鼠标左键");
        }
        if (Input.GetMouseButtonDown(1))
        {
            print("当前玩家按下鼠标右键");
        }
        if (Input.GetMouseButtonUp(2))
        {
            print("当前玩家抬起鼠标中键（从按下状态松开滚轮）");
        }
    } 
}
