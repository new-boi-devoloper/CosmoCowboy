using System;
using System.Collections.Generic;
using EnemyScripts.EnemyMovement;
using UnityEngine;

namespace EnemyScripts.EnemyManagement
{
    public class EnemyMovementSystem
    {
        private MoveType _moveType;

        public EnemyMovementSystem(GameObject gameObject, MoveType currentMoveType, List<GameObject> patrolPoints)
        {
            _moveType = currentMoveType;
            switch (currentMoveType)
            {
                case MoveType.Patrol:
                    MovementStrategy = new PatrolEnemyMoveStrategy(patrolPoints);
                    break;
                case MoveType.Flee:
                    MovementStrategy = new FleeEnemyMoveStrategy(Vector2.zero, 5f);
                    break;
                case MoveType.Wander:
                    MovementStrategy = new WanderEnemyMoveStrategy(gameObject.transform.position, 7f);
                    break;
                case MoveType.Still:
                    MovementStrategy = new StillEnemyMoveStrategy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"not choosen enemy move type in {gameObject}");
            }
        }

        public IEnemyMovementStrategy MovementStrategy { get; private set; }
    }

    public enum MoveType
    {
        Patrol,
        Flee,
        Wander,
        Still
    }
}