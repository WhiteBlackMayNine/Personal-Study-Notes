using System;

namespace Lesson6_练习题
{
    #region 练习题一 
    //用泛型实现一个单例模式基类

    class SingleBase<T> where T:new()
    {
        private static T instance = new T();

        public static T Instance
        {
            get
            {
                return instance;
            }
        }
    }

    class GameMgr : SingleBase<GameMgr>
    {
        public int value = 10;
    }

    class Test : SingleBase<Test>
    {
        public int value = 10;
    }

    class Test2
    {
        private static Test2 instance = new Test2();

        public int value = 10;
        private Test2()
        {

        }

        public static Test2 Instance
        {
            get
            {
                return instance;
            }
        }
    }
    #endregion

    #region 练习题二
    //利用泛型知识点，仿造ArrayList实现一个不确定数组类型的类
    //实现增删查改方法

    class ArrayList<T>
    {
        private T[] array;

        //当前正儿八经存了多少数
        private int count;

        public ArrayList()
        {
            count = 0;
            //一开始的容量就是16
            array = new T[16];
        }

        public void Add(T value)
        {
            //是否需要扩容
            if( count >= Capacity )
            {
                //搬家 每次 家扩容2倍
                T[] temp = new T[Capacity * 2];
                for (int i = 0; i < Capacity; i++)
                {
                    temp[i] = array[i];
                }
                //重新指向地址
                array = temp;
            }
            
            //不需要扩容 直接加
            array[count] = value;
            ++count;
        }

        public void Remove(T value)
        {
            //这个地方 不是小于数组的容量
            //是小于 具体存了几个值
            int index = -1;
            for (int i = 0; i < Count; i++)
            {
                //不能用==去判断 因为 不是所有的类型都重载了运算符 
                if( array[i].Equals(value))
                {
                    index = i;
                    break;
                }
            }
            //只要不等于-1 就证明找到了 那就去移除
            if(index != -1)
            {
                RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            //索引合法不
            if( index < 0 || index >= Count )
            {
                Console.WriteLine("索引不合法");
                return;
            }

            //后面的往前放
            for (; index < Count - 1; index++)
            {
                array[index] = array[index + 1];
            }
            //把一个数移除了 后面的往前面放 那么最后一个 要移除
            array[Count - 1] = default(T);
            --count;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    Console.WriteLine("索引不合法");
                    return default(T);
                }
                return array[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    Console.WriteLine("索引不合法");
                    return;
                }
                array[index] = value;
            }
        }

        /// <summary>
        /// 获取容量
        /// </summary>
        public int Capacity
        {
            get
            {
                return array.Length;
            }
        }

        /// <summary>
        /// 得到具体存了几个值
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("泛型约束练习题");

            Test.Instance.value = 10;

            //Test t = new Test();
            Test2.Instance.value = 10;
            GameMgr.Instance.value = 10;

            //GameMgr g = new GameMgr();

            ArrayList<int> array = new ArrayList<int>();
            Console.WriteLine(array.Count);
            Console.WriteLine(array.Capacity);
            array.Add(1);
            array.Add(2);
            array.Add(3);
            Console.WriteLine(array.Count);
            Console.WriteLine(array.Capacity);

            Console.WriteLine(array[1]);
            Console.WriteLine(array[-1]);
            Console.WriteLine(array[3]);

            array.RemoveAt(0);
            Console.WriteLine(array.Count);
            for (int i = 0; i < array.Count; i++)
            {
                Console.WriteLine(array[i]);
            }

            Console.WriteLine(array[0]);
            array[0] = 99;
            Console.WriteLine(array[0]);

            ArrayList<string> array2 = new ArrayList<string>();
        }
    }
}
