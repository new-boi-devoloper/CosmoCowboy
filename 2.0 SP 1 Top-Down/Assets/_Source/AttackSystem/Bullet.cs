using EnemyScripts.EnemyManagement;
using UnityEngine;
using Utils;

namespace AttackSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer; 
        [SerializeField] private ParticleSystem _particleSystem;
        private Vector3 _direction;
        private float _lifetime;
        private float _moveSpeed;
        private float _projectileDamage;
        private float _projectileRange;

        private Vector3 _startPosition;

        private void Update()
        {
            MoveProjectile();
            DetectFireDistance();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerChecker.ContainsLayer(enemyLayer, other.gameObject))
            {
                if (other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    enemyHealth.ChangeHealth(-_projectileDamage); 

                    var particleInstance = Instantiate(_particleSystem, other.transform.position, Quaternion.identity);

                    var particleSystemComponent = particleInstance.GetComponent<ParticleSystem>();
                    if (particleSystemComponent != null)
                    {
                        particleSystemComponent.Play(); 
                        Destroy(particleInstance, particleSystemComponent.main.duration); // Удаляем после завершения
                    }
                }

                Destroy(gameObject);
            }
        }

        public Bullet Create(float moveSpeed, float projectileDamage, float projectileRange, Vector3 position,
            Quaternion rotation, Bullet bulletPrefab)
        {
            var bullet = Instantiate(bulletPrefab, position, rotation);
            bullet.UpdateStats(moveSpeed, projectileDamage, projectileRange, position);
            return bullet;
        }

        private void UpdateStats(float moveSpeed, float projectileDamage, float projectileRange, Vector3 position)
        {
            _moveSpeed = moveSpeed;
            _projectileDamage = projectileDamage;
            _projectileRange = projectileRange;

            _startPosition = position;
            Debug.Log("Spawn position changed");
        }

        private void MoveProjectile()
        {
            transform.Translate(-Vector3.right * (_moveSpeed * Time.deltaTime));
        }

        private void DetectFireDistance()
        {
            if (Vector3.Distance(transform.position, _startPosition) > _projectileRange) Destroy(gameObject);
        }
    }
}