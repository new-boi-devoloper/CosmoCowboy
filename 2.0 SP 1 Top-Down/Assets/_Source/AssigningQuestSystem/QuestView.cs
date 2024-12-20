using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace AssigningQuestSystem
{
    public class QuestView : MonoBehaviour
    {
        [field: SerializeField] private GameObject questWindow;
        [field: SerializeField] private GameObject winWindow;
        [field: SerializeField] private GameObject notificationToFinishQuestWindow;
        [field: SerializeField] private TextMeshProUGUI questText;
        [field: SerializeField] private float windowDisplayTime;

        public void ShowQuestInfo(int currentKills, int maxKills)
        {
            questText.text = $"Enemies finished {currentKills}/{maxKills}";
        }

        public void ShowQuestWindow()
        {
            questWindow.SetActive(true);
        }

        public void HideQuestWindow()
        {
            questWindow.SetActive(false);
        }

        public async void ShowWinWindow()
        {
            await ShowWindowForSeconds(winWindow);
        }

        public async void NotifyToFinishQuest()
        {
            await ShowWindowForSeconds(notificationToFinishQuestWindow);
        }

        private async UniTask ShowWindowForSeconds(GameObject window)
        {
            window.SetActive(true);

            // Ждём указанное количество секунд
            await UniTask.Delay((int)(windowDisplayTime * 1000));

            window.SetActive(false);
        }
    }
}