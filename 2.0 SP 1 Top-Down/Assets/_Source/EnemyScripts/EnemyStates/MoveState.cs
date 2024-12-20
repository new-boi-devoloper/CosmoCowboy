using EnemyScripts.EnemyManagement;

namespace EnemyScripts.EnemyStates
{
    public class MoveState : IState
    {
        public void Enter(EnemyBase enemy)
        {
            // Debug.Log("1111 Chase 1111");
        }

        public void Execute(EnemyBase enemy)
        {
            enemy.MovementSystem.MovementStrategy.Move(enemy.transform, enemy.Agent);
            // Debug.Log("2222 Chase 2222");
        }

        public void Exit(EnemyBase enemy)
        {
            // Debug.Log("3333 Chase 3333");
        }
    }
}