using UnityEngine;

namespace Utils
{
    public static class TimeManager
    {
        public static void TurnOnPause()
        {
            Time.timeScale = 0f;
        }

        public static void TurnOffPause()
        {
            Time.timeScale = 1f;
        }
    }
}