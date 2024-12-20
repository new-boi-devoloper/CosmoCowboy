using System;
using Managers.Sound;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInvoker : IDisposable
    {
        private readonly Player _player;
        private readonly PlayerCombatCommands _playerCombatCommands;
        private readonly PlayerMovement _playerMovement;

        public PlayerInvoker(Player player, PlayerMovement playerMovement, PlayerCombatCommands playerCombatCommands)
        {
            _player = player;
            _playerMovement = playerMovement;
            _playerCombatCommands = playerCombatCommands;

            InputListener.OnFire += InvokeFireArmShoot;
        }

        public void Dispose()
        {
            InputListener.OnFire -= InvokeFireArmShoot;
        }

        public void HandleInput(Vector2 inputMove)
        {
            InvokeMove(inputMove);
        }

        private void InvokeMove(Vector2 moveDirection)
        {
            _playerMovement.Move(_player.Rb, moveDirection, _player.PlayerSpeed);
        }

        private void InvokeFireArmShoot()
        {
            _playerCombatCommands.ExecuteCommand("FireWeapon");
        }
    }
}