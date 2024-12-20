using UnityEngine;

namespace EnemyScripts.EnemySO
{
    [CreateAssetMenu(fileName = "EnemyTeamConfig", menuName = "Enemy/Enemy Config", order = 1)]
    public class EnemyTeamConfig : ScriptableObject
    {
        public float detectPlayerDistance;
        public float informEnemiesDistance;
        public float attackDistance;
        public float lastPlayerPositionTimer;

        public int damage;
    }
}