using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lesson63 : MonoBehaviour
{
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 网格外连接组件是什么？
        //我们在烘焙地形数据的时候
        //可以生成网格外连接
        //但是它是满足条件的都会生成
        //而且是要在编辑模式下生成

        //如果我们只希望两个未连接的平面之间只有有限条连接路径可以跳跃过去
        //并且运行时可以动态添加
        //就可以使用网格外连接组件
        //达到“指哪打哪”的效果
        #endregion

        #region 知识点二 网格外连接组件的使用
        //1.使用两个对象作为两个平面之间的连接点（起点和终点）
        //2.添加Off Mesh Link脚本进行关联
        //3.设置参数

        agent = this.GetComponent<NavMeshAgent>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
