using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcDis : MonoBehaviour
{
    public Transform A;
    public Transform B;
    // Start is called before the first frame update
    void Start()
    {
        print(Vector3.Distance(A.position, B.position));
        //BA向量
        print((A.position - B.position).magnitude);
        //AB向量
        print((B.position - A.position).magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
