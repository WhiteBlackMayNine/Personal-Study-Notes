using System;
using System.Collections;

namespace Lesson3_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Queue练习题");

            #region 练习题一
            //请简述队列的存储规则
            //先进先出
            #endregion

            #region 练习题二
            //使用队列存储消息，一次性存10条消息，每隔一段时间打印一条消息
            //控制台打印消息时要有明显停顿感
            Queue queue = new Queue();
            queue.Enqueue("获得10枚金币");
            queue.Enqueue("获得屠龙刀x1");
            queue.Enqueue("获得99枚金币");
            queue.Enqueue("获得血瓶x1");
            queue.Enqueue("获得85枚金币");
            queue.Enqueue("获得11枚金币");
            queue.Enqueue("获得2枚金币");
            queue.Enqueue("获得91枚金币");
            queue.Enqueue("获得73枚金币");
            queue.Enqueue("获得645枚金币");

            int updateIndex = 1;
            while (true)
            {
                if( updateIndex % 99999999 == 0 )
                {
                    if(queue.Count > 0)
                    {
                        Console.WriteLine(queue.Dequeue());
                    }
                    updateIndex = 0;
                }
                ++updateIndex;
            }
            #endregion
        }
    }
}
