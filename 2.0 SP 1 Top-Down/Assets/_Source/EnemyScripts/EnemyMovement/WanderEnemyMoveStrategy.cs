using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts.EnemyMovement
{
    public class WanderEnemyMoveStrategy : IEnemyMovementStrategy
    {
        private readonly Vector2 wanderCenter;
        private readonly float wanderRadius;
        private Vector2 targetPosition;

        public WanderEnemyMoveStrategy(Vector2 center, float radius)
        {
            wanderCenter = center;
            wanderRadius = radius;
            targetPosition = GetRandomPointInCircle(); // Инициализация начальной цели
        }

        public void Move(Transform transform, NavMeshAgent agent)
        {
            // Проверяем, достиг ли враг текущей цели
            if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
            {
                // Генерируем новую случайную точку в пределах окружности
                targetPosition = GetRandomPointInCircle();
                agent.SetDestination(targetPosition);
            }
        }

        private Vector2 GetRandomPointInCircle()
        {
            // Генерируем случайную точку в пределах окружности
            var randomDirection = Random.insideUnitCircle * wanderRadius;
            return wanderCenter + randomDirection;
        }
    }
}