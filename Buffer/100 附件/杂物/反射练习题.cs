using System;
using System.Reflection;

namespace Lesson20_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("反射练习题");
            #region 练习题
            //新建一个类库工程
            //有一个Player类，有姓名，血量，攻击力，防御力，位置等信息
            //有一个无参构造函数
            //再新建一个控制台工程
            //通过反射的形式使用类库工程生成的dll程序集
            //实例化一个Player对象
            //加载类库生成的 程序集 dll库文件
            Assembly assembly = Assembly.LoadFrom(@"C:\Users\MECHREVO\Desktop\CSharp进阶教学\测试\bin\Debug\测试");
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Console.WriteLine(types[i]);
            }

            Type player = assembly.GetType("MrTang.Player");
            
            object obj = Activator.CreateInstance(player);
            Console.WriteLine(obj);
            #endregion
        }
    }
}
