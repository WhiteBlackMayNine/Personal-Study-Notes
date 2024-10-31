using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public int age;
    public string name;
    public float height;
    public bool sex;

    public List<int> list;

    public Dictionary<int, string> dic;

    public ItemInfo itemInfo;

    public List<ItemInfo> itemList;

    public Dictionary<int, ItemInfo> itemDic;
}

public class ItemInfo
{
    public int id;
    public int num;

    public ItemInfo()
    {

    }

    public ItemInfo(int id, int num)
    {
        this.id = id;
        this.num = num;
    }
}


public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //读取数据
        PlayerInfo p = PlayerPrefsDataMgr.Instance.LoadData(typeof(PlayerInfo), "Player1") as PlayerInfo;

        //游戏逻辑中 会去 修改这个玩家数据
        p.age = 18;
        p.name = "唐老狮";
        p.height = 1000;
        p.sex = true;

        p.itemList.Add(new ItemInfo(1, 99));
        p.itemList.Add(new ItemInfo(2, 199));

        //存了一次数据 再执行这的代码 里面已经有3的数据了 字典key不能重复 所以报错
        p.itemDic.Add(3, new ItemInfo(3, 1));
        p.itemDic.Add(4, new ItemInfo(4, 2));

        //游戏数据存储
        PlayerPrefsDataMgr.Instance.SaveData(p, "Player1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
