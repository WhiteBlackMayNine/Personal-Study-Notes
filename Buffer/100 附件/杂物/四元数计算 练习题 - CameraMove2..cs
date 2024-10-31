using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove2 : MonoBehaviour
{
    //目标对象
    public Transform target;
    //相对头顶的偏移位置
    public float headOffsetH = 1;
    //倾斜角度
    public float angle = 45;
    //默认的 摄像机离观测点的距离
    public float dis = 5;

    //距离必须是3和10之间
    public float minDis = 3;
    public float maxDis = 10;
    //鼠标中间滚动控制的移动速度
    public float roundSpeed = 1;
    //看向对象时 四元数 旋转的速度
    public float lookAtSpeed = 2;
    //跟随对象移动的 速度
    public float moveSpeed = 2;
    //当前摄像机应该在的位置
    Vector3 nowPos;

    private Vector3 nowDir;
    // Update is called once per frame
    void Update()
    {
        //实现了鼠标中键 滚动 来改变摄像机远近
        dis += Input.GetAxis("Mouse ScrollWheel")*roundSpeed;
        dis = Mathf.Clamp(dis, minDis, maxDis);

        //向头顶偏移位置
        nowPos = target.position + target.up * headOffsetH;
        //往后方偏移位置
        nowDir = Quaternion.AngleAxis(angle, target.right) * -target.forward;
        nowPos = nowPos + nowDir * dis;

        //直接把算出来的位置 进行赋值
        //this.transform.position = nowPos;
        this.transform.position = Vector3.Lerp(this.transform.position, nowPos, Time.deltaTime* moveSpeed);

        Debug.DrawLine(this.transform.position, target.position + target.up * headOffsetH);
        //这里是 通过插值运算 来缓动 看向 物体
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(-nowDir), Time.deltaTime* lookAtSpeed);
    }
}
