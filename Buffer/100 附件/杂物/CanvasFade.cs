using DG.Tweening;
using Tools.Manager.GameEvent;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.Scene
{
    public class CanvasFade : MonoBehaviour
    {
        public Image fadeImage;
        private void OnEnable()
        {
            GameEventManager.MainInstance.AddEventListener<float>("逐渐变黑",CanvasFadeIn);
            GameEventManager.MainInstance.AddEventListener<float>("逐渐变透明",CanvasFadeOut);
        }

        private void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEventListener<float>("逐渐变黑",CanvasFadeIn);
            GameEventManager.MainInstance.RemoveEventListener<float>("逐渐变透明",CanvasFadeOut);
        }
        private void CanvasFadeIn(float duration)
        {
            fadeImage.DOBlendableColor(Color.black, duration);
        }
        private void CanvasFadeOut(float duration)
        {
            fadeImage.DOBlendableColor(Color.clear, duration);
        }
    }
}