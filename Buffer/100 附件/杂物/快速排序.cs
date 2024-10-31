using System;

namespace Lesson28_快速排序
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("快速排序");
            int[] arr = new int[] { 8, 7, 1, 5, 4, 2, 6, 3, 9 };
            QuickSort(arr, 0, arr.Length - 1);

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

        #region 知识点一 快速排序基本原理
        //选取基准
        //产生左右标识
        //左右比基准
        //满足则换位

        //排完一次
        //基准定位

        //左右递归
        //直到有序
        #endregion

        #region 知识点二 代码实现
        //第一步：
        //申明用于快速排序的函数
        public static void QuickSort(int[] array, int left, int right)
        {
            //第七步：
            //递归函数结束条件
            if (left >= right)
                return;

            //第二步：
            //记录基准值
            //左游标
            //右游标
            int tempLeft, tempRight, temp;
            temp = array[left];
            tempLeft = left;
            tempRight = right;
            //第三步：
            //核心交换逻辑
            //左右游标会不同变化 要不相同时才能继续变化
            while(tempLeft != tempRight)
            {
                //第四步：比较位置交换
                //首先从右边开始 比较 看值有没有资格放到表示的右侧
                while (tempLeft < tempRight &&
                    array[tempRight] > temp)
                {
                    tempRight--;
                }
                //移动结束证明可以换位置
                array[tempLeft] = array[tempRight];

                //上面是移动右侧游标
                //接着移动完右侧游标 就要来移动左侧游标
                while (tempLeft < tempRight &&
                    array[tempLeft] < temp)
                {
                    tempLeft++;
                }
                //移动结束证明可以换位置
                array[tempRight] = array[tempLeft];
            }
            //第五步：放置基准值
            //跳出循环后 把基准值放在中间位置
            //此时tempRight和tempLeft一定是相等的
            array[tempRight] = temp;

            //第六步：
            //递归继续
            QuickSort(array, left, tempRight - 1);
            QuickSort(array, tempLeft + 1, right);
        }
        #endregion

        #region 知识点三 总结
        //归并排序和快速排序都会用到递归
        //两者的区别
        //相同点：
        //1.他们都会用到递归
        //2.都会把数组分成几部分
        //不同点：
        //1.归并排序递归过程中会不停产生新数组用于合并；快速排序不会产生新数组
        //2.归并排序是拆分数组完毕后再进行排序；快速排序是边排序边拆分

        //基本原理
        //选取基准
        //产生左右标识
        //左右比基准
        //满足则换位
        //排完一次 基准定位
        //基准左右递归
        //直到有序

        //套路写法
        //基准值变量
        //左右游标记录

        //3层while循环
        //游标不停左右移动
        //重合则结束
        //结束定基准

        //递归排左右
        //错位则结束

        //注意事项
        //左右互放
        //while循环外定基准
        #endregion
    }
}
