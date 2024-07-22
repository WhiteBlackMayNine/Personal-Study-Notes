using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson59 : MonoBehaviour
{
    private CharacterController cc;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 角色控制器是什么？
        //角色控制器是让角色可以受制于碰撞，但是不会被刚体所牵制
        //如果我们对角色使用刚体判断碰撞，可能会出现一些奇怪的表现
        //比如：
        //1.在斜坡上往下滑动
        //2.不加约束的情况碰撞可能让自己被撞飞
        //等等
        //而角色控制器会让角色表现的更加稳定
        //Unity提供了角色控制器脚本专门用于控制角色

        //注意：
        //添加角色控制器后，不用再添加刚体
        //能检测碰撞函数
        //能检测触发器函数
        //能被射线检测
        #endregion

        #region 知识点二 角色控制器的使用
        //1.参数相关
        //2.代码相关
        cc = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        //关键参数
        //是否接触了地面
        if ( cc.isGrounded )
        {
            print("接触地面了");
        }
        //关键方法
        //受重力作用的移动
        //cc.SimpleMove(Vector3.forward * 10 * Time.deltaTime);
        //不受重力作用的移动
        //cc.Move(Vector3.forward * 10 * Time.deltaTime);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Speed", (int)Input.GetAxisRaw("Vertical"));

        cc.Move(this.transform.forward * 80 * Time.deltaTime * Input.GetAxisRaw("Vertical"));

        if (cc.isGrounded)
        {
            print("接触地面了");
        }
    }

    //当角色控制器想要判断和别的碰撞器产生碰撞时 使用该函数
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print(hit.collider.gameObject.name);
    }

    //对角色控制器没用 
    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("碰撞触发");
    //}

    //可以检测触发器
    private void OnTriggerEnter(Collider other)
    {
        print("触发器触发");
    }

}
