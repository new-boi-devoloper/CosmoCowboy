using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Sound
{
    public class AudioManager : MonoBehaviour
    {
        [field: SerializeField] public AudioSetupView AudioSetupView { get; private set; }
        [field: SerializeField] public List<SoundUnit> SoundsList { get; private set; }

        public static AudioManager Instance;

        private readonly List<AudioSource> _audioSources = new(); 
        private AudioSource _backgroundMusicSource; 

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach (var soundUnit in SoundsList)
            {
                var audioObject = new GameObject($"AudioObject_{soundUnit.SoundId}");
                audioObject.transform.SetParent(transform);
                var audioSource = audioObject.AddComponent<AudioSource>();
                audioSource.clip = soundUnit.SoundClip; 
                audioSource.volume = 0.3f; 
                _audioSources.Add(audioSource);
            }

            _backgroundMusicSource = new GameObject("BackgroundMusic").AddComponent<AudioSource>();
            _backgroundMusicSource.transform.SetParent(transform);
            _backgroundMusicSource.loop = true; 
            _backgroundMusicSource.volume = 1f; 

            var backgroundMusic = SoundsList.Find(sound => sound.SoundId == 99);
            if (backgroundMusic != null)
            {
                _backgroundMusicSource.clip = backgroundMusic.SoundClip;
                _backgroundMusicSource.Play(); 
            }
            else
            {
                Debug.LogWarning("Фоновая музыка с ID 99 не найдена!");
            }
        }

        private void OnEnable()
        {
            AudioSetupView.OnSoundVolumeChange += SetVolumeForAllSounds;
        }

        private void OnDisable()
        {
            AudioSetupView.OnSoundVolumeChange -= SetVolumeForAllSounds;
        }

        private void SetVolumeForAllSounds(float soundVolume)
        {
            foreach (var audioSource in _audioSources)
            {
                audioSource.volume = soundVolume;
            }

            // Устанавливаем громкость фоновой музыки
            _backgroundMusicSource.volume = soundVolume;
        }

        public void PlaySound(int soundId)
        {
            var soundToPlay = SoundsList.Find(element => element.SoundId == soundId);

            if (soundToPlay != null)
            {
                var audioSource = _audioSources.Find(source => source.clip == soundToPlay.SoundClip);
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                else
                {
                    Debug.LogWarning($"AudioSource для звука с ID {soundId} не найден!");
                }
            }
            else
            {
                Debug.LogWarning($"Звук с ID {soundId} не найден!");
            }
        }
        
        public void StopBackgroundMusic()
        {
            if (_backgroundMusicSource != null)
            {
                _backgroundMusicSource.Stop();
            }
        }
        
        public void PlayBackgroundMusic()
        {
            if (_backgroundMusicSource != null && !_backgroundMusicSource.isPlaying)
            {
                _backgroundMusicSource.Play();
            }
        }
    }

    [Serializable]
    public class SoundUnit
    {
        [field: SerializeField] public AudioClip SoundClip { get; private set; } // Звук, который нужно воспроизвести
        [field: SerializeField] public int SoundId { get; private set; } // Уникальный ID звука
    }
}