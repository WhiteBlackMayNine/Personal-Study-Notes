using System;
using System.Collections.Generic;

namespace Lesson7_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("List练习题");

            #region 练习题一
            //请描述List和ArrayList的区别
            //List内部封装的是一个泛型数组
            //ArrayList内部封装的是一个object数组
            #endregion

            #region 练习题二
            //建立一个整形List，为它添加10~1
            //删除List中第五个元素
            //遍历剩余元素并打印

            List<int> list = new List<int>();
            for (int i = 10; i > 0; i--)
            {
                list.Add(i);
            }
            list.RemoveAt(4);
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region 练习题三
            //一个Monster基类，Boss和Gablin类继承它。
            //在怪物类的构造函数中，将其存储到一个怪物List中
            //遍历列表可以让Boss和Gablin对象产生不同攻击

            Boss b = new Boss();
            Gablin g = new Gablin();
            Boss b2 = new Boss();
            Gablin g2 = new Gablin();

            for (int i = 0; i < Monster.monsters.Count; i++)
            {
                Monster.monsters[i].Atk();
            }
            #endregion
        }
    }

    abstract class Monster
    {
        public static List<Monster> monsters = new List<Monster>();
        public Monster()
        {
            monsters.Add(this);
        }

        public abstract void Atk();
    }

    class Gablin : Monster
    {
        public override void Atk()
        {
            Console.WriteLine("哥布林的攻击");
        }
    }

    class Boss:Monster
    {
        public override void Atk()
        {
            Console.WriteLine("Boss的攻击");
        }
    }
}
