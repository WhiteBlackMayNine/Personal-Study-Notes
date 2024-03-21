using System;

namespace Lesson24_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("string和StringBuilder");
            #region 练习题一 请描述string和stringbuilder的区别
            //1.string相对stringbuilder 更容易产生垃圾 每次修改拼接都会产生垃圾
            //2.string相对stringbuilder 更加灵活 因为它提供了更多的方法供使用
            //如何选择他们两
            //需要频繁修改拼接的字符串可以使用stringbuilder
            //需要使用string独特的一些方法来处理一些特殊逻辑时可以使用string
            #endregion

            #region 练习题二 如何优化内存
            //内存优化 从两个方面去解答 
            //1.如何节约内存
            //2.如何尽量少的GC（垃圾回收）

            //少new对象 少产生垃圾
            //合理使用static 
            //合理使用string和stringbuilder
            #endregion
        }
    }
}
