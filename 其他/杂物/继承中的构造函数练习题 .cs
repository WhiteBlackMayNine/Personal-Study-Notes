using System;

namespace Lesson14_练习题
{
    #region 练习题
    //有一个打工人基类，有工种，工作内容两个特征，一个工作方法
    //程序员、策划、美术分别继承打工人
    //请用继承中的构造函数这个知识点
    //实例化3个对象，分别是程序员、策划、美术

    class Worker
    {
        public string type;
        public string content;

        public Worker(string type, string content)
        {
            this.type = type;
            this.content = content;
        }

        public void Work()
        {
            Console.WriteLine("{0}{1}", type, content);
        }
    }

    class Programmer:Worker
    {
        public Programmer():base("程序员", "编程")
        {

        }
    }

    class Plan:Worker
    {
        public Plan():base("策划", "设计游戏")
        {

        }
    }

    class Art:Worker
    {
        public Art():base("美术", "画画")
        {

        }
    }

    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("继承中的构造练习题");

            Programmer p = new Programmer();
            p.Work();

            Plan p2 = new Plan();
            p2.Work();

            Art p3 = new Art();
            p3.Work();
        }
    }
}
