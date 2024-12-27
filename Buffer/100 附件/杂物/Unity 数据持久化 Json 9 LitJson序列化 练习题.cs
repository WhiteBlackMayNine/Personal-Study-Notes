using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Item2
{
    public int id;
    public int num;

    //自定义类 要保证存在无参构造 这样在反序列化时 才不会报错
    public Item2() { }

    public Item2(int id, int num)
    {
        this.id = id;
        this.num = num;
    }
}

public class PlayerInfo2
{
    public string name;
    public int atk;
    public int def;
    public float moveSpeed;
    public double roundSpeed;
    public Item weapon;
    public List<int> listInt;
    public List<Item2> itemList;

    public Dictionary<string, Item2> itemDic2;
}

public class Lesson2_Exercises : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo2 player = new PlayerInfo2();
        player.name = "唐老狮";
        player.atk = 11;
        player.def = 5;
        player.moveSpeed = 20.5f;
        player.roundSpeed = 21.4f;

        player.weapon = null;

        player.listInt = new List<int>() { 1, 2, 3, 4, 5 };
        player.itemList = new List<Item2>() { new Item2(1, 99), new Item2(2, 44) };

        player.itemDic2 = new Dictionary<string, Item2>() { { "1", new Item2(1, 12) }, { "2", new Item2(2, 22) } };

        SaveData(player, "PlayerInfo2");

        PlayerInfo2 p = LoadData("PlayerInfo2");
    }

    public void SaveData(PlayerInfo2 data, string path)
    {
        string jsonStr = JsonMapper.ToJson(data);
        print(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/" + path + ".json", jsonStr);
    }

    public PlayerInfo2 LoadData(string path)
    {
        string jsonStr = File.ReadAllText(Application.persistentDataPath + "/" + path + ".json");

        return JsonMapper.ToObject<PlayerInfo2>(jsonStr);
    }
}
