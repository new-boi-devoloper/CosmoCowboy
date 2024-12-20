using System;
using System.Collections.Generic;
using EnemyScripts.Enemies;
using EnemyScripts.EnemyManagement;
using UnityEngine;

namespace Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        [field: SerializeField] private List<EnemySpawnEntity> enemiesForSpawn;

        private int _totalEnemies; // Общее количество врагов на уровне
        private LevelType _currentLevel; // Текущий уровень

        private void Start()
        {
            // Подписываемся на событие уничтожения врага
            EnemyHealth.OnDeath += OnEnemyDeath;
        }

        private void OnDestroy()
        {
            // Отписываемся от события при уничтожении объекта
            EnemyHealth.OnDeath -= OnEnemyDeath;
        }

        public void GenerateEnemies(LevelType currentLevel)
        {
            _currentLevel = currentLevel;
            _totalEnemies = 0; // Сбрасываем счетчик

            foreach (var enemyEntity in enemiesForSpawn)
            {
                Debug.Log($"enemies generated on {currentLevel}");
                if (enemyEntity.EnemyLevel == currentLevel)
                {
                    SpawnEnemies(enemyEntity);
                    _totalEnemies += enemyEntity.AmountToGenerate; // Увеличиваем счетчик
                }
            }
        }

        private void SpawnEnemies(EnemySpawnEntity enemyEntity)
        {
            for (var i = 0; i < enemyEntity.AmountToGenerate; i++)
            {
                Debug.Log($"enemy spawned on {enemyEntity.SpawnPoint.position}");
                Instantiate(enemyEntity.EnemyPrefab, enemyEntity.SpawnPoint.position, Quaternion.identity);
            }
        }

        private void OnEnemyDeath()
        {
            _totalEnemies--; // Уменьшаем счетчик при уничтожении врага
            Debug.Log($"Enemy died, remaining enemies: {_totalEnemies}");

            if (_totalEnemies <= 0)
            {
                Debug.Log("All enemies are dead, respawning...");
                GenerateEnemies(_currentLevel); // Респавним врагов
            }
        }
    }

    public enum LevelType
    {
        Level1,
        Level2,
        Level3,
        Level99
    }

    [Serializable]
    internal class EnemySpawnEntity
    {
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
        [field: SerializeField] public int AmountToGenerate { get; private set; }
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
        [field: SerializeField] public LevelType EnemyLevel { get; private set; }
    }
}