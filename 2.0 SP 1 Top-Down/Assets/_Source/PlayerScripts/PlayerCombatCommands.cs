using System.Collections.Generic;
using Managers.CommandPatern;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerCombatCommands
    {
        private readonly Dictionary<string, ICommand> _commands;

        public PlayerCombatCommands()
        {
            _commands = new Dictionary<string, ICommand>();
        }

        public void RegisterCommand(string commandName, ICommand command)
        {
            _commands[commandName] = command;
        }

        public void ExecuteCommand(string commandName)
        {
            if (_commands.TryGetValue(commandName, out var command))
                command.Execute();
            else
                Debug.LogWarning($"Command {commandName} not found.");
        }
    }
}