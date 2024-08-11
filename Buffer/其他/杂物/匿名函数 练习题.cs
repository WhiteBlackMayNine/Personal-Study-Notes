using System;

namespace Lesson14_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("匿名函数练习题");

            Func<int, int> fun = TestFun(2);
            Console.WriteLine(fun(3));
        }
        #region 练习题
        //写一个函数传入一个整数，返回一个函数
        //之后执行这个匿名函数时传入一个整数和之前那个函数传入的数相乘
        //返回结果
        static Func<int, int> TestFun(int i)
        {
            //这种写法会改变 i的生命周期
            return delegate (int v)
            {
                return i * v;
            };
        }

        #endregion
    }
}
