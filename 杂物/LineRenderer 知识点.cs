using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson21 : MonoBehaviour
{
    private Material m;

    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 LineRenderer是什么
        //LineRenderer是Unity提供的一个用于画线的组件
        //使用它我们可以在场景中绘制线段
        //一般可以用于
        //1绘制攻击范围
        //2武器红外线
        //3辅助功能
        //4其它画线功能
        #endregion

        #region 知识点二 LineRender参数相关

        #endregion

        #region 知识点三 LineRender代码相关
        //动态添加一个线段
        GameObject line = new GameObject();
        line.name = "Line";
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

        //首尾相连
        lineRenderer.loop = true;

        //开始结束宽
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;

        //开始结束颜色
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.red;

        //设置材质
        m = Resources.Load<Material>("M");
        lineRenderer.material = m;

        //设置点
        //一定注意 设置点 要 先设置点的个数
        lineRenderer.positionCount = 4;
        //接着就设置 对应每个点的位置
        lineRenderer.SetPositions(new Vector3[] { new Vector3(0,0,0),
                                                  new Vector3(0,0,5),
                                                  new Vector3(5,0,5)});
        lineRenderer.SetPosition(3, new Vector3(5, 0, 0));

        //是否使用世界坐标系
        //决定了 是否随对象移动而移动
        lineRenderer.useWorldSpace = false;

        //让线段受光影响 会接受光数据 进行着色器计算
        lineRenderer.generateLightingData = true;

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
