using Managers;
using UnityEngine;
using Utils;

namespace TutorialSystem
{
    public class TutorView : MonoBehaviour, ITutorView
    {
        [field: SerializeField] private GameObject tutorWindow;

        public void CloseTutorWindow()
        {
            tutorWindow.SetActive(false);
            TimeManager.TurnOffPause();
        }

        public void OpenTutorWindow()
        {
            tutorWindow.SetActive(true);
            TimeManager.TurnOnPause();
        }
    }
}