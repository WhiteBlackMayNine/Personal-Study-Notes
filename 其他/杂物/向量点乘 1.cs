using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    public Transform B;

    // Update is called once per frame
    void Update()
    {
        //if(Vector3.Distance(this.transform.position, B.transform.position) <= 5)
        //{
        //    //第一步 算出点乘结果（方向向量）
        //    float dotResult = Vector3.Dot(this.transform.forward, (B.transform.position - this.transform.position).normalized);
        //    //第二步 通过反余弦函数 算出 夹角
        //    if(Mathf.Acos(dotResult) * Mathf.Rad2Deg <= 22.5f)
        //    {
        //        print("发现入侵者");
        //    }
        //}

        if( Vector3.Distance(this.transform.position, B.transform.position) <= 5 &&
            Vector3.Angle(this.transform.forward, B.transform.position - this.transform.position) <= 22.5f)
        {
            print("发现入侵者");
        }

    }
}
