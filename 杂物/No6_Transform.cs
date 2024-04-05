using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：变换组件
//***************************************** 
public class No6_Transform : MonoBehaviour
{
    public GameObject grisGo;

    public float moveSpeed=1;

    void Start()
    {
        //场景中的每个对象都有一个变换组件。 它用于存储和操作对象的位置、旋转和缩放。
        //1.访问与获取
        Debug.Log(this.transform);
        Debug.Log(grisGo.transform);
        Transform grisTrans = grisGo.transform;
        //2.成员变量
        Debug.Log("Gris变换组件所挂载的游戏物体名字是："+grisTrans.name);
        Debug.Log("Gris变换组件所挂载的游戏物体引用是："+grisTrans.gameObject);
        Debug.Log("Gris下的子对象（指Transform）的个数是："+grisTrans.childCount);
        Debug.Log("Gris世界空间中的坐标位置是："+grisTrans.position);
        Debug.Log("Gris以四元数形式表示的旋转是："+grisTrans.rotation);
        Debug.Log("Gris以欧拉角形式表示的旋转（以度数为单位）是"+grisTrans.eulerAngles);
        Debug.Log("Gris的父级Transform是："+grisTrans.parent);
        Debug.Log("Gris相对于父对象的位置坐标是："+grisTrans.localPosition);
        Debug.Log("Gris相对于父对象以四元数形式表示的旋转是：" + grisTrans.localRotation);
        Debug.Log("Gris相对于父对象以欧拉角形式表示的旋转（以度数为单位）是：" + grisTrans.localEulerAngles);
        Debug.Log("Gris相对于父对象的变换缩放是："+grisTrans.localScale);
        Debug.Log("Gris的自身坐标正前方（Z轴正方向）是："+grisTrans.forward);
        Debug.Log("Gris的自身坐标正右方（X轴正方向）是：" + grisTrans.right);
        Debug.Log("Gris的自身坐标正上方（Y轴正方向）是：" + grisTrans.up);
        //共有方法
        //3.查找
        Debug.Log("当前脚本挂载的游戏对象下的叫Gris的子对象身上的Transform组件是："+transform.Find("Gris"));
        Debug.Log("当前脚本挂载的游戏对象下的第一个（0号索引）子对象的Transform引用是："+transform.GetChild(0));
        Debug.Log("Gris当前在此父对象同级里所在的索引位置："+ grisTrans.GetSiblingIndex());
        //静态方法
        //Transform.Destroy(grisTrans);
        //Transform.Destroy(grisTrans.gameObject);
        //Transform.FindObjectOfType();
        //Transform.Instantiate();
    }

    void Update()
    {
        //移动
        //0.第二个参数不填（实际情况按自身坐标系移动,space.self）
        //grisGo.transform.Translate(Vector2.left*moveSpeed);//自身坐标系
        //grisGo.transform.Translate(-grisGo.transform.right*moveSpeed);//世界坐标系
        //1.第一个参数按世界坐标系移动，第二个参数指定世界坐标系（实际情况按世界坐标系移动）
        //grisGo.transform.Translate(Vector2.left*moveSpeed,Space.World);
        //2.第一个参数按世界坐标系移动，第二个参数指定自身坐标系（实际情况按自身坐标系移动）
        //grisGo.transform.Translate(Vector2.left * moveSpeed, Space.Self);
        //3.第一个参数按自身坐标系移动，第二个参数指定世界坐标系（实际情况按自身坐标系移动）
        //grisGo.transform.Translate(-grisGo.transform.right * moveSpeed, Space.World);
        //4.第一个参数按自身坐标系移动，第二个参数指定自身坐标系（实际情况按世界坐标系移动）(一般不使用)
        //grisGo.transform.Translate(-grisGo.transform.right * moveSpeed, Space.Self);
        //旋转
        //grisGo.transform.Rotate(new Vector3(0,0,1));
        grisGo.transform.Rotate(Vector3.forward,1);
    } 

}
