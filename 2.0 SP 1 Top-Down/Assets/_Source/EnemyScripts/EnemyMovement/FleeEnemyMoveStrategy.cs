using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts.EnemyMovement
{
    public class FleeEnemyMoveStrategy : IEnemyMovementStrategy
    {
        private readonly Vector2 fleeCenter;
        private readonly float fleeRadius;

        public FleeEnemyMoveStrategy(Vector2 center, float radius)
        {
            fleeCenter = center;
            fleeRadius = radius;
        }

        public void Move(Transform transform, NavMeshAgent agent)
        {
            var fleeDirection = (Vector2)transform.position - fleeCenter;
            var targetPosition = (Vector2)transform.position + fleeDirection.normalized * fleeRadius;
            agent.SetDestination(targetPosition);
        }
    }
}