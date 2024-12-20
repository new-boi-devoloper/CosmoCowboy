using System;
using System.Collections.Generic;
using DialogSystem;
using Managers;
using PickUpSystem;
using PlayerScripts;
using UnityEngine;

namespace TutorialSystem
{
    public class TutorController : MonoBehaviour
    {
        [field: SerializeField] private List<TutorWindow> tutorWindows;

        private void OnEnable()
        {
            AgentWeapon.OnTutorRequest += TurnOnTutor;
            PlayerTeleport.OnTutorRequest += TurnOnTutor;
            TutorItemPickUp.OnTutorRequest += TurnOnTutor;
        }

        private void OnDisable()
        {
            AgentWeapon.OnTutorRequest -= TurnOnTutor;
            PlayerTeleport.OnTutorRequest -= TurnOnTutor;
            TutorItemPickUp.OnTutorRequest -= TurnOnTutor;
        }

        private void TurnOnTutor(int indexOfTutor)
        {
            // Проверяем, что список tutorWindows не пуст
            if (tutorWindows == null || tutorWindows.Count == 0)
            {
                Debug.LogError("TutorWindows list is empty or not initialized!");
                return;
            }

            // Закрываем все открытые окна
            foreach (var tutorWindow in tutorWindows)
            {
                if (tutorWindow.Window != null)
                {
                    tutorWindow.Window.CloseTutorWindow();
                }
                else
                {
                    Debug.LogWarning($"TutorWindow with index {tutorWindow.Index} has a null Window reference!");
                }
            }

            // Ищем окно с нужным индексом
            var tutor = tutorWindows.Find(element => element.Index == indexOfTutor);

            // Проверяем, найдено ли окно
            if (tutor == null)
            {
                Debug.LogError($"No tutor window found with index {indexOfTutor}!");
                return;
            }

            // Проверяем, что окно имеет ссылку на Window
            if (tutor.Window == null)
            {
                Debug.LogError($"Tutor window with index {indexOfTutor} has a null Window reference!");
                return;
            }

            // Открываем окно
            tutor.Window.OpenTutorWindow();
        }
    }

    [Serializable]
    public class TutorWindow
    {
        [field: SerializeField] public TutorView Window { get; private set; }
        [field: SerializeField] public int Index { get; private set; }
    }
}