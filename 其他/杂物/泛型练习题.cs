using System;

namespace Lesson5_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("泛型练习题");
            #region 练习题
            //定义一个泛型方法，方法内判断该类型为何类型，并返回类型的名称与占有的字节数
            //如果是int，则返回“整形，4字节”
            //只考虑以下类型
            //int：整形
            //char：字符
            //float：单精度浮点数
            //string：字符串
            //如果是其它类型，则返回“其它类型”
            //（可以通过typeof(类型) == typeof(类型)的方式进行类型判断）

            Console.WriteLine(Fun<int>());
            Console.WriteLine(Fun<string>());
            Console.WriteLine(Fun<char>());
            Console.WriteLine(Fun<float>());
            Console.WriteLine(Fun<double>());
            Console.WriteLine(Fun<uint>());
            #endregion
        }

        static string Fun<T>()
        {
            if( typeof(T) == typeof(int) )
            {
                return string.Format("{0},{1}字节", "整形", sizeof(int));
            }
            else if (typeof(T) == typeof(char))
            {
                return string.Format("{0},{1}字节", "字符", sizeof(char));
            }
            else if (typeof(T) == typeof(float))
            {
                return string.Format("{0},{1}字节", "单精度浮点数", sizeof(float));
            }
            else if (typeof(T) == typeof(string))
            {
                return string.Format("{0},{1}字节", "字符串", "?");
            }
            return "其它类型";
        }
    }
}
