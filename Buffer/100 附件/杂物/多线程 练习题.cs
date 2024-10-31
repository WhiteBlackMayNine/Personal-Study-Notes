using System;
using System.Threading;

namespace Lesson18_练习题
{
    
    enum E_MoveDir
    {
        Up,
        Down,
        Right,
        Left,
    }
    class Icon
    {
        //当前移动的方向
        public E_MoveDir dir;
        //当前位置
        public int x;
        public int y;
        public Icon(int x, int y, E_MoveDir dir)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }

        //移动
        public void Move()
        {
            switch (dir)
            {
                case E_MoveDir.Up:
                    y -= 1;
                    break;
                case E_MoveDir.Down:
                    y += 1;
                    break;
                case E_MoveDir.Right:
                    x += 2;
                    break;
                case E_MoveDir.Left:
                    x -= 2;
                    break;
            }
        }

        //绘制
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write("■");
        }
        //擦除
        public void Clear()
        {
            Console.SetCursorPosition(x, y);
            Console.Write("  ");
        }
        //转向
        public void ChangeDir(E_MoveDir dir)
        {
            this.dir = dir;
        }
    }

    class Program
    {
        static Icon icon;

        static void Main(string[] args)
        {
            Console.WriteLine("多线程练习题");
            #region 练习题
            //控制台中有一个■
            //它会如贪食蛇一样自动移动
            //请开启一个多线程来检测输入，控制它的转向
            Console.CursorVisible = false;
            icon = new Icon(10, 5, E_MoveDir.Right);
            icon.Draw();

            //开启多线程
            Thread t = new Thread(NewThreadLogic);
            t.IsBackground = true;
            t.Start();
           
            while (true)
            {
                Thread.Sleep(500);
                icon.Clear();
                icon.Move();
                icon.Draw();
            }
            

            #endregion
        }

        static void NewThreadLogic()
        {
            while (true)
            {
                switch(Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        icon.ChangeDir(E_MoveDir.Up);
                        break;
                    case ConsoleKey.A:
                        icon.ChangeDir(E_MoveDir.Left);
                        break;
                    case ConsoleKey.S:
                        icon.ChangeDir(E_MoveDir.Down);
                        break;
                    case ConsoleKey.D:
                        icon.ChangeDir(E_MoveDir.Right);
                        break;
                }
            }
        }
    }
}
