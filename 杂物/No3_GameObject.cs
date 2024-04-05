using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人： Trigger 
//功能说明：游戏物体
//***************************************** 
public class No3_GameObject : MonoBehaviour
{
    public GameObject grisGo;

    void Start()
    {
        //1.创建方式
        //a.使用构造函数（声明+实例化） 创建一个空的游戏对象
        //GameObject myGo = new GameObject("MyGameObject");
        //b.根据现有的预制体（游戏物体）资源或者游戏场景已有的游戏物体来实例化
        //GameObject.Instantiate(grisGo);
        //c.使用特别的API创建一些基本的游戏物体类型(原始几何体)
        //GameObject.CreatePrimitive(PrimitiveType.Plane);
        //2.游戏物体的查找和获取
        //this;//当前组件No3_GameObject
        //this.grisGo;
        //Test();this.Test();
        //this.gameObject;
        Debug.Log("当前脚本挂载到的游戏物体名称是："+gameObject.name);
        //GameObject.Instantiate;
        //GameObject.CreatePrimitive;
        Debug.Log("当前游戏物体标签是："+gameObject.tag);
        Debug.Log("当前游戏物体层级是："+gameObject.layer);
        //游戏物体的激活失活
        gameObject.SetActive(true);
        Debug.Log("当前游戏物体的状态是："+gameObject.activeInHierarchy);
        Debug.Log("当前游戏物体的状态是：" +gameObject.activeSelf);
        //有引用
        //对自己 this.gameObject
        //对其他游戏物体 
        Debug.Log("gris游戏物体的状态是："+ grisGo.activeSelf);
        //没有直接引用
        //对其他游戏物体查找（这时游戏物体必须是激活状态）
        //a.通过名称查找
        //GameObject mainCameraGo= GameObject.Find("Main Camera");
        //Debug.Log("mainCamera游戏物体的标签是：" + mainCameraGo.tag);
        //b.通过标签查找
        GameObject mainCameraGo = GameObject.FindGameObjectWithTag("MainCamera");
        Debug.Log("mainCamera游戏物体的名字是：" + mainCameraGo.name);
        //c.通过类型查找
        No2_EventFunction no2_EventFunction= GameObject.FindObjectOfType<No2_EventFunction>();
        Debug.Log("no2_EventFunction游戏物体的名字是：" + no2_EventFunction.name);
        //d.多数查找与获取
        GameObject[] enemyGos= GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyGos.Length; i++)
        {
            Debug.Log("查找到的敌人游戏物体名称是："+enemyGos[i].name);
        }
        BoxCollider[] colliders= GameObject.FindObjectsOfType<BoxCollider>();
        Debug.Log("--------------------------------------------------");
        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log("查找到的敌人碰撞器名称是：" + colliders[i].name);
        }
    }

    void Update()
    {

    }

    private void Test() { }
}
