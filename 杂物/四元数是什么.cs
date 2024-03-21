using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson10 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 四元数 Quaternion
        //四元数Q = [cos(β/2),  sin(β/2)x, sin(β/2)y, sin(β/2)z]
        //计算原理
        //Quaternion q = new Quaternion(Mathf.Sin(30 * Mathf.Deg2Rad), 0, 0, Mathf.Cos(30 * Mathf.Deg2Rad));
        //提供的轴角对 初始化 四元数的方法
        Quaternion q = Quaternion.AngleAxis(60, Vector3.right);

        //创建一个立方体
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.rotation = q;

        #endregion

        #region 知识点二 四元数和欧拉角转换
        //1.欧拉角转四元数
        Quaternion q2 = Quaternion.Euler(60, 0, 0);
        GameObject obj2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj2.transform.rotation = q2;
        //2.四元数转欧拉角
        print(q2.eulerAngles);
        #endregion

        #region 知识点三 四元数弥补的欧拉角缺点
        //1.同一旋转的表示不唯一  四元数旋转后 转换后的欧拉角 始终是 -180~180度

        //2.万向节死锁 通过四元数旋转对象可以避免万向节死锁

        #endregion

    }

    // Update is called once per frame
    Vector3 e;
    void Update()
    {
        this.transform.rotation *= Quaternion.AngleAxis(1, Vector3.up);
        //e = this.transform.rotation.eulerAngles;
        //e += Vector3.forward;
        //this.transform.rotation = Quaternion.Euler(e);
    }
}
