using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMove : MonoBehaviour
{
    //面朝向移动速度
    public float moveSpeed = 5;
    //左右曲线移动变化的速度
    public float changeSpeed = 2;
    //左右曲线移动距离控制
    public float changeSize = 0.5f;

    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        //面朝向移动
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //左右曲线移动
        time += Time.deltaTime * changeSpeed;
        this.transform.Translate(Vector3.right * changeSize * Time.deltaTime * Mathf.Sin(time));
    }
}
