using EnemyScripts.EnemyManagement;
using UnityEngine;

namespace EnemyScripts.EnemyStates
{
    public class AttackState : IState
    {
        public void Enter(EnemyBase enemy)
        {
            Debug.Log(" 1111 Attack 1111");
        }

        public void Execute(EnemyBase enemy)
        {
            enemy.AttackSystem.EnemyAttackStrategy.Attack();
            Debug.Log(" 2222 Attack 2222");
        }

        public void Exit(EnemyBase enemy)
        {
            Debug.Log(" 3333 Attack 3333");
        }
    }
}