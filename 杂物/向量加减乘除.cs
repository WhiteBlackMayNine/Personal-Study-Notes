using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 向量加法
        //this.transform.position += new Vector3(1, 2, 3);
        this.transform.Translate(Vector3.forward * 5);
        #endregion

        #region 知识点二 向量减法
        //this.transform.position -= new Vector3(1, 2, 3);
        this.transform.Translate(-Vector3.forward * 5);
        #endregion

        #region 知识点三 向量乘除标量
        this.transform.localScale *= 2;
        this.transform.localScale /= 2;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
