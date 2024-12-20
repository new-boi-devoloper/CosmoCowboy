using PlayerScripts;

namespace Managers.CommandPatern
{
    public class FireWeaponCommand : ICommand
    {
        private readonly AgentWeapon _agentWeapon;

        public FireWeaponCommand(AgentWeapon agentWeapon)
        {
            _agentWeapon = agentWeapon;
        }

        public void Execute()
        {
            _agentWeapon.FireCurrentFireWeapon();
        }
    }
}