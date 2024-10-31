using System;

namespace Lesson13_练习题
{
    #region 练习题一
    //is和as的区别是什么
    // is 用于判断  类对象判断是不是某一个类型的对象   bool 
    // as 用于转换  把一个类对象 转换成一个指定类型的对象   null
    #endregion

    #region 练习题二
    //写一个Monster类，它派生出Boss和Goblin两个类，
    //Boss有技能；小怪有攻击；
    //随机生成10个怪，装载到数组中
    //遍历这个数组，调用他们的攻击方法，如果是boss就释放技能
    class Monster
    {

    }

    class Boss : Monster
    {
        public void Skill()
        {
            Console.WriteLine("boss技能");
        }
    }

    class Goblin:Monster
    {
        public void Atk()
        {
            Console.WriteLine("哥布林攻击");
        }
    }
    #endregion

    #region 练习题三
    //FPS游戏模拟
    //写一个玩家类，玩家可以拥有各种武器
    //现在有四种武器，冲锋枪，散弹枪，手枪，匕首
    //玩家默认拥有匕首
    //请在玩家类中写一个方法，可以拾取不同的武器替换自己拥有的武器
    class Weapon
    {

    }
    //冲锋枪
    class SubmachineGun : Weapon
    {

    }
    //散弹枪
    class ShotGun : Weapon
    {

    }
    //手枪
    class Pistol : Weapon
    {

    }
    //匕首
    class Dagger:Weapon
    {

    }
    
    class Player
    {
        private Weapon nowHaveWeapon;

        public Player()
        {
            nowHaveWeapon = new Dagger();
        }

        public void PickUp(Weapon weapon)
        {
            nowHaveWeapon = weapon;
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("里氏替换练习题");

            //随机生成10个怪物 
            Random r = new Random();
            int randomNum;
            Monster[] monsters = new Monster[10];
            for (int i = 0; i < monsters.Length; i++)
            {
                randomNum = r.Next(1, 101);
                if( randomNum < 50 )
                {
                    monsters[i] = new Boss();
                }
                else
                {
                    monsters[i] = new Goblin();
                }
            }
            //遍历10个怪物 执行对应方法
            for (int i = 0; i < monsters.Length; i++)
            {
                if( monsters[i] is Boss )
                {
                    (monsters[i] as Boss).Skill();
                }
                else
                {
                    (monsters[i] as Goblin).Atk();
                }
            }

            Player p = new Player();

            SubmachineGun s = new SubmachineGun();
            p.PickUp(s);

            ShotGun sg = new ShotGun();
            p.PickUp(sg);
        }
    }
}
