using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson7 : MonoBehaviour
{
    public Transform A;
    public Transform B;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 叉乘计算
        print(Vector3.Cross(A.position, B.position));
        #endregion

        #region 知识点二 叉乘几何意义
        //假设向量 A和B 都在 XZ平面上
        //向量A 叉乘 向量 B
        //y大于0 证明 B在A右侧
        //y小于0 证明 B在A左侧
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 C = Vector3.Cross(A.position, B.position);
        //if( C.y > 0)
        //{
        //    print("B在A的右侧");
        //}
        //else
        //{
        //    print("B在A的左侧");
        //}

        Vector3 C = Vector3.Cross(B.position, A.position);
        if (C.y > 0)
        {
            print("A在B的右侧");
        }
        else
        {
            print("A在B的左侧");
        }
    }
}
