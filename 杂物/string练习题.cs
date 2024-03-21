using System;

namespace Lesson23_练习题
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("string练习题");
            #region 练习题一
            //请写出string中提供的截取和替换对应的函数名
            //string str = "唐老狮唐老狮";
            //str = str.Substring(1);
            //str = str.Substring(1, 1);

            //str = "唐老狮唐老狮";
            //str = str.Replace("唐老狮", "哈哈哈");
            #endregion

            #region 练习题二
            //请将字符串1 | 2 | 3 | 4 | 5 | 6 | 7
            //变为     2 | 3 | 4 | 5 | 6 | 7 | 8
            //并输出
            //（使用字符串切割的方法）
            //str = "1 | 2 | 3 | 4 | 5 | 6 | 7";
            //string[] strs = str.Split('|');
            //str = "";
            //for (int i = 0; i < strs.Length; i++)
            //{
            //    str += int.Parse(strs[i]) + 1;
            //    if( i != strs.Length - 1 )
            //    {
            //        str += "|";
            //    }
            //}
            //Console.WriteLine(str);
            #endregion

            #region 练习题三
            //String和string、Int32和int、Int16和short、Int64和long他们的区别是什么？
            //string int short long
            #endregion

            #region 练习题四
            string str = null;
            str = "123";
            string str2 = str;
            str2 = "321";
            str2 += "123";
            //请问，上面这段代码，分配了多少个新的堆空间
            #endregion

            #region 练习题五
            //编写一个函数，将输入的字符串反转。不要使用中间商，你必须原地修改输入数组。交换过程中不使用额外空间
            //比如：输入{‘h’,‘e’,‘l’,‘l’,‘o’}
            //输出      {‘o’,‘l’,‘l’,‘e’,‘h’}

            Console.WriteLine("请输入内容");
            string str3 = Console.ReadLine();
            char[] chars = str3.ToCharArray();
            for (int i = 0; i < chars.Length / 2; i++)
            {
                //交换
                //char temp = chars[i];
                //chars[i] = chars[chars.Length - 1 - i];
                //chars[chars.Length - 1 - i] = temp;
                chars[i] = (char)(chars[i] + chars[chars.Length - 1 - i]);
                chars[chars.Length - 1 - i] = (char)(chars[i] - chars[chars.Length - 1 - i]);
                chars[i] = (char)(chars[i] - chars[chars.Length - 1 - i]);
            }

            for (int i = 0; i < chars.Length; i++)
            {
                Console.Write(chars[i]);
            }
            Console.WriteLine();
            str3 = new string(chars);
            Console.WriteLine(str3);

            #endregion
        }
    }
}
