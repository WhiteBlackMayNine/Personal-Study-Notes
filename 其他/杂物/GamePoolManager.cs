using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool.Singleton;
using GGG.Tool;

public class GamePoolManager : Singleton<GamePoolManager>
{

    #region 类PoolItem信息

    [System.Serializable]
    private class PoolItem
    {
        public string ItemName;
        public GameObject Item;
        public int InitMaxCount;
    }

    #endregion

    #region 变量相关

    [SerializeField] private List<PoolItem> _configPoolItem = new List<PoolItem>();//配置信息

    private Dictionary<string, Queue<GameObject>> _poolItem = new Dictionary<string, Queue<GameObject>>();//保存信息

    private GameObject _poolItemParent;//对象池的父对象

    #endregion

    #region 初始化对象池
    private void InitPool()
    {
        if (_configPoolItem.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _configPoolItem.Count; i++)//对列表中的每一个元素进行遍历
        {
            for (int j = 0; j < _configPoolItem[i].InitMaxCount; j++)//对每一个元素进行初始化
            {
                //创建
                var item = Instantiate(_configPoolItem[i].Item);
                //设置为失活
                item.SetActive(false);
                //设置为子对象
                item.transform.SetParent(_poolItemParent.transform);

                //判断字典中是否有这个物体对象
                if (_poolItem.ContainsKey(_configPoolItem[i].ItemName))
                {
                    _poolItem[_configPoolItem[i].ItemName].Enqueue(item);//如果有就添加进去
                }
                else
                {
                    //如果没有就先创建再添加
                    _poolItem.Add(_configPoolItem[i].ItemName, new Queue<GameObject>());
                    _poolItem[_configPoolItem[i].ItemName].Enqueue(item);
                }

            }
        }
    }

    #endregion

    #region 调用对象池

    public void TryGetOnePoolItem(string name,Vector3 position,Quaternion roataion)
    {
        if (_poolItem.ContainsKey(name))//判断是否有这个名字的对象
        {
            //如果有就拿出来
            var item = _poolItem[name].Dequeue();
            item.transform.position = position;
            item.transform.rotation = roataion;
            item.SetActive(true);
            _poolItem[name].Enqueue(item);
        }
        else
        {
            DevelopmentToos.WTF("当前申请的池子可能不存在，申请的池子名为" + name);
        }
    }

    public GameObject TryGetOnePoolItem(string name)
    {
        if (_poolItem.ContainsKey(name))
        {
            var item = _poolItem[name].Dequeue();
            item.SetActive(true);
            _poolItem[name].Enqueue(item);

            return item;
        }
        else
        {
            DevelopmentToos.WTF("当前申请的池子可能不存在，申请的池子名为" + name);
        }

        return null;
    }

    #endregion




    private void Start()
    {
        _poolItemParent = new GameObject("对象池的父对象");
        _poolItemParent.transform.SetParent(this.transform);
        //如果希望更加细分，将不同的子对象分别放在其相应的父对象下
        //按照这个逻辑写下去就行 —— 位置设置父对象SetParent，然后设置为隐藏SetActive(false)
        InitPool();
    }

}
