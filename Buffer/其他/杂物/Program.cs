using System;

namespace Lesson6_练习题
{
    #region 练习题
    //自定义一个整形数组类，该类中有一个整形数组变量
    //为它封装增删查改的方法

    class IntArray
    {
        private int[] array;
        //房间容量
        private int capacity;
        //当前放了几个房间
        private int length;

        public IntArray()
        {
            capacity = 5;
            length = 0;
            array = new int[capacity];
        }

        //增
        public void Add(int value)
        {
            //如果要增加就涉及扩容
            //扩容就涉及 "搬家"
            if( length < capacity )
            {
                array[length] = value;
                ++length;
            }
            //扩容“搬家”
            else
            {
                capacity *= 2;
                //新房子
                int[] tempArray = new int[capacity];
                //老东西 放进新房子里
                for (int i = 0; i < array.Length; i++)
                {
                    tempArray[i] = array[i];
                }
                //老的房子地址 指向新房子地址
                array = tempArray;

                //往后面放
                array[length] = value;
                ++length;
            }
        }
        //删
        public void Remove(int value)
        {
            //找到 传入值 在哪个位置
            for (int i = 0; i < length; i++)
            {
                if(array[i] == value)
                {
                    RemoveAt(i);
                    return;
                }
            }
            Console.WriteLine("没有在数组中找到{0}", value);
        }
        public void RemoveAt(int index)
        {
            if( index > length - 1 )
            {
                Console.WriteLine("当前数组只有{0},你越界了", length);
                return;
            }
            for (int i = index; i < length - 1; i++)
            {
                array[i] = array[i + 1];
            }
            --length;
        }
        //查改
        public int this[int index]
        {
            get
            {
                if( index >= length || index < 0)
                {
                    Console.WriteLine("越界");
                    return 0;
                }
                return array[index];
            }
            set
            {
                if (index >= length || index < 0)
                {
                    Console.WriteLine("越界");
                }
                array[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("索引器练习题");

            IntArray array = new IntArray();
            array.Add(100);
            array.Add(200);
            ///array.RemoveAt(1);
            Console.WriteLine(array.Length);
            array.Remove(200);
            //Console.WriteLine(array[1]);
            Console.WriteLine(array.Length);
            
        }
    }
}
