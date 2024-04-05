using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：用于表示2D向量和点
//***************************************** 
public class No7_Vector2 : MonoBehaviour
{
    public Transform grisTrans;
    public Transform targetTrans;
    public float percent;
    public float lerpSpeed;
    Vector2 currentVelocity = new Vector2(1, 0);

    //结构体 值类型
    public struct MyStruct
    {
        public string name;
        public int age;
    }
    //类 引用类型
    public class MyClass
    {
        public string name;
        public int age;
    }

    void Start()
    {
        ////静态变量
        //print(Vector2.down);
        //print(Vector2.up);//Y轴正方向
        //print(Vector2.left);
        //print(Vector2.right);//X轴正方向
        //print(Vector2.one);
        //print(Vector2.zero);
        ////构造函数
        //Vector2 v2 = new Vector2(2, 2);
        //print("V2向量是：" + v2);
        ////成员变量
        //print("V2向量的模长是：" + v2.magnitude);
        //print("V2向量的模长的平方是：" + v2.sqrMagnitude);
        //print("V2向量单位化之后是：" + v2.normalized);
        //print("V2向量的XY值分别是：" + v2.x + "," + v2.y);
        //print("V2向量的XY值分别是（使用索引器形式访问）：" + v2[0] + "," + v2[1]);
        ////公共函数
        //bool equal = v2.Equals(new Vector2(1, 1));
        //print("V2向量与向量（1,1）是否相等？" + equal);
        //Vector2 v2E = new Vector2(1, 3);
        //bool equalv2E = v2E.Equals(new Vector2(3, 1));
        //print("v2E向量与向量（3,1）是否相等？" + equal);
        //print("V2向量是：" + v2);
        //print("V2向量的单位化向量是：" + v2.normalized + "但是V2向量的值还是：" + v2);
        //v2.Normalize();
        //print("V2向量是：" + v2);
        //v2.Set(5, 9);
        //print("V2向量是：" + v2);
        //transform.position = v2;

        ////transform.position.x = 4;//不可以单独赋值某一个值，比如x
        ////结论1：用属性和方法返回的结构体是不能修改其字段的 

        //MyStruct myStruct = new MyStruct();//A
        //myStruct.name = "Trigger";
        //myStruct.age = 100;
        ////结论2：直接访问公有的结构体是可以修改其字段的

        //MyStruct yourStruct = myStruct;//B
        //yourStruct.name = "小可爱";
        //yourStruct.age = 18;
        //print("原本的结构体对象名字是：" + myStruct.name);
        //print("修改后的结构体对象名字是：" + yourStruct.name);

        //MyClass myClass = new MyClass();//A
        //myClass.name = "Trigger";
        //myClass.age = 100;
        //MyClass yourClass = myClass;//B跟A是同一块内存空间
        //yourClass.name = "小阔爱";
        //yourClass.age = 18;
        //print("原本的结构体对象名字是：" + myClass.name);
        //print("修改后的结构体对象名字是：" + yourClass.name);
        ////总结导致这个问题的原因：
        ////1,Transform中的position是属性(换成方法也一样，因为属性的实现本质上还是方法)而不是公有字段
        ////2,position的类型是Vector的，而Vector是Struct类型
        ////3,Struct之间的赋值是拷贝而不是引用

        ////修改位置
        //transform.position = new Vector2(3, 3);
        //Vector2 vector2 = transform.position;
        //vector2.x = 2;
        //transform.position = vector2;

        ////静态函数
        Vector2 va = new Vector2(1, 0);
        Vector2 vb = new Vector2(0, 1);
        //Debug.Log("从va指向vb方向计算的无符号夹角是：" + Vector2.Angle(va, vb));
        //print("va点与vb点之间的距离是：" + Vector2.Distance(va, vb));
        //print("向量va与向量vb的点积是：" + Vector2.Dot(va, vb));
        //print("向量va和向量vb在各个方向上的最大分量组成的新向量是：" + Vector2.Max(va, vb));
        //print("向量va和向量vb在各个方向上的最小分量组成的新向量是：" + Vector2.Min(va, vb));
        ////具体得到的新向量的结果的计算公式是:a+(b-a)*t
        //print("va向vb按照0.5的比例进行线性插值变化之后的结果是" + Vector2.Lerp(va, vb, 0.5f));
        //print("va向vb按照参数为-1的形式进行（无限制）线性插值变化之后的结果是" + Vector2.LerpUnclamped(va, vb, -1));
        //float maxDistance = 0.5f;
        //print("将点va以最大距离不超过maxDistance为移动步频移向vb" + Vector2.MoveTowards(va, vb, maxDistance));
        print("va和vb之间的有符号角度（以度为单位，逆时针为正）是" + Vector2.SignedAngle(va, vb));
        print("vb和va之间的有符号角度（以度为单位，逆时针为正）是" + Vector2.SignedAngle(vb, va));
        //print("va和vb在各个方向上的分量相乘得到的新向量是：" + Vector2.Scale(va, vb));
        //Vector2 currentVelocity = new Vector2(1, 0);
        //print(Vector2.SmoothDamp(va, vb, ref currentVelocity, 0.1f));

        ////运算符
        //print("va加上vb向量是：" + (va + vb));
        //print("va减去vb向量是：" + (va - vb));
        //print("va减去vb向量是：" + va * 10);
        //print("va与vb是同一个向量吗" + (va == vb));
    }

    void Update()
    {
        //grisTrans.position= Vector2.Lerp(grisTrans.position,targetTrans.position,0.01f);
        //percent += 1 * lerpSpeed * Time.deltaTime;
        //grisTrans.position = Vector2.Lerp(grisTrans.position, targetTrans.position, percent);
        //lerp是先快后慢，moveTowards匀速
        grisTrans.position = Vector2.MoveTowards(grisTrans.position, targetTrans.position, 0.05f);
        //平滑阻尼       
        //grisTrans.position = Vector2.SmoothDamp(grisTrans.position,targetTrans.position,ref currentVelocity,1);
    }
}
