using System;

namespace Lesson16_练习题
{
    #region 练习题
    //定义一个载具类
    //有速度，最大速度，可乘人数，司机和乘客等，有上车，下车，行驶，车祸等方法，
    //用载具类声明一个对象，并将若干人装载上车

    class Person
    {

    }

    class Driver:Person
    {

    }

    class Passenger:Person
    {

    }

    class Car
    {
        public int speed;
        public int maxSpeed;
        //当前装载了多少人
        public int num;

        public Person[] persons;

        public Car( int speed, int maxSpeed, int num )
        {
            this.speed = speed;
            this.maxSpeed = maxSpeed;
            this.num = 0;
            persons = new Person[num];
        }

        public void GetIn( Person p )
        {
            if( num >= persons.Length )
            {
                Console.WriteLine("满载");
                return;
            }
            persons[num] = p;
            ++num;
        }

        public void GetOff(Person p)
        {
            for (int i = 0; i < persons.Length; i++)
            {
                if (persons[i] == null)
                {
                    break;
                }
                if( persons[i] == p )
                {
                    //移动位置
                    for (int j = i; j < num - 1; j++)
                    {
                        persons[j] = persons[j + 1];
                    }
                    //最后一个位置清空
                    persons[num - 1] = null;
                    --num;
                    break;
                }
            }
        }

        public void Move()
        {

        }

        public void Boom()
        {

        }
    }

    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("封装继承综合练习");

            Car c = new Car(10, 20, 20);
            Driver d = new Driver();
            c.GetIn(d);

            Passenger p = new Passenger();
            c.GetIn(p);
            Passenger p2 = new Passenger();
            c.GetIn(p2);
            Passenger p3 = new Passenger();
            c.GetIn(p3);

            c.GetOff(p2);
        }
    }
}
