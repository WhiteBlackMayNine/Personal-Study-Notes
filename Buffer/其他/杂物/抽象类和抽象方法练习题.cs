using System;

namespace Lesson18_练习题
{
    #region 练习题一
    //写一个动物抽象类，写三个子类
    //人叫，狗叫，猫叫
    abstract class Animal
    {
        public abstract void Speak();
    }

    class Person : Animal
    {
        public override void Speak()
        {
            Console.WriteLine("你好");
        }
    }

    class Dog:Animal
    {
        public override void Speak()
        {
            Console.WriteLine("汪汪");
        }
    }

    class Cat:Animal
    {
        public override void Speak()
        {
            Console.WriteLine("喵喵");
        }
    }

    #endregion

    #region 练习题二
    //创建一个图形类，有求面积和周长两个方法
    //创建矩形类，正方形类，圆形类继承图形类
    //实例化矩形、正方形、圆形对象求面积和周长
    abstract class Graph
    {
        public abstract float GetLength();
        public abstract float GetArea();
    }

    class Rect : Graph
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
            return 2 * (w + h);
        }

        public override float GetArea()
        {
            return w * h;
        }
    }

    class Square : Graph
    {
        private float l;

        public Square(int l)
        {
            this.l = l;
        }

        public override float GetLength()
        {
            return 4 * l;
        }

        public override float GetArea()
        {
            return l * l;
        }
    }

    class Circular : Graph
    {
        private float r;

        public Circular(float r)
        {
            this.r = r;
        }

        public override float GetLength()
        {
            return 2 * 3.14f * r;
        }

        public override float GetArea()
        {
            return 3.14f * r * r;
        }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("抽象类抽象方法练习题");

            Animal p = new Person();
            p.Speak();

            Dog d = new Dog();
            d.Speak();

            Animal c = new Cat();
            c.Speak();
        }
    }
}
