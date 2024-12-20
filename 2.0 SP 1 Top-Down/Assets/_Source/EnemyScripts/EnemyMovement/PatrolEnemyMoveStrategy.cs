using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts.EnemyMovement
{
    public class PatrolEnemyMoveStrategy : IEnemyMovementStrategy
    {
        private readonly List<GameObject> _patrolPoints;
        private int _currentPointIndex;

        public PatrolEnemyMoveStrategy(List<GameObject> points)
        {
            _patrolPoints = points;
        }

        public void Move(Transform transform, NavMeshAgent agent)
        {
            if (!agent.hasPath || agent.remainingDistance < 0.5f)
            {
                agent.SetDestination(_patrolPoints[_currentPointIndex].transform.position);

                // Проверка достижимости точки
                if (!agent.pathPending && agent.pathStatus == NavMeshPathStatus.PathInvalid)
                {
                    Debug.Log("Move broken");

                    // Если точка недостижима, переключиться на первую точку
                    _currentPointIndex = 0;
                    agent.SetDestination(_patrolPoints[_currentPointIndex].transform.position);
                }

                UpdatePointIndex();
            }

            // Инвертируем scale в зависимости от направления движения
            FlipSprite(transform, agent);
        }

        private void UpdatePointIndex()
        {
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Count;
        }

        private void FlipSprite(Transform transform, NavMeshAgent agent)
        {
            // Получаем направление движения
            Vector3 direction = agent.steeringTarget - transform.position;

            // Инвертируем scale по оси X, если направление движения влево
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}