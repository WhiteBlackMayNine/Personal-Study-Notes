﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lesson16_List排序
{
    class Item : IComparable<Item>
    {
        public int money;

        public Item(int money)
        {
            this.money = money;
        }

        public int CompareTo(Item other)
        {
            //返回值的含义
            //小于0：
            //放在传入对象的前面
            //等于0:
            //保持当前的位置不变
            //大于0：
            //放在传入对象的后面

            //可以简单理解 传入对象的位置 就是0
            //如果你的返回为负数 就放在它的左边 也就前面
            //如果你返回正数 就放在它的右边 也就是后面

            if( this.money > other.money )
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    class ShopItem
    {
        public int id;

        public ShopItem(int id)
        {
            this.id = id;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("List排序");
            #region 知识点一 List自带排序方法
            List<int> list = new List<int>();
            list.Add(3);
            list.Add(2);
            list.Add(6);
            list.Add(1);
            list.Add(4);
            list.Add(5);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            //list提供了排序方法
            list.Sort();
            Console.WriteLine("**************");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            //ArrayList中也有Sort排序方法
            #endregion

            #region 知识点二 自定义类的排序
            List<Item> itemList = new List<Item>();
            itemList.Add(new Item(45));
            itemList.Add(new Item(10));
            itemList.Add(new Item(99));
            itemList.Add(new Item(24));
            itemList.Add(new Item(100));
            itemList.Add(new Item(12));
            //排序方法
            itemList.Sort();
            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine(itemList[i].money);
            }
            #endregion

            #region 知识点三 通过委托函数进行排序
            List<ShopItem> shopItems = new List<ShopItem>();
            shopItems.Add(new ShopItem(2));
            shopItems.Add(new ShopItem(1));
            shopItems.Add(new ShopItem(4));
            shopItems.Add(new ShopItem(3));
            shopItems.Add(new ShopItem(6));
            shopItems.Add(new ShopItem(5));

            //shopItems.Sort(SortShopItem);
            //匿名函数
            //shopItems.Sort(delegate (ShopItem a, ShopItem b)
            //{
            //    if (a.id > b.id)
            //    {
            //        return -1;
            //    }
            //    else
            //    {
            //        return 1;
            //    }
            //});
            //lambad表达式 配合 三目运算符的 完美呈现
            shopItems.Sort((a, b) =>{ return a.id > b.id ? 1 : -1;});

            Console.WriteLine("*********************");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine(shopItems[i].id);
            }
            #endregion
        }

        static int SortShopItem( ShopItem a, ShopItem b )
        {
            //传入的两个对象 为列表中的两个对象
            //进行两两的比较  用左边的和右边的条件 比较
            //返回值规则 和之前一样 0做标准 负数在左（前） 正数在右（后）
            if (a.id > b.id)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    //总结
    //系统自带的变量(int,float,double.....) 一般都可以直接Sort
    //自定义类SOrt有两种方式
    //1.继承接口 IComparable
    //2.在Sort中传入委托函数

}
