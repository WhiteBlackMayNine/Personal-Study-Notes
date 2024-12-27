using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public abstract class BasePanel : MonoBehaviour
    {
        //整体控制淡入淡出的 画布组件
        private CanvasGroup _canvasGroup;
        private float _alphaSpeed = 10f;//淡入淡出的时间
        private bool _isShow;
        private UnityAction _hideCallback;
        protected virtual void Awake()
        {
            //获取挂载的 CanvasGroup 组件，如果没有就添加一个
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        protected virtual void Start()
        {
            Init();
        }

        private void Update()
        {
            FadeOut();
            FadeIn();
        }
        
        public abstract void Init();//用于初始化按钮 事件监听等

        //淡入效果
        private void FadeIn()
        {
            if (_isShow && _canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += _alphaSpeed * Time.deltaTime;
                if (_canvasGroup.alpha >= 1f)
                {
                    _canvasGroup.alpha = 1f;
                }
            } 
        }

        //淡出效果
        private void FadeOut()
        {
            if (_isShow) return;
            _canvasGroup.alpha -= _alphaSpeed * Time.deltaTime;
            if (_canvasGroup.alpha <= 0)
            {
                _canvasGroup.alpha = 0f;
                //TODO:淡出完毕后 让 UI管理器删除自己
                _hideCallback?.Invoke();
            }
        }
        
        //外部调用 显示自己
        public virtual void ShowMe()
        {
            _isShow = true;
            _canvasGroup.alpha = 0f;
        }

        //外部调用 为什么添加 callback 当透明度为0时在删除，而不是一开始就删掉
        public virtual void HideMe(UnityAction callback)
        {
            _isShow = false;
            _canvasGroup.alpha = 1f;
            _hideCallback = callback;
        }


    }
}