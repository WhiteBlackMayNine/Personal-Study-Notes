using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson39 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是IK？
        //在骨骼动画中，构建骨骼的方法被称为正向动力学
        //它的表现形式是，子骨骼（关节）的位置根据父骨骼（关节）的旋转而改变
        //用我们人体举例子
        //当我们抬起手臂时，是肩部关节带动的整个手臂的运动，用父子骨骼理解的话就是父带动了子

        //而IK全称是Inverse Kinematics，翻译过来的意思就是反向动力学的意思
        //它和正向动力学恰恰相反
        //它的表现形式是，子骨骼（关节）末端的位置改变会带动自己以及自己的父骨骼（关节）旋转
        //用我们人体举例子
        //当我们拿起一个杯子的时候是用手掌去拿，以杯子为参照物，我们移动杯子的位置，手臂会随着杯子一起移动
        //用父子骨骼理解的话就是子带动了父
        #endregion

        #region 知识点二 2D IK包引入
        //在Package Manager窗口中引入2D IK工具包
        //需要在Advanced高级选项中选中Show preview packages（显示预览包）
        //这样才能看到2D IK相关内容

        //注意：如果在引入包时报错，需要在Windows防火墙中添加入站规则
        #endregion

        #region 知识点三 2D IK的使用

        #endregion

        #region 知识点四 IK对于我们的意义
        //1.瞄准功能
        //2.头部朝向功能
        //3.拾取物品功能
        //等等有指向性的功能时 我们都可以通过IK来达到目的

        //最大的作用，可以方便我们进行动画制作
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
