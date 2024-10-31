using UnityEngine;

namespace Tools.InteractableSign
{
    public class InteractableChest : MonoBehaviour,IInteractable
    {
        private SpriteRenderer _spriteRenderer;
        public Sprite openSprite;
        public Sprite closeSprite;
        public bool isDone;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _spriteRenderer.sprite = isDone ? openSprite : closeSprite;
        }

        public void TriggerAction()
        {
            Debug.Log("Open Chest!");
            if (!isDone)
            {
                OpenChest();
            }
        }

        private void OpenChest()
        {
            _spriteRenderer.sprite = openSprite;
            isDone = true;
            this.gameObject.tag = "Untagged";
        }
    }
}