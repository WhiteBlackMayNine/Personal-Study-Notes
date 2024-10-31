using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson11 : MonoBehaviour
{
    public Transform testObj;

    public Transform target;
    public Transform A;
    public Transform B;

    private Quaternion start;
    private float time;

    public Transform lookA;
    public Transform lookB;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 单位四元数
        print(Quaternion.identity);
        //testObj.rotation = Quaternion.identity;

        Instantiate(testObj, Vector3.zero, Quaternion.identity);
        #endregion

        #region 知识点二 插值运算
        start = B.transform.rotation;
        #endregion

        
    }

    // Update is called once per frame
    void Update()
    {
        //无限接近 先快后慢
        A.transform.rotation = Quaternion.Slerp(A.transform.rotation, target.rotation, Time.deltaTime);

        //匀速变化 time>=1到达目标
        time += Time.deltaTime;
        B.transform.rotation = Quaternion.Slerp(start, target.rotation, time);


        #region 知识点三 LookRotation
        //Quaternion q = Quaternion.LookRotation(lookB.position - lookA.position);
        //lookA.rotation = q;
        lookA.MyLookAt(lookB);
        #endregion
    }
}
