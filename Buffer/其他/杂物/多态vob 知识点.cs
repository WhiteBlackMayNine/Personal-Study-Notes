using System;

namespace Lesson17_多态_vob
{
    #region 知识回顾

    #region 封装——用程序语言来形容对象
    class 类名
    {
        #region 特征——成员变量

        #endregion

        #region 行为——成员方法

        #endregion

        #region 初始化调用——构造函数

        #endregion

        #region 释放时调用——析构函数

        #endregion

        #region 保护成员变量——成员属性

        #endregion

        #region 像数组一样使用——索引器

        #endregion

        #region 类名点出使用——静态成员

        #endregion

        #region 自定义对象可计算——运算符重载

        #endregion
    }

    #region 静态类和静态构造函数

    #endregion

    #region 拓展方法

    #endregion
    #endregion

    #region 继承——复用封装对象的代码；儿子继承父亲，复用现成代码

    #region 继承中的构造函数

    #endregion

    #region 里氏替换原则

    #endregion

    #region 万物之父

    #endregion

    #region 装箱拆箱

    #endregion

    #region 密封类

    #endregion

    #endregion

    #endregion

    #region 知识点一 多态的概念
    // 多态按字面的意思就是“多种状态”
    // 让继承同一父类的子类们 在执行相同方法时有不同的表现（状态）
    // 主要目的
    // 同一父类的对象 执行相同行为（方法）有不同的表现
    // 解决的问题
    // 让同一个对象有唯一行为的特征
    #endregion

    #region 知识点二 解决的问题
    class Father
    {
        public void SpeakName()
        {
            Console.WriteLine("Father的方法");
        }
    }

    class Son:Father
    {
        public new void SpeakName()
        {
            Console.WriteLine("Son的方法");
        }
    }
    #endregion

    #region 知识点三 多态的实现
    //我们目前已经学过的多态
    //编译时多态——函数重载，开始就写好的

    //我们将学习的：
    //运行时多态( vob、抽象函数、接口 )
    //我们今天学习 vob
    //v: virtual(虚函数)
    //o: override(重写)
    //b: base(父类)

    class GameObject
    {
        public string name;
        public GameObject(string name)
        {
            this.name = name;
        }

        //虚函数 可以被子类重写
        public virtual void Atk()
        {
            Console.WriteLine("游戏对象进行攻击");
        }
    }

    class Player:GameObject
    {
        public Player(string name):base(name)
        {

        }

        //重写虚函数
        public override void Atk()
        {
            //base的作用
            //代表父类 可以通过base来保留父类的行为
            base.Atk();
            Console.WriteLine("玩家对象进行攻击");
        }
    }

    class Monster:GameObject
    {
        public Monster(string name):base(name)
        {

        }

        public override void Atk()
        {
            Console.WriteLine("怪物对象进行攻击");
        }
    }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("多态vob");

            #region 解决的问题
            Father f = new Son();
            f.SpeakName();
            (f as Son).SpeakName();
            #endregion

            #region 多态的使用
            GameObject p = new Player("唐老狮");
            p.Atk();
            (p as Player).Atk();

            GameObject m = new Monster("小怪物");
            m.Atk();
            (m as Monster).Atk();
            #endregion
        }
    }

    //总结：
    //多态：让同一类型的对象，执行相同行为时有不同的表现
    //解决的问题： 让同一对象有唯一的行为特征
    //vob:
    // v:virtual 虚函数
    // o:override 重写
    // b:base 父类
    // v和o一定是结合使用的 来实现多态
    // b是否使用根据实际需求 保留父类行为
}
