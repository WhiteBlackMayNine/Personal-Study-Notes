using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson13_Exercises : MonoBehaviour
{
    private int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        #region 练习题一 利用延时函数实现一个计秒器
        //InvokeRepeating("DelayFun", 0, 1);

        //DelayFun2();
        #endregion

        #region 练习题二 请用两种方式延时销毁一个指定对象
        //通过Destroy来进行延迟销毁
        Destroy(this.gameObject, 5);

        Invoke("DelayDes", 5);
        #endregion
    }
    
    private void DelayDes()
    {
        Destroy(this.gameObject);
    }

    private void DelayFun()
    {
        print(time + "秒");
        ++time;
    }

    private void DelayFun2()
    {
        print(time + "秒");
        ++time;
        Invoke("DelayFun2", 1);
    }
}
