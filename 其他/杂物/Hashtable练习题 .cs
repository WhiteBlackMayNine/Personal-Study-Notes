using System;
using System.Collections;

namespace Lesson4_练习题
{
    #region 练习题一
    //请描述Hashtable的存储规则

    // 一个键值对形式存储的 容器
    // 一个键 对应一个值
    // 类型是object
    #endregion

    #region 练习题二
    //制作一个怪物管理器，提供创建怪物
    //移除怪物的方法。每个怪物都有自己的唯一ID
    
    /// <summary>
    /// 怪物管理器 因为一般 管理器 都是唯一的 所以把它做成 一个单例模式的对象
    /// </summary>
    class MonsterMgr
    {
        private static MonsterMgr instance = new MonsterMgr();

        private Hashtable monstersTable = new Hashtable();

        private MonsterMgr()
        {

        }

        public static MonsterMgr Instance
        {
            get
            {
                return instance;
            }
        }

        private int monsterID = 0;

        public void AddMonster()
        {
            Monster monster = new Monster(monsterID);
            Console.WriteLine("创建了id为{0}的怪物", monsterID);
            ++monsterID;
            //Hashtable 的键 是不能重复的 所以一定是用唯一ID
            monstersTable.Add(monster.id, monster);
        }

        public void RemoveMonster(int monsterID)
        {
            if( monstersTable.ContainsKey(monsterID) )
            {
                (monstersTable[monsterID] as Monster).Dead();
                monstersTable.Remove(monsterID);
            }
        }
    }

    class Monster
    {
        public int id;

        public Monster(int id)
        {
            this.id = id;
        }

        public void Dead()
        {
            Console.WriteLine("怪物{0}死亡", id);
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hashtable练习题");

            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();
            MonsterMgr.Instance.AddMonster();

            MonsterMgr.Instance.RemoveMonster(0);
            MonsterMgr.Instance.RemoveMonster(3);
        }
    }
}
