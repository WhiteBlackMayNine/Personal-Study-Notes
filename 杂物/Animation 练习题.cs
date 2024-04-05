using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson31_Exercises : MonoBehaviour
{
    private Animation animation;

    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        #region 练习题
        //使用Animation相关知识
        //请自己用Unity自带几何体拼凑一个机器人，有手有脚
        //为它制作两个动作，一个待机动作（站立不动），一个走路动作
        //我们可以通过键盘控制它移动切换动作

        animation = this.GetComponent<Animation>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.W) )
        {
            animation.CrossFade("Move");
            isMoving = true;
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            animation.CrossFade("Idle");
            isMoving = false;
        }

        if (isMoving)
            this.transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        
    }
}
