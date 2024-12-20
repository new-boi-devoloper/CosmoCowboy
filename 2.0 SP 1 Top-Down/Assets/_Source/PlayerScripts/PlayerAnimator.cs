using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        [field: SerializeField] private Player player;
        private Animator _animator;
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            player.OnPlayAnimHit += PlayHitAnim;
            player.OnSpeedChanged += PlaySpeedAnims;
        }

        private void OnDisable()
        {
            player.OnPlayAnimHit -= PlayHitAnim;
            player.OnSpeedChanged -= PlaySpeedAnims;
        }

        private void PlayHitAnim()
        {
            _animator.SetTrigger(Hurt);
        }

        private void PlaySpeedAnims(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }  
        
        private void PlayDeadAnim()
        {
            _animator.SetBool(IsDead, true);
        }
    }
}