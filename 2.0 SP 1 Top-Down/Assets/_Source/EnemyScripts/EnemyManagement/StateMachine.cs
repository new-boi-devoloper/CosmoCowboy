using EnemyScripts.EnemyStates;

namespace EnemyScripts.EnemyManagement
{
    public class StateMachine
    {
        private IState _currentState;

        public void ChangeState(IState newState, EnemyBase enemy)
        {
            _currentState?.Exit(enemy);
            _currentState = newState;
            _currentState?.Enter(enemy);
        }

        public void Execute(EnemyBase enemy)
        {
            _currentState?.Execute(enemy);
            // Debug.Log($"Текущее состояние: {_currentState}");
        }
    }
}