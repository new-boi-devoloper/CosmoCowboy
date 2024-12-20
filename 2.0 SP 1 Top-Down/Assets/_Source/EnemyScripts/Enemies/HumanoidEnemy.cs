using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks; // Подключаем UniTask
using EnemyScripts.EnemyInterfaces;
using EnemyScripts.EnemyManagement;
using EnemyScripts.EnemySO;
using EnemyScripts.EnemyStates;
using Managers.Sound;
using PlayerScripts;
using UnityEngine;
using Utils;

namespace EnemyScripts.Enemies
{
    public class HumanoidEnemy :
        EnemyBase,
        IDie,
        IDamageable,
        IAttackable,
        IChaseable,
        IMovable
    {
        [field: SerializeField] private List<GameObject> patrolPoints;
        [field: SerializeField] private EnemyTeamConfig enemyTeamConfig;
        [field: SerializeField] private LayerMask playerLayer;

        private AttackState _attackState;
        private ChaseState _chaseState;
        private DamagedState _damagedState;

        private DeathState _deathState;
        private MoveState _moveState;

        public event Action OnAttack;

        private bool _isAttacking = false;
        private float _attackCooldown = 2f;
        [SerializeField] private bool robot;


        private void Update()
        {
            ManageEnemyState(Player.PlayerTransform, transform);
            StateMachine.Execute(this);
        }

        public async void Attack()
        {
            if (_isAttacking)
                return;

            _isAttacking = true;

            await UniTask.Delay(TimeSpan.FromSeconds(_attackCooldown));

            if (this == null || gameObject == null || !gameObject.activeInHierarchy)
            {
                return;
            }

            if (Vector3.Distance(Player.PlayerTransform.position, transform.position) <= enemyTeamConfig.attackDistance)
            {
                StateMachine.ChangeState(_attackState, this);
                Player.TryChangeHealth(enemyTeamConfig.damage);

                if (robot)
                {
                    AudioManager.Instance.PlaySound(3);
                }
                else
                {
                    AudioManager.Instance.PlaySound(1);
                }

                OnAttack?.Invoke();
            }

            _isAttacking = false;
        }

        public void ChaseState()
        {
            StateMachine.ChangeState(_chaseState, this);
        }

        public void TakeDamage(float damage)
        {
            StateMachine.ChangeState(_damagedState, this);
        }

        public void Die()
        {
            StateMachine.ChangeState(_deathState, this);
        }

        public void MoveState()
        {
            StateMachine.ChangeState(_moveState, this);
        }

        protected override void ManageEnemyState(Transform playerTransform, Transform enemyTransform)
        {
            var playerEnemyDistance = (playerTransform.position - enemyTransform.position).magnitude;

            if (playerEnemyDistance <= enemyTeamConfig.attackDistance)
            {
                Attack();
            }
            else if (playerEnemyDistance <= enemyTeamConfig.detectPlayerDistance)
            {
                ChaseState();
            }
            else
            {
                MoveState();
            }
        }

        protected override void InitializeStrategies()
        {
            if (isMoving) MovementSystem = new EnemyMovementSystem(gameObject, moveType, patrolPoints);

            if (isAttacking) AttackSystem = new EnemyAttackSystem(gameObject, attackType);
        }

        protected override void InitializeStates()
        {
            _deathState = new DeathState();
            _damagedState = new DamagedState();
            _chaseState = new ChaseState();
            _moveState = new MoveState();
            _attackState = new AttackState();
        }
    }
}