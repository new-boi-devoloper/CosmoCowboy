using System;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

namespace Managers
{
    public class PlayerTeleport : MonoBehaviour
    {
        [field: SerializeField] private List<PlayerSpawnPoint> positionToSpawn;

        [Header("Tutor Info")] [SerializeField]
        private int index = 1;

        private bool isFirstPhraseShown = true;

        public static event Action<int> OnTutorRequest;

        public void SpawnPlayer(LevelType levelType, Player player)
        {
            var spawnPoint = positionToSpawn.Find(point => point.Level == levelType);

            if (spawnPoint != null)
            {
                OneTimeSolutionForTutor();
                player.transform.position = spawnPoint.SpawnPosition.position;
                Debug.Log($"Player spawned at {spawnPoint.SpawnPosition.position} for level {levelType}");
            }
            else
            {
                Debug.LogWarning($"No spawn point found for level {levelType}");
            }
        }

        private void OneTimeSolutionForTutor()
        {
            if (isFirstPhraseShown)
            {
                OnTutorRequest?.Invoke(index);
                isFirstPhraseShown = false;
            }
        }
    }

    [Serializable]
    internal class PlayerSpawnPoint
    {
        [field: SerializeField] public Transform SpawnPosition { get; private set; }
        [field: SerializeField] public LevelType Level { get; private set; }
    }
}