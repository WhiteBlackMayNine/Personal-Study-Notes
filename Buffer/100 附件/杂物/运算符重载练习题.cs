using System;

namespace Lesson10_练习题
{
    #region 练习题一
    //定义一个位置结构体或类，为其重载判断是否相等的运算符
    //(x1, y1) == (x2, y2)   =>  两个值同时相等才为true
    struct Position
    {
        public int x;
        public int y;

        public static bool operator ==(Position p1, Position p2)
        {
            if( p1.x == p2.x && p1.y == p2.y )
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Position p1, Position p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
            {
                return false;
            }
            return true;
        }
    }
    #endregion

    #region 练习题二
    //定义一个Vector3类（x,y,z）通过重载运算符实现以下运算
    //(x1, y1, z1) + (x2, y2, z2) = (x1+x2,y1+y2,z1+z2)
    //(x1, y1, z1) - (x2, y2, z2) = (x1-x2,y1-y2,z1-z2)
    //(x1, y1, z1) * num = (x1 * num, y1 * num, z1 * num)

    class Vector3
    {
        public int x;
        public int y;
        public int z;
        public Vector3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3 operator *(Vector3 v1, int num)
        {
            return new Vector3(v1.x * num, v1.y * num, v1.z * num);
        }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("运算符重载练习题");

            Position p;
            p.x = 1;
            p.y = 1;
            Position p2;
            p2.x = 2;
            p2.y = 1;
            if( p != p2 )
            {
                Console.WriteLine("不相等");
            }

            Vector3 v1 = new Vector3(1, 1, 1);
            Vector3 v2 = new Vector3(2, 2, 2);
            Vector3 v3 = v1 - v2;
            Console.WriteLine("{0}|{1}|{2}", v3.x, v3.y, v3.z);

            v3 *= 10;
            Console.WriteLine("{0}|{1}|{2}", v3.x, v3.y, v3.z);
        }
    }
}
