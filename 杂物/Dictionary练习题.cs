using System;
using System.Collections.Generic;

namespace Lesson8_练习题
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dictionary练习题");

            #region 练习题一
            //使用字典存储0~9的数字对应的大写文字
            //提示用户输入一个不超过三位的数，提供一个方法，返回数的大写
            //例如：306，返回叁零陆
            try
            {
                Console.WriteLine("请输入一个不超过3位的数");
                Console.WriteLine(GetInfo(int.Parse(Console.ReadLine())));
            }
            catch
            {
                Console.WriteLine("请输入数字");
            }
            #endregion

            #region 练习题二
            //计算每个字母出现的次数“Welcome to Unity World！”，使用字典存储，最后遍历整个字典，不区分大小写
            Dictionary<char, int> dic = new Dictionary<char, int>();
            string str = "Welcome to Unity World！";
            str = str.ToLower();
            for (int i = 0; i < str.Length; i++)
            {
                if( dic.ContainsKey(str[i]) )
                {
                    dic[str[i]] += 1;
                }
                else
                {
                    dic.Add(str[i], 1);
                }
            }

            foreach (char item in dic.Keys)
            {
                Console.WriteLine("字母{0}出现了{1}次", item, dic[item]);
            }
            #endregion
        }

        static string GetInfo(int num)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, "零");
            dic.Add(1, "壹");
            dic.Add(2, "贰");
            dic.Add(3, "叁");
            dic.Add(4, "肆");
            dic.Add(5, "伍");
            dic.Add(6, "陆");
            dic.Add(7, "柒");
            dic.Add(8, "捌");
            dic.Add(9, "玖");

            string str = "";
            //得百位
            int b = num / 100;
            if( b != 0 )
            {
                str += dic[b];
            }
            //得十位数
            int s = num % 100 / 10;
            if( s != 0 || str != "" )
            {
                str += dic[s];
            }
            //得个位
            int g = num % 10;
            str += dic[g];

            return str;
        }
    }
}
