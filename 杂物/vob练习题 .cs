using System;

namespace Lesson17_练习题
{
    #region 练习题一
    //真的鸭子嘎嘎叫，木头鸭子吱吱叫，橡皮鸭子唧唧叫
    class Duck
    {
        public virtual void Speak()
        {
            Console.WriteLine("嘎嘎叫");
        }
    }

    class WoodDuck:Duck
    {
        public override void Speak()
        {
            Console.WriteLine("吱吱叫");
        }
    }

    class RubberDuck:Duck
    {
        public override void Speak()
        {
            Console.WriteLine("唧唧叫");
        }
    }

    #endregion

    #region 练习题二
    //所有员工9点打卡
    //但经理十一点打卡，程序员不打卡

    class Staff
    {
        public virtual void PunchTheClock()
        {
            Console.WriteLine("9点打卡");
        }
    }

    class Manager:Staff
    {
        public override void PunchTheClock()
        {
            Console.WriteLine("11点打卡");
        }
    }

    class Programmer:Staff
    {
        public override void PunchTheClock()
        {
            Console.WriteLine("老子不打卡");
        }
    }
    #endregion

    #region 练习题三
    //创建一个图形类，有求面积和周长两个方法
    //创建矩形类，正方形类，圆形类继承图形类
    //实例化矩形、正方形、圆形对象求面积和周长

    class Graph
    {
        public virtual float GetLength()
        {
            return 0;
        }

        public virtual float GetArea()
        {
            return 0;
        }
    }

    class Rect:Graph
    {
        private float w;
        private float h;

        public Rect(int w, int h)
        {
            this.w = w;
            this.h = h;
        }
        public override float GetLength()
        {
            return 2*(w+h);
        }

        public override float GetArea()
        {
            return w*h;
        }
    }

    class Square:Graph
    {
        private float l;

        public Square(int l)
        {
            this.l = l;
        }

        public override float GetLength()
        {
            return 4*l;
        }

        public override float GetArea()
        {
            return l*l;
        }
    }

    class Circular:Graph
    {
        private float r;

        public Circular(float r)
        {
            this.r = r;
        }

        public override float GetLength()
        {
            return 2*3.14f*r;
        }

        public override float GetArea()
        {
            return 3.14f*r*r;
        }
    }

    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("多态vob练习题");

            Duck d = new Duck();
            d.Speak();

            WoodDuck wd = new WoodDuck();
            wd.Speak();

            RubberDuck rd = new RubberDuck();
            rd.Speak();

            Staff s = new Staff();
            s.PunchTheClock();

            Manager m = new Manager();
            m.PunchTheClock();

            Programmer p = new Programmer();
            p.PunchTheClock();

            Rect r = new Rect(2, 3);
            Console.WriteLine(r.GetArea());
            Console.WriteLine(r.GetLength());

            Square sq = new Square(4);
            Console.WriteLine(sq.GetArea());
            Console.WriteLine(sq.GetLength());

            Circular c = new Circular(2);
            Console.WriteLine(c.GetArea());
            Console.WriteLine(c.GetLength());
        }
    }
}
