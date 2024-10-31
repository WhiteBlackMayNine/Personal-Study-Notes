using UnityEngine;
using Tools.InteractableSign;
using Tools.Manager.GameInput;


public class InteractableSign : MonoBehaviour
    {
        public GameObject Sign;
        public Transform PlayerTransform;
        private Animator _signAnimator;
        private bool _canPress;
        private IInteractable _interactable;

        private void Awake()
        {
            _signAnimator = Sign.GetComponent<Animator>();
        }

        private void Update()
        {
            Sign.GetComponent<SpriteRenderer>().enabled = _canPress;
            transform.localScale = PlayerTransform.localScale;
            OnOpenChest();
        }

        private void OnOpenChest()
        {        
            if (GameInputManager.MainInstance.Grap && _interactable != null && _canPress)
            {
                _interactable.TriggerAction();
            }
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Interactable"))
            {
                _canPress = true;
                _signAnimator.Play("InteractableSign");
                _interactable = other.GetComponent<IInteractable>();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            _canPress = false;
        }
    }
