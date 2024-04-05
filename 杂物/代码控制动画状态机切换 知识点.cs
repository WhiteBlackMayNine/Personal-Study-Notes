using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson34 : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 新动画系统创建动画

        #endregion

        #region 知识点一 关键组件Animator
        //参数相关
        #endregion

        #region 知识点二 Animator中的API
        //我们用代码控制状态机切换主要使用的就是Animator提供给我们的API
        //我们知道一共有四种切换条件 int float bool trigger
        //所以对应的API也是和这四种类型有关系的
        animator = this.GetComponent<Animator>();
        //1.通过状态机条件切换动画
        //animator.SetFloat("条件名", 1.2f);
        //animator.SetInteger("条件名", 5);
        //animator.SetBool("条件名", true);
        //animator.SetTrigger("条件名");

        //animator.GetFloat("条件名");
        //animator.GetInteger("条件名");
        //animator.GetBool("条件名");

        //2.直接切换动画 除非特殊情况 不然一般不使用
        //animator.Play("状态名");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.A) )
        {
            animator.SetBool("change", true);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("change", false);
        }
    }
}
