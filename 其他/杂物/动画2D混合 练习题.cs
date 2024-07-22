using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson53_Exercises : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        #region 练习题
        //通过2D混合树制作前进后退左右旋转的功能
        animator = this.GetComponent<Animator>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("x", Input.GetAxis("Horizontal"));
        animator.SetFloat("y", Input.GetAxis("Vertical"));
    }
}
