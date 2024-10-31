using System;
using System.Collections.Generic;

namespace Lesson10_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LinkedList练习题");

            #region 练习题
            //使用Linkedlist，向其中加入10个随机整形变量
            //正向遍历一次打印出信息
            //反向遍历一次打印出信息

            LinkedList<int> linkedList = new LinkedList<int>();
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                linkedList.AddLast(r.Next(1, 101));
            }

            LinkedListNode<int> nowNode = linkedList.First;
            while (nowNode != null)
            {
                Console.WriteLine(nowNode.Value);
                nowNode = nowNode.Next;
            }

            Console.WriteLine("********************");
            nowNode = linkedList.Last;
            while (nowNode != null)
            {
                Console.WriteLine(nowNode.Value);
                nowNode = nowNode.Previous;
            }

            #endregion
        }
    }
}
