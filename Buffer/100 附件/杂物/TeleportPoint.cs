using Tools.InteractableSign;
using Tools.Manager.GameEvent;
using UnityEngine;

namespace Tools.Scene
{
    public class TeleportPoint : MonoBehaviour, IInteractable
    {
        [Header("下一个场景")]
        public GameSceneSO sceneToGo;
        public Vector3 positionToGo;

        public void TriggerAction()
        {
            Debug.Log("传送！");

            GameEventManager.MainInstance.CallEvent("场景加载", sceneToGo, positionToGo, true);
        }
    }
}