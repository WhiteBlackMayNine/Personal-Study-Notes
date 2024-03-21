using System;

namespace Lesson13_冒泡排序
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("冒泡排序");

            #region 知识点一 排序的基本概念
            //排序是计算机内经常进行的一种操作，其目的是将一组“无序”的记录序列调整为“有序”的记录序列
            //常用的排序例子
            // 8 7 1 5 4 2 6 3 9
            // 把上面的这个无序序列 变为 有序（升序或降序）序列的过程
            // 1 2 3 4 5 6 7 8 9 （升序）
            // 9 8 7 6 5 4 3 2 1 （降序）

            //在程序中 序列一般 存储在数组当中
            //所以 排序往往是对 数组进行排序
            int[] arr = new int[] { 8, 7, 1, 5, 4, 2, 6, 3, 9, };
            //把数组里面的内容变为有序的
            #endregion

            #region 知识点二 冒泡排序基本原理
            // 8 7 1 5 4 2 6 3 9
            // 两两相邻
            // 不停比较
            // 不停交换
            // 比较n轮

            #endregion

            #region 知识点三 代码实现

            //第一步 如何比较数组中两两相邻的数
            //8, 7, 1, 5, 4, 2, 6, 3, 9
            //从头开始
            //第n个数和第n+1个数 比较
            //for (int n = 0; n < arr.Length - 1; n++)
            //{
            //    //如果 第n个数 比第n+1个数 大 他们就要交换位置
            //    if( arr[n] > arr[n + 1] )
            //    {
            //        // 第二步 交换位置
            //        // 中间商不赚差价 
            //        int temp = arr[n];
            //        arr[n] = arr[n + 1];
            //        arr[n + 1] = temp;
            //    }
            //}

            //第三部 如何换m轮？
            //有几个数 就比较多少轮
            //for (int m = 0; m < arr.Length; m++)
            //{
            //    // 尽一次循环 就需要比较一轮
            //    for (int n = 0; n < arr.Length - 1; n++)
            //    {
            //        //如果 第n个数 比第n+1个数 大 他们就要交换位置
            //        if (arr[n] > arr[n + 1])
            //        {
            //            // 第二步 交换位置
            //            // 中间商不赚差价 
            //            int temp = arr[n];
            //            arr[n] = arr[n + 1];
            //            arr[n + 1] = temp;
            //        }
            //    }
            //}

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.WriteLine(arr[i]);
            //}

            //第四步 优化
            //1.确定位置的数字 不用比较了 
            // 确定了一轮后 极值（最大或者最小）已经放到了对应的位置（往后放）
            // 所以 没完成n轮 后面位置的数 就不用再参与比较了
            //for (int m = 0; m < arr.Length; m++)
            //{
            //    // 尽一次循环 就需要比较一轮
            //    for (int n = 0; n < arr.Length - 1 - m; n++)
            //    {
            //        //如果 第n个数 比第n+1个数 大 他们就要交换位置
            //        if (arr[n] > arr[n + 1])
            //        {
            //            // 第二步 交换位置
            //            // 中间商不赚差价 
            //            int temp = arr[n];
            //            arr[n] = arr[n + 1];
            //            arr[n + 1] = temp;
            //        }
            //    }
            //}
            //外面申明一个标识 来表示 该轮是否进行了交换
            bool isSort = false;
            //2.特使情况的优化
            for (int m = 0; m < arr.Length; m++)
            {
                //每一轮开始时 默认没有进行过交换
                isSort = false;
                // 尽一次循环 就需要比较一轮
                for (int n = 0; n < arr.Length - 1 - m; n++)
                {
                    //如果 第n个数 比第n+1个数 大 他们就要交换位置
                    if (arr[n] > arr[n + 1])
                    {
                        isSort = true;
                        // 第二步 交换位置
                        // 中间商不赚差价 
                        int temp = arr[n];
                        arr[n] = arr[n + 1];
                        arr[n + 1] = temp;
                    }
                }
                //当一轮结束过后 如果isSort这个标识 还是false
                //那就意味着 已经是最终的序列了 不需要再判断交换了
                if( !isSort )
                {
                    break;
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

            #endregion

            //总结
            //基本概念
            //两两相邻
            //不停比较
            //不停交换
            //比较m轮

            //套路写法
            //两层循环
            //外层轮数
            //内层比较
            //两值比较
            //满足交换

            //如果优化
            //1.比过不比 
            //2.加入bool
        }
    }
}
