using System;

namespace Lesson27_归并排序
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("归并排序");
            int[] arr = new int[] { 8, 7, 1, 5, 4, 2, 6, 3, 9 };

            arr = Merge(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

        #region 知识点一 归并排序基本原理
        //归并 = 递归 + 合并

        //数组分左右
        //左右元素相比较
        //满足条件放入新数组
        //一侧用完放对面

        //递归不停分
        //分完再排序
        //排序结束往上走
        //边走边合并
        //走到头顶出结果

        //归并排序分成两部分
        //1.基本排序规则
        //2.递归平分数组

        //递归平分数组：
        //不停进行分割
        //长度小于2停止
        //开始比较
        //一层一层向上比

        //基本排序规则：
        //左右元素进行比较
        //依次放入新数组中
        //一侧没有了另一侧直接放入新数组
        #endregion

        #region 知识点二 代码实现
        
        //第一步：
        //基本排序规则
        //左右元素相比较
        //满足条件放进去
        //一侧用完直接放
        public static int[] Sort(int[] left, int[] right)
        {
            //先准备一个新数组
            int[] array = new int[left.Length + right.Length];
            int leftIndex = 0;//左数组索引
            int rightIndex = 0;//右数组索引

            //最终目的是要填满这个新数组
            //不会出现两侧都放完还在进循环 
            //因为这个新数组的长度 是根据左右两个数组长度计算出来的
            for (int i = 0; i < array.Length; i++)
            {
                //左侧放完了 直接放对面右侧
                if( leftIndex >= left.Length )
                {
                    array[i] = right[rightIndex];
                    //已经放入了一个右侧元素进入新数组
                    //所以 标识应该指向下一个嘛
                    rightIndex++;
                }
                //右侧放完了 直接放对面左侧
                else if( rightIndex >= right.Length )
                {
                    array[i] = left[leftIndex];
                    //已经放入了一个左侧元素进入新数组
                    //所以 标识应该指向下一个嘛
                    leftIndex++;
                }
                else if( left[leftIndex] < right[rightIndex] )
                {
                    array[i] = left[leftIndex];
                    //已经放入了一个左侧元素进入新数组
                    //所以 标识应该指向下一个嘛
                    leftIndex++;
                }
                else
                {
                    array[i] = right[rightIndex];
                    //已经放入了一个右侧元素进入新数组
                    //所以 标识应该指向下一个嘛
                    rightIndex++;
                }
            }

            //得到了新数组 直接返回出去
            return array;
        }

        //第二步：
        //递归平分数组
        //结束条件为长度小于2

        public static int[] Merge(int[] array)
        {
            //递归结束条件
            if (array.Length < 2)
                return array;
            //1.数组分两段  得到一个中间索引
            int mid = array.Length / 2;
            //2.初始化左右数组
            //左数组
            int[] left = new int[mid];
            //右数组
            int[] right = new int[array.Length - mid];
            //左右初始化内容
            for (int i = 0; i < array.Length; i++)
            {
                if (i < mid)
                    left[i] = array[i];
                else
                    right[i - mid] = array[i];
            }
            //3.递归再分再排序
            return Sort(Merge(left), Merge(right));
        }
        #endregion

        #region 知识点三 总结
        //理解递归逻辑
        //一开始不会执行Sort函数的
        //要先找到最小容量数组时
        //才会回头递归调用Sort进行排序

        //基本原理
        // 归并 = 递归 + 合并
        // 数组分左右
        // 左右元素相比较
        // 一侧用完放对面
        // 不停放入新数组

        //递归不停分
        //分完再排序
        //排序结束往上走
        //边走边合并
        //走到头顶出结果

        //套路写法
        //两个函数
        //一个基本排序规则
        //一个递归平分数组

        //注意事项
        //排序规则函数 在 平分数组函数
        //内部 return调用

        #endregion
    }
}
