using System;

namespace Lesson23_面向对象相关_string
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("string");

            #region 知识点一 字符串指定位置获取
            //字符串本质是char数组
            string str = "唐老狮";
            Console.WriteLine(str[0]);
            //转为char数组
            char[] chars = str.ToCharArray();
            Console.WriteLine(chars[1]);

            for (int i = 0; i < str.Length; i++)
            {
                Console.WriteLine(str[i]);
            }
            #endregion

            #region 知识点二 字符串拼接
            str = string.Format("{0}{1}", 1, 3333);
            Console.WriteLine(str);
            #endregion

            #region 知识点三 正向查找字符位置
            str = "我是唐老狮！";
            int index = str.IndexOf("唐");
            Console.WriteLine(index);

            index = str.IndexOf("吊");
            Console.WriteLine(index);
            #endregion

            #region 知识点四 反向查找指定字符串位置
            str = "我是唐老狮唐老狮";

            index = str.LastIndexOf("唐老狮");
            Console.WriteLine(index);

            index = str.LastIndexOf("唐老师");
            Console.WriteLine(index);
            #endregion

            #region 知识点五 移除指定位置后的字符
            str = "我是唐老狮唐老狮";
            str.Remove(4);
            Console.WriteLine(str);
            str = str.Remove(4);
            Console.WriteLine(str);

            //执行两个参数进行移除
            //参数1 开始位置
            //参数2 字符个数
            str = str.Remove(1, 1);
            Console.WriteLine(str);
            #endregion

            #region 知识点六 替换指定字符串
            str = "我是唐老狮唐老狮";
            str.Replace("唐老狮", "老炮儿");
            Console.WriteLine(str);
            str = str.Replace("唐老狮", "老炮儿");
            Console.WriteLine(str);
            #endregion

            #region 知识点七 大小写转换
            str = "ksdfasdfasfasdfsasdfasdf";
            str.ToUpper();
            Console.WriteLine(str);
            str = str.ToUpper();
            Console.WriteLine(str);

            str.ToLower();
            Console.WriteLine(str);
            str = str.ToLower();
            Console.WriteLine(str);
            #endregion

            #region 知识点八 字符串截取
            str = "唐老狮唐老狮";
            //截取从指定位置开始之后的字符串
            str.Substring(2);
            Console.WriteLine(str);
            str = str.Substring(2);
            Console.WriteLine(str);

            //参数一 开始位置
            //参数二 指定个数
            //不会自动的帮助你判断是否越界 你需要自己去判断
            str = str.Substring(2, 2);
            Console.WriteLine(str);
            #endregion

            #region 知识点九 字符串切割
            str = "1_1|2_2|3_3|5_1|6_1|7_2|8_3";
            string[] strs = str.Split('|');
            for (int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine(strs[i]);
            }
            #endregion
        }
    }
}
