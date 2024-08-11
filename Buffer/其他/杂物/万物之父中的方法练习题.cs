using System;

namespace Lesson22_练习题
{
    #region 练习题一
    //有一个玩家类，有姓名，血量，攻击力，防御力，闪避率等特征
    //请在控制台打印出“玩家XX，血量XX，攻击力XX，防御力XX”XX为具体内容

    class Player
    {
        private string name;
        private int hp;
        private int atk;
        private int def;
        private int miss;

        public Player(string name, int hp, int atk, int def, int miss)
        {
            this.name = name;
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.miss = miss;
        }

        public override string ToString()
        {
            return string.Format("玩家{0},血量{1},攻击力{2},防御力{3},闪避{4}", name, hp, atk, def, miss);
        }
    }
    #endregion

    #region 练习题二
    //一个Monster类的引用对象A，Monster类有 攻击力、防御力、血量、技能ID等属性。
    //我想复制一个和A对象一模一样的B对象。并且改变了B的属性，A不会受到影响。请问如何实现？

    class Monster
    {
        public Monster(int atk, int def, int hp, int skillID)
        {
            Atk = atk;
            Def = def;
            Hp = hp;
            SkillID = skillID;
        }

        public int Atk
        {
            get;
            set;
        }

        public int Def
        {
            get;
            set;
        }
        public int Hp
        {
            get;
            set;
        }
        public int SkillID
        {
            get;
            set;
        }

        public Monster Clone()
        {
            return MemberwiseClone() as Monster;
        }

        public override string ToString()
        {
            return string.Format("攻击里{0},防御力{1},血量{2},技能{3}", Atk, Def, Hp, SkillID);
        }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("万物之父中的方法");

            Player p = new Player("唐老狮", 100, 10, 5, 20);
            Console.WriteLine(p);

            Monster m = new Monster(10, 20, 100, 1);
            Monster m2 = m.Clone();
            m2.Atk = 30;
            m2.SkillID = 100;
            Console.WriteLine(m);
            Console.WriteLine(m2);
        }
    }
}
