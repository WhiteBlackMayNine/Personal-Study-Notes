using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：刚体(通过物理模拟控制对象的位置)
//***************************************** 
public class No17_Rigidbody : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed=1;
    private float angle=60;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        print(rb.position);
        print(rb.gravityScale+",该对象受重力影响的程度");
        rb.gravityScale = 0;
        //rb.AddForce(Vector2.right*10);
        //rb.AddForce(Vector2.right * 10,ForceMode2D.Impulse);
        rb.velocity = Vector2.right *moveSpeed;
        //rb.MoveRotation(90);
    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position+Vector2.right*moveSpeed*Time.fixedDeltaTime);  
        rb.MoveRotation(rb.rotation+angle*Time.fixedDeltaTime);
    } 
}
