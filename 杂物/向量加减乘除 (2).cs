using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float zOffect = 4;
    public float yOffect = 7;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        //摄像机的位置 等于目标的位置 进行向量偏移
        //先朝目标对象的 面朝向的反方向平移4米 再朝目标的头顶位置 平移7米
        this.transform.position = target.position + -target.forward * zOffect + target.up * yOffect;

        this.transform.LookAt(target);
    }
}
