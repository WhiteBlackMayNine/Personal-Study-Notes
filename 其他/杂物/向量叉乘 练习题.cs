using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy2 : MonoBehaviour
{
    public Transform A;
    public Transform B;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float dotResult;
    private Vector3 crossResult;
    // Update is called once per frame
    void Update()
    {
        //第一题
        dotResult = Vector3.Dot(A.forward, B.position - A.position);
        crossResult = Vector3.Cross(A.forward, B.position - A.position);
        //判断前后
        if (dotResult >= 0 )
        {
            //右侧
            if(crossResult.y >= 0)
            {
                print("右前");
            }
            //左侧
            else
            {
                print("左前");
            }
        }
        else
        {
            //右侧
            if (crossResult.y >= 0)
            {
                print("右后");
            }
            //左侧
            else
            {
                print("左后");
            }
        }

        //第二题
        if( Vector3.Distance(A.position, B.position) <= 5 )
        {
            if( crossResult.y >= 0 && Vector3.Angle(A.forward, B.position - A.position) <= 30 ||
                crossResult.y < 0 && Vector3.Angle(A.forward, B.position - A.position) <= 20)
            {
                print("发现入侵者");
            }
        }
    }
}
