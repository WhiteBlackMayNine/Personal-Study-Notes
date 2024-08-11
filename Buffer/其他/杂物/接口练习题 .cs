using System;

namespace Lesson19_练习题
{
    #region 练习题一
    //人、汽车、房子都需要登记，人需要到派出所登记，汽车需要去车管所登记，房子需要去房管局登记
    //使用接口实现登记方法
    interface IRegister
    {
        void Register();
    }

    class Person:IRegister
    {
        public void Register()
        {
            Console.WriteLine("派出所登记");
        }
    }

    class Car:IRegister
    {
        public void Register()
        {
            Console.WriteLine("车管所登记");
        }
    }

    class Home:IRegister
    {
        public void Register()
        {
            Console.WriteLine("房管局登记");
        }
    }

    #endregion

    #region 练习题二
    //麻雀、鸵鸟、企鹅、鹦鹉、直升机、天鹅
    //直升机和部分鸟能飞
    //鸵鸟和企鹅不能飞
    //企鹅和天鹅能游泳
    //除直升机，其它都能走
    //请用面向对象相关知识实现

    abstract class Bird
    {
        public abstract void Walk();
    }

    interface IFly
    {
        void Fly();
    }

    interface ISwimming
    {
        void Swimming();
    }

    class Sparrow : Bird, IFly
    {
        public void Fly()
        {
           
        }

        public override void Walk()
        {
            
        }
    }

    class Ostrich:Bird
    {
        public override void Walk()
        {

        }
    }

    class Penguin : Bird,ISwimming
    {
        public void Swimming()
        {
            
        }

        public override void Walk()
        {

        }
    }

    class Parrot : Bird,IFly
    {
        public void Fly()
        {
            
        }

        public override void Walk()
        {

        }
    }

    class Swan : Bird,IFly,ISwimming
    {
        public void Fly()
        {
            
        }

        public void Swimming()
        {
            
        }

        public override void Walk()
        {

        }
    }

    class Helicopter : IFly
    {
        public void Fly()
        {
            
        }
    }
    #endregion

    #region 练习题三
    //多态来模拟移动硬盘、U盘、MP3查到电脑上读取数据
    //移动硬盘与U盘都属于存储设备
    //MP3属于播放设备
    //但他们都能插在电脑上传输数据
    //电脑提供了一个USB接口
    //请实现电脑的传输数据的功能

    interface IUSB
    {
        void ReadData();
    }

    class StorageDevice : IUSB
    {
        public string name;
        public StorageDevice(string name)
        {
            this.name = name;
        }

        public void ReadData()
        {
            Console.WriteLine("{0}传输数据", name);
        }
    }

    class MP3 : IUSB
    {
        public void ReadData()
        {
            Console.WriteLine("MP3传输数据");
        }
    }

    class Computer
    {
        public IUSB usb1;
    }

    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("接口练习题");

            IRegister[] arr = new IRegister[] { new Person(), new Home(), new Car() };

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].Register();
            }

            StorageDevice hd = new StorageDevice("移动硬盘");
            StorageDevice ud = new StorageDevice("U盘");
            MP3 mp3 = new MP3();

            Computer c = new Computer();

            c.usb1 = hd;
            c.usb1.ReadData();

            c.usb1 = ud;
            c.usb1.ReadData();

            c.usb1 = mp3;
            c.usb1.ReadData();

        }
    }
}
