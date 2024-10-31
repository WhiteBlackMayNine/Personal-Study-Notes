using Manage.TimeManage;
using UnityEngine;
using UnityEngine.Events;

namespace General
{
    public class Character : MonoBehaviour
    {
        [SerializeField, Header("基本属性")]
        public float maxHealth;
        public float currentHealth;

        [Header("无敌时间")]
        public float invulnerableTime;//无敌时间
        public bool isInvulnerable;//是否无敌
        
        public UnityEvent onDeath;
        public UnityEvent<Transform> onTakeDamage;
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(Attack attacker)
        {
            if(isInvulnerable) return;
            if(attacker.transform == transform) return;
            if (currentHealth - attacker.damage > 0)
            {
                currentHealth -= attacker.damage;
                TriggerInvulnerability();
                onTakeDamage?.Invoke(attacker.transform);
            }
            else
            {
                currentHealth = 0;
                //触发死亡
                onDeath?.Invoke();
            }
        }

        private void TriggerInvulnerability()
        {
            if (isInvulnerable) return;
            isInvulnerable = true;
            GameTimeManage.MainInstance.TryGetOneTime(invulnerableTime,ResetInvulnerability);
        }

        private void ResetInvulnerability()
        {
            isInvulnerable = false;
        }
        
        
    }
}