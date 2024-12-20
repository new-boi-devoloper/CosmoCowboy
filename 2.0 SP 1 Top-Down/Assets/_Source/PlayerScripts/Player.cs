using System;
using InventorySystem.Models;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float PlayerSpeed { get; private set; }
        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
        [field: SerializeField] public int Money { get; private set; }

        public static Transform PlayerTransform { get; private set; }

        public Rigidbody2D Rb { get; private set; }

        public int currentHealth { get; private set; }

        private Vector3 _previousPosition; // Предыдущая позиция игрока
        private bool _isFacingRight = true; // Флаг для отслеживания направления спрайта

        private void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            PlayerTransform = transform;
            currentHealth = MaxHealth;
            _previousPosition = transform.position; // Инициализация предыдущей позиции
            EdibleItemSO.OnAddHPToPlayer += ChangeHealth;
        }

        private void OnDestroy()
        {
            EdibleItemSO.OnAddHPToPlayer += ChangeHealth;
        }

        private void ChangeHealth(int amount)
        {
            if (currentHealth + amount <= 0)
            {
                return;
            }

            currentHealth += amount;

            OnPlayAnimHit?.Invoke();
            OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        }

        private void Update()
        {
            PlayerTransform = transform;
            UpdateSpeed();
            // UpdateDirection(); // Обновляем направление движения
            _previousPosition = transform.position; // Сохраняем текущую позицию для следующего кадра
        }

        private void UpdateSpeed()
        {
            Vector3 movement = transform.position - _previousPosition;

            float speed = movement.magnitude;

            OnSpeedChanged?.Invoke(speed);
        }

        // private void UpdateDirection()
        // {
        //     // Вычисляем направление движения по оси X
        //     float movementX = transform.position.x - _previousPosition.x;
        //
        //     // Если движение влево и спрайт смотрит вправо, разворачиваем
        //     if (movementX < 0 && _isFacingRight)
        //     {
        //         FlipSprite();
        //     }
        //     // Если движение вправо и спрайт смотрит влево, разворачиваем
        //     else if (movementX > 0 && !_isFacingRight)
        //     {
        //         FlipSprite();
        //     }
        // }
        //
        // private void FlipSprite()
        // {
        //     // Меняем направление спрайта
        //     _isFacingRight = !_isFacingRight;
        //
        //     // Разворачиваем спрайт по оси X
        //     Vector3 scale = transform.localScale;
        //     scale.x *= -1;
        //     transform.localScale = scale;
        // }

        public event Action<int, int> OnHealthChanged;
        public event Action OnPlayAnimHit;
        public event Action OnPlayAnimDead;
        public event Action<float> OnSpeedChanged;

        public bool TryChangeCoins(int amount)
        {
            if (Money + amount <= 0) return false;

            Money += amount;
            return true;
        }

        public bool TryChangeHealth(int amount)
        {
            if (currentHealth - amount <= 0)
            {
                OnPlayAnimDead?.Invoke();
                OnHealthChanged?.Invoke(currentHealth, MaxHealth);
                return false;
            }

            currentHealth -= amount;

            OnPlayAnimHit?.Invoke();
            OnHealthChanged?.Invoke(currentHealth, MaxHealth);

            return true;
        }
    }
}