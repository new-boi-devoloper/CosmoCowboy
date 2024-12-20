using UnityEngine;

namespace EnemyScripts.EnemyAttack
{
    public class DistantEnemyAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack()
        {
            Debug.Log("Melee Attacked");
        }
    }
}