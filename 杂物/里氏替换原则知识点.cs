lusing System;

namespace Lesson13_继承_里氏替换原则
{
    #region 知识点一 基本概念
    // 里氏替换原则是面向对象七大原则中最重要的原则
    // 概念：
    // 任何父类出现的地方，子类都可以替代
    // 重点：
    // 语法表现——父类容器装子类对象,因为子类对象包含了父类的所有内容
    // 作用：
    // 方便进行对象存储和管理
    #endregion

    #region 知识点二 基本实现
    class GameObject
    {

    }

    class Player:GameObject
    {
        public void PlayerAtk()
        {
            Console.WriteLine("玩家攻击");
        }
    }

    class Monster:GameObject
    {
        public void MonsterAtk()
        {
            Console.WriteLine("怪物攻击");
        }
    }

    class Boss:GameObject
    {
        public void BossAtk()
        {
            Console.WriteLine("Boss攻击");
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("里氏替换原则");

            //里氏替换原则 用父类容器 装载子类对象
            GameObject player = new Player();
            //player变量的类型是GameObject，但player引用的对象的实际类型是Player
            GameObject monster = new Monster();
            GameObject boss = new Boss();

            GameObject[] objects = new GameObject[] { new Player(), new Monster(), new Boss() };

            #region 知识点三 is和as
            //基本概念 
            // is：判断一个对象是否是指定类对象
            // 返回值：bool  是为真 不是为假

            // as：将一个对象转换为指定类对象
            // 返回值：指定类型对象
            // 成功返回指定类型对象，失败返回null

            //基本语法
            // 类对象 is 类名   该语句块 会有一个bool返回值 true和false
            // 类对象 as 类名   该语句块 会有一个对象返回值 对象和null

            if( player is Player )
            {
                //Player p = player as Player;
                //p.PlayerAtk();
                (player as Player).PlayerAtk();
            }

            for (int i = 0; i < objects.Length; i++)
            {
                if( objects[i] is Player )
                {
                    (objects[i] as Player).PlayerAtk();
                }
                else if( objects[i] is Monster )
                {
                    (objects[i] as Monster).MonsterAtk();
                }
                else if (objects[i] is Boss)
                {
                    (objects[i] as Boss).BossAtk();
                }
            }

            #endregion
        }
    }
    //总结
    //概念：父类容器装子类对象
    //作用：方便进行对象的存储和管理
    //使用：is和as
    // is 用于判断
    // as 用于转换
    // 注意：不能用子类容器装父类对象
}
