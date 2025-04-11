using ScriptObject.Skill.CheckISkillItem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Characters.Skill.CheckSkill
{
    public class SelectHorizontalScrollItem : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
    {
        [Header("选项SO文件")]
        public CheckSkillItemSO checkSkillItemSO;
        [Header("RectTransform")]
        public RectTransform rectTransform;
        [Header("CanvasGroup")]
        public CanvasGroup canvasGroup;
        [Header("当前状态")] public bool isDrag;

        public Image image;
        public TextMeshProUGUI nameText;
        public string descriptionText;
        
        public int itemIndex;
        public int infoIndex;
        
        public int SkillID => checkSkillItemSO.skillID;
        public Sprite UnlockIcon => checkSkillItemSO.unlockIcon;
        public Sprite UnlockBackGround => checkSkillItemSO.unlockBackGround;
        public Sprite NoUnlockIcon => checkSkillItemSO.noUnlockIcon;
        public Sprite NoUnlockBackGround => checkSkillItemSO.noUnlockBackGround;
        public string SkillName => checkSkillItemSO.skillName;
        public string SkillDescription => checkSkillItemSO.description;
        
        private SelectHorizontalScroll _selectHorizontalScroll;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetInfo(Sprite sprite,string itemName,string description,int index
            ,SelectHorizontalScroll selectHorizontalScroll)
        {
            image.sprite = sprite;
            nameText.text = itemName;
            descriptionText = description; 
            infoIndex = index;
            _selectHorizontalScroll = selectHorizontalScroll;            
        }

        public void SetAlpha(float alpha)
        {
            canvasGroup.alpha = alpha;   
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isDrag = false;
            _selectHorizontalScroll.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isDrag)
            {
                //索引 信息索引 位置
                _selectHorizontalScroll.Select(itemIndex, infoIndex, rectTransform);
            }
            
            _selectHorizontalScroll.OnPointerUp(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            isDrag = true;
            _selectHorizontalScroll.OnDrag(eventData);
        }
    }
}