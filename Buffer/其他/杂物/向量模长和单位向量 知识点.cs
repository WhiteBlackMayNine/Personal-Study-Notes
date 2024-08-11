using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 向量
        //三维向量 - Vector3
        //Vector3有两种几何意义
        //1.位置 —— 代表一个点
        print(this.transform.position);

        //2.方向 —— 代表一个方向
        print(this.transform.forward);
        print(this.transform.up);

        Vector3 v = new Vector3(1, 2, 3);
        Vector2 v2 = new Vector2(1, 2);
        #endregion

        #region 知识点二 两点决定一向量
        //A和B此时 几何意义 是两个点
        Vector3 A = new Vector3(1, 2, 3);
        Vector3 B = new Vector3(5, 1, 5);
        //求向量
        //此时 AB和 BA 他们的几何意义 是两个向量
        Vector3 AB = B - A;
        Vector3 BA = A - B;
        #endregion

        #region 知识点三 零向量和负向量
        print(Vector3.zero);

        print(Vector3.forward);
        print(-Vector3.forward);
        #endregion

        #region 知识点四 向量的模长
        //Vector3中提供了获取向量模长的成员属性
        //magnitude
        print(AB.magnitude);
        Vector3 C = new Vector3(5, 6, 7);
        print(C.magnitude);

        print(Vector3.Distance(A, B));

        #endregion

        #region 知识点五 单位向量
        //Vector3中提供了获取单位向量的成员属性
        //normalized
        print(AB.normalized);
        print(AB / AB.magnitude);
        #endregion
    }

    //总结
    //1.Vector3这边变量 可以表示一个点 也可以表示一个向量 具体表示什么 是根据我们的具体需求和逻辑决定
    //2.如何在Unity里面得到向量 重点减起点 就可以得到向量  点C也可以代表向量 代表的就是 OC向量 O是坐标系原点
    //3.得到了向量 就可以利用 Vector3中提供的 成员属性 得到模长和单位向量
    //4.模长相当于可以得到 两点之间的距离  单位向量 主要是用来进行移动计算的 它不会影响我们想要的移动效果


    // Update is called once per frame
    void Update()
    {
        
    }
}
