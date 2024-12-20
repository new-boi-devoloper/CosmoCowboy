using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Managers.UIManage
{
    public class GameWinOverView : MonoBehaviour
    {
        [field: SerializeField] private GameObject winWindow;
        [field: SerializeField] private GameObject loseWindow;

        // private Game _game;

        // public void Construct(Game game)
        // {
        //     _game = game;
        //     if (_game != null)
        //     {
        //         _game.OnGameWin += ShowWinWindow;
        //         _game.OnGameOver += ShowGameOverWindow;
        //     }
        //     else
        //     {
        //         Debug.LogError("Game object is null in GameWinOverView");
        //     }
        // }

        // private void OnDestroy()
        // {
        //     _game.OnGameWin -= ShowWinWindow;
        //     _game.OnGameOver -= ShowGameOverWindow;
        // }

        public void ShowWinWindow()
        {
            TimeManager.TurnOnPause();
            winWindow.SetActive(true);
            Debug.Log($"win window is {winWindow.activeInHierarchy}");
        }

        public void ShowGameOverWindow()
        {
            TimeManager.TurnOnPause();
            loseWindow.SetActive(true);
        }
    }
}