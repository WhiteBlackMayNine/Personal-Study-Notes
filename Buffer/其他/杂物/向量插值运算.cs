using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson8 : MonoBehaviour
{
    public Transform target;
    public Transform A;
    public Transform B;
    public Transform C;

    private Vector3 startPos;
    private float time;

    private Vector3 nowTarget;

    // Start is called before the first frame update
    void Start()
    {
        startPos = B.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region 知识点一 线性插值
        //result = start + (end - start) * t

        //1.先快后慢 每帧改变start位置 位置无限接近 但不会得到end位置
        A.position = Vector3.Lerp(A.position, target.position, Time.deltaTime);

        //2.匀速 每帧改变时间  当t>=1时 得到结果
        //这种匀速移动 当time>=1时  我改变了 目标位置后  它会直接瞬移到我们的目标位置
        if(nowTarget != target.position)
        {
            nowTarget = target.position;
            time = 0;
            startPos = B.position;
        }
        time += Time.deltaTime;
        B.position = Vector3.Lerp(startPos, nowTarget, time);
        #endregion

        #region 知识点二 球形插值

        C.position = Vector3.Slerp(Vector3.right * 10, Vector3.left * 10 + Vector3.up*0.1f, time*0.01f);
        #endregion
    }
}
