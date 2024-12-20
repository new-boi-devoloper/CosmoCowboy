using System;
using AssigningQuestSystem;
using Managers.UIManage;
using NPC_Variants;
using PlayerScripts;
using UnityEngine;

namespace Managers
{
    public class Game
    {
        private readonly LevelType _levelType = LevelType.Level1;
        private EnemySpawner _enemySpawner;
        private PlayerTeleport _levelTeleport;
        private Player _player;
        private GameWinOverView _gameWinOverView;

        public event Action OnGameWin;
        public event Action OnGameOver;

        public void StartGame(Player player, PlayerTeleport playerTeleport, EnemySpawner enemySpawner, GameWinOverView gameWinOverView)
        {
            _levelTeleport = playerTeleport;
            _player = player;
            _enemySpawner = enemySpawner;
            _gameWinOverView = gameWinOverView;
            
            player.OnPlayAnimDead += LoseGame;

            SwitchLevel(_levelType);
        }


        public void SwitchLevel(LevelType levelType) // передавать LevelType чтобы включать следующие уровни 
        {
            if (_player != null)
            {
                if (levelType == LevelType.Level99)
                {
                    _gameWinOverView.ShowWinWindow();
                    // WinGame();
                    return;
                }
                _levelTeleport.SpawnPlayer(levelType, _player);
                _enemySpawner.GenerateEnemies(levelType);
            }
            else
            {
                Debug.Log($"{_player.GetType()} null");
            }
        }


        // private void WinGame()
        // {
        //     OnGameWin?.Invoke();
        // }

        private void LoseGame()
        {
            _gameWinOverView.ShowGameOverWindow();
        }
    }
}