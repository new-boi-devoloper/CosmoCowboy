using System;
using EnemyScripts.EnemyAttack;
using UnityEngine;

namespace EnemyScripts.EnemyManagement
{
    public class EnemyAttackSystem
    {
        public EnemyAttackSystem(GameObject gameObject, AttackType chosenAttackType)
        {
            attackType = chosenAttackType;
            switch (chosenAttackType)
            {
                case AttackType.Distant:
                    EnemyAttackStrategy = new DistantEnemyAttackStrategy();
                    break;
                case AttackType.Melee:
                    EnemyAttackStrategy = new MeleeEnemyAttackStrategy();
                    break;
                default:
                    throw new Exception($"not valid attack type on {gameObject}");
            }
        }

        public AttackType attackType { get; private set; }
        public IEnemyAttackStrategy EnemyAttackStrategy { get; private set; }
    }

    public enum AttackType
    {
        Distant,
        Melee
    }
}