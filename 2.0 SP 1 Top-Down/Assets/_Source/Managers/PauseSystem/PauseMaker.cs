using UnityEngine;
using Utils;

namespace Managers.PauseSystem
{
    public class PauseMaker : MonoBehaviour
    {
        private bool _isPaused = false; 

        public void TurnOnPause()
        {
            if (!_isPaused)
            {
                TimeManager.TurnOnPause();
                _isPaused = true;
            }
        }

        public void TurnOffPause()
        {
            if (_isPaused)
            {
                TimeManager.TurnOffPause();
                _isPaused = false;
            }
        }
    }
}