using System;
using System.Reflection;

namespace Lesson21_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("特性练习题");

            #region 练习题
            //为反射练习题中的Player对象
            //随便为其中一个成员变量加一个自定义特性
            //同样实现反射练习题中的要求

            //但是当在设置加了自定义特性的成员变量时，在控制台中打印一句
            //非法操作，随意修改XXX成员

            Assembly assembly = Assembly.LoadFrom(@"C:\Users\MECHREVO\Desktop\CSharp进阶教学\测试\bin\Debug\测试");
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Console.WriteLine(types[i]);
            }
            //得type
            Type playerType = assembly.GetType("MrTang.Player");
            //实例化
            object playerObj = Activator.CreateInstance(playerType);
            Console.WriteLine(playerObj);

            FieldInfo[] fields = playerType.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                Console.WriteLine(fields[i]);
            }

            //首先要得到我们自定特性的Type
            Type attribute = assembly.GetType("MrTang.MyCustomAttribute");

            //赋值名字
            FieldInfo fildStr = playerType.GetField("name");
            //得到的特性如果不为空 就证明有
            if(fildStr.GetCustomAttribute(attribute) != null)
            {
                Console.WriteLine("非法操作，随意修改name成员");
            }
            else
            {
                //检测是否被自定义特性修饰 如果是 就不能修改 而是提示
                fildStr.SetValue(playerObj, "123123");
            }
           

            #endregion
        }
    }
}
