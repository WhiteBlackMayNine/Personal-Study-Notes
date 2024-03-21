using System;

namespace Lesson7_练习题
{
    #region 练习题一
    //请说出const和static的区别
    //相同点
    //他们都可以通过类名点出来使用
    //不同点
    //1.const必须初始化不能被修改，static没有这个要求
    //2.const是只能修饰变量，static可以修饰很多
    //3.访问修饰符要写在const前面，static随意
    #endregion

    #region 练习题二
    //请用静态成员相关知识实现
    //一个类对象，在整个应用程序的生命周期中，有且仅会有一个该对象的存在，
    //不能在外部实例化，直接通过该类类名就能够得到唯一的对象

    class Test
    {
        private static Test t = new Test();

        public int testInt = 10;

        public static Test T
        {
            get
            {
                return t;
            }
        }

        private Test()
        {

        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("静态成员练习题");

            Console.WriteLine(Test.T.testInt);
        }
    }
}
