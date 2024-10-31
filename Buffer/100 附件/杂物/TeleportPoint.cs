using Tools.InteractableSign;
using Tools.Manager.GameEvent;
using UnityEngine;

namespace Tools.Scene
{
    public class TeleportPoint : MonoBehaviour, IInteractable
    {
        [Header("��һ������")]
        public GameSceneSO sceneToGo;
        public Vector3 positionToGo;

        public void TriggerAction()
        {
            Debug.Log("���ͣ�");

            GameEventManager.MainInstance.CallEvent("��������", sceneToGo, positionToGo, true);
        }
    }
}