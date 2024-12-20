using PlayerScripts;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts.EnemyManagement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyHealth))]
    public abstract class EnemyBase : MonoBehaviour
    {
        //Base Mechanics
        [SerializeField] internal bool isMoving;
        [SerializeField] internal bool isAttacking;
        [SerializeField] internal bool isAlerting;

        //Base types of mechanics
        [SerializeField] internal MoveType moveType;
        [SerializeField] internal AttackType attackType;
        [SerializeField] internal CharacterType EnemyCharacter;

        // //Patrol needs
        // [field: SerializeField] public List<GameObject> PatrolPoints { get; protected set; }

        internal EnemyMovementSystem MovementSystem { get; set; }
        internal EnemyAttackSystem AttackSystem { get; set; }
        internal StateMachine StateMachine { get; set; }

        internal NavMeshAgent Agent { get; set; }

        internal Vector3 InitialPosition { get; set; }
        internal Player Player;

        protected virtual void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            StateMachine = new StateMachine();
            InitializeStrategies();
            InitializeStates();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;

            Player = FindObjectOfType<Player>();
        }

        //TODO добавить проверку приближения к игроку через статическую переменную его положения и крутить StateMachine /?в Update или событийно?/

        protected abstract void InitializeStrategies();
        protected abstract void InitializeStates();
        protected abstract void ManageEnemyState(Transform playerTransform, Transform enemyTransform);
    }

    public enum CharacterType
    {
        Neutral,
        Passive,
        Aggressive
    }
}