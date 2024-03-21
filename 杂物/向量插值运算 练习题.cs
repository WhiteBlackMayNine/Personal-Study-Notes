using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float zOffect = 4;
    public float yOffect = 7;
    public Transform target;

    private Vector3 targetPos;
    public float moveSpeed;
    private Vector3 startPos;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        //先快后慢的移动
        //if(targetPos != target.position + -target.forward * zOffect + target.up * yOffect)
        //{
        //    targetPos = target.position + -target.forward * zOffect + target.up * yOffect;
        //}
        ////摄像机的位置 等于目标的位置 进行向量偏移
        ////先朝目标对象的 面朝向的反方向平移4米 再朝目标的头顶位置 平移7米
        //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime*moveSpeed);

        //匀速移动
        if (targetPos != target.position + -target.forward * zOffect + target.up * yOffect)
        {
            targetPos = target.position + -target.forward * zOffect + target.up * yOffect;
            startPos = this.transform.position;
            time = 0;
        }
        time += Time.deltaTime;
        this.transform.position = Vector3.Lerp(startPos, targetPos, time* moveSpeed);

        this.transform.LookAt(target);
    }
}
