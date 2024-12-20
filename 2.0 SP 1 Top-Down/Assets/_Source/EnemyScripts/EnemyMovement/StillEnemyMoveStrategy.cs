using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts.EnemyMovement
{
    public class StillEnemyMoveStrategy : IEnemyMovementStrategy
    {
        public void Move(Transform transform, NavMeshAgent agent)
        {
            // Do nothing, enemy stays still
        }
    }
}