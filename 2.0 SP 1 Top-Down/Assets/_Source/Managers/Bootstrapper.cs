using System;
using Managers.CommandPatern;
using Managers.UIManage;
using NPC_Variants;
using PlayerScripts;
using UnityEngine;

namespace Managers
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Tutor Info")] [SerializeField]
        private int index = 1;

        [Header("Game Bindings")] [SerializeField]
        private InputListener inputListener;

        [SerializeField] private Player player;
        [SerializeField] private AgentWeapon agentWeapon;
        [SerializeField] private PlayerTeleport playerTeleport;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private QuestNpc[] questNpc;
        [SerializeField] private LoreNpc loreNpc;
        [SerializeField] private GameWinOverView gameWinOverView;

        
        // [SerializeField] private PlayerUI playerUI;
        private Game _game;

        private PlayerCombatCommands _playerCombatCommands;
        private PlayerControls _playerControls;

        private PlayerInvoker _playerInvoker;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerControls = new PlayerControls();
            _playerMovement = new PlayerMovement();
            _playerCombatCommands = new PlayerCombatCommands();

            _playerInvoker = new PlayerInvoker(player, _playerMovement, _playerCombatCommands);

            _game = new Game();

            AttackCommandsInitialize();

            // Активируем контроллер ввода
            _playerControls.Enable();

            inputListener.Initialize(_playerControls, _playerInvoker);
            // agentWeapon.Construct(player);


            _game.StartGame(player, playerTeleport, enemySpawner, gameWinOverView);

            foreach (var npc in questNpc)
            {
                npc.Construct(_game);
            }
            
            
            // if (gameWinOverView != null)
            // {
            //     gameWinOverView.Construct(_game);
            // }
            // else
            // {
            //     Debug.LogError("GameWinOverView is not assigned in Bootstrapper");
            // }
            
            OnTutorRequest?.Invoke(index);
        }

        private void OnDestroy()
        {
            _playerControls.Disable();
        }

        public static event Action<int> OnTutorRequest;

        private void AttackCommandsInitialize()
        {
            // Регистрация команд
            _playerCombatCommands.RegisterCommand("FireWeapon", new FireWeaponCommand(agentWeapon));
        }
    }
}