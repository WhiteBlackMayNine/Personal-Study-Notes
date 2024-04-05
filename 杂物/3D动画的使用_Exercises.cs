using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson50_Exercises : MonoBehaviour
{
    private Animator animator;

    public float roundSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Speed", (int)Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetBool("Jump", true);

        this.transform.Rotate(Vector3.up, Input.GetAxisRaw("Horizontal") * roundSpeed * Time.deltaTime);
    }

    public void JumpOver()
    {
        animator.SetBool("Jump", false);
    }
}
