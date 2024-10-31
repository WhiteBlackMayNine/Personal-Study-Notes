using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：
//***************************************** 
public class No19_Collider : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰撞到的游戏物体名字
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("在碰撞器里");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("从碰撞器里移出");
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //碰撞到的游戏物体名字
    //    Debug.Log(collision.gameObject.name);
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("在触发器里");
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("从触发器里移出");
    //}
}
