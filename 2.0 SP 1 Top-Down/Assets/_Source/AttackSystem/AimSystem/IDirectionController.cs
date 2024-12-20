using UnityEngine;

namespace AttackSystem.AimSystem
{
    public interface IDirectionController
    {
        void UpdateDirection(Vector2 direction);
    }
}