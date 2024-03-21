using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson12 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 四元数相乘
        Quaternion q = Quaternion.AngleAxis(20, Vector3.up);
        this.transform.rotation *= q;

        this.transform.rotation *= q;
        #endregion

        #region 知识点二 四元数乘向量
        Vector3 v = Vector3.forward;
        print(v);
        v = Quaternion.AngleAxis(45, Vector3.up) * v;
        print(v);
        v = Quaternion.AngleAxis(45, Vector3.up) * v;
        print(v);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
