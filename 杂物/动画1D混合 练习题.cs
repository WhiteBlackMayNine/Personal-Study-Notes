using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson52_Exercises : MonoBehaviour
{
    private Animator animator;

    private float dValue = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        #region 练习题
        //在之前练习题的基础上，通过1D混合树制作前进后退的功能

        animator = this.GetComponent<Animator>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Input.GetAxis("Vertical")* dValue);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            dValue = 1;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            dValue = 0.5f;


        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("Jump");
    }
}
