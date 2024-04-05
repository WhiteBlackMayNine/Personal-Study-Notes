using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lesson64 : MonoBehaviour
{
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 导航网格动态障碍组件用来干啥？
        //在游戏中常常会有这样的一个功能
        //场景中有一道门，如果这道门没有被破坏是不能自动导航到门后场景的
        //只有当这道门被破坏了，才可以通过此处前往下一场景
        //而类似这样的物体本身是不需要进行寻路的所以没有必要为它添加NavMeshAgent脚本
        //这时就会使用动态障碍组件实现该功能
        #endregion

        agent = this.GetComponent<NavMeshAgent>();

        #region 知识点二 导航动态障碍物组件的使用
        //1.为需要进行动态阻挡的对象添加NavMeshObstacle组件
        //2.设置相关参数
        //3.代码逻辑控制其的移动或者显隐
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) )
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
