using UnityEngine;

namespace EnemyScripts.EnemyManagement
{
    public class EnemyAlertSystem
    {
        public void AlertEnemiesInRadius(Vector2 position, float radius, Vector2 lastKnownPlayerPosition)
        {
            var colliders = Physics2D.OverlapCircleAll(position, radius);

            foreach (var collider in colliders)
                if (collider.TryGetComponent(out EnemyBase enemy))
                {
                }
        }
    }
}