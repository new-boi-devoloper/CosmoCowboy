using EnemyScripts.EnemyManagement;
using UnityEngine;

namespace EnemyScripts.EnemyStates
{
    public class DeathState : IState
    {
        public void Enter(EnemyBase enemy)
        {
            Debug.Log("Entering Death IState");
            enemy.GetComponent<Animator>().SetTrigger("Die");
        }

        public void Execute(EnemyBase enemy)
        {
        }

        public void Exit(EnemyBase enemy)
        {
            Debug.Log("Exiting Death IState");
        }
    }
}