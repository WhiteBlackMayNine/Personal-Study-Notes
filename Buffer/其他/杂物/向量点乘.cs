using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson6 : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        #region 补充知识 调试画线
        //画线段 
        //前两个参数 分别是 起点 终点
        //Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward, Color.red);
        //画射线
        //前两个参数 分别是 起点 方向
        //Debug.DrawRay(this.transform.position, this.transform.forward, Color.white);
        #endregion

        #region 知识点一 通过点乘判断对象方位
        //Vector3 提供了计算点乘的方法
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.red);
        Debug.DrawRay(this.transform.position, target.position - this.transform.position, Color.red);
        //得到两个向量的点乘结果
        //向量 a 点乘 AB 的结果
        float dotResult = Vector3.Dot(this.transform.forward, target.position - this.transform.position);
        if( dotResult >= 0 )
        {
            print("它在我前方");
        }
        else
        {
            print("它在我后方");
        }
        #endregion

        #region 知识点二 通过点乘推导公式算出夹角
        //步骤
        //1.用单位向量算出点乘结果
        dotResult = Vector3.Dot(this.transform.forward, (target.position - this.transform.position).normalized);
        //2.用反三角函数得出角度
        print("角度-" + Mathf.Acos(dotResult) * Mathf.Rad2Deg);

        //Vector3中提供了 得到两个向量之间夹角的方法 
        print("角度2-" + Vector3.Angle(this.transform.forward, target.position - this.transform.position));
        #endregion
    }
}
