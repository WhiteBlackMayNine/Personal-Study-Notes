#define Unity2020
using System;

namespace Lesson19_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("预处理器指令练习题");

            #region 练习题一
            //请说出至少4种预处理器指令
            //#define 定义一个符号 （没有值的变量）
            //#undef 取消定义一个符号

            //#if
            //#elif
            //#else
            //#endif

            //#warning
            //#error
            #endregion

            #region 练习题二
            //请使用预处理器指令实现
            //写一个函数计算两个数
            //当是Unity5版本时算加法
            //当是Unity2017版本时算乘法
            //当时Unity2020版本时算减法
            //都不是返回0

            Console.WriteLine(Calc(1, 2));
            #endregion
        }

        static int Calc(int a, int b)
        {
#if Unity5
            return a + b;
#elif Unity2017
            return a * b;
#elif Unity2020
            return a - b;
#else
            return 0;
#endif
        }
    }
}
