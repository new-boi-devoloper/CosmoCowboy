using UnityEngine;

namespace EnemyScripts.EnemyManagement
{
    public class OneTimeSolutionForFlipping : MonoBehaviour
    {
        private Vector3 _previousPosition;
        private bool _isFacingRight = true; // Флаг для отслеживания направления спрайта

        void Start()
        {
            _previousPosition = transform.position;
        }

        void Update()
        {
            // Вычисляем направление движения по оси X
            float horizontalMovement = transform.position.x - _previousPosition.x;

            // Если движение вправо и спрайт смотрит влево, разворачиваем
            if (horizontalMovement > 0 && !_isFacingRight)
            {
                FlipSprite();
            }
            // Если движение влево и спрайт смотрит вправо, разворачиваем
            else if (horizontalMovement < 0 && _isFacingRight)
            {
                FlipSprite();
            }

            // Сохраняем текущую позицию для следующего кадра
            _previousPosition = transform.position;
        }

        private void FlipSprite()
        {
            // Меняем направление спрайта
            _isFacingRight = !_isFacingRight;

            // Разворачиваем спрайт по оси X
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}