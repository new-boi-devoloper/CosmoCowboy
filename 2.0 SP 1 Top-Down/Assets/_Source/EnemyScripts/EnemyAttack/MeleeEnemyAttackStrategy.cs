using UnityEngine;

namespace EnemyScripts.EnemyAttack
{
    public class MeleeEnemyAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack()
        {
            Debug.Log("Melee Attacked");
        }
    }
}