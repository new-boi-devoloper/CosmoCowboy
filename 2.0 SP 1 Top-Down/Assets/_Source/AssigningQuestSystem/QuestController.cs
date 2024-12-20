using System;
using EnemyScripts.EnemyManagement;
using UnityEngine;

namespace AssigningQuestSystem
{
    public class QuestController : MonoBehaviour
    {
        [field: SerializeField] private QuestView questView;

        private int _currentKilledEnemies;
        private bool _isQuestAssigned;
        private int _questTarget;
        private Action currentEndQuestCallback;
        public event Action OnQuestComplete;

        private void OnEnable()
        {
            EnemyHealth.OnDeath += CountEnemies;
        }

        private void OnDisable()
        {
            EnemyHealth.OnDeath -= CountEnemies;
        }

        public void CreateQuest(int amountToKill, Action endCallback)
        {
            if (_isQuestAssigned)
            {
                questView.NotifyToFinishQuest();
                return;
            }

            currentEndQuestCallback = endCallback;
            _currentKilledEnemies = 0;
            _questTarget = amountToKill;
            questView.ShowQuestWindow();
            questView.ShowQuestInfo(_currentKilledEnemies, amountToKill);
            _isQuestAssigned = true;
        }

        private void CountEnemies()
        {
            if (_isQuestAssigned)
            {
                _currentKilledEnemies++;
                questView.ShowQuestInfo(_currentKilledEnemies, _questTarget);

                if (_currentKilledEnemies >= _questTarget)
                {
                    questView.ShowQuestInfo(_currentKilledEnemies, _questTarget);
                    questView.ShowWinWindow();
                    questView.HideQuestWindow();
                    _isQuestAssigned = false;
                    currentEndQuestCallback?.Invoke();
                }
            }
        }
    }
}