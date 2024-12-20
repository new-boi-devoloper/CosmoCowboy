using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts.EnemyMovement
{
    public interface IEnemyMovementStrategy
    {
        void Move(Transform transform, NavMeshAgent agent);
    }
}