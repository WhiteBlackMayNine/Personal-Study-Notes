using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGG.Tool.Singleton;
using GGG.Tool;

public class GamePoolManager : Singleton<GamePoolManager>
{

    #region ��PoolItem��Ϣ

    [System.Serializable]
    private class PoolItem
    {
        public string ItemName;
        public GameObject Item;
        public int InitMaxCount;
    }

    #endregion

    #region �������

    [SerializeField] private List<PoolItem> _configPoolItem = new List<PoolItem>();//������Ϣ

    private Dictionary<string, Queue<GameObject>> _poolItem = new Dictionary<string, Queue<GameObject>>();//������Ϣ

    private GameObject _poolItemParent;//����صĸ�����

    #endregion

    #region ��ʼ�������
    private void InitPool()
    {
        if (_configPoolItem.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _configPoolItem.Count; i++)//���б��е�ÿһ��Ԫ�ؽ��б���
        {
            for (int j = 0; j < _configPoolItem[i].InitMaxCount; j++)//��ÿһ��Ԫ�ؽ��г�ʼ��
            {
                //����
                var item = Instantiate(_configPoolItem[i].Item);
                //����Ϊʧ��
                item.SetActive(false);
                //����Ϊ�Ӷ���
                item.transform.SetParent(_poolItemParent.transform);

                //�ж��ֵ����Ƿ�������������
                if (_poolItem.ContainsKey(_configPoolItem[i].ItemName))
                {
                    _poolItem[_configPoolItem[i].ItemName].Enqueue(item);//����о���ӽ�ȥ
                }
                else
                {
                    //���û�о��ȴ��������
                    _poolItem.Add(_configPoolItem[i].ItemName, new Queue<GameObject>());
                    _poolItem[_configPoolItem[i].ItemName].Enqueue(item);
                }

            }
        }
    }

    #endregion

    #region ���ö����

    public void TryGetOnePoolItem(string name,Vector3 position,Quaternion roataion)
    {
        if (_poolItem.ContainsKey(name))//�ж��Ƿ���������ֵĶ���
        {
            //����о��ó���
            var item = _poolItem[name].Dequeue();
            item.transform.position = position;
            item.transform.rotation = roataion;
            item.SetActive(true);
            _poolItem[name].Enqueue(item);
        }
        else
        {
            DevelopmentToos.WTF("��ǰ����ĳ��ӿ��ܲ����ڣ�����ĳ�����Ϊ" + name);
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
            DevelopmentToos.WTF("��ǰ����ĳ��ӿ��ܲ����ڣ�����ĳ�����Ϊ" + name);
        }

        return null;
    }

    #endregion




    private void Start()
    {
        _poolItemParent = new GameObject("����صĸ�����");
        _poolItemParent.transform.SetParent(this.transform);
        //���ϣ������ϸ�֣�����ͬ���Ӷ���ֱ��������Ӧ�ĸ�������
        //��������߼�д��ȥ���� ���� λ�����ø�����SetParent��Ȼ������Ϊ����SetActive(false)
        InitPool();
    }

}
