using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 Mathf和Math
        //Math是C#中封装好的用于数学计算的工具类 —— 位于System命名空间中

        //Mathf是Unity中封装好的用于数学计算的工具结构体 —— 位于UnityEngine命名空间中

        //他们都是提供来用于进行数学相关计算的
        #endregion

        #region 知识点二 他们的区别
        //Mathf 和 Math中的相关方法几乎一样
        //Math 是C#自带的工具类 主要就提供一些数学相关计算方法
        //Mathf 是Unity专门封装的，不仅包含Math中的方法，还多了一些适用于游戏开发的方法
        //所以我们在进行Unity游戏开发时
        //使用Mathf中的方法用于数学计算即可

        #endregion

        #region 知识点三 Mathf中的常用方法——一般计算一次
        //1.π - PI
        print(Mathf.PI);

        //2.取绝对值 - Abs
        print(Mathf.Abs(-10));
        print(Mathf.Abs(-20));
        print(Mathf.Abs(1));
        //3.向上取整 - CeilToInt
        float f = 1.3f;
        int i = (int)f;
        print(i);
        print(Mathf.CeilToInt(f));
        print(Mathf.CeilToInt(1.00001f));

        //4.向下取整 - FloorToInt
        print(Mathf.FloorToInt(9.6f));

        //5.钳制函数 - Clamp
        print(Mathf.Clamp(10, 11, 20));
        print(Mathf.Clamp(21, 11, 20));
        print(Mathf.Clamp(15, 11, 20));

        //6.获取最大值 - Max
        print(Mathf.Max(1, 2, 3, 4, 5, 6, 7, 8));
        print(Mathf.Max(1, 2));

        //7.获取最小值 - Min
        print(Mathf.Min(1, 2, 3, 4, 545, 6, 1123, 123));
        print(Mathf.Min(1.1f, 0.4f));

        //8.一个数的n次幂 - Pow
        print("一个数的n次方" + Mathf.Pow(4, 2));
        print("一个数的n次方" + Mathf.Pow(2, 3));

        //9.四舍五入 - RoundToInt
        print("四舍五入" + Mathf.RoundToInt(1.3f));
        print("四舍五入" + Mathf.RoundToInt(1.5f));

        //10.返回一个数的平方根 - Sqrt
        print("返回一个数的平方根" + Mathf.Sqrt(4));
        print("返回一个数的平方根" + Mathf.Sqrt(16));
        print("返回一个数的平方根" + Mathf.Sqrt(64));

        //11.判断一个数是否是2的n次方 - IsPowerOfTwo
        print("判断一个数是否是2的n次方" + Mathf.IsPowerOfTwo(4));
        print("判断一个数是否是2的n次方" + Mathf.IsPowerOfTwo(8));
        print("判断一个数是否是2的n次方" + Mathf.IsPowerOfTwo(3));
        print("判断一个数是否是2的n次方" + Mathf.IsPowerOfTwo(1));

        //12.判断正负数 - Sign
        print("判断正负数" + Mathf.Sign(0));
        print("判断正负数" + Mathf.Sign(10));
        print("判断正负数" + Mathf.Sign(-10));
        print("判断正负数" + Mathf.Sign(3));
        print("判断正负数" + Mathf.Sign(-2));
        #endregion
    }

    //开始值
    float start = 0;
    float result = 0;
    float time = 0;
    // Update is called once per frame
    void Update()
    {
        #region 知识点四 Mathf中的常用方法——一般不停计算
        //插值运算 - Lerp

        //Lerp函数公式
        //result = Mathf.Lerp(start, end, t);

        //t为插值系数，取值范围为 0~1
        //result = start + (end - start)*t

        //插值运算用法一
        //每帧改变start的值——变化速度先快后慢，位置无限接近，但是不会得到end位置
        start = Mathf.Lerp(start, 10, Time.deltaTime);

        //插值运算用法二
        //每帧改变t的值——变化速度匀速，位置每帧接近，当t>=1时，得到结果
        time += Time.deltaTime;
        result = Mathf.Lerp(start, 10, time);
        #endregion
    }
}
