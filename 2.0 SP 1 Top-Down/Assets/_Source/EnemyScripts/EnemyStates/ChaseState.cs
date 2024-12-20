using EnemyScripts.EnemyManagement;
using PlayerScripts;

namespace EnemyScripts.EnemyStates
{
    public class ChaseState : IState
    {
        public void Enter(EnemyBase enemy)
        {
            // Реализация входа в состояние преследования
            // Debug.Log("1111 Chase 1111");
        }

        public void Execute(EnemyBase enemy)
        {
            // Debug.Log("2222 Chase 2222");
            // Реализация выполнения состояния преследования
            enemy.Agent.SetDestination(Player.PlayerTransform.position);
        }

        public void Exit(EnemyBase enemy)
        {
            // Реализация выхода из состояния преследования
            // Debug.Log("3333 Chase 3333");
        }
    }
}