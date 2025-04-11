using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Characters.Skill.CheckSkill
{
    public class SelectHorizontalScroll : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
    {
        [Serializable]
        public struct ItemInfo
        {
            public string itemName;
            public Sprite itemSprite;
            public string itemDescription;

            public ItemInfo(string itemName, Sprite itemSprite, string itemDescription)
            {
                this.itemName = itemName;
                this.itemSprite = itemSprite;
                this.itemDescription = itemDescription;
            }
        }
        [Header("选项预制体")]
        public GameObject itemPrefab;
        [Header("选项父物体")]
        public RectTransform itemParent;
        [Header("描述文字")]
        public TextMeshProUGUI itemDescription; 
        [Header("选项信息")]
        public ItemInfo[] itemsInfo;
        [Header("显示数量 奇数")]
        public int displayerNumber;
        [Header("选项间隔")]
        public float itemSpace;
        [Header("移动插值")]
        public float moveSmooth;
        [Header("拖动速度")]
        public float dragSpeed;
        [Header("缩放倍率")]
        public float scaleMultiplier;
        [Header("透明度倍率")]
        public float alphaMultiplier;

        public event Action<int> SelectAction;
        
        private SelectHorizontalScrollItem[] _items;//物品
        private float _displayerWidth;//显示的范围
        private int _offsetTimes;
        private bool _isDrag;
        private int _currentItemIndex;
        private float[] _distances;
        private float _selectItemX;
        private bool _isSelectMove;

        private bool _isSelected;

        private void Start()
        {
            Init();
            MoveItems(0);
        }

        private void Update()
        {
            //根据是否拖拽进行自动吸附
            if (!_isDrag)
            {
                Adsorption();
            }
            
            //根据拖拽距离判断是否需要移动列表
            var currentOffsetTimes = Mathf.FloorToInt(itemParent.localPosition.x / itemSpace);

            if (currentOffsetTimes != _offsetTimes)
            {
                _offsetTimes = currentOffsetTimes;
                MoveItems(_offsetTimes);
            }
            
            ItemsControl();
        }

        //初始化
        private void Init()
        {
            _displayerWidth = (displayerNumber - 1) * itemSpace;
            _items = new SelectHorizontalScrollItem[displayerNumber];

            for (var i = 0; i < displayerNumber; i++)
            {
                var item = Instantiate(itemPrefab, itemParent).
                    GetComponent<SelectHorizontalScrollItem>();

                item.itemIndex = i;
                
                _items[i] = item;
            }
        }

        //设置选项信息
        public void SetItemsInfo(string[] names, Sprite[] sprites, string[] descriptions)
        {
            itemsInfo = new ItemInfo[names.Length];
            for (var i = 0; i < itemsInfo.Length; i++)
            {
                itemsInfo[i] = new ItemInfo(names[i], sprites[i], descriptions[i]);
            }

            SelectAction = null;
            _isSelected = false;
        }
        
        //点击选择
        public void Select(int itemIndex, int infoIndex, RectTransform itemRectTransform)
        {
            //判断是否为居中的选项
            if (!_isSelected && itemIndex == _currentItemIndex)
            {
                //是 则执行事件
                SelectAction?.Invoke(infoIndex);
                _isSelected = true;
                Debug.Log("Selected" + (infoIndex + 1));
            }
            else
            {
                //否则 移动选项
                _isSelectMove = true;
                _selectItemX = itemRectTransform.localPosition.x;
            }
        }

        //移动列表
        private void MoveItems(int offsetTimes)
        {
            //先把所有选项移动正确的位置
            for (var i = 0; i < displayerNumber; i++)
            {
                var x = itemSpace * (i - offsetTimes) - _displayerWidth / 2;
                _items[i].rectTransform.localPosition = new Vector2(x,_items[i].rectTransform.localPosition.y);
            }

            //找到中间的那一个
            int middle;

            if (offsetTimes > 0)
            {
                middle = itemsInfo.Length - offsetTimes % itemsInfo.Length;
            }
            else
            {
                middle = -offsetTimes % itemsInfo.Length;
            }

            var infoIndex = middle;

            //从中间正向赋值
            for (var i = Mathf.FloorToInt(displayerNumber / 2f); i < displayerNumber; i++)
            {
                if (infoIndex >= itemsInfo.Length)
                {
                    infoIndex = 0;
                }
                
                _items[i].SetInfo(itemsInfo[infoIndex].itemSprite, itemsInfo[infoIndex].itemName,
                    itemsInfo[infoIndex].itemDescription,infoIndex,this);

                infoIndex++;
            }
            
            //从中间的上一个反向循环赋值
            
            infoIndex = middle - 1;

            for (var i = Mathf.FloorToInt(displayerNumber / 2f) - 1; i >= 0; i--)
            {
                if (infoIndex <= -1)
                {
                    infoIndex = itemsInfo.Length - 1;
                }
                
                _items[i].SetInfo(itemsInfo[infoIndex].itemSprite, itemsInfo[infoIndex].itemName,
                    itemsInfo[infoIndex].itemDescription,infoIndex,this);
                infoIndex--;
            }
        }

        //控制选项的透明度与缩放 获取中间的选项
        private void ItemsControl()
        {
            //根据每个选项到中心的距离 控制它的缩放与透明度
            _distances = new float[displayerNumber];

            for (var i = 0; i < displayerNumber; i++)
            {
                var distance = Mathf.Abs(_items[i].rectTransform.position.x - transform.position.x);
                _distances[i] = distance;
                var scale = 1 - distance * scaleMultiplier;
                
                _items[i].rectTransform.localScale = new Vector3(scale, scale, 1);
                _items[i].SetAlpha(1 - distance * alphaMultiplier);
            }
            
            //比较长度 得到中间选项
            float minDistance = itemSpace * displayerNumber;
            int minIndex = 0;

            for (int i = 0; i < displayerNumber; i++)
            {
                if (_distances[i] < minDistance)
                {
                    minDistance = _distances[i];
                    minIndex = i;
                }
            }
            
            itemDescription.text = _items[minIndex].descriptionText;
            _currentItemIndex = _items[minIndex].itemIndex;
            
        }

        //自动吸附
        private void Adsorption()
        {
            float targetX;
            
            //先判断有没有选择选项的移动
            if (!_isSelectMove)
            {
                //没有则找最近的一个
                
                var distance = itemParent.localPosition.x % itemSpace;
                var times = Mathf.FloorToInt(itemParent.localPosition.x / itemSpace);

                if (distance > 0)
                {
                    if (distance < itemSpace / 2)
                    {
                        targetX = times * itemSpace;
                    }
                    else
                    {
                        targetX = (times + 1) * itemSpace;
                    }
                }
                else
                {
                    if (distance < -itemSpace / 2)
                    {
                        targetX = times * itemSpace;
                    }
                    else
                    {
                        targetX = (times + 1) * itemSpace;
                    }
                }
            }
            else
            {
                //如果有 就是要选择的选项
                targetX = -_selectItemX;
            }
            
            //根据选项的X坐标进行插值移动
            itemParent.localPosition = new Vector2(Mathf.Lerp(itemParent.localPosition.x,targetX,
                moveSmooth / 10), itemParent.localPosition.y);
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            _isSelectMove = false;
            itemParent.localPosition = new Vector2(itemParent.localPosition.x + eventData.delta.x * dragSpeed,
                itemParent.localPosition.y);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDrag = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDrag = false;
        }
    }
}