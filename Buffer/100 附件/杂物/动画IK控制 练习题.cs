using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson53_Exercises : MonoBehaviour
{
    private Animator animator;

    public Transform headPos;
    //x方向鼠标旋转了多少角度
    private float changeAngleX;
    //y方向鼠标旋转了多少角度
    private float changeAngleY;
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

        if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("Fire");

        //角度就在累加
        changeAngleX += Input.GetAxis("Mouse X");
        changeAngleX = Mathf.Clamp(changeAngleX, -30, 30);

        changeAngleY += Input.GetAxis("Mouse Y");
        changeAngleY = Mathf.Clamp(changeAngleY, -30, 30);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtWeight(1, 1, 1);
        Vector3 pos = Quaternion.AngleAxis(changeAngleX, Vector3.up) * (headPos.position + headPos.forward * 10);
        pos = Quaternion.AngleAxis(changeAngleY, Vector3.right) * pos;
        animator.SetLookAtPosition(pos);
    }
}
