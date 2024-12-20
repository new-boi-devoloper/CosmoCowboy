using EnemyScripts.Enemies;
using UnityEngine;

namespace EnemyScripts.EnemyManagement
{
    [RequireComponent(typeof(Animator))]
    public class NpcEnemyAnimator : MonoBehaviour
    {
        [field: SerializeField] private bool isNpcEnemy = false;
        [field: SerializeField] private HumanoidEnemy humanoidEnemy;
        [field: SerializeField] private EnemyHealth enemyHealth;

        private Animator _animator;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int Enable = Animator.StringToHash("Enable");
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        private void OnEnable()
        {
            humanoidEnemy.OnAttack += AttackAnim;
            enemyHealth.OnDeadAnimPlay += DeathAnim;
            enemyHealth.OnTakeHitAnim += TakeHitAnim;
        }

        private void OnDisable()
        {
            humanoidEnemy.OnAttack -= AttackAnim;
            enemyHealth.OnDeadAnimPlay -= DeathAnim;
            enemyHealth.OnTakeHitAnim -= TakeHitAnim;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();

            EnableAnim();
        }

        private void AttackAnim()
        {
            _animator.SetTrigger(Attack);
        }

        private void EnableAnim()
        {
            if (isNpcEnemy)
            {
                _animator.SetTrigger(Enable);
            }
        }

        private void DeathAnim()
        {
            _animator.SetBool(IsDead, true);
        }

        private void TakeHitAnim()
        {
            _animator.SetTrigger(Hurt);
        }
    }
}