using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson13 : MonoBehaviour
{
    public Test t;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是延迟函数
        //延迟函数顾名思义
        //就是会延时执行的函数
        //我们可以自己设定延时要执行的函数和具体延时的时间
        //是MonoBehaviour基类中实现好的方法
        #endregion

        #region 知识点二 延迟函数的使用
        //1.延迟函数
        //Invoke
        //参数一：函数名 字符串
        //参数二：延迟时间 秒为单位
        Invoke("DelayDoSomething", 1);

        //注意：
        //1-1.延时函数第一个参数传入的是函数名字符串
        //1-2.延时函数没办法传入参数 只有包裹一层
        //1-3.函数名必须是该脚本上申明的函数

        //2.延迟重复执行函数
        //InvokeRepeating
        //参数一：函数名字符串
        //参数二：第一次执行的延迟时间
        //参数三：之后每次执行的间隔时间
        InvokeRepeating("DelayRe", 5, 1);

        //注意：
        //它的注意事项和延时函数一致

        //3.取消延迟函数
        //3-1取消该脚本上的所有延时函数执行
        //CancelInvoke();

        //3-2指定函数名取消
        //只要取消了指定延迟 不管之前该函数开启了多少次 延迟执行 都会统一取消
        //CancelInvoke("DelayDoSomething");

        //4.判断是否有延迟函数
        if( IsInvoking() )
        {
            print("存在延迟函数");
        }
        if( IsInvoking("DelayDoSomething") )
        {
            print("存在延迟函数DelayDoSomething");
        }
        #endregion

        #region 知识点三 延迟函数受对象失活销毁影响
        //脚本依附对象失活 或者 脚本自己失活
        //延迟函数可以继续执行 不会受到影响的

        //脚本依附对象销毁或者脚本移除
        //延迟函数无法继续执行
        #endregion

        #region 总结
        //继承MonoBehavior的脚本可以使用延时相关函数
        //函数相关
        //Invoke 延时函数
        //InvokeRepeating 延时重复函数
        //CancelInvoke 停止所有或者指定延时函数
        //IsInvoking 判断是否有延时函数待执行
        //使用相关
        //1.参数都是函数名，无法传参数
        //2.只能执行该脚本中申明的函数
        //3.对象或脚本失活无法停止延时函数执行，只有销毁组件或者对象才会停止或者代码停止
        #endregion
    }

    private void OnEnable()
    {
        //对象激活 的生命周期函数中 开启延迟（重复执行的延迟）
    }

    private void OnDisable()
    {
        //对象失活 的生命周期函数中 停止延迟
    }

    private void DelayDoSomething()
    {
        print("延时执行的函数");

        TestFun(2);

        t.TestFun();
    }

    private void DelayRe()
    {
        print("重复执行");
    }

    private void TestFun()
    {
        print("无参重载");
    }

    private void TestFun(int i)
    {
        print("传入参数" + i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
