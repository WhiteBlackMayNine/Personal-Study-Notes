using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：组件 附加到游戏物体的所有内容的基类
//***************************************** 
public class No5_Component : MonoBehaviour
{
    public int testValue;

    public GameObject enemyGos;

    void Start()
    {
        //Mono继承自Behaviour,Behaviour继承自Compontent，Compontent继承自Object
        //子辈拥有父辈以及父辈以上（派生程度低的基类）所有的公有，保护的属性，成员变量
        //方法等功能，挂载功能其实是Component，也就是我们写的脚本组件其实指的是Component组件
        //而Mono是在此基础上进行的封装与扩展
        //组件的使用与获取
        //a.组件都是在某一个游戏物体身上挂载的，可以通过游戏物体查找获取之后使用
        No5_Component no5_Component = gameObject.GetComponent<No5_Component>();
        //No2_EventFunction no2_EventFunction = gameObject.GetComponent<No2_EventFunction>();
        //Debug.Log(no2_EventFunction);
        //Debug.Log(no2_EventFunction.attackValue);
        Debug.Log(no5_Component.testValue);
        GameObject grisGo = GameObject.Find("Gris");
        Debug.Log(grisGo.GetComponent<SpriteRenderer>());
        Debug.Log(enemyGos.GetComponentInChildren<BoxCollider>());
        Debug.Log(enemyGos.GetComponentsInChildren<BoxCollider>());
        Debug.Log(enemyGos.GetComponentInParent<BoxCollider>());
        //b.通过其他组件查找
        SpriteRenderer sr= grisGo.GetComponent<SpriteRenderer>();
        sr.GetComponent<Transform>();
        this.GetComponent<Transform>();
    }

    void Update()
    {

    } 
}
