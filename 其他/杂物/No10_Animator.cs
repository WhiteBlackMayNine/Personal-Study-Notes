using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：动画
//***************************************** 
public class No10_Animator : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        //animator.Play("Run");

        //animator.speed = 5;
        //animator.speed = 0.3f;

        //animator.SetFloat("Speed",1);
        //animator.SetBool("Dead",false);
        //animator.SetInteger("HP",100);
        //print("当前速度参数的值是："+animator.GetFloat("Speed"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.CrossFade("Walk",1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //以标准单位化时间进行淡入淡出效果来播放动画
            animator.CrossFade("Run", 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //以秒为单位进行淡入淡出效果来播放动画
            animator.CrossFadeInFixedTime("Run", 0.5f);
        }
        animator.SetFloat("Speed",Input.GetAxisRaw("Horizontal"));
    } 
}
