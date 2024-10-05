using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _11
{
    #region 拓展方法类 必须在顶级

    static class TuoZhanFangFaClass
    {
        #region 拓展方法

        //为现有的类型添加新的方法
        //this 要拓展的类型 参数 ，参数1，参数2
        public static void TuoZhanFangFa(this int a, int b)
        {

        }

        //写完之后，Int 类型就多出了一个 TuoZhanFangFa 

        #endregion
    }

    #endregion

    class Program
    {
        #region 结构体
        //结构体只能声明到类外部
        //结构体和类很像
        struct MyStudent
        {
            public int age;
            public float high;
            private int old;


            public void Add()
            {
                this.age = 800;
            }


            //结构不能包含显示的无参构造函数
            //所以构造函数必须要传入参数
            public MyStudent(int age, float high, int old)
            {
                this.age = age;
                this.high = high;
                this.old = old;
            }
        }

        #endregion

        #region 函数重载 例子

        class Edit
        {
            void Add(int a)
            {

            }

            void Add(int a, int b)
            {

            }
        }

        #endregion

        #region 类和对象 —— 封装

        class Text
        {
            #region 成员变量、访问修饰符、成员方法

            public string name;
            public int id;
            public float high;
            private float high2;//外部不可访问（包括子类）
            protected float high3;//外部不可访问，但内部可以访问

            public void Add()
            {
                id += 10;
            }

            public void Remove(int a)
            {
                id -= a;
            }

            protected void Add2()
            {

            }

            //函数重载
            private void Remove()
            {

            }

            #endregion

            #region 成员属性

            //传统的属性声明方式
            private string nameShuXing;
            public string Name
            {
                get
                {
                    return nameShuXing;
                }
                //属性内部也可以添加 访问修饰符
                private set
                {
                    //value 为 外部调用时 传进来的参数
                    nameShuXing = value;
                }
            }

            //这也是一个属性，表达式体属性声明，只包含 get 只读
            //返回的是 age 的值
            private int age = 20;
            public int Age => age;

            #endregion

            #region 构造函数 析构函数

            #region 构造函数

            //构造函数 当类实例化的时候会自动调用，用于初始化类的变量
            //如果不写，默认存在一个无参构造函数
            //注意 如果不自己实现无参构造函数 却 实现了 有参构造函数，会失去默认的无参构造

            //this ——> 指代类本身的变量
            //——> 调用同一个类的其他构造函数（根据传入的参数去寻找符合的 构造函数重载）

            //语法 无返回值，函数名与类名相同
            public Text()
            {
                id = 10;
                name = "asdfg";
            }
            //重载 有参数的
            public Text(int a)
            {
                high = a;
            }
            //重载 两个参数 同时 会先去调用一个 无参构造
            public Text(int b, int c) : this()
            {

            }
            //重载 三个参数 同时会先去调用一个 传入参数为 两个 int 的 有参构造函数
            public Text(int b, int c, int d) : this(10, 20)
            {

            }

            #endregion

            #region 析构函数 仅作了解

            //当引用类型的堆内存被回收时，会调用该函数
            //对于需要手动管理内存的语言（C++），需要在析构函数中做一些内存回收处理
            //但 C# 中存在自动垃圾回收机制GC
            //在Unity开发中析构函数几乎不会被使用

            //基本语法
            // ~类名()

            ~Text()
            {

            }

            #endregion

            #region 引用类型生命周期

            //出生在构造函数（new 一个构造函数）
            //析构函数是它的死亡，垃圾回收时会死亡
            //内存满或手动调用GC时会触发垃圾回收

            #endregion

            #endregion

            #region 静态成员

            public static int Static_a;

            int Static_bShiLiHuaDuiXiang = 0;

            public static void AddStatic()
            {
                Static_a += 10;
                //静态方法中只能调用静态变量与其他的静态方法
                //不能调用实例对象与方法
                //Static_bShiLiHuaDuiXiang += 10; //报错
            }

            #endregion

            #region 运算符重载 （可以写在静态类中）

            //特点
            //一定是一个公共的静态方法  返回值写在 operator（关键字）前 不能使用 ref 和 out
            //参数之中 必须包含 这个类的实例化对象

            public static int operator +(Text a, float b)
            {
                float c = a.high - b;
                return (int)(c - 10);
            }


            #endregion

            #region 内部类和分部类

            //内部类 ——> 在一个类的内部去声明一个类

            public class TextNeiBuLei
            {
                public int textNeiBuLei_id = 0;
                public void TextNeiBuLei_Add()
                {

                }
            }

            //分部类 略
            //谁没事用这玩意？？？

            #endregion

        }

        #region 静态类

        public static class StaticClass
        {
            #region 静态成员

            //静态类只能声明 静态变量、静态方法
            //int a; 报错

            //静态类 不能被实例化，与程序一同生成消耗，且只有一份
            //所以一般用于制作工具类（比如 计算角度、距离）

            public static int staticClass_id;

            public static int StaticClass_id => staticClass_id;

            public static int StaticClass_age
            {
                get
                {
                    return 10;
                }
                set
                {
                    staticClass_id += value;
                }
            }

            public static void StaticClassAdd()
            {

            }

            //静态构造函数
            //在普通的构造函数前面加上 static 
            //普通的类 也可以有静态构造函数
            static StaticClass()
            {

            }

            #endregion

        }

        #endregion


        #endregion

        #region 类和对象 —— 继承

        #region 继承的基本规则

        //首先，一个类仅能继承一个父类
        //public ——> 外部子类皆可调用 private 仅自己 protect 子类可调用
        //继承之后，子类可使用父类所有的成员（访问修饰符）

        //单根性：子类只能有一个父类
        //传递性：子类可以间接继承父类的父类

        class JiChengText_1
        {
            public int JiChengText_1_temp;
            private int JiChengText_1_pritemp;
            protected int JiChengText_1_protemp;
            public void JiChengText_1_FangFa()
            {

            }
        }

        class JiChengText_2 : JiChengText_1
        {
            public int JiChengText_2_temp;
            public void JiChengText_2_FangFa()
            {

            }
        }

        #endregion

        #region 里氏替换原则

        class GameObjectFuLei
        {
            public int a = 10;
            public void FuLeiFangFa()
            {

            }
        }

        class ZiLei_1 : GameObjectFuLei
        {
            public void ZiLeiFangFa1()
            {

            }
        }

        class ZiLei_2 : GameObjectFuLei
        {
            public void ZiLeiFangFa2()
            {

            }
        }


        #endregion

        #region 继承中的构造函数

        //当实例化一个子类对象时
        //会自动调用父类的无参构造
        //然后再去调用子类的构造函数

        public class JiChengZhongGouZao_Father
        {
            public JiChengZhongGouZao_Father()
            {

            }
            public JiChengZhongGouZao_Father(int a)
            {

            }
        }

        public class JiChengZhongGouZao_Son : JiChengZhongGouZao_Father
        {
            //可以使用 base 指定 调用父类的构造函数
            //可以使用 this 指定 调用子类自己的构造函数（不是新的知识点，利用 this 调用构造函数）
            public JiChengZhongGouZao_Son(int i) : base(i)//调用父类中的 一个参数重载 的构造函数
            {

            }
        }

        #endregion

        #region 万物之父与装箱拆箱

        //object 是所有类型的基类
        //可以用 object 来存在任何类对象
        object object_text1 = new Text();
        //也可以用 object 装载数据
        object object_a = 10;
        object object_b = "dsada";

        #region 装箱拆箱

        //用 object 存储 值类型
        //——> 原本 值类型 原本存储在 栈中，但使用 object 存储时，会存储到 堆中
        //——> 也就是 值类型 使用 引用类型 存储，把原本应该存储在栈中的存到堆中
        //拆箱
        //——> 相反的操作，引用类型 使用 值类型 存储，把原本应该存储到堆中的存到栈中

        //备注 这里没有写在主函数内，所有必须添加 static 否则是不需要写的
        static int object_num = 10;
        static object obj_ZhuangXiangChaiXiang = object_num;//装箱操作
        static int obj_ChaiXiang = (int)obj_ZhuangXiangChaiXiang;//拆箱操作

        #endregion

        #endregion

        #region 密封类

        //使用 sealed 进行修饰的类
        //这个类不能被继承

        class SealedFather
        {

        }

        sealed class SealedSon1 : SealedFather
        {

        }

        //不能继承 被 sealed 修饰的 类
        //class SealedSon2 : SealedSon1
        //{

        //}

        #endregion

        #endregion

        #region 类和对象 —— 多态

        #region 多态 vob

        //分别为 v virual 虚函数 o override 重写 b base 父类

        class VobClassText
        {
            //写了一个虚方法
            public virtual void Text_1()
            {
                int a = 10;
                int b = 20;
                int c = a + b;
            }
        }

        class VobClassText_2 : VobClassText
        {
            //子类可以使用 override 重写
            public override void Text_1()
            {
                base.Text_1();//保留父类的行为逻辑
                //然后去执行子类独有的逻辑
                Console.WriteLine("多态 vob 子类独有逻辑");
            }
        }

        #endregion

        #region 抽象类和抽象方法

        //封装的所有知识点都可以写在抽象类中
        //但请注意 抽象类中的抽象方法没有函数体
        //且继承抽象类的子类，必须重写所有抽象方法  抽象方法只能在抽象类中声明

        abstract class AbstractClassFather
        {
            //普通的成员
            public int id;
            public int Age
            {
                get
                {
                    return 20;
                }
                set
                {

                }
            }
            public void Add()
            {

            }

            //抽象方法不能写函数体
            public abstract void AbstractAdd();

        }

        class AbstractClassSon : AbstractClassFather
        {
            //继承抽象类的子类必须实现抽象类中的抽象方法
            public override void AbstractAdd()
            {
                throw new NotImplementedException();//这一句代码没用，可以直接删掉
            }
        }


        #endregion

        #region 接口

        //一个类可以继承多个接口，接口可以继承接口
        //继承接口后必须实现接口的所有成员
        //接口内无法声明变量，但可以声明 属性、方法、索引器、事件
        //备注 根据C# 使用的 .NET 决定能不能声明属性

        //备注 一个类 Class 继承了接口 IE ，这个类实例化后 的对象，可以被视为 这个接口 类型 的对象

        interface IClass
        {
            //前面为什么不加 public ——> C# 版本过低不让用 
            void InterfaceClass();
        }

        //接口继承接口
        interface IClass2 : IClass
        {
            void Interface2Class();
        }

        //类继承接口
        class InterfaceClass_Class : IClass2
        {
            //继承了接口，就要去实现接口内的所有方法
            //因为继承的是 IClass2 这个接口还继承了 IClass
            //所有需要实现 两个接口内的所有方法
            public void Interface2Class()
            {
                throw new NotImplementedException();
            }

            public void InterfaceClass()
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region 密封方法

        //真的会用到这个吗？？？
        //特点 ——> sealed 与 override 一起出现
        //作用 ——> 让虚方法或抽象方法之后不能再被重写

        //这里以 虚方法为例
        class MiFengFangFaClass_Father
        {
            public virtual void Speak()
            {

            }
        }

        class MiFengFangFaClass_Son : MiFengFangFaClass_Father
        {
            public sealed override void Speak()
            {
                base.Speak();
            }
        }


        #endregion

        #endregion

        #region 泛型

        #region 基本概念

        //不必想的太过复杂，只需记住以下几点
        //——> 泛型用来代替某一个不确定的类型
        //——> 不同类型 却有着相同的逻辑处理 那么就使用泛型替代

        //如果泛型类需要传入多个泛型，使用 , 隔开
        class FanXing_Class<T>//创建一个 泛型类 即 当创建的时候，GetT 会根据传入的类型而改变
        {
            public int a;
            public T GetT;

            //泛型方法  泛型为 ——> K
            public void Add<K>(T t, K k)//T 为类的泛型 而 K 为调用方法时需要传入的泛型
            {
                //可以不写 T 的，这里仅作shili
                Console.WriteLine(t + " " + k);
            }

            //这个不是泛型方法，因为 T 是 泛型类被声明时就指定了
            public void Remove(T k)
            {

            }
        }

        #endregion

        #region 泛型约束

        //关键字 where 
        //用来限制泛型的

        class FanXingYueShu_Class<T> where T : struct //约束 泛型T 必须为 值类型
        {

        }

        class FanXingYueShu_Class_1<T> where T : class //约束 泛型T 必须为 引用类型
        {

        }

        class FanXingYueShu_Class_2<T> where T : new()//约束 泛型T 必须 有一个无参构造函数
        {

        }

        //泛型约束 约束的就是 传入的泛型 
        //必须要达成某些需求，才能被传进去

        #endregion

        #endregion

        #region 委托和事件

        #region 委托

        #region 委托声明

        //简单理解 ——> 委托用来存储函数 直接视为一个变量类型

        //声明 delegate   访问修饰符 + delegate + 返回值 + 委托名 + 参数列表
        //一般声明都在 namespece 中声明

        public delegate void Delegate_Fun_1(int a, int b);//无返回值两个参数的委托
        public delegate void Delegate_Fun_2();//无参无返回
        public delegate int Delegate_Fun_3(int a);//有参有返回
        public delegate void Delegate_Fun_4(Delegate_Fun_1 a);


        //委托可以做为函数参数（详见 Delegate_Text_4 示例函数）、类中的成员

        class Delegate_Class_Text
        {
            public Delegate_Fun_1 delegate_Fun_11;//Delegate_Fun_1 委托 的 变量
        }

        #endregion

        //委托的作用
        //——> 处理其他逻辑，然后去执行委托里存入的函数

        //多播委托 ——> 一个委托变量存储多个函数

        #region 系统自带委托

        //C# 自带了两个委托 ——> 无返回值的 Action 与 有返回值的 Func
        //如果需要传入参数，那么就需要使用 泛型

        //除此之外，还有泛型委托
        //但一般都使用 Action 与 Func
        public delegate T Delegate_FanXing_Myfun<T, K>(T t, K k);

        #endregion

        #endregion

        #region 事件

        //与委托十分相似，但比委托更加安全 
        //委托在 类的内部/外部 都可以调用，但事件只能在 类的内部 赋值调用，在 类的外部只能加减

        //首先 事件是 基于委托存在的 ，但只能做为类的成员变量
        //存在与类和接口以及结构体中

        //声明方式 public evnet 委托类型(Action / Func) 事件名；
        //可以使用 泛型

        //备注 ——> C# 版本过低的话 写 public 会报错

        interface IEventClass
        {
            event Action event_interface_action_1;
        }

        class EventClass : IEventClass
        {
            public event Action event_interface_action_1;//实现接口
            public event Action<int> event_class_action_1;//类的成员
            public event Func<int> event_class_func_1;
            public event Func<int, int> event_class_func_2;
            private event Func<int, int> event_class_func_3;

            public void Add(Action action)
            {
                event_interface_action_1 += action;
                event_interface_action_1 -= action;
            }
        }

        class EventClass_Son : EventClass
        {
            void Add_2(Action action)
            {
                event_interface_action_1 += action;
            }
        }

        //事件就是 特殊的委托  相当于对委托进行了一次封装，使其更加安全
        //使用方式跟委托一模一样
        //为什么要有事件 ——> 防止外部 随意 置空/调用 委托 ，导致出错


        #endregion

        #region 匿名函数

        //顾名思义 没有名字的函数
        //一般选择使用 Lambad 表达式 而不是 匿名函数

        //作用 ——> 直接往 委托/事件 里面添加函数
        //声明  委托/事件 名字 = delegate(参数列表) { 函数逻辑 (如果有返回值 就加上 return) }; 分号不可省略

        //备注 ——> 因为没有名字，无法单独移除，只能（将委托/事件）全部清空

        class NiMinHanShu_Class
        {
            public Action niMinHanShu_action_1 = delegate ()
            {
                //函数逻辑
            };


            public Func<int> niMinHanShu_func_1 = delegate ()
            {
                //函数逻辑
                return 14;
            };

            public Func<int, int> niMinHanShu_func_2 = delegate (int a)
            {
                //函数逻辑
                return 14;
            };

            public event Action<int> niMinHanShu_event_1 = delegate (int a)
            {
                //函数逻辑
            };

            public event Func<int, int> niMinHanShu_event_2 = delegate (int a)
            {
                //函数逻辑
                return 15;
            };
        }

        //如果写在一个类的外部，需要添加 static 才能调用
        public static Action<int, int> niMinHanShu_action_2 = delegate (int a, int b)
        {
            //函数逻辑
        };



        #endregion

        #region Lambad 表达式

        //简单理解 ——> 匿名函数的简写
        //除了写法不一样，使用上和匿名函数一模一样
        //声明 ()=> / => 

        class Lambad_Class
        {
            private int age;
            public int Age => age;//这也是一个 Lambab 表达式

            public Action Lambad_Class_action_1 = () =>
            {

            };

            public Func<int, int> Lambad_Class_action_2 = (a) =>
             {
                 return 15;
             };

        }

        Action Lambad_action_1 = () => //Lambad 表达式
        {
            //函数逻辑
        };

        //() 内的参数 ，要么全写 数据类型，要么全都不写 数据类型

        Action<int, float> Lambad_action_2 = (int a, float b) =>
        {

        };

        Action<int, float, string> Lambad_action_3 = (a, b, c) =>
        {

        };

        event Action Lanbad_event_1 = () =>
        {

        };

        event Func<int> Lanbad_event_2 = () =>
        {
            return 15;
        };

        #endregion

        #endregion

        #region 协变逆变

        //概念 （用于泛型中）
        //协变 ——> 父类装子类 子类变父类 例如 string 变 object  子类有父类的全部成员 
        //逆变 ——> 子类装父类 父类变子类 例如 object 变 string  子类有父类没有的成员
        //关键字 协变out（修饰后只能做返回值） 逆变in（修饰后只能做参数） ——> 用于再泛型中修饰泛型字母

        //1.返回值 和 参数

        //用out修饰的泛型 只能作为返回值
        delegate T TestOut<out T>();

        //用in修饰的泛型 只能作为参数
        delegate void TestIn<in T>(T t);

        //2.结合里氏替换原则理解
        class Father
        {

        }

        class Son : Father
        {

        }

        #endregion

        static void Main(string[] args)
        {

            #region 一维数组

            //一维数组的四种声明方式
            int[] text1;
            int[] text2 = new int[10];
            int[] text3 = new int[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] text4 = { 1, 2, 3, 4, 5, 6 };
            //.Length 获取数组的长度
            for (int i = 0; i < text4.Length; i++)
            {
                Console.WriteLine(i);
            }
            //通过索引修改数组元素
            text4[2] = 7;

            //增加数组元素
            //将一个数组赋值给另一个数组的方法
            text2 = text3;

            //删除数组元素
            //将一个数组赋值给另一个数组 但数组是空的
            text1 = null;
            text2 = text1;

            //查找
            //只有通过遍历才能确定数组中是否存储了一个目标元素


            #endregion

            #region 二维数组

            //二维数组
            //可以理解为 一个行标 一个列表

            //五种声明方式
            int[,] text_2;
            int[,] text_21 = new int[5, 5];
            //一个大括号——> 一行 括号内有几个元素 ——> 几列
            int[,] text_22 = new int[2, 5] { { 1, 2, 3, 4, 5 }, { 1, 2, 3, 4, 5 } };
            int[,] text_23 = new int[,] { { 1, 2, 3, }, { 1, 2, 3 } };
            int[,] text_24 = { { 1, 2 }, { 2, 3 } };

            //获取行数
            int ErWeiShuZhuHangShu = text_22.GetLength(0);
            //获取列数
            int ErWeiShuZhuLieShu = text_22.GetLength(1);

            //二维数组的遍历
            for (int i = 0; i < ErWeiShuZhuHangShu; i++)
            {
                for (int j = 0; j < ErWeiShuZhuLieShu; j++)
                {
                    Console.WriteLine(text_22[i, j]);
                }
            }

            //修改
            text_22[1, 4] = 3;

            //增加数组长度
            //方法同 一维数组 用一个数组赋值给另一个数组
            text_24 = text_23;

            //删除数组元素
            //方法同 一维数组 用一个数组赋值给另一个数组
            text_2 = null;
            text_22 = text_2;

            //查找数组元素
            //只有通过遍历才能确定数组中是否存储了一个目标元素

            #endregion

            #region 交错数组

            //概念：数组的数组 交错数组里的元素是一维数组
            //每个维度的数量不同（一行代表一个一维数组，各个一维数组内的元素可以不同，导致每行列数不同）

            //五种声明方式
            //将一维数组视为一个元素
            int[][] text_3;
            int[][] text_31 = new int[2][];
            int[][] text_32 = new int[2][] { new int[3] { 1, 2, 3 }, new int[3] { 1, 2, 3 } };
            int[][] text_33 = new int[][] { new int[3] { 1, 2, 3 }, new int[3] { 1, 2, 3 } };
            int[][] text_34 = { new int[3] { 1, 2, 3 }, new int[3] { 1, 2, 3 } };

            text_3 = null;
            //得到行数
            int JiaoCuoShuZhuHangShu = text_32.GetLength(0);
            //得到列数
            //从某一行（也就是某一个一维数组）得到列数
            //一行就是一个一维数组，那么这一行的列数 = 这个一维数组的长度
            int JiaoCuoShuZhulieShu = text_32[1].Length;
            //删除
            text_32 = text_3;

            #endregion

            #region 结构体的使用

            //因为写了有参构造函数，所以实例化对象的时候必须将参数写上去
            MyStudent my = new MyStudent(10, 50.5f, 20);

            //使用 点运算符 进行调用成员变量、成员函数
            //my.old = 70; private 不可访问
            my.high = 70f;
            my.age = 102;
            my.Add();

            #endregion

            #region 函数

            //有参无返回的函数
            void AddNumber(int a, int b)
            {
                Console.WriteLine(a + b);
            }

            //有参有返回
            int AddNumber2(int a, int b)
            {
                return a + b;
            }
            //调用无返回值函数
            AddNumber(5, 6);

            //调用有返回值函数
            int c = AddNumber2(9, 8);

            #region ref 与 out

            //ref 关键字 ——> 使用前必须将参数初始化
            //——> 在函数内部修改参数的值，会影响实参
            //——> 通过引用传递参数，传递的是实参的地址，在函数内部修改形参，等效于修改实参

            int ref_aShiCan = 100;

            void RefUpdataNumber(ref int ref_a)
            {
                ref_a += 20;
            }

            //传入参数的时候，也要带上关键字 ref
            RefUpdataNumber(ref ref_aShiCan);

            Console.WriteLine(ref_aShiCan);

            //out 关键字 ——> 试用期不需要初始化
            //——> 但在函数返回之前，必须在函数内部进行赋值
            //——> 通过引用返回参数，返回的是形参的地址，在函数内部修改形参，然后将地址返回给实参
            //——> 这样就实现了，修改实参 的作用

            int out_aShiCan;

            void OutUpdataNumber(out int out_xingcana)
            {
                out_xingcana = 50;//必须赋值
                out_xingcana += 10;
            }

            //传入参数的时候，也要带上关键字 out
            OutUpdataNumber(out out_aShiCan);

            // 甚至可以这样用：直接不传参数，坐等函数返回一个值

            //void OutUpdataNumber(out int out_xingcana)
            //{
            //    out_xingcana = 50;//必须赋值
            //    out_xingcana += 10;
            //}

            ////传入参数的时候，也要带上关键字 out
            //OutUpdataNumber(out _);

            #endregion

            #region 变长参数和参数默认值

            //参数默认值
            //有默认值的参数，一般称为可选参数，在调用函数的时候可以不传入参数
            //不传就会默认使用 默认值 做为这个参数的值

            void CanShuMoRenZhi(int a, int b = 10)
            {
                a += b;
            }
            //只传入了 形参a ，而不传入形参b，此时 形参b的值默认为10
            CanShuMoRenZhi(10);


            //变长参数
            //必须是最后一个参数
            //可以直接传入一个数组，也可以传入多个参数，会自动整合为一个数组
            //变长参数在函数内部实际上为一个数组

            void BianChangCanShu(int a, int b, params int[] arr)
            {
                int BianChangCanShu_temp = a + b;
                foreach (var i in arr)
                {
                    BianChangCanShu_temp += i;
                }
            }
            //10 20 为形参 a b ，从第三个到最后都是变长参数，为一个 int 类型数组
            BianChangCanShu(10, 20, 5, 6, 5, 7, 8, 5, 6, 5, 6, 1);

            #endregion

            #region 函数重载与 ref out

            //函数重载 不同参数数量、不同参数类型、不同返回值类型
            //但函数名相同
            //在主函数中不能写函数重载，只能在类或者结构体中写

            //同一个函数 的重载，不能既有 ref 又有 out


            void ExampleMethod(ref int value)
            {
                value += 10;
            }

            int ExampleMethod_refexample = 10;

            ExampleMethod(ref ExampleMethod_refexample);


            // void ExampleMethod(out int value)  // 编译错误，与 ref 重载冲突
            //{
            //    value = 10;
            //}

            #endregion

            #region 函数递归

            int HanShuDiGui_temp = 0;

            void HanShuDiGui()
            {
                HanShuDiGui_temp += 1;
                if (HanShuDiGui_temp < 10)
                {
                    HanShuDiGui();
                }
            }

            #endregion

            #endregion

            #region 类和对象 —— 封装

            #region 实例化对象 与 构造函数

            //实例化对象 调用的是无参构造函数
            Text Text_text = new Text();
            //调用 两次参数的有参构造函数
            Text Text_text_1 = new Text(1, 2);

            #endregion

            #region 成员变量 方法 的使用

            //使用 点运输符 点出来
            //根据访问修饰符 public 为可访问 private 子类和外部都不能访问，仅内部可以 protected 子类可以外部不行
            Text_text_1.Add();
            Text_text.id = 20;
            Text_text.Remove(20);

            #endregion

            #region 静态成员 方法 

            //通过类名 直接点出 静态方法
            Text.AddStatic();
            Text.Static_a = 20;

            #endregion

            #region 静态类的使用

            //直接用类名点出来就行
            StaticClass.staticClass_id = 20;
            StaticClass.StaticClassAdd();

            #endregion

            #region 拓展方法

            //因为我们的拓展方法中的 this 指向的拓展 类型为 int
            //所以，创建 Int 类型的变量后，可以使用点运算符点出创建的拓展方法
            //其中，this 所指代的参数为 这个变量本身，后面才是要传入的参数
            int tuozhanfangfa = 10;
            tuozhanfangfa.TuoZhanFangFa(20);

            #endregion

            #region 内部类与分部类

            //内部类的使用 ——> 在类名处，使用点运算符点出内部类，然后实例化对象
            Text.TextNeiBuLei textNeiBuLei = new Text.TextNeiBuLei();

            //分部类 略
            //谁没事用这玩意？？？

            #endregion

            #endregion

            #region 类和对象 —— 继承

            #region 继承的基本规则

            //子类可以去（根据访问修饰符）调用父类的所有成员

            JiChengText_1 jiChengText_1 = new JiChengText_1();
            JiChengText_2 jiChengText_2 = new JiChengText_2();

            int temp_JiCheng_1 = jiChengText_2.JiChengText_1_temp;
            temp_JiCheng_1 = jiChengText_2.JiChengText_2_temp;
            jiChengText_2.JiChengText_2_FangFa();
            jiChengText_2.JiChengText_1_FangFa();

            #endregion

            #region 里氏替换原则

            //首先，父类容器 可以 装载 子类对象
            //此时，只能调用 父类和子类 都有的成员（或者说 只能调用父类成员）
            //声明的 liShiTiHuan_1 为 GameObjectFuLei 类型，创建的是 ZiLei_1 对象
            //——> 实例化了一个类型为 GameObjectFuLei 的 ZiLei_1 对象
            GameObjectFuLei liShiTiHuan_1 = new ZiLei_1();
            GameObjectFuLei liShiTiHuan_2 = new ZiLei_2();

            int liShiTiHuan_temp = liShiTiHuan_1.a;
            liShiTiHuan_1.FuLeiFangFa();

            //用数组来存储
            //因为 liShiTiHuan_1 liShiTiHuan_2 的类型都是 GameObjectFuLei 所有可以放在 数组内
            GameObjectFuLei[] liShiTiHuan_ShuZu = new GameObjectFuLei[] { liShiTiHuan_1, liShiTiHuan_2 };


            #region is 和 as

            //is ——> 判断一个对象是否为指定类对象 是 返回 true   不是返回 false
            //as ——> 将一个对象转化为指定类对象  成功则返回指定类对象  失败则返回 null

            if (liShiTiHuan_1 is ZiLei_2)
            {
                //因为 liShiTiHuan_1 是 ZiLei_1 的实例，所有会走 else 内的语句
            }
            else
            {
                //liShiTiHuan_3 最终为 null
                //因为 liShiTiHuan_2 为 ZiLei_2 的实例 不是 ZiLei_1 的 无法转换
                GameObjectFuLei liShiTIHuan_3 = liShiTiHuan_2 as ZiLei_1;
                ZiLei_2 liShiTIHuan_4 = liShiTiHuan_2 as ZiLei_2;
                liShiTIHuan_4.ZiLeiFangFa2();//最终 liShiTIHuan_4 的值 为 ZiLei_1 类型的 实例对象

                //也可以结合起来一起写
                (liShiTiHuan_1 as ZiLei_1).ZiLeiFangFa1();
            }

            #endregion


            #endregion

            #endregion

            #region 简单数据结构类

            //简单数据结构类 都需要引用命名空间

            #region ArrayList

            //本质上是一个可以自动扩容的object数组
            //可以往里面添加任何类型的数据

            //声明
            ArrayList arrayList = new ArrayList();
            ArrayList arrayList2 = new ArrayList();

            #region 增删查改

            //增
            arrayList.Add(15);
            arrayList.Add("dsawd");
            arrayList.Add(0.245);
            arrayList.Add(true);
            arrayList.Add(new Text());//一个类的实例
            arrayList2.Add(123456);
            arrayList.AddRange(arrayList2);//直接增加一个 ArrayList，将其所有元素添加到末尾
            arrayList.Insert(2, 25);//插入 参数一为索引位置，参数二 为 插入的值

            //删
            arrayList.Remove(15);//从头开始找，移除指定元素
            arrayList.RemoveAt(2);//移除指定索引的元素
            //arrayList.Clear();//清空

            //查

            var arrayList_temp = arrayList[3];//取出指定索引的元素
            if (arrayList.Contains(123456))
            {
                //查找 arrayList 内是否有某个元素
                //如果有就返回 true 没用则返回 false
            }

            arrayList_temp = arrayList.IndexOf(25);//正向查找某个元素，找到返回索引值，没有则返回 -1
            arrayList_temp = arrayList.LastIndexOf(25);//反向查找，找到返回索引值，没有则返回 -1

            //改

            arrayList[3] = 15;//通过索引值来改

            //遍历相关

            arrayList_temp = arrayList.Count;//得到数组的长度
            arrayList_temp = arrayList.Capacity;//得到容量（一般比长度要大）
            //arrayList.Sort();//自带的升序排序方法

            #endregion

            #endregion

            #region Stack 栈

            //先进后出 （单口管道）
            //与 ArrayList 一样，是一个 object 数组

            //声明

            Stack stack = new Stack();

            #region 增取查改

            //增 （又称之为 压/存栈）

            stack.Push(123);
            stack.Push(0.12);
            stack.Push("dsawdsa");
            stack.Push(new Text());


            //取 （又称之为 弹栈  栈不存在删的概念）

            var stack_temp = stack.Pop();

            //查 （栈无法查看指定位置元素的信息，只能查看栈顶的内容）

            stack_temp = stack.Peek();//注意，只是看一看，并没有从中取出来

            if (stack.Contains(3))
            {
                //如果栈中存在这个元素，返回 true 否则返回 false
            }

            //改（栈无法改变其中的元素，只能 压栈 与 弹栈 还有 清空）

            //stack.Clear(); 清空

            //遍历相关

            stack_temp = stack.Count;//栈的长度

            foreach (var item in stack)
            {
                //遍历的顺序 ——> 从 栈顶 到 栈底
            }

            object[] stack_array = stack.ToArray();//也可以选择将 stack 转换为 数组，然后用 for 循环

            //循环弹栈
            //使用循环结构来连续地从栈（Stack）中移除（取出）元素，直到满足某个特定的条件
            //一般使用 while

            while (stack.Count > 1)
            {
                //每循环一次，弹栈一次，直到栈内元素的数量小于等于1
                var stack_XunHuanTanZhan_temp = stack.Pop();
                Console.WriteLine(stack_XunHuanTanZhan_temp);
            }

            #endregion

            #endregion

            #region Queue 队列

            //先进先出（两头管道 一头进一头出）
            //与前两个相同 都是 object 类型数组

            //声明

            Queue queue = new Queue();

            #region 增取查改

            //增
            queue.Enqueue(10);
            queue.Enqueue(0.5);
            queue.Enqueue("dsadwa");
            queue.Enqueue(new Text());
            queue.Enqueue(new ArrayList());

            //取 （队列没有删的概念 只有取  取出先添加进去的元素）

            var queue_temp = queue.Dequeue();

            //查

            queue_temp = queue.Peek();//查看队列头部元素，但并不取出
            if (queue.Contains(16))
            {
                //如果队列中存在 16 这个元素，返回 true 否则返回 false
            }

            //改
            //队列无法改变其中的变量，只能取出
            //queue.Clear(); //清空

            //遍历相关

            queue_temp = queue.Count;

            foreach (var item in queue)
            {
                object[] queue_object = queue.ToArray();//转换为 object 数组
            }

            //循环出列

            while (queue.Count > 1)
            {
                var temp = queue.Dequeue();//不断将元素一个一个出列
            }

            #endregion

            #endregion

            #region Hashtable 哈希表

            //又叫 散列表 是基于哈希代码组织起来的 键值对
            //通过键来访问元素（通过 键 映射 值）

            //声明

            Hashtable hashtable = new Hashtable();

            #region 增删查改

            //增 一个键对应一个值  键与值的类型皆为 object  参数一 键 参数二 值

            hashtable.Add(1, 1);
            hashtable.Add("dsadw", new Text());
            hashtable.Add(new Text(), "dsawdsa");
            hashtable.Add(0.52, new Queue());

            //删  通过键来删除，如果删除存在的键没有反应

            hashtable.Remove(1);
            hashtable.Remove(152);//删除不存在的键 没有反应 不会报错 

            //查  通过键来查找 与 通过值类查找 两种方式

            if (hashtable.Contains(15))
            {
                if (hashtable.ContainsKey("dsaw"))
                {
                    //这两种方式效果相同 都是通过键来查找的
                    //如果 哈希表里有这个键，那么返回 true 没有 则是 false
                }

                if (hashtable.ContainsValue("dsaw"))
                {
                    //通过值类查找
                    //如果哈希表里有这个值，那么返回 true 否则返回 false
                }
            }

            //改 

            hashtable[1] = 10;//哈希表[键] = 值 通过键来修改值

            //遍历相关

            foreach (var item in hashtable.Keys)
            {
                //遍历哈希表中的键
                //item 为 hashtable 中的键
                Console.WriteLine(hashtable[item]);//输出的为 这个键对应的值
            }

            foreach (var item in hashtable.Values)
            {
                //遍历哈希表中的值
                //item 为 hashtable 中的值
                Console.WriteLine(item);//输出遍历的值
            }

            foreach (DictionaryEntry item in hashtable)
            {
                //键值一起遍历 （固定写法）
                //item 为 键值对
                Console.WriteLine("键为" + item.Key + "值为" + item.Value);
            }

            #endregion

            #endregion

            #endregion

            #region 泛型

            #region 基本概念的使用

            //实例化泛型类时 需要传入泛型
            FanXing_Class<int> fanXing_Class = new FanXing_Class<int>();
            //传入的泛型为 int 所以 GetT 的类型为 int
            fanXing_Class.GetT = 20;
            //因此 泛型方法也就需要传入两个参数 一为 int 泛型类的 一为 string 泛型方法的
            fanXing_Class.Add<string>(15, "dsadw");

            #endregion

            #endregion

            #region 常用泛型数据结构类

            //需要引用命名空间

            #region List

            //声明
            List<int> list_int = new List<int>();//创建一个 传入泛型为 int 的列表
            List<string> list_string = new List<string>();//创建一个 传入泛型为 string 的列表
            List<Text> list_Class_Text = new List<Text>();//创建一个 传入泛型为 Text(一个类) 的列表

            #region 增删查改

            //与 ArraryList 相同，但类型不再为 Object ，类型为 传入的泛型类型
            //这里以 泛型为 int 的 为例子

            //增

            list_int.Add(1);//因为传入的泛型为 int 所以只能往里面添加 int 类型的数据
            list_string.Add("dsawd");//同理 只能添加 string 类型数据
            list_Class_Text.Add(new Text());//同理 只能添加 Text（类）的实例化对象 

            List<int> list_int_2 = new List<int>();
            list_int.AddRange(list_int_2);//只能添加 相同泛型的 列表

            list_int.Insert(2, 15);//插入 参数一为索引 参数二为插入的值

            //删
            list_int.Remove(1);
            list_int.RemoveAt(2);
            //list_int.Clear(); //清空

            //查 

            var list_int_temp = list_int[2];//取出指定索引的元素
            if (list_int.Contains(12))
            {
                //查找 列表中 有没有指定元素
            }

            list_int_temp = list_int.IndexOf(15);//正向查找 找到返回索引值 没找到返回 -1
            list_int_temp = list_int.LastIndexOf(15);//方向查找

            //改

            list_int[3] = 16;

            //遍历相关
            int list_int_Count = list_int.Count;//长度
            int list_int_Capacity = list_int.Capacity;//容量

            foreach (var item in list_int)
            {
                //var 为 泛型类型 
                //这里懒得写就直接 var 了
            }

            #endregion

            #region List 与 Arraylist 区别

            //List内部封装的是一个 泛型 数组 ——> 重点是 泛型
            //ArrayList内部封装的是一个 object 数组 ——> 重点是 object  

            #endregion

            #endregion

            #region Dictionary

            //可以理解为 泛型的 哈希表
            //基于 哈希表的 键值对 来实现功能的，只是多了泛型
            //键值对类型从 object 变为了 自己指定的泛型

            //声明

            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            Dictionary<int, Text> keyValuePairs_1 = new Dictionary<int, Text>();//键为 int 值为 Text 类对象
            Dictionary<string, ArrayList> keyValuePairs_2 = new Dictionary<string, ArrayList>();//string ArrayList
            Dictionary<float, IClass> keyValuePairs_3 = new Dictionary<float, IClass>();//float IClass 继承接口

            #region 增删查改

            //增

            keyValuePairs.Add(1, "sdawd");
            keyValuePairs.Add(2, "ddkdkdkd");
            keyValuePairs.Add(16, "dldldldl");
            //keyValuePairs.Add(1, "dawdsa");//键不可以重复，虽然不会报错 但有安全隐患
            //keyValuePairs.Add(1, 1);//如果填入的键值对不符合泛型要求，将会报错

            //删

            keyValuePairs.Remove(2);//通过键来删除

            //查

            var keyValuePairs_temp = keyValuePairs[16];//通过键来查找，找到返回相应的值，找不到则返回空

            //改

            keyValuePairs[1] = "dsawd";//通过键来访问，然后赋予新的值（要符合泛型）

            //遍历相关

            var keyValuePairs_temp2 = keyValuePairs.Count;//获得键值对的数量

            foreach (var item in keyValuePairs.Keys)
            {
                //遍历字典中的键
                //此时 item 为字典中的键对象
                Console.WriteLine(keyValuePairs[item]);
            }

            foreach (var item in keyValuePairs.Values)
            {
                //遍历字典中的值
                //此时 item 为字典中的值对象
                Console.WriteLine(item);
            }

            foreach (KeyValuePair<int, string> item in keyValuePairs)
            {
                //固定写法 传入键值对的泛型 键值对一起遍历
                //此时 item 为 键值对
                Console.WriteLine(item.Key + item.Value);
            }

            //获取 指定键 关联的 值

            if (keyValuePairs.TryGetValue(1, out var e))
            {
                //如果存在 指定的键 那么会返回true 并将 相应的 值对象 一同返回
                //如果不存在，就会返回 true 而不是抛出异常
                Console.WriteLine(e);
            }

            #endregion

            #endregion

            #region LinkedList

            //可变类型的泛型双向链表

            //声明

            LinkedList<int> linkedlist = new LinkedList<int>();
            LinkedList<string> linkedlist_string = new LinkedList<string>();
            LinkedList<Text> linkedlist_Text = new LinkedList<Text>();

            #region 增删查改

            //增

            linkedlist.AddFirst(15);//在链表头添加元素
            linkedlist.AddLast(3);//在链表尾添加元素

            LinkedListNode<int> linkedListNode_Zeng = linkedlist.Find(3);//先寻找一个节点  传入参数为一个值 需要传入泛型
            linkedlist.AddAfter(linkedListNode_Zeng, 33);//在这个节点后面，添加一个元素

            //删

            linkedlist.RemoveFirst();//删除链表头元素
            linkedlist.RemoveLast();//删除链表尾元素
            linkedlist.Remove(15);//删除指定节点
            //linkedlist.Clear();//清空链表

            //查

            LinkedListNode<int> linkedListNode_Cha_1 = linkedlist.First;//得到链表头元素
            Console.WriteLine(linkedListNode_Cha_1.Value + " " + linkedListNode_Cha_1.Next + " "
                + linkedListNode_Cha_1.Previous);//依次获得 当前节点 下个节点 上个节点 的值

            LinkedListNode<int> linkedListNode_Cha_2 = linkedlist.Last;//得到链表尾元素
            Console.WriteLine(linkedListNode_Cha_2.Value + " " + linkedListNode_Cha_2.Next + " "
                + linkedListNode_Cha_2.Previous);//依次获得 当前节点 下个节点 上个节点 的值

            LinkedListNode<int> linkedListNode_Cha_3 = linkedlist.Find(15);//找到指定值的节点
            Console.WriteLine(linkedListNode_Cha_3.Value + " " + linkedListNode_Cha_3.Next + " "
                + linkedListNode_Cha_3.Previous);//依次获得 当前节点 下个节点 上个节点 的值

            linkedlist.Contains(12);//判断链表中是否有这个元素

            //改

            linkedlist.First.Value = 15;//必须先获得一个节点，然后再去修改值
            LinkedListNode<int> linkedListNode_Gai = linkedlist.Find(5);
            linkedListNode_Gai.Value = 16;

            //遍历相关

            foreach (var item in linkedlist)
            {
                //item 为 当前遍历到的节点的值
                int value = item;
            }

            //从头遍历到尾
            LinkedListNode<int> linkedListNode_BianLi_First = linkedlist.First;//获取链表头元素
            while (linkedListNode_BianLi_First != null)//当访问到链表尾 或 一个不存在的节点时 退出循环
            {
                linkedListNode_BianLi_First = linkedListNode_BianLi_First.Next;//获取下一个元素
            }

            //从尾遍历到头

            LinkedListNode<int> linkedListNode_BianLi_Last = linkedlist.Last;
            while (linkedListNode_BianLi_Last != null)//当访问到链表头 或 一个不存在的节点时 退出循环
            {
                linkedListNode_BianLi_Last = linkedListNode_BianLi_Last.Previous;
            }

            #endregion

            #endregion

            #region 泛型栈(Stack<T>)和泛型队列(Queue<T>)

            //使用上跟 Stack 与 Queue 一模一样
            //就是多了个 泛型

            Stack<int> stack_int = new Stack<int>();
            Queue<int> queue_int = new Queue<int>();

            #region 增删查改

            //增

            stack_int.Push(15);//压栈
            queue_int.Enqueue(16);//入列

            //取
            var stack_temp_int = stack_int.Pop();//弹栈
            var queue_temp_int = queue_int.Dequeue();//出列

            //查
            stack_temp_int = stack_int.Peek();//查看栈顶元素，但不删除
            queue_temp_int = queue_int.Peek();//查看队列头部元素，但并不取出

            if (queue_int.Contains(16))
            {
                //如果队列中存在 16 这个元素，返回 true 否则返回 false
            }

            if (stack_int.Contains(16))
            {
                //如果栈中存在 16 这个元素，返回 true 否则返回 false
            }

            //改 两者都没有改的概念 只有删的概念

            //stack_int.Clear();//清空
            //queue_int.Clear();//清空


            #endregion

            #endregion

            #endregion

            #region 委托和事件

            #region 委托

            #region 示例函数

            void Delegate_Text_1(int a, int b)
            {

            }

            void Delegate_Text_2()
            {

            }

            void Delegate_Text_21()
            {

            }

            void Delegate_Text_22()
            {

            }

            int Delegate_Text_3(int a)
            {
                return a + 10;
            }

            void Delegate_Text_4(Delegate_Fun_1 a)
            {

            }

            void Delegate_Text_5<T, K>(T t1, K k)
            {

            }

            int Delegate_Text_6<T, K>(T t1, K k)
            {
                return 15;
            }

            int Delegate_Text_7()
            {
                return 15;
            }

            #endregion

            #region 委托的使用

            //实例化委托变量时，需要传入一个函数
            //传入的参数 必须跟 委托定义时的 返回值、参数 都要相同
            Delegate_Fun_1 delegate_Fun_1 = new Delegate_Fun_1(Delegate_Text_1);
            Delegate_Fun_2 delegate_Fun_2 = new Delegate_Fun_2(Delegate_Text_2);
            Delegate_Fun_3 delegate_Fun_3 = Delegate_Text_3;//或者直接这样声明（推荐）
            Delegate_Fun_4 delegate_Fun_4 = Delegate_Text_4;

            delegate_Fun_1.Invoke(1, 10);//调用时也需要传入相应的参数
            delegate_Fun_2();//可以不写 Invoke （不推荐）
            int delegate_Text_temp = delegate_Fun_3.Invoke(10);

            Delegate_Class_Text delegate_Class_Text = new Delegate_Class_Text();
            delegate_Class_Text.delegate_Fun_11 = Delegate_Text_1;//类中委托的声明

            #region 增减 清空

            //使用 复合运算符 += -= 完成相关逻辑

            delegate_Fun_2 += Delegate_Text_21;//往委托里面添加函数
            delegate_Fun_2 += Delegate_Text_22;

            delegate_Fun_2 -= Delegate_Text_21;//往委托里面减去函数

            delegate_Fun_2 = null;//清空委托

            #endregion

            #endregion

            #region 系统自带委托

            Action WeiTuo_action_1 = Delegate_Text_2;//Action 可以添加无返回值的
            Action<int, int> WeiTuo_action_2 = Delegate_Text_5;//<int,int>;可以在事件后面也写上泛型

            Func<int> WeiTuo_func_1 = Delegate_Text_7;//Fun 的泛型为 返回值类型（添加的函数返回值必须相同）
            Func<int, int> WeiTuo_func_2 = Delegate_Text_3;//最后一个为返回值，第一个为参数
            Func<int, float, int> WeiTuo_func_3 = Delegate_Text_6;

            #endregion

            //记住 ——> 委托用来存储函数 ——> "函数"类型变量，函数的参数要与委托相同

            #endregion

            #region 事件

            //event Action ShiJian_action; //在类外声明会报错

            //使用方式 ——> 按照 类和对象中的成员变量一样 去使用点运算符 进行调用

            EventClass eventClass = new EventClass();
            eventClass.event_interface_action_1 += Delegate_Text_2;
            eventClass.Add(Delegate_Text_2);

            #endregion

            #region 匿名函数

            //使用
            //就像普通的委托/事件那样执行就行 a(参数列表) 或者 a.Invoke(参数列表)

            NiMinHanShu_Class niMinHanShu_Class = new NiMinHanShu_Class();

            niMinHanShu_Class.niMinHanShu_action_1();

            niMinHanShu_action_2(15, 20);//如果不写 static 会报错

            #endregion

            #region Lambad 表达式

            Lambad_Class lambad_Class = new Lambad_Class();
            lambad_Class.Lambad_Class_action_1.Invoke();
            int lambad_Class_temp = lambad_Class.Lambad_Class_action_2(15);

            #endregion

            #endregion

            #region 协变逆变

            //1.协变 out ——> 父类委托 装载 子类委托

            TestOut<Son> outSon = () => //返回的是 Son 类型对象 ——> outSon 为 Son 类型
            {
                return new Son();
            };

            //父类装子类，但实际上返回的是一个 类型为 Father 的 Son 对象（里氏替换原则）
            TestOut<Father> outFather = outSon;//返回的是 Father 类型对象 ——> outFather 为 Father 类型 
            Father father = outFather();

            /* out 关键字会自动判断父子关系
             * outSon 是一个 Son 类型的Son对象
             * outFather 是一个 Father 类型的，但是 指向 outSon 中的 Son 对象
             * father 是一个 Father 类型的变量，所以可以接受 outFather，但实际上，接受的仍是 Son 对象
             * 
             * 依旧是 父类装子类，但同时这里为 父类委托outFather 装载 子类委托outSon ——> 协变
             */

            //2.逆变 int ——> 子类委托 装载 父类委托

            TestIn<Father> inFather = (value) =>//value 为一个 Father 类型参数
            {

            };

            TestIn<Son> inSon = inFather;
            inSon(new Son());

            /* in ——> 传入一个 泛型T 类型的变量进去
             * TestIn<Son> 指定了要传入一个 Son 类型的参数，所以调用 inSon 需要 new 一个 Son，而不是 new 一个 Father
             * 但实际上 inSon 接受了 inFather，而 inFather 是需要传入 Father 类型的
             * 因此 最后调用时传入的参数，依旧是 父类装子类 （Father参数 ——> new Son()）
             * 
             * 但同时，这里为 子类委托 装载 父类委托 ——> 逆变
             */

            #endregion

            #region 多线程

            //多线程 ——> VS 从头读到尾 就是一个线程
            //多个线程 顾名思义 就是在一个程序内同时执行多个任务

            #region 基本语法

            //线程类 Thread
            //需要引用命名空间 using System.Threading;

            //1.申明一个新的线程
            //  注意 线程执行的代码 需要封装到一个函数中
            //  新线程 将要执行的代码逻辑 被封装到了一个函数语句块中

            Thread thread = new Thread(Thread_Text_1);

            void Thread_Text_1()
            {
                //新开线程要执行的代码逻辑
            }

            //2.启动线程
            thread.Start();

            //3.设置为后台线程
            //默认开启的线程为前台线程，前台线程不受主线程结束影响
            //所以需要将其设置为后台线程
            thread.IsBackground = true;//主线程结束时，后台线程也会结束

            //4.关闭释放一个线程
            //如果开启的线程中不是死循环 是能够结束的逻辑 那么 不用刻意的去关闭它
            //如果是死循环 想要中止这个线程 有两种方式

            //4.1 ——> 给死循环添加一个 bool 标识

            //isRuning = false;//设置的全局静态变量

            //4.2 ——> 通过线程提供的方法(注意在.Net core版本中无法中止 会报错)
            //中止线程
            //try
            //{
            //    thread.Abort();
            //    thread = null;
            //}
            //catch
            //{

            //}

            //5.线程休眠
            //让线程休眠多少毫秒  1s = 1000毫秒
            //在哪个线程里执行 就休眠哪个线程
            //Thread.Sleep(1000);


            #endregion

            #region 线程之间共享数据

            //多个线程使用的内存是共享的，都属于该应用程序(进程)
            //所以要注意 当多线程 同时操作同一片内存区域时可能会出问题
            //可以通过加锁的形式避免问题

            //lock
            //当我们在多个线程当中想要访问同样的东西 进行逻辑处理时
            //为了避免不必要的逻辑顺序执行的差错，可以在某线程中使用 lock 锁住
            //lock(引用类型对象)

            object obj = new object();//这个应该声明为全局静态变量
            while (true)
            {
                lock (obj) 
                {
                    //当有一个线程（A）执行到这里，其他线程就无法进入了（锁住了）
                    //直到线程（A）执行完 锁 内部的所有逻辑后
                    //其他线程才能开始执行（一个一个执行）
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("●");
                }
            }

            #endregion

            #endregion

            #region 2024/9/22 

            //C#进阶 从反射开始 到 值与引用类型补充
            //都没复习

            #endregion

        }
    }
}
