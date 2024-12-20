using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers.Sound
{
    public class AudioSetupView : MonoBehaviour
    {
        public event Action<float> OnSoundVolumeChange;

        [SerializeField] private Slider soundSlider; // Slider для управления громкостью

        private void Start()
        {
            // Подписываемся на событие изменения значения Slider
            soundSlider.onValueChanged.AddListener(OnSliderValueChanged);

            // Инициализируем начальное состояние (например, устанавливаем громкость на 1)
            soundSlider.value = 0.3f;
            OnSliderValueChanged(soundSlider.value);
        }

        private void OnSliderValueChanged(float volume)
        {
            // Вызываем событие для изменения громкости
            OnSoundVolumeChange?.Invoke(volume);
        }
    }
}