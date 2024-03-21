using System;

namespace Lesson29_堆排序
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("堆排序");
            int[] arr = new int[] { 8, 7, 1, 5, 4, 2, 6, 3, 9 };
            HeapSort(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }
        #region 知识点一 堆排序基本原理
        //构建二叉树
        //大堆顶调整
        //堆顶往后方
        //不停变堆顶

        //关键规则
        //最大非叶子节点：
        //数组长度/2 - 1

        //父节点和叶子节点：
        //父节点为i 
        //左节点2i+1
        //右节点2i+2
        #endregion

        #region 知识点二 代码实现
        //第一步：实现父节点和左右节点比较
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array">需要排序的数组</param>
        /// <param name="nowIndex">当前作为根节点的索引</param>
        /// <param name="arrayLength">哪些位置没有确定</param>
        static void HeapCompare(int[] array, int nowIndex, int arrayLength)
        {
            //通过传入的索引 得到它对应的左右叶子节点的索引
            //可能算出来的会溢出数组的索引 我们一会再判断
            int left = 2 * nowIndex + 1;
            int right = 2 * nowIndex + 2;
            //用于记录较大数的索引
            int biggerIndex = nowIndex;
            //先比左 再比右
            //不能溢出 
            if( left < arrayLength && array[left] > array[biggerIndex])
            {
                //认为目前最大的是左节点 记录索引
                biggerIndex = left;
            }
            //比较右节点
            if( right < arrayLength && array[right] > array[biggerIndex] )
            {
                biggerIndex = right;
            }
            //如果比较过后 发现最大索引发生变化了 那就以为这要换位置了
            if( biggerIndex != nowIndex )
            {
                int temp = array[nowIndex];
                array[nowIndex] = array[biggerIndex];
                array[biggerIndex] = temp;

                //通过递归 看是否影响了叶子节点他们的三角关系
                HeapCompare(array, biggerIndex, arrayLength);
            }
        }

        //第二步：构建大堆顶
        static void BuildBigHeap(int[] array)
        {
            //从最大的非叶子节点索引 开始 不停的往前 去构建大堆顶
            for (int i = array.Length / 2 - 1; i >= 0 ; i--)
            {
                HeapCompare(array, i, array.Length);
            }
        }

        //第三步：结合大堆顶和节点比较 实现堆排序 把堆顶不停往后移动
        static void HeapSort(int[] array)
        {
            //构建大堆顶
            BuildBigHeap(array);
            //执行过后
            //最大的数肯定就在最上层

            //往屁股后面放 得到 屁股后面最后一个索引
            for (int i = array.Length - 1; i > 0; i--)
            {
                //直接把 堆顶端的数 放到最后一个位置即可
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                //重新进行大堆顶调整
                HeapCompare(array, 0, i);
            }
        }
        #endregion

        #region 知识点三 总结
        //基本原理
        //构建二叉树
        //大堆顶调整
        //堆顶往后方
        //不停变堆顶

        //套路写法
        //3个函数
        //1个堆顶比较
        //1个构建大堆顶
        //1个堆排序

        //重要规则
        //最大非叶子节点索引：
        //数组长度/2 - 1

        //父节点和叶子节点索引：
        //父节点为i 
        //左节点2i+1
        //右节点2i-1

        //注意：
        //堆是一类特殊的树
        //堆的通用特点就是父节点会大于或小于所有子节点
        //我们并没有真正的把数组变成堆
        //只是利用了堆的特点来解决排序问题
        #endregion
    }
}
