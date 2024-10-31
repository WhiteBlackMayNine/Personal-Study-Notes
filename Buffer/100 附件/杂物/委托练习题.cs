using System;

namespace Lesson12_练习题
{
    #region 练习题一
    //一家三口，妈妈做饭，爸爸妈妈和孩子都要吃饭
    //用委托模拟做饭——>开饭——>吃饭的过程
    abstract class Person
    {
        public abstract void Eat();
    }

    class Mother : Person
    {
        public Action beginEat;

        public override void Eat()
        {
            Console.WriteLine("妈妈吃饭");
        }

        public void DoFood()
        {
            Console.WriteLine("妈妈做饭");

            Console.WriteLine("妈妈做饭做好了");

            //执行委托函数
            if(beginEat != null)
            {
                beginEat();
            }
        }
    }

    class Father:Person
    {
        public override void Eat()
        {
            Console.WriteLine("爸爸吃饭");
        }
    }

    class Son:Person
    {
        public override void Eat()
        {
            Console.WriteLine("孩子吃饭");
        }
    }

    #endregion

    #region 练习题二
    //怪物死亡后，玩家要加10块钱，界面要更新数据
    //成就要累加怪物击杀数，请用委托来模拟实现这些功能
    //只用写核心逻辑表现这个过程，不用写的太复杂

    class Monster
    {
        //当怪物死亡时 把自己作为参数传出去 
        public Action<Monster> deadDoSomthing;
        //怪物成员变量 特征 价值多少钱
        public int money = 10;

        public void Dead()
        {
            Console.WriteLine("怪物死亡");
            if(deadDoSomthing != null)
            {
                deadDoSomthing(this);
            }
            //一般情况下 委托关联的函数 有加 就有减（或者直接清空）
            deadDoSomthing = null;
        }
    }

    class Player
    {
        private int myMoney = 0;

        public void MonsterDeadDoSomthing(Monster m)
        {
            myMoney += m.money;
            Console.WriteLine("现在有{0}元钱", myMoney);
        }
    }

    class Panel
    {
        private int nowShowMoney = 0;

        public void MonsterDeadDo(Monster m)
        {
            nowShowMoney += m.money;
            Console.WriteLine("当前面板显示{0}元钱", nowShowMoney);
        }
    }

    class CJ
    {
        private int nowKillMonsterNum = 0;

        public void MonsterDeadDo(Monster m)
        {
            nowKillMonsterNum += 1;
            Console.WriteLine("当前击杀了{0}怪物", nowKillMonsterNum);
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("委托练习题");

            Mother m = new Mother();
            Father f = new Father();
            Son s = new Son();
            //告诉妈妈 一会做好了 我要吃
            m.beginEat += f.Eat;
            m.beginEat += s.Eat;
            m.beginEat += m.Eat;
            //做饭
            m.DoFood();

            Monster monster = new Monster();
            Player p = new Player();
            Panel panel = new Panel();
            CJ cj = new CJ();

            monster.deadDoSomthing += p.MonsterDeadDoSomthing;
            monster.deadDoSomthing += panel.MonsterDeadDo;
            monster.deadDoSomthing += cj.MonsterDeadDo;

            monster.Dead();
            monster.Dead();

            Monster monster2 = new Monster();
            monster2.deadDoSomthing += p.MonsterDeadDoSomthing;
            monster2.deadDoSomthing += panel.MonsterDeadDo;
            monster2.deadDoSomthing += cj.MonsterDeadDo;
            monster2.Dead();
        }
    }
}
