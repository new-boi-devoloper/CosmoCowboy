using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EnemyScripts.EnemyManagement
{
    public class EnemyHealth : MonoBehaviour
    {
        [field: SerializeField] public float MaxHealth { get; private set; }

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = MaxHealth;
        }

        public static event Action OnDamaged;
        public static event Action OnDeath;
        public event Action OnDeadAnimPlay;
        public event Action OnTakeHitAnim;
        public void ChangeHealth(float damage)
        {
            _currentHealth += damage;
            OnDamaged?.Invoke();
            OnTakeHitAnim?.Invoke();
            Debug.Log($"EnemyHealth{gameObject} {_currentHealth}");

            if (_currentHealth <= 0) Die();
        }

        private void Die()
        {
            OnDeath?.Invoke();
            Debug.Log($"Dead{gameObject}");
            OnDeadAnimPlay?.Invoke();
            UniTask.Delay(900);
            Destroy(gameObject);
        }
    }
}