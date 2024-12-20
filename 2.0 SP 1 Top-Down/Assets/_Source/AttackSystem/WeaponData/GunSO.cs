using UnityEngine;

namespace AttackSystem.WeaponData
{
    [CreateAssetMenu(fileName = "GunData", menuName = "GunSO/GunData")]
    public class GunSO : ScriptableObject
    {
        public GameObject weaponPrefab;
        public float weaponCooldown;
        public int weaponDamage;
        public float weaponRange;
        public float bulletSpeed;
    }
}