using AttackSystem.WeaponData;
using UnityEngine;

namespace AttackSystem.Weapons
{
    public class FireArm : AbstractWeapon
    {
        [field: SerializeField] public GunSO GunSo { get; private set; }
        [field: SerializeField] public Transform SpawnPosition { get; private set; }
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }

        public override void Shoot()
        {
            // Create the projectile with the correct rotation
            var bullet = BulletPrefab.Create(
                GunSo.bulletSpeed,
                GunSo.weaponDamage,
                GunSo.weaponRange,
                SpawnPosition.position,
                transform.rotation,
                BulletPrefab);
        }
    }
}